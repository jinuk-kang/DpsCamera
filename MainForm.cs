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
        public MainForm() {
            InitializeComponent();

            this.inqureButton.Click += inquireButtonClicked;
        }

        public void inquireButtonClicked(object sender, EventArgs e) {
            InquireForm inquireForm = new InquireForm();
            inquireForm.Show();
        }
    }
}
