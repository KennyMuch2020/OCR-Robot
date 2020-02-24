using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;


namespace OCRTrainee
{
    public class PubStore
    {
        #region variable
        
        /// <summary>
        /// transfer variable
        /// </summary>
        public string m_type; //bridge from frmMain to frmExe

        #endregion

        #region PubStore Singleton

        private static PubStore _instance = null;

        public static PubStore Instance()
        {
            if (_instance == null)
            {
                _instance = new PubStore();
            }
            return _instance;
        }
        #endregion


        /// <summary>
        /// PubStore Singleton Pattern
        /// </summary>
        public Tools m_Tools = new Tools(); 
        public CamParam m_camParam = new CamParam();
        public Threshold m_Thres = new Threshold();
        public SelectShape m_Select = new SelectShape();
        public ShapeModel m_shapeM = new ShapeModel();


    }
}
