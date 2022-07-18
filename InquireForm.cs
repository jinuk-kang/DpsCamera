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

        public InquireForm() {
            InitializeComponent();

            this.Text = "조회하기";

            this.inqureButton.Click += inqureButtonClicked;
            this.resultListView.Hide();

            search();
        }

        public void inqureButtonClicked(object sender, EventArgs e) {
            string[] files = makeSearchPath(false);
        
            foreach (string file in files) {
                Console.WriteLine(file);
            }
        }

        private void ngInquireButtonClicked(object sender, EventArgs e) {
            string[] files = makeSearchPath(true);

            foreach (string file in files) {
                Console.WriteLine(file);
            }
        }

        private string[] makeSearchPath(bool isNoRead) {
            string yearString = selectedDate.ToString("yyyy");
            string monthDayString = selectedDate.ToString("MM-dd");

            string yearDirPath = "C:\\Users\\" + LOCAL_USER_DIR_NAME + "\\Desktop\\Barcode_Image\\" + yearString;

            if (isNoRead) {
                string noReadPath = yearDirPath + "\\" + monthDayString + "_NG";

                try {
                    return Directory.GetFiles(noReadPath, $"*.*");
                } catch (System.IO.DirectoryNotFoundException) {
                    string[] er = { "NoData" };
                    return er;
                }
            } else {
                string normalPath = yearDirPath + "\\" + monthDayString;
                try {
                    return Directory.GetFiles(normalPath, $"*.*");
                } catch (System.IO.DirectoryNotFoundException) {
                    string[] er = { "NoData" };
                    return er;
                }
            }
        }
        private string search() {
            // string[] files = Directory.GetFiles("");

            //foreach (string fileName in files) {
            //     resultListView.Items.Add("a");
            //  }

            string[] files = { "A", "B", "C" };


            resultListView.View = View.Details;

            resultListView.CheckBoxes = true; // check box 나오게 세팅 하는 부분이다. properties에서 해도 된다.
            resultListView.BeginUpdate();

            ListViewItem item = new ListViewItem(files);

            resultListView.Items.Add(item); // ListView.Items.Add 로 Listview에 넣어 준다.
            resultListView.EndUpdate();

            //응용해서 for문 돌려서 입력을 한번에도 할 수 있다.
            /*
            for (int i = 0; i < textValueLine.Length; i++) {
                string[] itemValue = textValueLine[i].Split(AppSettingValue.delimiterChars);
                ListViewItem itm = new ListViewItem(itemValue);
                ShowListView.Items.Add(itm);
            }
            */


            return "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            selectedDate = dateTimePicker1.Value.Date;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            storeCode = textBox1.Text;
        }
    }
}
