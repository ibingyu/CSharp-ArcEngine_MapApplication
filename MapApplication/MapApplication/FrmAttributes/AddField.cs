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
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace MapApplication.FrmAttributes
{
    public partial class AddField : Form
    {
        private IFeatureLayer _FeatureLayer = null;
        private DataGridView _dgv;
        public AddField(IFeatureLayer pFeatureLayer, DataGridView dgv)
        {
            InitializeComponent();
            _FeatureLayer = pFeatureLayer;
            _dgv = dgv;
        }

        private void AddField_Load(object sender, EventArgs e)
        {
            this.cmbFieldType.Items.Add("长整型");
            this.cmbFieldType.Items.Add("短整型");
            this.cmbFieldType.Items.Add("浮点型");
            this.cmbFieldType.Items.Add("双精度");
            this.cmbFieldType.Items.Add("文本型");
            this.cmbFieldType.Items.Add("日期型");
            this.cmbFieldType.SelectedIndex = 0;
        }

        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFieldType = cmbFieldType.Text;
            switch (strFieldType)
            {
                case "长整型":
                    {
                        txtPrecision.Visible = true;
                        txtScale.Visible = false;
                        break;
                    }
                case "短整型":
                    {
                        txtPrecision.Visible = true;
                        txtScale.Visible = false;
                        break;
                    }
                case "浮点型":
                    {
                        txtPrecision.Visible = true;
                        txtScale.Visible = true;
                        break;
                    }
                case "双精度":
                    {
                        txtPrecision.Visible = true;
                        txtScale.Visible = true;
                        break;
                    }
                case "文本型":
                    {
                        txtPrecision.Visible = true;
                        txtScale.Visible = false;
                        label3.Text = "长度";
                        break;
                    }
                default://日期型0  
                    {
                        txtPrecision.Visible = false;
                        txtScale.Visible = false;
                        break;
                    }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strFieldName = txtFieldName.Text;
            string strFieldType = cmbFieldType.Text;
            try
            {
                IFeatureLayer editAttributeLayer = _FeatureLayer;

                //Field collection  
                IFieldsEdit pFieldsEdit;
                //获取FeatureLayer  
                IFeatureLayer pFeatureLayer = editAttributeLayer;

                //从FeatureLayer获取工作空间  
                IDataset pDataSet = pFeatureLayer.FeatureClass as IDataset;
                IWorkspace pWorkSpace = pDataSet.Workspace;
                //设置字段属性  
                IField pNewField = new FieldClass();
                IFieldEdit pFieldEdit = pNewField as IFieldEdit;
                pFieldEdit.AliasName_2 = strFieldName;
                pFieldEdit.Name_2 = strFieldName;
                switch (strFieldType)
                {
                    case "长整型":
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                            pFieldEdit.Precision_2 = int.Parse(txtPrecision.Text);
                            break;
                        }
                    case "短整型":
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                            pFieldEdit.Precision_2 = int.Parse(txtPrecision.Text);
                            break;
                        }
                    case "浮点型":
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSingle;
                            pFieldEdit.Precision_2 = int.Parse(txtPrecision.Text);
                            pFieldEdit.Scale_2 = int.Parse(txtScale.Text);
                            break;
                        }
                    case "双精度":
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                            pFieldEdit.Precision_2 = int.Parse(txtPrecision.Text);
                            pFieldEdit.Scale_2 = int.Parse(txtScale.Text);
                            break;
                        }
                    case "文本型":
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                            pFieldEdit.Length_2 = int.Parse(txtPrecision.Text);
                            break;
                        }
                    default://日期型0  
                        {
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                            break;
                        }
                }
                //添加字段  
                try
                {
                    int theField = pFeatureLayer.FeatureClass.Fields.FindField(strFieldName);
                    if (theField == -1)
                    {
                        pFeatureLayer.FeatureClass.AddField(pFieldEdit);
                        initField(pFeatureLayer.FeatureClass,strFieldName);
                        MessageBox.Show("字段添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("字段已经存在！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Field " + pFieldEdit.Name + " was not added due to an error (" + ex.Message + " )");
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            RefreshTable refresh = new RefreshTable();
            refresh.Refresh(_dgv, _FeatureLayer);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initField(IFeatureClass pFeatureClass, string strFieldName)
        {
            //IQueryFilter pFilter = new QueryFilterClass();
            //pFilter.WhereClause = '"'+strFieldName+"="+null+'"';
            //pFilter.SubFields = '"'+strFieldName+'"';
            //int nIndex = pFeatureClass.FindField(strFieldName);
            //ITable pTable = pFeatureClass as ITable;
            //IRowBuffer pBuffer = pTable.CreateRowBuffer();
            //pBuffer.set_Value(nIndex, 0);
            //pTable.UpdateSearchedRows(pFilter, pBuffer);
            //Marshal.ReleaseComObject(pFilter);
            for (int i = 0; i < pFeatureClass.FeatureCount(null); i++)
            {
                IFeature pFeature = pFeatureClass.GetFeature(i);
                pFeature.set_Value(pFeature.Fields.FindField(strFieldName),0);
                pFeature.Store();
            }
        }
    }
}
