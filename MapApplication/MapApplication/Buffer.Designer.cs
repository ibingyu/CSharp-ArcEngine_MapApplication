namespace MapApplication
{
    partial class Buffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buffer));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FilePath = new System.Windows.Forms.Button();
            this.TxtOutPutPath = new System.Windows.Forms.TextBox();
            this.Units = new System.Windows.Forms.ComboBox();
            this.BufferDistance = new System.Windows.Forms.TextBox();
            this.chooseLayer = new System.Windows.Forms.ComboBox();
            this.outBuffer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.close = new System.Windows.Forms.Button();
            this.axLicenseControl1 = new AxESRI.ArcGIS.Controls.AxLicenseControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择图层：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FilePath);
            this.groupBox1.Controls.Add(this.TxtOutPutPath);
            this.groupBox1.Controls.Add(this.Units);
            this.groupBox1.Controls.Add(this.BufferDistance);
            this.groupBox1.Controls.Add(this.chooseLayer);
            this.groupBox1.Controls.Add(this.outBuffer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 186);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(405, 92);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(60, 30);
            this.FilePath.TabIndex = 8;
            this.FilePath.Text = "...";
            this.FilePath.UseVisualStyleBackColor = true;
            this.FilePath.Click += new System.EventHandler(this.FilePath_Click);
            // 
            // TxtOutPutPath
            // 
            this.TxtOutPutPath.Location = new System.Drawing.Point(109, 97);
            this.TxtOutPutPath.Name = "TxtOutPutPath";
            this.TxtOutPutPath.ReadOnly = true;
            this.TxtOutPutPath.Size = new System.Drawing.Size(290, 25);
            this.TxtOutPutPath.TabIndex = 7;
            // 
            // Units
            // 
            this.Units.FormattingEnabled = true;
            this.Units.Items.AddRange(new object[] {
            "Unknown",
            "Inches",
            "Points",
            "Feet",
            "Yards",
            "Miles",
            "NauticalMiles",
            "Millimeters",
            "Centimeters",
            "Meters",
            "Kilometers",
            "DecimalDegrees",
            "Decimeters"});
            this.Units.Location = new System.Drawing.Point(235, 53);
            this.Units.Name = "Units";
            this.Units.Size = new System.Drawing.Size(155, 23);
            this.Units.TabIndex = 6;
            // 
            // BufferDistance
            // 
            this.BufferDistance.Location = new System.Drawing.Point(109, 52);
            this.BufferDistance.Name = "BufferDistance";
            this.BufferDistance.Size = new System.Drawing.Size(100, 25);
            this.BufferDistance.TabIndex = 5;
            this.BufferDistance.Text = "1";
            // 
            // chooseLayer
            // 
            this.chooseLayer.FormattingEnabled = true;
            this.chooseLayer.Location = new System.Drawing.Point(109, 18);
            this.chooseLayer.Name = "chooseLayer";
            this.chooseLayer.Size = new System.Drawing.Size(356, 23);
            this.chooseLayer.TabIndex = 4;
            // 
            // outBuffer
            // 
            this.outBuffer.Location = new System.Drawing.Point(204, 140);
            this.outBuffer.Name = "outBuffer";
            this.outBuffer.Size = new System.Drawing.Size(80, 30);
            this.outBuffer.TabIndex = 3;
            this.outBuffer.Text = "分析";
            this.outBuffer.UseVisualStyleBackColor = true;
            this.outBuffer.Click += new System.EventHandler(this.outBuffer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "输出图层：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "距离：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Location = new System.Drawing.Point(12, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 135);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 24);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(466, 104);
            this.txtMessage.TabIndex = 0;
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(216, 372);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(80, 30);
            this.close.TabIndex = 3;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(464, 193);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // Buffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 414);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Buffer";
            this.Text = "缓冲区分析";
            this.Load += new System.EventHandler(this.Buffer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button FilePath;
        private System.Windows.Forms.TextBox TxtOutPutPath;
        private System.Windows.Forms.ComboBox Units;
        private System.Windows.Forms.TextBox BufferDistance;
        private System.Windows.Forms.ComboBox chooseLayer;
        private System.Windows.Forms.Button outBuffer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button close;
        private AxESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}