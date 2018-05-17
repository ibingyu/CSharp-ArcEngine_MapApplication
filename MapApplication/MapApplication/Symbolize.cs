using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MapApplication
{
    public partial class Symbolize : Form
    {
        private AxTOCControl axTOCControl1 = null;
        private AxMapControl _MapControl = null;
        //选中图层 
        private IFeatureLayer mFeatureLayer=null;
        //根据所选择的图层查询得到的特征类
        private IFeatureClass pFeatureClass = null;
        private string[] strFields = new string[100000];
        private int index = 0;
        //图层 
        ILayer pLayer;
        public Symbolize(AxMapControl _axMap,AxTOCControl _axTocControl)
        {
            InitializeComponent();
            _MapControl = _axMap;
            axTOCControl1 = _axTocControl;
        }
        


        //单一或多字段符号化
        private void UniqueValueRenderer(IFeatureLayer pFeatLyr, string[] sFieldName)
        {

            IFeatureLayer pFLayer = pLayer as IFeatureLayer;

            IGeoFeatureLayer geoLayer = pLayer as IGeoFeatureLayer;

            IFeatureClass fcls = pFLayer.FeatureClass;

            IQueryFilter pQueryFilter = new QueryFilterClass();

            IFeatureCursor fCursor = fcls.Search(pQueryFilter, false);

            IRandomColorRamp rx = new RandomColorRampClass();

            rx.MinSaturation = 15;

            rx.MaxSaturation = 30;

            rx.MinValue = 85;

            rx.MaxValue = 100;

            rx.StartHue = 0;

            rx.EndHue = 360;

            rx.Size = 100;

            bool ok; ;

            rx.CreateRamp(out ok);

            IEnumColors RColors = rx.Colors;

            RColors.Reset();

            IUniqueValueRenderer pRender = new UniqueValueRendererClass();

            pRender.FieldCount = 1;

            pRender.set_Field(0, sFieldName[0]);

            IFeature pFeat = fCursor.NextFeature();

            int index = pFeat.Fields.FindField(sFieldName[0]);

            while (pFeat != null)

            {

                ISimpleFillSymbol symd = new SimpleFillSymbolClass();

                symd.Style = esriSimpleFillStyle.esriSFSSolid;

                symd.Outline.Width = 1;

                symd.Color = RColors.Next();

                string valuestr = pFeat.get_Value(index).ToString();

                pRender.AddValue(valuestr, valuestr, symd as ISymbol);

                pFeat = fCursor.NextFeature();

            }

            geoLayer.Renderer = pRender as IFeatureRenderer;
            //刷新地图和TOOCotrol
            IActiveView pActiveView = _MapControl.Map as IActiveView;
            pActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void Symbolize_Load(object sender, EventArgs e)
        {
            //MapControl中没有图层时返回 
            if (this._MapControl.LayerCount <= 0)
                return;
            //获取MapControl中的全部图层名称，并加入ComboBox 
            //图层 
            ILayer pLayer1;
            //图层名称 
            string strLayerName;
            for (int i = 0; i < this._MapControl.LayerCount; i++)
            {
                pLayer1 = this._MapControl.get_Layer(i);
                strLayerName = pLayer1.Name;
                //图层名称加入cboLayer 
                this.cboLayer.Items.Add(strLayerName);
            }
            //默认显示第一个选项 
            this.cboLayer.SelectedIndex = 0;
            pLayer = _MapControl.get_Layer(cboLayer.SelectedIndex);
            mFeatureLayer = _MapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
            dt.Columns.Add(new DataColumn("字段"));
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空listBoxField控件的内容
            this.listBoxField.Items.Clear();
            pLayer= _MapControl.get_Layer(cboLayer.SelectedIndex);
            //获取cboLayer中选中的图层 
            mFeatureLayer = _MapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
            pFeatureClass = mFeatureLayer.FeatureClass;
            //字段名称 
            string strFldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFldName = pFeatureClass.Fields.get_Field(i).Name;
                //图层名称加入cboField 
                this.listBoxField.Items.Add(strFldName);
            }
            //默认显示第一个选项 
            this.listBoxField.SelectedIndex = 0;
        }

        private DataTable dt = new DataTable();
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            //index = this.dataGridView1.Rows.Add();
            
            DataRow dr=dt.NewRow();
            dr[0]= listBoxField.SelectedItem.ToString();
            dt.Rows.Add(dr);
            listBoxField.Items.RemoveAt(listBoxField.SelectedIndex);
            dataGridView1.DataSource = dt;
            listBoxField.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count>0)
            {
                listBoxField.Items.Add(dt.Rows[0][0]);
                dt.Rows.RemoveAt(0);
                dataGridView1.DataSource = dt;
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    strFields[i] = dt.Rows[i][0].ToString();
            //}
            strFields[0] = "class";
            UniqueValueRender(_MapControl,mFeatureLayer,100,strFields[0]);
            //UniqueValueRenderer(mFeatureLayer,strFields);
            //获取当前图层 ，并把它设置成IGeoFeatureLayer的实例
            //IMap pMap = _MapControl.Map;
            //ILayer pLayer = pMap.get_Layer(0) as IFeatureLayer;
            //mFeatureLayer = pLayer as IFeatureLayer;
            //IGeoFeatureLayer pGeoFeatureLayer = pLayer as IGeoFeatureLayer;


            //获取图层上的feature
            //IFeatureClass pFeatureClass = mFeatureLayer.FeatureClass;

            //IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);

            //定义单值图渲染组件
            //IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRendererClass();
            //设置渲染字段对象
            //pUniqueValueRenderer.FieldCount = 1;
            //pUniqueValueRenderer.set_Field(0, strFields[0]);
            //创建填充符号
            //ISimpleFillSymbol PFillSymbol = new SimpleFillSymbolClass();
            //pUniqueValueRenderer.DefaultSymbol = (ISymbol)PFillSymbol;
            //pUniqueValueRenderer.UseDefaultSymbol = false;

            //QI the table from the geoFeatureLayer and get the field number of
            //ITable pTable;
            //int fieldNumber;
            //pTable = pGeoFeatureLayer as ITable;
            //fieldNumber = pTable.FindField(strFields[0]);
            //if (fieldNumber == -1)
            //{
            //    MessageBox.Show("Can't find field called ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}

            //创建并设置随机色谱
            //IRandomColorRamp pColorRamp = new RandomColorRampClass();
            //pColorRamp.StartHue = 0;
            //pColorRamp.MinValue = 99;
            //pColorRamp.MinSaturation = 15;
            //pColorRamp.EndHue = 360;
            //pColorRamp.MaxValue = 100;
            //pColorRamp.MaxSaturation = 30;
            //pColorRamp.Size = 100;
            //pColorRamp.Size = pUniqueValueRenderer.ValueCount;
            //bool ok = true;
            //pColorRamp.CreateRamp(out ok);
            //IEnumColors pEnumRamp;
            //pEnumRamp = pColorRamp.Colors;

            //为每个值设置一个符号
            //int n = pFeatureClass.FeatureCount(null);
            //for (int i = 0; i < n; i++)
            //{
            //    IFeature pFeature = pFeatureCursor.NextFeature();
            //    IClone pSourceClone = PFillSymbol as IClone;
            //    ISimpleFillSymbol pSimpleFillSymbol = pSourceClone.Clone() as ISimpleFillSymbol;
            //    string pFeatureValue = pFeature.get_Value(pFeature.Fields.FindField(strFields[0])).ToString();
            //    pSimpleFillSymbol.Outline.Width = 2;
            //    pUniqueValueRenderer.AddValue(pFeatureValue, strFields[0], (ISymbol)pSimpleFillSymbol);
            //}
            //为每个符号设置颜色
            //for (int i = 0; i <= pUniqueValueRenderer.ValueCount - 1; i++)
            //{
            //    string xv = pUniqueValueRenderer.get_Value(i);
            //    if (xv != "")
            //    {
            //        ISimpleFillSymbol pNextSymbol = (ISimpleFillSymbol)pUniqueValueRenderer.get_Symbol(xv);
            //        pNextSymbol.Color = pEnumRamp.Next();
            //        pUniqueValueRenderer.set_Symbol(xv, (ISymbol)pNextSymbol);

            //    }
            //}
            //将单值图渲染对象与渲染图层挂钩
            //pGeoFeatureLayer.Renderer = (IFeatureRenderer)pUniqueValueRenderer;
            //pGeoFeatureLayer.DisplayField = strFields[0];
            //刷新地图和TOOCotrol
            //IActiveView pActiveView = _MapControl.Map as IActiveView;
            //pActiveView.Refresh();
            //axTOCControl1.Update();

        }
        public void UniqueValueRender(AxMapControl pMapcontrol, IFeatureLayer pFtLayer, int pCount, string pFieldName)

        {

            IGeoFeatureLayer pGeoFeaturelayer = pFtLayer as IGeoFeatureLayer;
            IUniqueValueRenderer pUnique = new UniqueValueRendererClass();
            pUnique.FieldCount = 1;

            pUnique.set_Field(0, pFieldName);

            ISimpleFillSymbol pSimFill = new SimpleFillSymbolClass();

            //给颜色

            IFeatureCursor pFtCursor = pFtLayer.FeatureClass.Search(null, false);
            IFeature pFt = pFtCursor.NextFeature();

            IFillSymbol pFillSymbol1;

            ////添加第一个符号

            //pFillSymbol1 = new SimpleFillSymbolClass();

            //pFillSymbol1.Color = GetRGBColor(103, 252, 179) as IColor;

            ////添加第二个符号

            //IFillSymbol pFillSymbol2 = new SimpleFillSymbolClass();

            //pFillSymbol2.Color = GetRGBColor(125, 155, 251) as IColor;

            //创建并设置随机色谱从上面的的图可以看出我们要给每一个值定义一种颜色,我 们可以创建色谱,但是色谱的这些参数

            IRandomColorRamp pColorRamp = new RandomColorRampClass();
            pColorRamp.StartHue = 0;

            pColorRamp.MinValue = 20;

            pColorRamp.MinSaturation = 15;

            pColorRamp.EndHue = 360;

            pColorRamp.MaxValue = 100;

            pColorRamp.MaxSaturation = 30;
            pColorRamp.Size = pCount;

            //pColorRamp.Size = pUniqueValueRenderer.ValueCount; 
            bool ok = true;

            pColorRamp.CreateRamp(out ok);
            IEnumColors pEnumRamp = pColorRamp.Colors;

            //IColor pColor = pEnumRamp.Next();

            int pIndex = pFt.Fields.FindField(pFieldName);

            //因为我只有24条记录,所以改变这些,这些都不会超过255或者为负数.求余 

            int i = 0;

            while (pFt != null)

            {

                IColor pColor = pEnumRamp.Next();
                if (pColor == null)

                {

                    pEnumRamp.Reset();

                    pColor = pEnumRamp.Next();

                }

                //以下注释代码为自定义的两种颜色 ,如果不使用随机的颜色,可以采用这样的

                //if (i % 2 == 0)

                //{

                // pUnique.AddValue(Convert.ToString(pFt.get\_Value(pIndex)) , pFieldName, pFillSymbol1 as ISymbol);

                //}

                //else

                //{

                // pUnique.AddValue(Convert.ToString(pFt.get\_Value(pIndex)) , pFieldName, pFillSymbol2 as ISymbol);

                //}

                //i++;

                pFillSymbol1 = new SimpleFillSymbolClass();

                pFillSymbol1.Color = pColor;
                pUnique.AddValue(Convert.ToString(pFt.get_Value(pIndex)), pFieldName, pFillSymbol1 as ISymbol);

                pFt = pFtCursor.NextFeature();

                // pColor = pEnumRamp.Next();

            }

            pGeoFeaturelayer.Renderer = pUnique as IFeatureRenderer;

            pMapcontrol.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

        }

        private IRgbColor GetRGBColor(int R, int G, int B) //子类赋给父类

        {

            IRgbColor pRGB;

            pRGB = new RgbColorClass();
            pRGB.Red = R;

            pRGB.Green = G;
            pRGB.Green = B;
            return pRGB;

        }
    }
}
