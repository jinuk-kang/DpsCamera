using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpsCamera {
    public partial class MainForm : Form {
        bool isWorking = false;

        public MainForm() {
            InitializeComponent();

            this.inqureButton.Click += inquireButtonClicked;
            this.startButton.Click += startButtonClicked;
        }

        public void inquireButtonClicked(object sender, EventArgs e) {
            InquireForm inquireForm = new InquireForm();
            inquireForm.Show();
        }

        public void startButtonClicked(object sedner, EventArgs e) {
            if (isWorking) {
                this.startButton.Text = "작업 시작";
            } else {
                this.startButton.Text = "작업 종료";
            }

            isWorking = !isWorking;
        }
    }
}
