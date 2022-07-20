using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpsCamera {
    public partial class InquireForm : Form {
        private String LOCAL_USER_DIR_NAME = "YEIN";

        private bool isAllDay = false;
        private DateTime selectedDate = DateTime.Now;
        private string storeCode = "";

        private string selectedFilePath = "";

        private List<String> files = new List<String>();

        public InquireForm() {
            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Text = "조회하기";

            openImageButton.Enabled = false;
            this.inqureButton.Click += inqureButtonClicked;

            this.WindowState = FormWindowState.Maximized;
        }
        public void inqureButtonClicked(object sender, EventArgs e) {
            files.Clear();
            files = makeSearchPath(false);
            resultGridView.Rows.Clear();

            if (files[0] == "NoData") {
                MessageBox.Show("조회된 데이터가 없습니다.");
                resultGridView.Rows.Clear();
                resultImage.Image = null;
                selectedFilePath = "";
                openImageButton.Enabled = true;
            } else {
                if (storeCode != "") {
                    List<String> newFiles = new List<String>();

                    foreach (string file in files) {
                        Console.WriteLine(file.Substring(file.Length - 13, 5));
                        if (file.Substring(file.Length-13, 5) == storeCode) {
                            newFiles.Add(file);
                        }
                    }

                    files = newFiles;
                }

                if (files.Count > 0) {
                    resultImage.Load(@files[0]);
                    selectedFilePath = files[0];
                    openImageButton.Enabled = true;

                    foreach (string file in files) {
                        string[] pa = file.Split('\\');
                        List<String> showList = new List<string>();

                        for(int i=0;i<pa.Length;i++) {
                            if (i >= 4) {
                                showList.Add(pa[i]);

                                if (i != pa.Length-1) {
                                    showList.Add("\\");
                                }
                            }
                        }

                        string finalFilePath = "";

                        foreach (string s in showList) {
                            finalFilePath += s;
                        }

                        resultGridView.Rows.Add(finalFilePath);
                    }
                } else {
                    resultImage.Image = null;
                    selectedFilePath = "";
                    openImageButton.Enabled = false;
                    resultGridView.Rows.Clear();
                    MessageBox.Show("조회된 데이터가 없습니다.");
                }
            }
        }

        private void ngInquireButtonClicked(object sender, EventArgs e) {
            storeCode = "";
            textBox1.Text = storeCode;
            resultGridView.Rows.Clear();

            files.Clear();
            files = makeSearchPath(true);

            if (files[0] == "NoData") {
                MessageBox.Show("조회된 데이터가 없습니다.");
                resultGridView.Rows.Clear();
                resultImage.Image = null;
                selectedFilePath = "";
                openImageButton.Enabled = false;
            } else {
                if (files.Count > 0) {
                    resultImage.Load(@files[0]);
                    selectedFilePath = files[0];
                    openImageButton.Enabled = true;

                    foreach (string file in files) {
                        string[] pa = file.Split('\\');
                        List<String> showList = new List<string>();

                        for (int i = 0; i < pa.Length; i++) {
                            if (i >= 4) {
                                showList.Add(pa[i]);

                                if (i != pa.Length - 1) {
                                    showList.Add("\\");
                                }
                            }
                        }

                        string finalFilePath = "";

                        foreach (string s in showList) {
                            finalFilePath += s;
                        }

                        resultGridView.Rows.Add(finalFilePath);
                    }
                } else {
                    resultImage.Image = null;
                    selectedFilePath = "";
                    openImageButton.Enabled = false;
                    resultGridView.Rows.Clear();
                    MessageBox.Show("조회된 데이터가 없습니다.");
                }
            }
        }

        private List<String> makeSearchPath(bool isNoRead) {
            if (!isAllDay) {
                string dateString = selectedDate.ToString("yyyy-MM-dd");

                string dateDirPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image\\" + dateString;

                if (isNoRead) {
                    string noReadPath = dateDirPath + "_NG";
                    List<String> result = new List<String>();

                    try {
                        foreach (string f in Directory.GetFiles(noReadPath, $"*.*")) {
                            result.Add(f);
                        }

                        return result;
                    } catch (System.IO.DirectoryNotFoundException) {
                        result.Add("NoData");

                        return result;
                    }
                } else {
                    List<String> result = new List<String>();

                    try {
                        foreach (string f in Directory.GetFiles(dateDirPath, $"*.*")) {
                            result.Add(f);
                        }

                        return result;
                    } catch (System.IO.DirectoryNotFoundException) {
                        result.Add("NoData");

                        return result;
                    }
                }
            } else {
                string parentPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image";

                DirectoryInfo parentDir = new DirectoryInfo(parentPath);
                List<String> result = new List<String>();

                foreach (DirectoryInfo d in parentDir.GetDirectories()) {
                    string[] fs = Directory.GetFiles(d.FullName, $"*.*");

                    foreach (string a in fs) {
                        string[] ss = a.Split('\\');

                        if (isNoRead) {
                            if (ss[ss.Length-1].Substring(0, 2) == "NG") {
                                result.Add(a);
                            }
                        } else {
                            if (ss[ss.Length - 1].Substring(0, 2) != "NG") {
                                result.Add(a);
                            }
                        }
                    }
                } 

                if (result.Count == 0) {
                    result.Add("NoData");
                }

                return result;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            storeCode = "";
            textBox1.Text = storeCode;
            selectedDate = dateTimePicker1.Value.Date;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            storeCode = textBox1.Text;
        }

        private void resultGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            int selectedIndex = resultGridView.CurrentCell.RowIndex;

            resultImage.Load(@files[selectedIndex]);
            selectedFilePath = files[selectedIndex];
            openImageButton.Enabled = true;
        }
        private void openImageButton_Click(object sender, EventArgs e) {
            if (selectedFilePath != "") {
                Process.Start(selectedFilePath);
            }
        }

        private void openDirButton_Click(object sender, EventArgs e) {
            if (selectedFilePath != "") {
                string dirPath = "";
                string[] sList = selectedFilePath.Split('\\');

                for (int i=0; i<sList.Length-1; i++) {
                    dirPath += sList[i];

                    if (i != sList.Length-2) {
                        dirPath += "\\";
                    }
                }

                Process.Start(dirPath);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            this.isAllDay = !this.isAllDay;
        }
    }
}
