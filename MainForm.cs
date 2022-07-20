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
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DpsCamera {
    public partial class MainForm : Form {
        private const int BCR_PORT = 2112;
        private const string BCR_IP = "192.168.0.1";
        private IPAddress ipAddress;
        private IPEndPoint remoteEP;
        private Socket client;

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty;

        private int PHOTO_DELAY_TIME = 1000; // milliseconds
        private String LOCAL_USER_DIR_NAME = "YEIN";

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
        int progressTime = 0;

        public MainForm() {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            ipAddress = IPAddress.Parse(BCR_IP);
            remoteEP = new IPEndPoint(ipAddress, BCR_PORT);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);

            this.MinimumSize = new System.Drawing.Size(1000, 658);

            this.Text = "Barcode Image Scanner";

            timer1.Interval = 1000;
            countLabel.Text = "0";
            progressTimeLabel.Text = "00:00:00";
            dateLabel.Text = DateTime.Now.ToString("yyyy-MM-dd");
            barcodeLabel.Text = "";
            roundLabel.Text = "";
            storeCodeLabel.Text = "";
            boxOrderLabel.Text = "";
            divergenceLabel.Text = "";

            //setTimerOffset();
            clearOldData();
            setWorkStatus(false);
            
            Control.CheckForIllegalCrossThreadCalls = false;

            start();
        }
        private void clearOldData() {
            string dirPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image";
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            if (dir.Exists) {
                DirectoryInfo[] dirList = dir.GetDirectories();

                foreach (DirectoryInfo di in dirList) {
                    if (di.LastWriteTime < DateTime.Now.AddDays(-30)) {
                        di.Attributes = FileAttributes.Normal;
                        di.Delete(true);
                    }
                }
            }
        }
        private void setTimerOffset() {
            string path = @"C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\BarcodeImageScanner\\timer_offset.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);

            PHOTO_DELAY_TIME = Convert.ToInt32(textValue[0]);
        }
        private void setWorkStatus(bool isWorking) {
            this.isWorking = isWorking;
            startButton.Enabled = !isWorking;
            endButton.Enabled = isWorking;

            if (isWorking) {
                timer1.Start();
            } else {
                timer1.Stop();
            }
        }
        private void saveAndShowData(String barcode) {
            if (ParseManager.isNoRead(barcode)) {
                barcodeLabel.Text = "NG";
                roundLabel.Text = "";
                storeCodeLabel.Text = "";
                boxOrderLabel.Text = "";
                divergenceLabel.Text = "";

                dataGridView.Rows.Add(
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    "NG", "", "", "", "");
            } else {
                barcodeLabel.Text = barcode;
                roundLabel.Text = ParseManager.parseRound(barcode);
                storeCodeLabel.Text = ParseManager.parseStoreCode(barcode);
                boxOrderLabel.Text = ParseManager.parseBoxOrder(barcode);
                divergenceLabel.Text = ParseManager.parseDivergence(barcode);

                dataGridView.Rows.Add(
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    barcode, 
                    ParseManager.parseRound(barcode),
                    ParseManager.parseStoreCode(barcode),
                    ParseManager.parseBoxOrder(barcode),
                    ParseManager.parseDivergence(barcode));
            }

            Thread.Sleep(PHOTO_DELAY_TIME);

            saveJpg(barcode);
        }
        private void start() {
            setWorkStatus(true);
            acquireCameraList();
            connectCamera();
            startGrab();

            Receive(client);
        }
        // BCR functions [START]
        private void ConnectCallback(IAsyncResult ar) {
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
        private void Receive(Socket client) {
            try {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            } catch (Exception e) {
                Console.WriteLine("Socket receive error : " + e.ToString());
            }
        }
        private void ReceiveCallback(IAsyncResult ar) {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0) {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    saveAndShowData(ParseManager.parseBarcode(Encoding.ASCII.GetString(state.buffer, 0, bytesRead)));

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
        private void startButton_Click(object sender, EventArgs e) {
            start();

            //Console.WriteLine("Response received : {0}", response);
        }
        private void endButton_Click(object sender, EventArgs e) {
            setWorkStatus(false);
            stopGrab();
            disconnectCamera();

            barcodeLabel.Text = "";
            roundLabel.Text = "";
            storeCodeLabel.Text = "";
            boxOrderLabel.Text = "";
            divergenceLabel.Text = "";
            captureImage.Image = null;
        }
        private void inquireButton_Click(object sender, EventArgs e) {
            foreach (Form frm in Application.OpenForms) {
                if (frm.Name == "InquireForm") {
                    frm.Activate();
                    return;
                }
            }

            InquireForm inquireForm = new InquireForm();
            inquireForm.Show();
        }
        private void goTime(object sender, EventArgs e) {
            progressTime++;

            int hour = progressTime / 3600;
            int min = (progressTime % 3600) / 60;
            int sec = (progressTime % 3600) % 60;

            String hourString = "";
            String minString = "";
            String secString = "";

            if (hour < 10) {
                hourString = "0" + hour.ToString();
            } else {
                hourString = hour.ToString();
            }

            if (min < 10) {
                minString = "0" + min.ToString();
            } else {
                minString = min.ToString();
            }

            if (sec < 10) {
                secString = "0" + sec.ToString();
            } else {
                secString = sec.ToString();
            }

            progressTimeLabel.Text = hourString + ":" + minString + ":" + secString;
        }
        private void formClosing(object sender, FormClosingEventArgs e) {
            setWorkStatus(false);

            stopGrab();
            disconnectCamera();

            barcodeLabel.Text = "";
            roundLabel.Text = "";
            storeCodeLabel.Text = "";
            boxOrderLabel.Text = "";
            divergenceLabel.Text = "";
            captureImage.Image = null;
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
        }
        private void stopGrab() {
            // Set flag bit false
            m_bGrabbing = false;

            if (m_hReceiveThread != null) {
                m_hReceiveThread.Join();
            } else {
                return;
            }

            // Stop Grabbing
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK) {
                MessageBox.Show("Stop Grabbing Fail!\n" + nRet);
            }
        }
        private void saveJpg(String name) {
            String imageName = makeImagePath(name);

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
                    //MessageBox.Show("Save Jpeg Fail!");
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
                stSaveFileParam.pImagePath = imageName;
                
                int nRet = m_MyCamera.MV_CC_SaveImageToFile_NET(ref stSaveFileParam);
                if (MyCamera.MV_OK != nRet) {
                    //MessageBox.Show("Save Jpeg Fail!\n" + nRet);
                    // TODO
                    return;
                }
            }

            captureImage.Load(@imageName);

            // MessageBox.Show("Save Succeed!");
            this.workCount++;
            countLabel.Text = this.workCount.ToString();
        }
        private string makeImagePath(String name) {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            
            string dateDirPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image\\" + dateString;
            DirectoryInfo dateDir = new DirectoryInfo(dateDirPath);

            if (!dateDir.Exists) {
                dateDir.Create();
            }

            if (ParseManager.isNoRead(name)) {
                string noReadPath = dateDir + "_NG";
                DirectoryInfo noReadDir = new DirectoryInfo(noReadPath);

                if (!noReadDir.Exists) {
                    noReadDir.Create();
                }

                return noReadPath + "\\NG_" + DateTime.Now.ToString("hh_mm_ss") + ".jpg";
            } else {
                return dateDirPath + "\\" + name + ".jpg";
            }
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

                    //stDisplayInfo.hWnd = productImage.Handle;
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
