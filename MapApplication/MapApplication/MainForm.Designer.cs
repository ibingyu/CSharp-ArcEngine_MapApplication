namespace MapApplication
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.空间分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缓冲区分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.叠加分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按属性查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.符号化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.唯一值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.EagleEyeMapControl = new AxESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl2 = new AxESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl2 = new AxESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl3 = new AxESRI.ArcGIS.Controls.AxMapControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip3.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EagleEyeMapControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl3)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip3
            // 
            this.menuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.空间分析ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.符号化ToolStripMenuItem});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(1082, 28);
            this.menuStrip3.TabIndex = 5;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(51, 24);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem3.Text = "新建";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem4.Text = "打开";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem5.Text = "保存";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem6.Text = "另存为";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem7.Text = "Exit";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(51, 24);
            this.toolStripMenuItem2.Text = "窗口";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(114, 26);
            this.toolStripMenuItem9.Text = "鹰眼";
            // 
            // 空间分析ToolStripMenuItem
            // 
            this.空间分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.缓冲区分析ToolStripMenuItem,
            this.叠加分析ToolStripMenuItem});
            this.空间分析ToolStripMenuItem.Name = "空间分析ToolStripMenuItem";
            this.空间分析ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.空间分析ToolStripMenuItem.Text = "空间分析";
            // 
            // 缓冲区分析ToolStripMenuItem
            // 
            this.缓冲区分析ToolStripMenuItem.Name = "缓冲区分析ToolStripMenuItem";
            this.缓冲区分析ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.缓冲区分析ToolStripMenuItem.Text = "缓冲区分析";
            this.缓冲区分析ToolStripMenuItem.Click += new System.EventHandler(this.缓冲区分析ToolStripMenuItem_Click);
            // 
            // 叠加分析ToolStripMenuItem
            // 
            this.叠加分析ToolStripMenuItem.Name = "叠加分析ToolStripMenuItem";
            this.叠加分析ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.叠加分析ToolStripMenuItem.Text = "叠置分析";
            this.叠加分析ToolStripMenuItem.Click += new System.EventHandler(this.叠置分析ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按属性查询ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // 按属性查询ToolStripMenuItem
            // 
            this.按属性查询ToolStripMenuItem.Name = "按属性查询ToolStripMenuItem";
            this.按属性查询ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.按属性查询ToolStripMenuItem.Text = "按属性查询";
            this.按属性查询ToolStripMenuItem.Click += new System.EventHandler(this.按属性查询ToolStripMenuItem_Click);
            // 
            // 符号化ToolStripMenuItem
            // 
            this.符号化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.唯一值ToolStripMenuItem});
            this.符号化ToolStripMenuItem.Name = "符号化ToolStripMenuItem";
            this.符号化ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.符号化ToolStripMenuItem.Text = "符号化";
            // 
            // 唯一值ToolStripMenuItem
            // 
            this.唯一值ToolStripMenuItem.Name = "唯一值ToolStripMenuItem";
            this.唯一值ToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.唯一值ToolStripMenuItem.Text = "唯一值";
            this.唯一值ToolStripMenuItem.Click += new System.EventHandler(this.唯一值ToolStripMenuItem_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip2.Location = new System.Drawing.Point(0, 678);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1082, 25);
            this.statusStrip2.TabIndex = 6;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(93, 20);
            this.toolStripStatusLabel2.Text = "coordinate";
            // 
            // EagleEyeMapControl
            // 
            this.EagleEyeMapControl.Location = new System.Drawing.Point(12, 408);
            this.EagleEyeMapControl.Name = "EagleEyeMapControl";
            this.EagleEyeMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("EagleEyeMapControl.OcxState")));
            this.EagleEyeMapControl.Size = new System.Drawing.Size(295, 261);
            this.EagleEyeMapControl.TabIndex = 3;
            this.EagleEyeMapControl.OnMouseDown += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEventHandler(this.EagleEyeMapControl_OnMouseDown);
            this.EagleEyeMapControl.OnMouseUp += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEventHandler(this.EagleEyeMapControl_OnMouseUp);
            this.EagleEyeMapControl.OnMouseMove += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEventHandler(this.EagleEyeMapControl_OnMouseMove);
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Location = new System.Drawing.Point(12, 37);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(802, 28);
            this.axToolbarControl2.TabIndex = 2;
            this.axToolbarControl2.OnMouseDown += new AxESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEventHandler(this.axToolbarControl2_OnMouseDown);
            // 
            // axTOCControl2
            // 
            this.axTOCControl2.Location = new System.Drawing.Point(12, 71);
            this.axTOCControl2.Name = "axTOCControl2";
            this.axTOCControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl2.OcxState")));
            this.axTOCControl2.Size = new System.Drawing.Size(295, 331);
            this.axTOCControl2.TabIndex = 1;
            this.axTOCControl2.OnMouseDown += new AxESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEventHandler(this.axTOCControl2_OnMouseDown);
            // 
            // axMapControl3
            // 
            this.axMapControl3.Location = new System.Drawing.Point(313, 71);
            this.axMapControl3.Name = "axMapControl3";
            this.axMapControl3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl3.OcxState")));
            this.axMapControl3.Size = new System.Drawing.Size(755, 598);
            this.axMapControl3.TabIndex = 0;
            this.axMapControl3.OnMouseMove += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEventHandler(this.axMapControl3_OnMouseMove);
            this.axMapControl3.OnExtentUpdated += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEventHandler(this.axMapControl3_OnExtentUpdated);
            this.axMapControl3.OnMapReplaced += new AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEventHandler(this.axMapControl3_OnMapReplaced);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.属性表ToolStripMenuItem,
            this.移除图层ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 52);
            // 
            // 属性表ToolStripMenuItem
            // 
            this.属性表ToolStripMenuItem.Name = "属性表ToolStripMenuItem";
            this.属性表ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.属性表ToolStripMenuItem.Text = "属性表";
            this.属性表ToolStripMenuItem.Click += new System.EventHandler(this.属性表ToolStripMenuItem_Click);
            // 
            // 移除图层ToolStripMenuItem
            // 
            this.移除图层ToolStripMenuItem.Name = "移除图层ToolStripMenuItem";
            this.移除图层ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.移除图层ToolStripMenuItem.Text = "移除图层";
            this.移除图层ToolStripMenuItem.Click += new System.EventHandler(this.移除图层ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1082, 703);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.EagleEyeMapControl);
            this.Controls.Add(this.axToolbarControl2);
            this.Controls.Add(this.axTOCControl2);
            this.Controls.Add(this.axMapControl3);
            this.Controls.Add(this.menuStrip3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EagleEyeMapControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl3)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private AxESRI.ArcGIS.Controls.AxMapControl axMapControl3;
        private AxESRI.ArcGIS.Controls.AxTOCControl axTOCControl2;
        private AxESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
        private AxESRI.ArcGIS.Controls.AxMapControl EagleEyeMapControl;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem 空间分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缓冲区分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 叠加分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按属性查询ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 符号化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 唯一值ToolStripMenuItem;
    }
}

