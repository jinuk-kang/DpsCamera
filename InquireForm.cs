using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpsCamera {
    public partial class InquireForm : Form {
        private String LOCAL_USER_DIR_NAME = "Jin";

        private DateTime selectedDate = DateTime.Now;
        private string storeCode = "";

        private List<String> files = new List<String>();

        public InquireForm() {
            InitializeComponent();

            this.Text = "조회하기";

            this.inqureButton.Click += inqureButtonClicked;
        }

        public void inqureButtonClicked(object sender, EventArgs e) {
            files.Clear();
            files = makeSearchPath(false);
            resultGridView.Rows.Clear();

            if (files[0] == "NoData") {
                MessageBox.Show("조회된 데이터가 없습니다.");
                resultGridView.Rows.Clear();
                resultImage.Image = null;
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
            } else {
                if (files.Count > 0) {
                    resultImage.Load(@files[0]);

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
                    resultGridView.Rows.Clear();
                    MessageBox.Show("조회된 데이터가 없습니다.");
                }
            }
        }

        private List<String> makeSearchPath(bool isNoRead) {
            string yearString = selectedDate.ToString("yyyy");
            string monthDayString = selectedDate.ToString("MM-dd");

            string yearDirPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image\\" + yearString;

            if (isNoRead) {
                string noReadPath = yearDirPath + "\\" + monthDayString + "_NG";
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
                string normalPath = yearDirPath + "\\" + monthDayString;
                List<String> result = new List<String>();

                try {
                    foreach (string f in Directory.GetFiles(normalPath, $"*.*")) {
                        result.Add(f);
                    }

                    return result;
                } catch (System.IO.DirectoryNotFoundException) {
                    result.Add("NoData");

                    return result;
                }
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
        }
    }
}
