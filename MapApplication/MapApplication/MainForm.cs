using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using MapApplication;
using MapApplication1;
using MyGis.form1;
using Path = System.IO.Path;


namespace MapApplication
{
    public partial class Form1 : Form
    {
        #region 成员变量

        private IMapControl3 mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region 鹰眼变量
        //鹰眼同步
        private bool bCanDrag;//鹰眼地图上矩形框可以动的标志
        private IPoint pMoveRectPoint; //移动鹰眼地图上的矩形时鼠标所在的位置
        private IEnvelope pEnv;//记录记录数据视图的Extent 
        #endregion

        #region 主窗体入口
        public Form1()
        {
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
        } 
        #endregion


        private void axToolbarControl2_OnMouseDown(object sender, AxESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }
        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            //get the MapControl
            mapControl = (IMapControl3)axMapControl3.Object;

            //disable the Save menu (since there is no document yet)
            toolStripMenuItem5.Enabled = false;
        }
        #region 文件功能
        
        //新建图层
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ICommand command = new CreateNewDocument();
            command.OnCreate(mapControl.Object);
            command.OnClick();
        }
        //打开文件
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //打开文件
            //IMapDocument mxdMapDocument=new MapDocumentClass();
            //OpenFileDialog mxdOpenFileDialog=new OpenFileDialog();
            //mxdOpenFileDialog.Filter = "地图文档(*.mxd)|*.mxd";

            //if (mxdOpenFileDialog.ShowDialog()==DialogResult.OK)
            //{
            //    string mxdFilePath = mxdOpenFileDialog.FileName;
            //    if (axMapControl1.CheckMxFile(mxdFilePath))
            //    {
            //        axMapControl1.Map.ClearLayers();
            //        axMapControl1.LoadMxFile(mxdFilePath);
            //    }
            //}
            //axMapControl1.ActiveView.Refresh();
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(mapControl.Object);
            command.OnClick();
        }

        //另存为其他文件
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(mapControl.Object);
            command.OnClick();
        }

        //文件保存
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }
        private void SaveDocument()
        {
            try
            {
                if (mapControl.CheckMxFile(m_mapDocumentName))
                {
                    //使用MapDocument对象保存文件
                    IMapDocument mapDocument = new MapDocumentClass();
                    mapDocument.Open(m_mapDocumentName, string.Empty);

                    //判断文件是不是只读的
                    if (mapDocument.get_IsReadOnly(m_mapDocumentName))
                    {
                        MessageBox.Show("地图文件是只读的！");
                        mapDocument.Close();
                        return;
                    }
                    //替换已有的文件
                    mapDocument.ReplaceContents((IMxdContents)mapControl.Map);
                    //保存文件
                    mapDocument.Save(mapDocument.UsesRelativePaths, false);
                    mapDocument.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //退出应用
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        
        #region 鹰眼功能实现
            
        

        //OnMapReplace事件
        private void axMapControl3_OnMapReplaced(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                toolStripMenuItem5.Enabled = false;
                toolStripStatusLabel2.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                toolStripMenuItem5.Enabled = true;
                toolStripStatusLabel2.Text = Path.GetFileName(m_mapDocumentName);
            }
            SynchronizeEagleEye();
        }
        
        //主视图加载
        private void SynchronizeEagleEye()
        {
            if (EagleEyeMapControl.LayerCount > 0)
            {
                EagleEyeMapControl.ClearLayers();
            }
            //设置鹰眼与主地图的坐标一致
            EagleEyeMapControl.SpatialReference = axMapControl3.SpatialReference;
            //EagleEyeMapControl.Map=new MapClass();
            for (int i = axMapControl3.LayerCount - 1; i >= 0; i--)
            {
                //使鹰眼视图与数据视图的图层顺序一致
                ILayer pLayer = axMapControl3.get_Layer(i);
                if (pLayer is IGroupLayer||pLayer is ICompositeLayer)
                {
                    ICompositeLayer pCompositeLayer = (ICompositeLayer) pLayer;
                    for (int j = pCompositeLayer.Count-1; j >= 0; j--)
                    {
                        ILayer pSubLayer = pCompositeLayer.get_Layer(j);
                        IFeatureLayer pFeatureLayer = pSubLayer as IFeatureLayer;
                        if (pFeatureLayer!=null)
                        {
                            //过滤点图层
                            if (pFeatureLayer.FeatureClass.ShapeType!=esriGeometryType.esriGeometryPoint&&pFeatureLayer.FeatureClass.ShapeType!=esriGeometryType.esriGeometryMultipoint)
                            {
                                EagleEyeMapControl.AddLayer(pFeatureLayer);
                            }
                        }
                    }
                }
                else
                {
                    IFeatureLayer pFeatureLayer=pLayer as IFeatureLayer;
                    if (pFeatureLayer!=null)
                    {
                        //过滤点图层
                        if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                        {
                            EagleEyeMapControl.AddLayer(pFeatureLayer);
                        }
                    }
                }
                EagleEyeMapControl.Extent = axMapControl3.FullExtent;
                pEnv=axMapControl3.Extent as IEnvelope;
                
                EagleEyeMapControl.ActiveView.Refresh();
            }
        }
        
        //画出鹰眼视图中的矩形框
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //绘图之前，清除视图中之前存在的矩形框
            IGraphicsContainer pGraphicsContainer=EagleEyeMapControl.Map as IGraphicsContainer;
            IActiveView pActiveView=pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            //得到当前视图
            IRectangleElement pRectangleElement=new RectangleElementClass();
            IElement pElement=pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //设置矩形框
            IRgbColor pColor=new RgbColorClass();
            pColor = getRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine=new SimpleLineSymbolClass();
            pOutLine.Width = 2;
            pOutLine.Color = pColor;
      
            IFillSymbol pFill=new SimpleFillSymbolClass();
            pColor=new RgbColorClass();
            pColor.Transparency = 0;
            pFill.Color = pColor;
            pFill.Outline = pOutLine;
            //向鹰眼视图中添加矩形
            IFillShapeElement pFillShapeElement=pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFill;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement,0);
            //刷新
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,null,null);

        }

        //获取颜色对象
        private IRgbColor getRgbColor(int R,int G,int B)
        {
            IRgbColor pRgbColor = null;
            if (R<0||R>255||G<0||G>255||B<0||B>255)
            {
                return pRgbColor;
            }
            pRgbColor=new RgbColorClass();
            pRgbColor.Red = R;
            pRgbColor.Green = G;
            pRgbColor.Blue = B;
            return pRgbColor;
        }
        private void EagleEyeMapControl_OnMouseMove(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                //如果鼠标移动到矩形框中，鼠标换成小手，表示可以拖动  
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerHand;
                if (e.button == 2)  //如果在内部按下鼠标右键，将鼠标演示设置为默认样式  
                {
                    EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else
            {
                //在其他位置将鼠标设为默认的样式  
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }

            if (bCanDrag)
            {
                double Dx, Dy;  //记录鼠标移动的距离  
                Dx = e.mapX - pMoveRectPoint.X;
                Dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(Dx, Dy); //根据偏移量更改 pEnv 位置  
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                axMapControl3.Extent = pEnv;
            }
        }

        private void EagleEyeMapControl_OnMouseDown(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button==1&&pMoveRectPoint!=null)
            {
                if (e.mapX==pMoveRectPoint.X&&e.mapY==pMoveRectPoint.Y)
                {
                    axMapControl3.CenterAt(pMoveRectPoint);
                }
                bCanDrag = false;
            }
        }

        private void EagleEyeMapControl_OnMouseUp(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {
            if (EagleEyeMapControl.Map.LayerCount>0)
            {
                if (e.button==1)
                {
                    if (e.mapX > pEnv.XMin &&e.mapY>pEnv.YMin&&e.mapX<pEnv.XMax&&e.mapY<pEnv.YMax)
                    {
                        bCanDrag = true;
                    }
                    pMoveRectPoint=new PointClass();
                    pMoveRectPoint.PutCoords(e.mapX,e.mapY);
                }
                else if (e.button==2)
                {
                    IEnvelope pEnvelope = EagleEyeMapControl.TrackRectangle();
                    IPoint pTemPoint=new PointClass();
                    pTemPoint.PutCoords(pEnvelope.XMin+pEnvelope.Width/2,pEnvelope.YMin+pEnvelope.Height/2);
                    axMapControl3.Extent = pEnvelope;
                    //对矩形做一个中心调整
                    axMapControl3.CenterAt(pTemPoint);
                }
                
                   
            }
        }

        private void axMapControl3_OnExtentUpdated(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            pEnv = (IEnvelope) e.newEnvelope;
            DrawRectangle(pEnv);
        }

        private void axMapControl3_OnMouseMove(object sender, AxESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            toolStripStatusLabel2.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"),axMapControl3.MapUnits.ToString().Substring(4));
        }
        #endregion

        #region 缓冲区分析

        
        private void 缓冲区分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buffer bufferForm=new Buffer(this.axMapControl3.Object);
            //bufferForm.Show();
            if (bufferForm.ShowDialog() == DialogResult.OK)
            {
                //获取输出文件路径
                string strBufferPath = bufferForm.strOutPutPath;
                //缓冲区图层载入到mapcontrol中
                int index = strBufferPath.LastIndexOf("\\");
                this.axMapControl3.AddShapeFile(strBufferPath.Substring(0, index), strBufferPath.Substring(index));

            }
        }
        #endregion

        #region 叠合分析

        
        private void 叠置分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverlayForm overlayForm=new OverlayForm();
            if (overlayForm.ShowDialog() == DialogResult.OK)
            {
                //获取输出文件路径
                string strBufferPath = overlayForm.strOutputPath;
                //缓冲区图层载入到MapControl
                int index = strBufferPath.LastIndexOf("\\");
                this.axMapControl3.AddShapeFile(strBufferPath.Substring(0, index), strBufferPath.Substring(index));
            }
        }
    
        #endregion

        #region 按属性查询要素
        private void 按属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttributeQueryForm attributeQuery = new AttributeQueryForm(this.axMapControl3);
            attributeQuery.Show();
        }
        #endregion

        #region 右键点击事件
        //选择的图层
        IFeatureLayer tocSelectFeatureLayer = null;//点击图层控件选择的图层
        private void axTOCControl2_OnMouseDown(object sender, AxESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                object data = null, unk = null;
                ILayer layer = null;
                //获取点击的位置，图层等
                axTOCControl2.HitTest(e.x, e.y, ref item, ref basicMap, ref layer, ref unk, ref data);
                tocSelectFeatureLayer = layer as IFeatureLayer;
                if (item == esriTOCControlItem.esriTOCControlItemLayer && tocSelectFeatureLayer != null)
                {
                    contextMenuStrip1.Show(Control.MousePosition);
                }

            }
        }

        private void 属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttribute formAttribute = new FrmAttribute();
            formAttribute.Show();
            formAttribute.currentLayer = tocSelectFeatureLayer;
            formAttribute._MapControl = axMapControl3;
            formAttribute.m_mapControl = mapControl;
            formAttribute.SetTable();
        }

        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl3.Map.DeleteLayer(tocSelectFeatureLayer);
        }
        #endregion

        #region 唯一值符号化

        private void 唯一值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Symbolize symbolize = new Symbolize(this.axMapControl3, this.axTOCControl2);
            symbolize.Show();
        } 
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }



}