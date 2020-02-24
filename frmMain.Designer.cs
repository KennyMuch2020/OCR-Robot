namespace OCRTrainee
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gb7 = new System.Windows.Forms.GroupBox();
            this.lb_status = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_type = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnLoadType = new System.Windows.Forms.Button();
            this.btnDelType = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.gb5 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.txType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ofdImage = new System.Windows.Forms.OpenFileDialog();
            this.gb7.SuspendLayout();
            this.gb5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb7
            // 
            this.gb7.Controls.Add(this.lb_status);
            this.gb7.Controls.Add(this.button2);
            this.gb7.Controls.Add(this.tb_type);
            this.gb7.Controls.Add(this.listBox1);
            this.gb7.Controls.Add(this.button3);
            this.gb7.Controls.Add(this.btnLoadType);
            this.gb7.Controls.Add(this.btnDelType);
            this.gb7.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gb7.Location = new System.Drawing.Point(14, 362);
            this.gb7.Name = "gb7";
            this.gb7.Size = new System.Drawing.Size(289, 249);
            this.gb7.TabIndex = 50;
            this.gb7.TabStop = false;
            this.gb7.Text = "Project";
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_status.Location = new System.Drawing.Point(162, 216);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(49, 20);
            this.lb_status.TabIndex = 65;
            this.lb_status.Text = "Status";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(142, 111);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 64;
            this.button2.Text = "Exe_Window";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tb_type
            // 
            this.tb_type.Location = new System.Drawing.Point(15, 216);
            this.tb_type.Name = "tb_type";
            this.tb_type.ReadOnly = true;
            this.tb_type.Size = new System.Drawing.Size(117, 27);
            this.tb_type.TabIndex = 61;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(16, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(117, 184);
            this.listBox1.TabIndex = 60;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(142, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 34);
            this.button3.TabIndex = 63;
            this.button3.Text = "Reboot";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btnLoadType
            // 
            this.btnLoadType.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLoadType.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadType.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLoadType.Location = new System.Drawing.Point(142, 27);
            this.btnLoadType.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadType.Name = "btnLoadType";
            this.btnLoadType.Size = new System.Drawing.Size(112, 34);
            this.btnLoadType.TabIndex = 51;
            this.btnLoadType.Text = "Load_Story";
            this.btnLoadType.UseVisualStyleBackColor = false;
            this.btnLoadType.Click += new System.EventHandler(this.btnLoadType_Click);
            // 
            // btnDelType
            // 
            this.btnDelType.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDelType.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelType.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDelType.Location = new System.Drawing.Point(142, 69);
            this.btnDelType.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelType.Name = "btnDelType";
            this.btnDelType.Size = new System.Drawing.Size(112, 34);
            this.btnDelType.TabIndex = 52;
            this.btnDelType.Text = "Remove_Story";
            this.btnDelType.UseVisualStyleBackColor = false;
            this.btnDelType.Click += new System.EventHandler(this.btnDelType_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 7);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(755, 442);
            this.richTextBox1.TabIndex = 51;
            this.richTextBox1.Text = "";
            // 
            // gb5
            // 
            this.gb5.BackColor = System.Drawing.Color.White;
            this.gb5.Controls.Add(this.button5);
            this.gb5.Controls.Add(this.txType);
            this.gb5.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gb5.Location = new System.Drawing.Point(18, 287);
            this.gb5.Margin = new System.Windows.Forms.Padding(4);
            this.gb5.Name = "gb5";
            this.gb5.Padding = new System.Windows.Forms.Padding(4);
            this.gb5.Size = new System.Drawing.Size(288, 77);
            this.gb5.TabIndex = 52;
            this.gb5.TabStop = false;
            this.gb5.Text = "New OCR";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.SteelBlue;
            this.button5.ForeColor = System.Drawing.SystemColors.Control;
            this.button5.Location = new System.Drawing.Point(138, 24);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 34);
            this.button5.TabIndex = 54;
            this.button5.Text = "New Story";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txType
            // 
            this.txType.Location = new System.Drawing.Point(13, 28);
            this.txType.Margin = new System.Windows.Forms.Padding(4);
            this.txType.Name = "txType";
            this.txType.Size = new System.Drawing.Size(117, 27);
            this.txType.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(152, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 84);
            this.label2.TabIndex = 1;
            this.label2.Text = "OCR ROBOT !";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(597, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "MUCH 2020";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.hScrollBar1);
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Location = new System.Drawing.Point(15, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 173);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Camera";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.SteelBlue;
            this.button7.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.Control;
            this.button7.Location = new System.Drawing.Point(137, 62);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 34);
            this.button7.TabIndex = 69;
            this.button7.Text = "Save_Img";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.SteelBlue;
            this.button6.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.SystemColors.Control;
            this.button6.Location = new System.Drawing.Point(19, 62);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 34);
            this.button6.TabIndex = 68;
            this.button6.Text = "Master_Img";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(137, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 67;
            this.button1.Text = "Close Cam";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(19, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 51;
            this.label4.Text = "Expo（us）";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.SteelBlue;
            this.button4.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.Control;
            this.button4.Location = new System.Drawing.Point(19, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 34);
            this.button4.TabIndex = 55;
            this.button4.Text = "Grab_Img";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(137, 108);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(112, 27);
            this.textBox1.TabIndex = 56;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(19, 136);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(230, 27);
            this.hScrollBar1.TabIndex = 52;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(313, 118);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 494);
            this.tabControl1.TabIndex = 70;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.hWindowControl3);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ImageGrad";
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(771, 453);
            this.hWindowControl3.TabIndex = 0;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(771, 453);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(767, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VisionScript";
            // 
            // ofdImage
            // 
            this.ofdImage.FileName = "ofdImage";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1092, 624);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gb5);
            this.Controls.Add(this.gb7);
            this.Name = "frmMain";
            this.Text = "OCRTrainee";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gb7.ResumeLayout(false);
            this.gb7.PerformLayout();
            this.gb5.ResumeLayout(false);
            this.gb5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gb7;
        private System.Windows.Forms.TextBox tb_type;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnLoadType;
        private System.Windows.Forms.Button btnDelType;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox gb5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private HalconDotNet.HWindowControl hWindowControl3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.OpenFileDialog ofdImage;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label lb_status;
    }
}

