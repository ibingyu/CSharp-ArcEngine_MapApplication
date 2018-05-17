using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace MapApplication
{
    public partial class Buffer : Form
    {
        public Buffer(object hook)
        {
            InitializeComponent();
            m_hookHelper.Hook = hook;
        }
        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr wnd,
                                          uint Msg,
                                          IntPtr wParam,
                                          IntPtr lParam);
        private IHookHelper m_hookHelper = new HookHelperClass();
        private const uint WM_VSCROLL = 0x0115;
        private const uint SB_BOTTOM = 7;
        public string strOutPutPath;
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Buffer_Load(object sender, EventArgs e)
        {
            if (m_hookHelper==null||m_hookHelper.Hook==null||m_hookHelper.FocusMap.LayerCount==0)
            {
                return;
            }

            IEnumLayer layers = GetLayers();
            layers.Reset();
            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                chooseLayer.Items.Add(layer.Name);
            }
            //选择图层
            if (chooseLayer.Items.Count > 0)
                chooseLayer.SelectedIndex = 0;

            string tempDir = System.IO.Path.GetTempPath();
            TxtOutPutPath.Text = System.IO.Path.Combine(tempDir, ((string)chooseLayer.SelectedItem + "_buffer.shp"));

            //设置默认的缓冲单位
            int units = Convert.ToInt32(m_hookHelper.FocusMap.MapUnits);
            Units.SelectedIndex = units;
        }

        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer layers = m_hookHelper.FocusMap.get_Layers(uid, true);

            return layers;
        }

        private void FilePath_Click(object sender, EventArgs e)
        {
            //设置输出的缓冲图层
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.CheckPathExists = true;
            saveDlg.Filter = "Shapefile (*.shp)|*.shp";
            saveDlg.OverwritePrompt = true;
            saveDlg.Title = "保存数据";
            saveDlg.RestoreDirectory = true;
            saveDlg.FileName = (string)chooseLayer.SelectedItem + "_buffer.shp";

            DialogResult dr = saveDlg.ShowDialog();
            if (dr == DialogResult.OK)
                TxtOutPutPath.Text = saveDlg.FileName;
        }

        private void outBuffer_Click(object sender, EventArgs e)
        {
            
            double bufferDistance;
            //转换distance为double类型
            double.TryParse(BufferDistance.Text, out bufferDistance);
            if (0.0 == bufferDistance)
            {
                MessageBox.Show("错误的距离!");
                return;
            }
            //判断输出路径是否合法
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(TxtOutPutPath.Text)) ||
              ".shp" != System.IO.Path.GetExtension(TxtOutPutPath.Text))
            {
                MessageBox.Show("错误的路径!");
                return;
            }
            //判断图层个数
            if (m_hookHelper.FocusMap.LayerCount == 0)
                return;

            //获取图层
            IFeatureLayer layer = GetFeatureLayer((string)chooseLayer.SelectedItem);
            if (null == layer)
            {
                txtMessage.Text += "图层 " + (string)chooseLayer.SelectedItem + "不能被建立!\r\n";
                return;
            }

            //将文本框滚动到底部
            ScrollToBottom();

            txtMessage.Text += "\r\n分析开始，这可能需要几分钟时间,请稍候..\r\n";
            txtMessage.Update();
            //get an instance of the geoprocessor
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            strOutPutPath = TxtOutPutPath.Text;
            //create a new instance of a buffer tool
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer(layer, TxtOutPutPath.Text, Convert.ToString(bufferDistance) + " " + (string)Units.SelectedItem);
            buffer.dissolve_option = "ALL";//这个要设成ALL,否则相交部分不会融合
                                           //buffer.line_side = "FULL";//默认是"FULL",最好不要改否则出错
                                           //buffer.line_end_type = "ROUND";//默认是"ROUND",最好不要改否则出错

            //execute the buffer tool (very easy :-))
            IGeoProcessorResult results = null;

            try
            {
                results = (IGeoProcessorResult)gp.Execute(buffer, null);
            }
            catch (Exception ex)
            {
                txtMessage.Text += "图层: " + layer.Name + "缓冲区生成失败！" + "\r\n";
            }
            if (results.Status != esriJobStatus.esriJobSucceeded)
            {
                txtMessage.Text += "图层: " + layer.Name + "缓冲区生成失败！" + "\r\n";
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                txtMessage.Text += "缓冲区生成成功！";
            }

            //scroll the textbox to the bottom
            ScrollToBottom();

            txtMessage.Text += "\r\n分析完成.\r\n";
            txtMessage.Text += "-----------------------------------------------------------------------------------------\r\n";
            //scroll the textbox to the bottom
            ScrollToBottom();
        }

        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            //获取图层
            IEnumLayer layers = GetLayers();
            layers.Reset();

            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }

            return null;
        }
        private void ScrollToBottom()
        {
            PostMessage((IntPtr)txtMessage.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, (IntPtr)IntPtr.Zero);
        }
    }

}
