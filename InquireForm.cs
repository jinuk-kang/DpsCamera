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
    public partial class InquireForm : Form {
        public InquireForm() {
            InitializeComponent();

            this.inqureButton.Click += inqureButtonClicked;
            this.emtpyMsgLabel.Hide();
            this.resultListView.Hide();
        }

        public void inqureButtonClicked(object sender, EventArgs e) {
            this.emtpyMsgLabel.Show();
        }
    }
}
