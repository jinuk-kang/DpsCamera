using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;

namespace DpsCamera {
    public class StateObject {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();


    }
    public partial class MainForm : Form {
        // Variable [BCR]
        private const int BCR_PORT = 2112;
        private const string BCR_IP = "192.168.0.1";
        private IPAddress ipAddress;
        private IPEndPoint remoteEP;
        private Socket client;

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty;

        // Variable [CAMERA]
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        IntPtr m_BufForDriver = IntPtr.Zero;

        List<string> cameraList = new List<string>();

        MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private MyCamera m_MyCamera = new MyCamera();
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        private static Object BufForDriverLock = new Object();
        UInt32 m_nBufSizeForDriver = 0;

        bool isWorking = false;
        int workCount = 0;

        public MainForm() {
            InitializeComponent();

            ipAddress = IPAddress.Parse(BCR_IP);
            remoteEP = new IPEndPoint(ipAddress, BCR_PORT);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);

            workCountLabel.Text = "0";
            setWorkStatus(false);
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void setWorkStatus(bool isWorking) {
            this.isWorking = isWorking;
            startButton.Enabled = !isWorking;
            endButton.Enabled = isWorking;

            connectCameraButton.Enabled = false; // TEST
            disconnectCameraButton.Enabled = false;
            startGrabButton.Enabled = false;
            stopGrabButton.Enabled = false;
            saveJpgButton.Enabled = false;
        }

        // BCR functions [START]
        private static void ConnectCallback(IAsyncResult ar) {
            try {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                client.RemoteEndPoint.ToString());

                connectDone.Set();
            } catch (Exception e) {
                Console.WriteLine("Socket not connected : " + e.ToString());
            }
        }
        private static void Receive(Socket client) {
            try {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            } catch (Exception e) {
                Console.WriteLine("Socket receive error : " + e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar) {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0) {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    Console.WriteLine(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                } else {
                    if (state.sb.Length > 1) {
                        response = state.sb.ToString();
                    }

                    receiveDone.Set();
                }
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
        /*
        private static void Send(Socket client, String data) {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }
        private static void SendCallback(IAsyncResult ar) {
            try {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                sendDone.Set();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
        */
        // BCR functions [END]

        // Action functions [START]
        private void inquireButtonClick(object sender, EventArgs e) {
            InquireForm inquireForm = new InquireForm();
            inquireForm.Show();
        }

        private void connectCameraButtonClick(object sender, EventArgs e) {
            connectCamera();
        }
        private void disconnectCameraButtonClick(object sender, EventArgs e) {
            disconnectCamera();
        }
        private void startButtonClick(object sender, EventArgs e) {
            setWorkStatus(true);
            acquireCameraList();
            connectCamera();
            startGrab();
            saveJpgButton.Enabled = true;

            Receive(client);

            Console.WriteLine("Response received : {0}", response);
        }
        private void endButtonClick(object sender, EventArgs e) {
            setWorkStatus(false);
            stopGrab();
            disconnectCamera();
            saveJpgButton.Enabled = false;
            productImage.Image = null;
        }

        private void startGrabButtonClick(object sender, EventArgs e) {
            startGrab();
        }

        private void stopGrabButtonClick(object sender, EventArgs e) {
            stopGrab();
        }

        private void saveJpgButtonClick(object sender, EventArgs e) {
            saveJpg();
        }
        // Action functions [END]

        // Camera functions [START]
        private void acquireCameraList() {
            // Create Device List
            System.GC.Collect();
            cameraList.Clear();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0 != nRet) {
                MessageBox.Show("Enumerate devices fail!");
                return;
            }

            // Display device name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++) {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE) {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    if (gigeInfo.chUserDefinedName != "") {
                        cameraList.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    } else {
                        cameraList.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                } else if (device.nTLayerType == MyCamera.MV_USB_DEVICE) {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "") {
                        cameraList.Add("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    } else {
                        cameraList.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            if (cameraList.Count == 0) {
                MessageBox.Show("CameraList is empty");
            } else {
                // MessageBox.Show(cameraList[0]);
            }
        }

        private void connectCamera() {
            if (m_stDeviceList.nDeviceNum == 0 || cameraList.Count == 0) {
                MessageBox.Show("No device, please select");
                return;
            }

            // Get selected device information
            MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[0], typeof(MyCamera.MV_CC_DEVICE_INFO));

            // Open device
            if (null == m_MyCamera) {
                m_MyCamera = new MyCamera();
                if (null == m_MyCamera) {
                    return;
                }
            }

            int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet) {
                return;
            }

            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet) {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                MessageBox.Show("Device open fail!\n" + nRet);
                return;
            }

            // Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE) {
                int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0) {
                    nRet = m_MyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK) {
                        MessageBox.Show("Set Packet Size failed!\n" + nRet);
                    }
                } else {
                    MessageBox.Show("Get Packet Size failed!\n" + nPacketSize);
                }
            }

            // Set Continues Aquisition Mode
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            //bnGetParam_Click(null, null);// Get parameters

            /*
            connectCameraButton.Enabled = false;
            disconnectCameraButton.Enabled = true;
            startGrabButton.Enabled = true;
            stopGrabButton.Enabled = false;
            */
        }

        private void disconnectCamera() {
            if (m_bGrabbing == true) {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
            }

            if (m_BufForDriver != IntPtr.Zero) {
                Marshal.Release(m_BufForDriver);
            }

            // Close Device
            m_MyCamera.MV_CC_CloseDevice_NET();
            m_MyCamera.MV_CC_DestroyDevice_NET();

            /*
            connectCameraButton.Enabled = true;
            disconnectCameraButton.Enabled = false;
            startGrabButton.Enabled = false;
            stopGrabButton.Enabled = false;
            */
        }

        private void startGrab() {
            // Set position bit true
            m_bGrabbing = true;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            m_stFrameInfo.nFrameLen = 0;
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;

            // Start Grabbing
            int nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet) {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                MessageBox.Show("Start Grabbing Fail!\n" + nRet);
                return;
            }

            /*
            startGrabButton.Enabled = false;
            stopGrabButton.Enabled = true;
            saveJpgButton.Enabled = true;
            */
        }

        private void stopGrab() {
            // Set flag bit false
            m_bGrabbing = false;
            m_hReceiveThread.Join();

            // Stop Grabbing
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK) {
                MessageBox.Show("Stop Grabbing Fail!\n" + nRet);
            }

            /*
            startGrabButton.Enabled = true;
            stopGrabButton.Enabled = false;
            saveJpgButton.Enabled = false;
            */
        }

        private void saveJpg() {
            if (false == m_bGrabbing) {
                MessageBox.Show("Not Start Grabbing");
                return;
            }

            if (RemoveCustomPixelFormats(m_stFrameInfo.enPixelType)) {
                MessageBox.Show("Not Support!");
                return;
            }

            MyCamera.MV_SAVE_IMG_TO_FILE_PARAM stSaveFileParam = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM();

            lock (BufForDriverLock) {
                if (m_stFrameInfo.nFrameLen == 0) {
                    MessageBox.Show("Save Jpeg Fail!");
                    return;
                }
                stSaveFileParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                stSaveFileParam.enPixelType = m_stFrameInfo.enPixelType;
                stSaveFileParam.pData = m_BufForDriver;
                stSaveFileParam.nDataLen = m_stFrameInfo.nFrameLen;
                stSaveFileParam.nHeight = m_stFrameInfo.nHeight;
                stSaveFileParam.nWidth = m_stFrameInfo.nWidth;
                stSaveFileParam.nQuality = 80;
                stSaveFileParam.iMethodValue = 2;
                stSaveFileParam.pImagePath = makeImagePath();
                int nRet = m_MyCamera.MV_CC_SaveImageToFile_NET(ref stSaveFileParam);
                if (MyCamera.MV_OK != nRet) {
                    MessageBox.Show("Save Jpeg Fail!\n" + nRet);
                    return;
                }
            }

            // MessageBox.Show("Save Succeed!");
            this.workCount++;
            workCountLabel.Text = this.workCount.ToString();
        }
        private string makeImagePath() {
            string path = "C:\\Users\\Jin\\Desktop\\";

            DateTime dateTime = DateTime.Now;
            string dateString = dateTime.ToString("yyyy_MM_dd_hh_mm_ss");

            return path + dateString + ".jpg";
        }
        private bool RemoveCustomPixelFormats(MyCamera.MvGvspPixelType enPixelFormat) {
            Int32 nResult = ((int)enPixelFormat) & (unchecked((Int32)0x80000000));
            if (0x80000000 == nResult) {
                return true;
            } else {
                return false;
            }
        }
        public void ReceiveThreadProcess() {
            MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();
            MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
            int nRet = MyCamera.MV_OK;

            while (m_bGrabbing) {
                nRet = m_MyCamera.MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                if (nRet == MyCamera.MV_OK) {
                    lock (BufForDriverLock) {
                        if (m_BufForDriver == IntPtr.Zero || stFrameInfo.stFrameInfo.nFrameLen > m_nBufSizeForDriver) {
                            if (m_BufForDriver != IntPtr.Zero) {
                                Marshal.Release(m_BufForDriver);
                                m_BufForDriver = IntPtr.Zero;
                            }

                            m_BufForDriver = Marshal.AllocHGlobal((Int32)stFrameInfo.stFrameInfo.nFrameLen);
                            if (m_BufForDriver == IntPtr.Zero) {
                                return;
                            }
                            m_nBufSizeForDriver = stFrameInfo.stFrameInfo.nFrameLen;
                        }

                        m_stFrameInfo = stFrameInfo.stFrameInfo;
                        CopyMemory(m_BufForDriver, stFrameInfo.pBufAddr, stFrameInfo.stFrameInfo.nFrameLen);
                    }

                    if (RemoveCustomPixelFormats(stFrameInfo.stFrameInfo.enPixelType)) {
                        m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                        continue;
                    }

                    stDisplayInfo.hWnd = productImage.Handle;
                    stDisplayInfo.pData = stFrameInfo.pBufAddr;
                    stDisplayInfo.nDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = stFrameInfo.stFrameInfo.nWidth;
                    stDisplayInfo.nHeight = stFrameInfo.stFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = stFrameInfo.stFrameInfo.enPixelType;
                    m_MyCamera.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);

                    m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                } else {
                    /*
                    if (bnTriggerMode.Checked) {
                        Thread.Sleep(5);
                    }
                    */
                }
            }
        }
        // Camera functions [END]
    }
}
