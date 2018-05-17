using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using MapApplication.FrmAttributes;


namespace MapApplication
{
    public partial class FrmAttribute : Form
    {
        private ILayer pLayer;//打开属性表的图层
        private IFeatureLayer pFeatureLayer;
        private IFeatureClass pFeatureClass;
        private ILayerFields pLayerFields;
        RowAndCol[] pRowAndCol = new RowAndCol[10000];
        private int count = 0;
        int row_index = 0;
        int col_index = 0;
        bool up = true;
        ITableSort pTs;//处理排序  
        public FrmAttribute()
        {
            InitializeComponent();

        }

        public IFeatureLayer currentLayer { get; set; }
        public AxMapControl _MapControl { get; set; }
        public IMapControl3 m_mapControl { get; set; }

        #region 初始化属性表
        //初始化属性表
        public void SetTable()
        {
            try
            {
                
                pLayer = currentLayer;

                pFeatureLayer = pLayer as IFeatureLayer;
                pFeatureClass = pFeatureLayer.FeatureClass;
                pLayerFields = pFeatureLayer as ILayerFields;
                DataSet ds = new DataSet("dsTest");
                DataTable dt = new DataTable(pFeatureLayer.Name);
                DataColumn dc = null;
                for (int i = 0; i < pLayerFields.FieldCount; i++)
                {
                    dc = new DataColumn(pLayerFields.get_Field(i).Name);
                    dt.Columns.Add(dc);
                    dc = null;
                }

                IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < pLayerFields.FieldCount; j++)
                    {
                        if (pLayerFields.FindField(pFeatureClass.ShapeFieldName) == j)
                        {
                            dr[j] = pFeatureClass.ShapeType.ToString();
                        }
                        else
                        {
                            dr[j] = pFeature.get_Value(j);
                        }
                    }
                    dt.Rows.Add(dr);
                    pFeature = pFeatureCursor.NextFeature();
                }
                dataGridView1.DataSource = dt;
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                
                //if (currentLayer == null) { return; }
                //IFeature feature = null;
                //DataTable dataTable = new DataTable();
                //DataRow dataRow = null;
                //DataColumn dataColumn = null;
                //IField field = null;
                ////创建表的字段名
                //for (int i = 0; i < currentLayer.FeatureClass.Fields.FieldCount; i++)
                //{
                //    dataColumn = new DataColumn();
                //    field = currentLayer.FeatureClass.Fields.get_Field(i);
                //    dataColumn.ColumnName = field.AliasName;
                //    dataTable.Columns.Add(dataColumn);
                //}
                ////添加具体的数据
                //IFeatureCursor cursor = currentLayer.Search(null, true);
                //feature = cursor.NextFeature();
                //while (feature != null)
                //{
                //    dataRow = dataTable.NewRow();
                //    for (int i = 0; i < dataTable.Columns.Count; i++)
                //    {
                //        dataRow[i] = feature.get_Value(i);
                //    }
                //    dataTable.Rows.Add(dataRow);
                //    cursor.NextFeature();
                //}
                ////释放指针
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                ////设置为数据源
                //dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Dispose();
            }
        } 
        #endregion
        //public static string getValidFeatureClassName(string FCname)

        //{

        //int dot = FCname.IndexOf(".");

        //if (dot != -1)

        //{

        //return FCname.Replace(".", "_");

        //}

        //return FCname;

        //}

        private void FrmAttribute_Load(object sender, EventArgs e)
        {
            //try
            //{               
            //    string tableName;
            //    tableName = getValidFeatureClassName(pLayer.Name);
            //    this.Text = tableName + "属性表".ToString();  //替换窗体名称
            //    pFeatureLayer = pLayer as IFeatureLayer;
            //    pFeatureClass = pFeatureLayer.FeatureClass;
            //    pLayerFields = pFeatureLayer as ILayerFields;
            //    DataSet ds = new DataSet("dsTest");
            //    DataTable dt = new DataTable(pFeatureLayer.Name);
            //    DataColumn dc = null;
            //    for (int i = 0; i < pLayerFields.FieldCount; i++)
            //    {
            //        dc = new DataColumn(pLayerFields.get_Field(i).Name);
            //        dt.Columns.Add(dc);
            //        dc = null;
            //    }
            //    IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            //    IFeature pFeature = pFeatureCursor.NextFeature();
            //    while (pFeature != null)
            //    {
            //        DataRow dr = dt.NewRow();
            //        for (int j = 0; j < pLayerFields.FieldCount; j++)
            //        {
            //            if (pLayerFields.FindField(pFeatureClass.ShapeFieldName) == j)
            //            {
            //                dr[j] = pFeatureClass.ShapeType.ToString();
            //            }
            //            else
            //            {
            //                dr[j] = pFeature.get_Value(j);
            //            }
            //        }
            //        dt.Rows.Add(dr);
            //        pFeature = pFeatureCursor.NextFeature();
            //    }
            //    dataGridView1.DataSource = dt;
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show("读取属性表失败：" + exc.Message);
            //    this.Dispose();
            //}


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        #region 添加字段
        //调用添加字段的窗体
        private void button1_Click(object sender, EventArgs e)
        {
            ILayer pLayer = currentLayer;
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            AddField addField = new AddField(pFeatureLayer, dataGridView1);
            addField.Show();
        }
        #endregion

        //要素存储
        public struct RowAndCol
        {
            //字段  
            private int row;
            private int column;
            private string _value;

            //行属性  
            public int Row
            {
                get
                {
                    return row;
                }
                set
                {
                    row = value;
                }
            }
            //列属性  
            public int Column
            {
                get
                {
                    return column;
                }
                set
                {
                    column = value;
                }
            }
            //值属性  
            public string Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }
        }

        #region 编辑属性
        //编辑属性
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 2].Cells[0];
        }
        #endregion

        #region 保存属性
        //保存属性
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            ILayer pLayer1 = currentLayer;
            IFeatureLayer pFeatureLayer1 = pLayer1 as IFeatureLayer;
            IFeatureClass pFeatureClass1 = pFeatureLayer1.FeatureClass;
            ITable pTable;
            //pTable = pFeatureClass.CreateFeature().Table;//很重要的一种获取shp表格的一种方式           
            pTable = pFeatureLayer1 as ITable;
            //将改变的记录值传给shp中的表  
            int i = 0;
            while (pRowAndCol[i].Column != 0 || pRowAndCol[i].Row != 0)
            {
                IRow pRow;
                pRow = pTable.GetRow(pRowAndCol[i].Row);
                pRow.set_Value(pRowAndCol[i].Column, pRowAndCol[i].Value);
                pRow.Store();
                i++;
            }
            count = 0;
            for (int j = 0; j < i; j++)
            {
                pRowAndCol[j].Row = 0;
                pRowAndCol[j].Column = 0;
                pRowAndCol[j].Value = null;
            }
            MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 记录值一旦改变触发此事件
            //在dataGridView中获取改变记录的行数，列数和记录值  
            pRowAndCol[count].Row = dataGridView1.CurrentCell.RowIndex;
            pRowAndCol[count].Column = dataGridView1.CurrentCell.ColumnIndex;
            pRowAndCol[count].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString();
            count++;
        }




        #endregion

        #region 删除选中要素
        //删除选中的要素
        private void button4_Click(object sender, EventArgs e)
        {
            row_index = dataGridView1.CurrentRow.Index;
            if (((MessageBox.Show("确定要删除吗", "警告", MessageBoxButtons.YesNo)) == DialogResult.Yes))
            {
                ILayer pLayer = currentLayer;
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                ITable pTable = pFeatureLayer as ITable;
                IRow pRow = pTable.GetRow(row_index);
                pRow.Delete();
                TableShow();
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK);
                _MapControl.ActiveView.Refresh();
                RefreshTable refresh = new RefreshTable();
                refresh.Refresh(dataGridView1, pFeatureLayer);
            }
        }

        private void TableShow()
        {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

            dataGridView1.Refresh();
        } 
        #endregion

        //调用字段计算器窗口
        private void button5_Click(object sender, EventArgs e)
        {
            int indexNum = dataGridView1.CurrentCell.ColumnIndex;
            string strField = dataGridView1.Columns[indexNum].HeaderText.ToString();
            ILayer pLayer = currentLayer;
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            CalculateField calculate =new CalculateField(pFeatureLayer,dataGridView1,strField);
            calculate.Show();
        }

        #region 选中要素高亮
        //选中要素高亮
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            IQueryFilter pQuery = new QueryFilterClass();
            int count = this.dataGridView1.SelectedRows.Count;
            string val;
            string col;
            col = this.dataGridView1.Columns[0].Name;
            //当只选中一行时  
            if (count == 1)
            {
                val = this.dataGridView1.SelectedRows[0].Cells[col].Value.ToString();
                //设置高亮要素的查询条件  
                pQuery.WhereClause = col + "=" + val;
            }
            else//当选中多行时  
            {
                //int i;
                //string str;
                //for (i = 0; i < count - 1; i++)
                //{
                //    val = this.dataGridView1.SelectedRows[i].Cells[col].Value.ToString();
                //    str = col + "=" + val + " OR ";
                //    pQuery.WhereClause += str;
                //}
                ////添加最后一个要素的条件  
                //val = this.dataGridView1.SelectedRows[i].Cells[col].Value.ToString();
                //str = col + "=" + val;
                //pQuery.WhereClause += str;
            }
            ILayer pLayer = currentLayer;
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            IFeatureSelection pFeatSelection;
            pFeatSelection = pFLayer as IFeatureSelection;
            pFeatSelection.SelectFeatures(pQuery, esriSelectionResultEnum.esriSelectionResultNew, false);
            _MapControl.ActiveView.Refresh();
        }
        #endregion
        /// <summary>  
        /// 数据排序  
        /// </summary>  
        /// <param name="pFeatureClass"></param>  
        //排序函数
        private void SortFeatures(IFeatureClass pFeatureClass,IFeatureLayer _pLayer)
        {
            ITableSort pTableSort = new TableSortClass();
            IFields pFields = pFeatureClass.Fields;
            IField pField = pFields.get_Field(col_index);
            col_index = 0;
            pTableSort.Fields = pField.Name;

            if (up)
            {
                pTableSort.set_Ascending(pField.Name, true);
            }
            else
            {
                pTableSort.set_Ascending(pField.Name, false);
            }
            pTableSort.set_CaseSensitive(pField.Name, true);
            pTableSort.Table = pFeatureClass as ITable;
            pTableSort.Sort(null);
            ICursor pCursor = pTableSort.Rows;
            pTs = pTableSort;
            RefreshTable(_pLayer,pFeatureClass,dataGridView1,pField.Name,pCursor);
            
        }

        private void RefreshTable(IFeatureLayer _layer,IFeatureClass pFeatureClass1,DataGridView _data,string strName, ICursor pCursor)
        {
            int index = pCursor.Fields.FindField(strName);
            IFields pFields = pFeatureClass1.Fields;
            DataSet ds = new DataSet("dsFields");
            DataTable dt = new DataTable(pFeatureLayer.Name);
            DataColumn dc = null;
            //ITable table=_layer as ITable;
            
            IRow row = null;
            //IRow tablerow = null;
            for (int i = 0; i < pFields.FieldCount; i++)
            {

                dc = new DataColumn(pFields.get_Field(i).Name);
                dt.Columns.Add(dc);
                dc = null;
            }
            while ((row = pCursor.NextRow()) != null)
            {
                //tablerow = table.GetRow(row.OID);
                DataRow dr = dt.NewRow();
                for (int j = 0; j < pFields.FieldCount; j++)
                {
                    if (pCursor.Fields.FindField(pFeatureClass.ShapeFieldName) == j)
                    {
                        dr[j] = pFeatureClass.ShapeType.ToString();
                    }
                    else
                    {
                        dr[j] = row.get_Value(j);
                        //tablerow.set_Value(j+1,row.get_Value(j+1));
                    }
                    
                    //tablerow.Store();
                }
                dt.Rows.Add(dr);
                
                
            }
            
            _data.DataSource = dt;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }
        //表头右键功能
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //点击的是鼠标右键，并且不是表头
            {
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示
            }
        }

        private void 排序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            col_index = dataGridView1.CurrentCell.ColumnIndex;
            ILayer pLayer = currentLayer;
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            up = true;
            SortFeatures(pFLayer.FeatureClass,pFLayer);
        }

        private void 逆序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            col_index= dataGridView1.CurrentCell.ColumnIndex;
            ILayer pLayer = currentLayer;
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            up = false;
            SortFeatures(pFLayer.FeatureClass,pFLayer);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indexNum = dataGridView1.CurrentCell.ColumnIndex;
            string strField = dataGridView1.Columns[indexNum].HeaderText.ToString();
            ILayer pLayer = (ILayer)m_mapControl.CustomProperty;
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            string strResult = "";
            if ((MessageBox.Show("确定要删除该字段吗?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                strResult = DeleteField(pFLayer, strField);
                dataGridView1.Columns.Remove(strField);
                MessageBox.Show(strResult, "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>  
        /// 删除属性表字段  
        /// </summary>  
        /// <param name="layer">需要添加字段的IFeatureLayer</param>  
        /// <param name="fieldName">添加的字段的名称</param>  
        /// <returns></returns>  
        public string DeleteField(IFeatureLayer layer, string fieldName)
        {
            try
            {
                ITable pTable = (ITable)layer;
                IFields pfields;
                IField pfield;
                pfields = pTable.Fields;
                int fieldIndex = pfields.FindField(fieldName);
                pfield = pfields.get_Field(fieldIndex);
                pTable.DeleteField(pfield);
                return "删除成功!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indexNum = dataGridView1.CurrentCell.ColumnIndex;
            string strField = dataGridView1.Columns[indexNum].HeaderText.ToString();
            ILayer pLayer = currentLayer;
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            CalculateField formcalfield = new CalculateField(pFLayer, dataGridView1, strField);
            formcalfield.Show();
        }
    }
}
