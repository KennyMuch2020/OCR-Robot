using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;

namespace OCRTrainee
{
    public class Threshold
    {
        #region local字段
        
        public HTuple RowCenter_R;
        public HTuple ColCenter_R;
        private HObject CrossCenter_R;
        
        public Tools m_tools = new Tools();
        //HObject C_Region
        #endregion

        #region Threshold 函数
        public void HThreshold(HWindowControl WindowControl, ref HObject image_R, ref HObject image_C,HTuple hWindowHandle, HTuple minGray,
            HTuple maxGray, ref HObject R_Region,ref HObject C_Region)
        {
            WindowControl.Focus();
            
            if (!m_tools.ObjectValided(image_R))
            {
             
                return;
            }
            try
            {
             
                //图像设置‘线宽与填充模式’            
                HOperatorSet.SetDraw(hWindowHandle, "fill");
                HOperatorSet.SetColor(hWindowHandle, "green");  //添加代码4

                //ReduceImage
                HOperatorSet.GenEmptyObj(out R_Region);       //初始化图像变量C_Region
                HOperatorSet.GenEmptyObj(out C_Region);       //初始化图像变量C_Region
                R_Region.Dispose();
                C_Region.Dispose();
                HOperatorSet.Threshold(image_R, out R_Region, minGray, maxGray);
                HOperatorSet.Threshold(image_C, out C_Region, minGray, maxGray);

                HOperatorSet.FillUp(R_Region, out R_Region);
                HOperatorSet.FillUp(C_Region, out C_Region);

                //刷新图形                      
                m_tools.Imgshow(image_C, hWindowHandle, image_C);
                HOperatorSet.DispObj(C_Region, hWindowHandle);
               
                //生命周期释放       
                //image_R.Dispose();            
                //R_Region.Dispose();
            }
            catch
            {
               
            }

        }
        #endregion

        #region  阈值化中心显示在窗口1中，且随阈值的不同，圆心变化
        public void CrossDisp(HWindowControl WindowControl, HObject Image, HTuple hWindowHandle)
        {
            WindowControl.Focus();
            try
            {
                HOperatorSet.GenEmptyObj(out CrossCenter_R);      //初始化图像变量CrossCenter
                CrossCenter_R.Dispose();
                HOperatorSet.GenCrossContourXld(out CrossCenter_R, RowCenter_R, ColCenter_R, 35, 0);

                //刷新图形
                HOperatorSet.DispObj(Image, hWindowHandle);
                HOperatorSet.SetColor(hWindowHandle, "red");    //单独设置CrossCenter的颜色     //修改颜色5
                HOperatorSet.DispObj(CrossCenter_R, hWindowHandle);

                //生命周期释放
                CrossCenter_R.Dispose();
            }
            catch
            {

            }
        }
        
        #endregion
    }
}
