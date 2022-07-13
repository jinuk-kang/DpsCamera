namespace DpsCamera {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.captureImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.countLabel = new System.Windows.Forms.Label();
            this.progressTimeLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.aaa = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.divergenceLabel = new System.Windows.Forms.Label();
            this.boxOrderLabel = new System.Windows.Forms.Label();
            this.storeCodeLabel = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.inquireButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.round = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boxOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.divergence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.captureImage)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // captureImage
            // 
            this.captureImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.captureImage.Location = new System.Drawing.Point(12, 12);
            this.captureImage.Name = "captureImage";
            this.captureImage.Size = new System.Drawing.Size(500, 500);
            this.captureImage.TabIndex = 0;
            this.captureImage.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(525, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.0221F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.9779F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 181);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "작업 현황";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.4523F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54771F));
            this.tableLayoutPanel2.Controls.Add(this.countLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.progressTimeLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.aaa, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(564, 144);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.BackColor = System.Drawing.Color.Transparent;
            this.countLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.countLabel.ForeColor = System.Drawing.Color.Black;
            this.countLabel.Location = new System.Drawing.Point(198, 89);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(362, 54);
            this.countLabel.TabIndex = 5;
            this.countLabel.Text = "0";
            this.countLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressTimeLabel
            // 
            this.progressTimeLabel.AutoSize = true;
            this.progressTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressTimeLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.progressTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.progressTimeLabel.Location = new System.Drawing.Point(198, 45);
            this.progressTimeLabel.Name = "progressTimeLabel";
            this.progressTimeLabel.Size = new System.Drawing.Size(362, 43);
            this.progressTimeLabel.TabIndex = 4;
            this.progressTimeLabel.Text = "00:20:11";
            this.progressTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.BackColor = System.Drawing.Color.Transparent;
            this.dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateLabel.ForeColor = System.Drawing.Color.Black;
            this.dateLabel.Location = new System.Drawing.Point(198, 1);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(362, 43);
            this.dateLabel.TabIndex = 3;
            this.dateLabel.Text = "2022-07-05";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkCyan;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 43);
            this.label3.TabIndex = 2;
            this.label3.Text = "진행시간";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aaa
            // 
            this.aaa.AutoSize = true;
            this.aaa.BackColor = System.Drawing.Color.DarkCyan;
            this.aaa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aaa.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.aaa.ForeColor = System.Drawing.Color.White;
            this.aaa.Location = new System.Drawing.Point(4, 1);
            this.aaa.Name = "aaa";
            this.aaa.Size = new System.Drawing.Size(187, 43);
            this.aaa.TabIndex = 0;
            this.aaa.Text = "작업일자";
            this.aaa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkCyan;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 54);
            this.label2.TabIndex = 1;
            this.label2.Text = "작업수량";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(566, 29);
            this.label7.TabIndex = 2;
            this.label7.Text = "입력 정보";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(525, 211);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.966778F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.03323F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(572, 301);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.9823F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.0177F));
            this.tableLayoutPanel4.Controls.Add(this.divergenceLabel, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.boxOrderLabel, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.storeCodeLabel, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.roundLabel, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.barcodeLabel, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.81967F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.18033F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(566, 266);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // divergenceLabel
            // 
            this.divergenceLabel.AutoSize = true;
            this.divergenceLabel.BackColor = System.Drawing.Color.Transparent;
            this.divergenceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divergenceLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.divergenceLabel.ForeColor = System.Drawing.Color.Black;
            this.divergenceLabel.Location = new System.Drawing.Point(196, 210);
            this.divergenceLabel.Name = "divergenceLabel";
            this.divergenceLabel.Size = new System.Drawing.Size(366, 55);
            this.divergenceLabel.TabIndex = 9;
            this.divergenceLabel.Text = "2";
            this.divergenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boxOrderLabel
            // 
            this.boxOrderLabel.AutoSize = true;
            this.boxOrderLabel.BackColor = System.Drawing.Color.Transparent;
            this.boxOrderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxOrderLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.boxOrderLabel.ForeColor = System.Drawing.Color.Black;
            this.boxOrderLabel.Location = new System.Drawing.Point(196, 159);
            this.boxOrderLabel.Name = "boxOrderLabel";
            this.boxOrderLabel.Size = new System.Drawing.Size(366, 50);
            this.boxOrderLabel.TabIndex = 8;
            this.boxOrderLabel.Text = "1";
            this.boxOrderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storeCodeLabel
            // 
            this.storeCodeLabel.AutoSize = true;
            this.storeCodeLabel.BackColor = System.Drawing.Color.Transparent;
            this.storeCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeCodeLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.storeCodeLabel.ForeColor = System.Drawing.Color.Black;
            this.storeCodeLabel.Location = new System.Drawing.Point(196, 103);
            this.storeCodeLabel.Name = "storeCodeLabel";
            this.storeCodeLabel.Size = new System.Drawing.Size(366, 55);
            this.storeCodeLabel.TabIndex = 7;
            this.storeCodeLabel.Text = "00109";
            this.storeCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.BackColor = System.Drawing.Color.Transparent;
            this.roundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundLabel.ForeColor = System.Drawing.Color.Black;
            this.roundLabel.Location = new System.Drawing.Point(196, 53);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(366, 49);
            this.roundLabel.TabIndex = 6;
            this.roundLabel.Text = "20220615007";
            this.roundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.BackColor = System.Drawing.Color.Transparent;
            this.barcodeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barcodeLabel.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcodeLabel.ForeColor = System.Drawing.Color.Black;
            this.barcodeLabel.Location = new System.Drawing.Point(196, 1);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(366, 51);
            this.barcodeLabel.TabIndex = 5;
            this.barcodeLabel.Text = "20220615000701-2";
            this.barcodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.DarkCyan;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(4, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(185, 55);
            this.label13.TabIndex = 4;
            this.label13.Text = "매장코드";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.DarkCyan;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(4, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(185, 49);
            this.label11.TabIndex = 2;
            this.label11.Text = "차수";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.DarkCyan;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(185, 51);
            this.label9.TabIndex = 0;
            this.label9.Text = "바코드";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.DarkCyan;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(4, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 50);
            this.label10.TabIndex = 1;
            this.label10.Text = "박스순번";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.DarkCyan;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(4, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(185, 55);
            this.label12.TabIndex = 3;
            this.label12.Text = "분기";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(451, 584);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 15);
            this.label8.TabIndex = 4;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.startButton.Location = new System.Drawing.Point(1112, 14);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(159, 102);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "작업 시작";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // inquireButton
            // 
            this.inquireButton.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.inquireButton.Location = new System.Drawing.Point(1112, 267);
            this.inquireButton.Name = "inquireButton";
            this.inquireButton.Size = new System.Drawing.Size(159, 102);
            this.inquireButton.TabIndex = 6;
            this.inquireButton.Text = "조회";
            this.inquireButton.UseVisualStyleBackColor = true;
            this.inquireButton.Click += new System.EventHandler(this.inquireButton_Click);
            // 
            // endButton
            // 
            this.endButton.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endButton.Location = new System.Drawing.Point(1112, 138);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(159, 102);
            this.endButton.TabIndex = 7;
            this.endButton.Text = "작업 종료";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.barcode,
            this.round,
            this.storeCode,
            this.boxOrder,
            this.divergence});
            this.dataGridView1.Location = new System.Drawing.Point(12, 535);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1259, 262);
            this.dataGridView1.TabIndex = 8;
            // 
            // date
            // 
            this.date.HeaderText = "작업 시간";
            this.date.MinimumWidth = 6;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 125;
            // 
            // barcode
            // 
            this.barcode.HeaderText = "바코드";
            this.barcode.MinimumWidth = 6;
            this.barcode.Name = "barcode";
            this.barcode.ReadOnly = true;
            this.barcode.Width = 125;
            // 
            // round
            // 
            this.round.HeaderText = "차수";
            this.round.MinimumWidth = 6;
            this.round.Name = "round";
            this.round.ReadOnly = true;
            this.round.Width = 125;
            // 
            // storeCode
            // 
            this.storeCode.HeaderText = "매장코드";
            this.storeCode.MinimumWidth = 6;
            this.storeCode.Name = "storeCode";
            this.storeCode.ReadOnly = true;
            this.storeCode.Width = 125;
            // 
            // boxOrder
            // 
            this.boxOrder.HeaderText = "박스순번";
            this.boxOrder.MinimumWidth = 6;
            this.boxOrder.Name = "boxOrder";
            this.boxOrder.ReadOnly = true;
            this.boxOrder.Width = 125;
            // 
            // divergence
            // 
            this.divergence.HeaderText = "분기";
            this.divergence.MinimumWidth = 6;
            this.divergence.Name = "divergence";
            this.divergence.ReadOnly = true;
            this.divergence.Width = 125;
            // 
            // saveImageButton
            // 
            this.saveImageButton.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveImageButton.Location = new System.Drawing.Point(1112, 410);
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.Size = new System.Drawing.Size(159, 102);
            this.saveImageButton.TabIndex = 9;
            this.saveImageButton.Text = "사진 저장";
            this.saveImageButton.UseVisualStyleBackColor = true;
            this.saveImageButton.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 809);
            this.Controls.Add(this.saveImageButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.inquireButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.captureImage);
            this.Name = "MainForm";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.captureImage)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox captureImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Label progressTimeLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label aaa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label divergenceLabel;
        private System.Windows.Forms.Label boxOrderLabel;
        private System.Windows.Forms.Label storeCodeLabel;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button inquireButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn round;
        private System.Windows.Forms.DataGridViewTextBoxColumn storeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn boxOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn divergence;
        private System.Windows.Forms.Button saveImageButton;
    }
}