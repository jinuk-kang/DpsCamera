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

namespace DpsCamera {
    public partial class MainForm : Form {
        bool isWorking = false;
        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        IntPtr m_BufForDriver = IntPtr.Zero;

        List<string> cameraList = new List<string>();
        MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private MyCamera m_MyCamera = new MyCamera();

        public MainForm() {
            InitializeComponent();

            acquireCameraList();
            setWorkStatus(false);
        }

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

            if(cameraList.Count == 0) {
                MessageBox.Show("CameraList is empty");
            } else {
                MessageBox.Show(cameraList[0]);
            }
        }

        private void connectCameraButtonClick(object sender, EventArgs e) {
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

            // Control operation
            connectCameraButton.Enabled = false;
            disconnectCameraButton.Enabled = true;
        }
        private void disconnectCameraButtonClick(object sender, EventArgs e) {
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

            connectCameraButton.Enabled = true;
            disconnectCameraButton.Enabled = false;
        }

        private void inquireButtonClick(object sender, EventArgs e) {
            InquireForm inquireForm = new InquireForm();
            inquireForm.Show();
        }

        private void startButtonClick(object sender, EventArgs e) {
            setWorkStatus(true);
        }
        private void endButtonClick(object sender, EventArgs e) {
            setWorkStatus(false);
        }

        private void setWorkStatus(bool isWorking) {
            this.isWorking = isWorking;
            startButton.Enabled = !isWorking;
            endButton.Enabled = isWorking;
            connectCameraButton.Enabled = isWorking;
            disconnectCameraButton.Enabled = false;
        }
    }
}
