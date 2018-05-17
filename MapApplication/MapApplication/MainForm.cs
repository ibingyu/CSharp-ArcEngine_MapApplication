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
        #region ��Ա����

        private IMapControl3 mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region ӥ�۱���
        //ӥ��ͬ��
        private bool bCanDrag;//ӥ�۵�ͼ�Ͼ��ο���Զ��ı�־
        private IPoint pMoveRectPoint; //�ƶ�ӥ�۵�ͼ�ϵľ���ʱ������ڵ�λ��
        private IEnvelope pEnv;//��¼��¼������ͼ��Extent 
        #endregion

        #region ���������
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
        #region �ļ�����
        
        //�½�ͼ��
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ICommand command = new CreateNewDocument();
            command.OnCreate(mapControl.Object);
            command.OnClick();
        }
        //���ļ�
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //���ļ�
            //IMapDocument mxdMapDocument=new MapDocumentClass();
            //OpenFileDialog mxdOpenFileDialog=new OpenFileDialog();
            //mxdOpenFileDialog.Filter = "��ͼ�ĵ�(*.mxd)|*.mxd";

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

        //���Ϊ�����ļ�
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(mapControl.Object);
            command.OnClick();
        }

        //�ļ�����
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
                    //ʹ��MapDocument���󱣴��ļ�
                    IMapDocument mapDocument = new MapDocumentClass();
                    mapDocument.Open(m_mapDocumentName, string.Empty);

                    //�ж��ļ��ǲ���ֻ����
                    if (mapDocument.get_IsReadOnly(m_mapDocumentName))
                    {
                        MessageBox.Show("��ͼ�ļ���ֻ���ģ�");
                        mapDocument.Close();
                        return;
                    }
                    //�滻���е��ļ�
                    mapDocument.ReplaceContents((IMxdContents)mapControl.Map);
                    //�����ļ�
                    mapDocument.Save(mapDocument.UsesRelativePaths, false);
                    mapDocument.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //�˳�Ӧ��
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        
        #region ӥ�۹���ʵ��
            
        

        //OnMapReplace�¼�
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
        
        //����ͼ����
        private void SynchronizeEagleEye()
        {
            if (EagleEyeMapControl.LayerCount > 0)
            {
                EagleEyeMapControl.ClearLayers();
            }
            //����ӥ��������ͼ������һ��
            EagleEyeMapControl.SpatialReference = axMapControl3.SpatialReference;
            //EagleEyeMapControl.Map=new MapClass();
            for (int i = axMapControl3.LayerCount - 1; i >= 0; i--)
            {
                //ʹӥ����ͼ��������ͼ��ͼ��˳��һ��
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
                            //���˵�ͼ��
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
                        //���˵�ͼ��
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
        
        //����ӥ����ͼ�еľ��ο�
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //��ͼ֮ǰ�������ͼ��֮ǰ���ڵľ��ο�
            IGraphicsContainer pGraphicsContainer=EagleEyeMapControl.Map as IGraphicsContainer;
            IActiveView pActiveView=pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            //�õ���ǰ��ͼ
            IRectangleElement pRectangleElement=new RectangleElementClass();
            IElement pElement=pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //���þ��ο�
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
            //��ӥ����ͼ����Ӿ���
            IFillShapeElement pFillShapeElement=pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFill;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement,0);
            //ˢ��
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,null,null);

        }

        //��ȡ��ɫ����
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
                //�������ƶ������ο��У���껻��С�֣���ʾ�����϶�  
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerHand;
                if (e.button == 2)  //������ڲ���������Ҽ����������ʾ����ΪĬ����ʽ  
                {
                    EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else
            {
                //������λ�ý������ΪĬ�ϵ���ʽ  
                EagleEyeMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }

            if (bCanDrag)
            {
                double Dx, Dy;  //��¼����ƶ��ľ���  
                Dx = e.mapX - pMoveRectPoint.X;
                Dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(Dx, Dy); //����ƫ�������� pEnv λ��  
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
                    //�Ծ�����һ�����ĵ���
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

        #region ����������

        
        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buffer bufferForm=new Buffer(this.axMapControl3.Object);
            //bufferForm.Show();
            if (bufferForm.ShowDialog() == DialogResult.OK)
            {
                //��ȡ����ļ�·��
                string strBufferPath = bufferForm.strOutPutPath;
                //������ͼ�����뵽mapcontrol��
                int index = strBufferPath.LastIndexOf("\\");
                this.axMapControl3.AddShapeFile(strBufferPath.Substring(0, index), strBufferPath.Substring(index));

            }
        }
        #endregion

        #region ���Ϸ���

        
        private void ���÷���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverlayForm overlayForm=new OverlayForm();
            if (overlayForm.ShowDialog() == DialogResult.OK)
            {
                //��ȡ����ļ�·��
                string strBufferPath = overlayForm.strOutputPath;
                //������ͼ�����뵽MapControl
                int index = strBufferPath.LastIndexOf("\\");
                this.axMapControl3.AddShapeFile(strBufferPath.Substring(0, index), strBufferPath.Substring(index));
            }
        }
    
        #endregion

        #region �����Բ�ѯҪ��
        private void �����Բ�ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttributeQueryForm attributeQuery = new AttributeQueryForm(this.axMapControl3);
            attributeQuery.Show();
        }
        #endregion

        #region �Ҽ�����¼�
        //ѡ���ͼ��
        IFeatureLayer tocSelectFeatureLayer = null;//���ͼ��ؼ�ѡ���ͼ��
        private void axTOCControl2_OnMouseDown(object sender, AxESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                object data = null, unk = null;
                ILayer layer = null;
                //��ȡ�����λ�ã�ͼ���
                axTOCControl2.HitTest(e.x, e.y, ref item, ref basicMap, ref layer, ref unk, ref data);
                tocSelectFeatureLayer = layer as IFeatureLayer;
                if (item == esriTOCControlItem.esriTOCControlItemLayer && tocSelectFeatureLayer != null)
                {
                    contextMenuStrip1.Show(Control.MousePosition);
                }

            }
        }

        private void ���Ա�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttribute formAttribute = new FrmAttribute();
            formAttribute.Show();
            formAttribute.currentLayer = tocSelectFeatureLayer;
            formAttribute._MapControl = axMapControl3;
            formAttribute.m_mapControl = mapControl;
            formAttribute.SetTable();
        }

        private void �Ƴ�ͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl3.Map.DeleteLayer(tocSelectFeatureLayer);
        }
        #endregion

        #region Ψһֵ���Ż�

        private void ΨһֵToolStripMenuItem_Click(object sender, EventArgs e)
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