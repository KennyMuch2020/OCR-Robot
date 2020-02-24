using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet; //

namespace OCRTrainee
{
    /// <summary>
    /// Select region with the aid of shape features.
    /// Further select target region after threshold
    /// </summary>
    public class SelectShape
    {
        #region local varialbes        
        private HObject connectRegion_T;
        private HObject connectRegion_C;

        public Tools m_tools = new Tools();
        #endregion

        #region selectShape method
        /// <summary>
        /// here do selecShape both for imageThresholded and imageCropped
        /// for imageCropped is only for display on the screen, bigger and clear to check for user
        /// </summary>
        /// <param name="WindowControl"></param>
        /// <param name="image_C"></param>
        /// <param name="Region_T"></param>
        /// <param name="Region_C"></param>
        /// <param name="hWindowHandle"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="SRegion_T"></param>
        /// <param name="SRegion_C"></param>
        public void selectShape(HWindowControl WindowControl, ref HObject image_C, ref HObject Region_T, ref HObject Region_C, HTuple hWindowHandle, HTuple min,
            HTuple max, out HObject SRegion_T, out HObject SRegion_C)
        {
            WindowControl.Focus();

            if (!m_tools.ObjectValided(Region_T))
            {
                SRegion_T = null;
                SRegion_C = null;
                return;
            }
            try
            {
                HOperatorSet.GenEmptyObj(out connectRegion_T);  //initialize variable connectRegion_T (imageThresholded)
                HOperatorSet.GenEmptyObj(out connectRegion_C);  //initialize variable connectRegion_C (imageCropped)
                HOperatorSet.Connection(Region_T,out connectRegion_T);
                HOperatorSet.Connection(Region_C, out connectRegion_C);

                //Graphics set for display               
                HOperatorSet.SetDraw(hWindowHandle, "fill");
                HOperatorSet.SetColor(hWindowHandle, "green");   

                //ReduceImage
                HOperatorSet.GenEmptyObj(out SRegion_T);       //initialize variable SRegion_T
                HOperatorSet.GenEmptyObj(out SRegion_C);       //initialize variable SRegion_C
                SRegion_T.Dispose();
                SRegion_C.Dispose();
                HOperatorSet.SelectShape(connectRegion_T, out SRegion_T, "area", "and", min, max);
                HOperatorSet.SelectShape(connectRegion_C, out SRegion_C, "area", "and", min, max);

                //Union segmented regions
                HOperatorSet.Union1(SRegion_T,out SRegion_T);
                HOperatorSet.Union1(SRegion_C, out SRegion_C);

                //Refresh window; Select_shape region for imageCropped                       
                m_tools.Imgshow(image_C, hWindowHandle, image_C);              
                HOperatorSet.DispObj(SRegion_C, hWindowHandle);

                //Release;  
                connectRegion_T.Dispose();
                connectRegion_C.Dispose();
            }
            catch
            {
                SRegion_T = null;
                SRegion_C = null;
                return;
                
            }

        }
        #endregion
    }
}
