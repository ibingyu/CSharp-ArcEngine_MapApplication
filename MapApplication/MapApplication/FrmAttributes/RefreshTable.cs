﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace MapApplication.FrmAttributes
{
    class RefreshTable
    {
        private IFeatureLayer pFeatureLayer;
        private IFeatureClass pFeatureClass;
        private ILayerFields pLayerFields;

        public void Refresh(DataGridView _data,IFeatureLayer pFLayer)
        {
            pFeatureLayer = pFLayer;
            pFeatureClass = pFeatureLayer.FeatureClass;
            pLayerFields = pFeatureLayer as ILayerFields;
            DataSet ds = new DataSet("dsTests");
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
            _data.DataSource = dt;
        }
        
                
    }
}
