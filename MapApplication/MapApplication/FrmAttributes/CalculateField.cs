using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;

namespace MapApplication.FrmAttributes
{
    public partial class CalculateField : Form
    {
        private IFeatureLayer pFeatureLayer = null;
        private IFeatureClass pFeatureClass = null;
        private DataGridView dgView = null;
        private string strField="";
        public CalculateField(IFeatureLayer _FeatureLayer,DataGridView _dgv, string _strField)
        {
            InitializeComponent();
            pFeatureLayer = _FeatureLayer;
            dgView = _dgv;
            strField = _strField;
        }

        #region 初始化字段列表
        private void CalculateField_Load(object sender, EventArgs e)
        {
            //初始化字段列表
            pFeatureClass = pFeatureLayer.FeatureClass;
            string strFieldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFieldName = pFeatureClass.Fields.get_Field(i).Name;
                this.listBoxField.Items.Add("!" + strFieldName + "!");//因为使用Python计算字段，所以使用！(python内部实现的格式)，若为VB则使用[]

            }
            this.listBoxField.SelectedIndex = 0;
            label2.Text = strField + "=";
        } 
        #endregion
        #region 点击添加文本

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "* ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "/ ";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "& ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "+ ";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "- ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = "= ";
        }

        private void listBoxField_DoubleClick(object sender, EventArgs e)
        {
            textBoxCal.SelectedText = this.listBoxField.SelectedItem.ToString();
        }
        #endregion

        private void listBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //取消
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 字段计算
        //执行计算并刷新
        private void btnOK_Click(object sender, EventArgs e)
        {
            string strResult = FieldCal(pFeatureLayer, strField, textBoxCal.Text);
            MessageBox.Show(strResult);
            this.Close();
            RefreshTable refreshtable = new RefreshTable();
            refreshtable.Refresh(dgView, pFeatureLayer);
        }
        //计算字段的函数
        private string FieldCal(IFeatureLayer pFtLayer, string strField, string strExpression)
        {

            try
            {
                Geoprocessor Gp = new Geoprocessor();
                Gp.OverwriteOutput = true;
                ESRI.ArcGIS.DataManagementTools.CalculateField calField = new ESRI.ArcGIS.DataManagementTools.CalculateField();
                calField.in_table = pFtLayer as ITable;
                calField.field = strField;
                calField.expression = strExpression;
                calField.expression_type = "PYTHON";
                Gp.Execute(calField, null);

                for (int i = 0; i < Gp.MessageCount; i++)
                {
                    txtMessage.Text += Gp.GetMessage(i).ToString() + "\r\n";
                }
                return "计算成功";
            }
            catch (Exception e)
            {
                txtMessage.Text += e.Message;
                return "计算失败" + e.Message;
            }
        } 
        #endregion
    }
}
