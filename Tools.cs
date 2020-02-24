using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HalconDotNet;
using System.Windows.Forms;

namespace OCRTrainee
{
    
    /// <summary>
    /// 公共方法类，FILE,FILESTREAM,DIRECTORY...
    /// 外部的视觉函数类
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// Partition & Intersection
        /// </summary>
        /// <param name="WindowControl"></param>
        /// <param name="Image"></param>
        /// <param name="hWindowHandle"></param>
        /// <param name="ptHeit"></param>
        /// <param name="ptWidth"></param>
        /// <param name="ho_ConnectedRegions"></param>
        /// <param name="ThresRegion"></param>
        public void Partition(ref HTuple ptHeit, ref HTuple ptWidth,
            ref HObject ho_ConnectedRegions,ref HObject ThresRegion,out HObject ho_CharCandidates, out HObject ho_RegionIntersection)
        {
            try
            {
                HObject ho_CharBlocks;
                HOperatorSet.GenEmptyObj(out ho_CharBlocks);
                HOperatorSet.GenEmptyObj(out ho_CharCandidates);
                HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
                ho_CharBlocks.Dispose();
                HOperatorSet.ShapeTrans(ho_ConnectedRegions, out ho_CharBlocks, "rectangle1");
                ho_CharCandidates.Dispose();
                HOperatorSet.PartitionRectangle(ho_CharBlocks, out ho_CharCandidates, ptHeit, ptWidth);

                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_CharCandidates, ThresRegion, out ho_RegionIntersection);
            }
            catch
            {
                ho_CharCandidates = null;
                ho_RegionIntersection = null;
                ho_CharCandidates.Dispose();
                ho_RegionIntersection.Dispose();
            }
        }

        public void LoadFileExtensionOMC(ListBox lbControl, ref string general_path)
        {
            try
            {
                if (!Directory.Exists(general_path))
                {
                    return;
                }
                lbControl.Items.Clear();
                string[] Dirs = Directory.GetFiles(general_path);  //Directory类的获取文件
                foreach (string TypePath in Dirs)
                {
                    try
                    {
                        if (Path.GetExtension(TypePath)==".omc")
                        {
                            string[] strTemp = TypePath.Split('\\');
                            string Type;
                            Type = strTemp[strTemp.Length - 1];
                            lbControl.Items.Add(Type);
                        }
                        else
                        {

                        }
                       
                    }
                    catch (Exception ex)
                    {
                        //ErrorLog(ex.Message);
                    }
                }

            }
            catch
            {

            }
        }
        

        #region 加载文件路径
        public void LoadFile(ListBox lbControl,ref string general_path)
        {
            try
            {
               
                if (!Directory.Exists(general_path))
                {
                    return;
                }
                lbControl.Items.Clear();
                string[] Dirs = Directory.GetFiles(general_path);  //Directory类的获取文件

                foreach (string TypePath in Dirs)
                {
                    try
                    {                   
                        string[] strTemp = TypePath.Split('\\');
                        string Type;
                        Type = strTemp[strTemp.Length - 1];
                        lbControl.Items.Add(Type);
                    }
                    catch (Exception ex)
                    {
                        //ErrorLog(ex.Message);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region 加载项目文件夹路径
        public void LoadType(ListBox lbControl, ref string general_path)
        {
            try
            {

                if (!Directory.Exists(general_path))
                {
                    return;
                }

                lbControl.Items.Clear();
                string[] Dirs = Directory.GetDirectories(general_path);  //Directory类的获取文件夹

                foreach (string TypePath in Dirs)
                {
                    try
                    {
                        string[] Files = Directory.GetFiles(TypePath);  //Directory类的获取文件;在此处也许可以不需要
                      
                        string[] strTemp = TypePath.Split('\\');
                        string Type;
                        Type = strTemp[strTemp.Length - 1];
                        lbControl.Items.Add(Type);
                    }
                    catch (Exception ex)
                    {
                        //ErrorLog(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message);
            }
        }
        #endregion

        #region 创建项目路径
        public void CreateAllDirectory(string strPath)
        {
            try
            {
                string str1, str2;
                int nPos = strPath.LastIndexOf("\\");
                if (nPos > -1)
                {
                    str1 = strPath.Substring(nPos + 1);
                    int nPos2 = str1.LastIndexOf(".");
                    if (nPos2 > -1)
                        str2 = strPath.Substring(0, nPos);
                    else
                        str2 = strPath;
                    if (!Directory.Exists(str2))
                    {
                        Directory.CreateDirectory(str2);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region *.TXT文件读取
        public void ReadStream(ref string _codepath,out string s)
        {
            try
            {

                //使用FileStream来读取数据
                FileStream fsRead = new FileStream(_codepath, FileMode.OpenOrCreate, FileAccess.Read);           
                byte[] buffer = new byte[1024 * 1024 * 5]; //字节数组5M            
                int r = fsRead.Read(buffer, 0, buffer.Length); //返回本次实际读取到的有效字节数     
                             
                s = Encoding.Default.GetString(buffer, 0, r); //将字节数组中每一个元素按照指定的编码格式解码成字符串        
                fsRead.Close();     //关闭流
                //释放流所占用的资源
                fsRead.Dispose();
                
            }
            catch
            {
                s = null;
            }
            finally
            {

            }
        }
        #endregion

        #region Load时，窗体公共设置函数
        public void HalconInit(HWindowControl WindowControl, out HTuple hWindowHandle)
        {
            HOperatorSet.SetSystem("width", 3000);
            HOperatorSet.SetSystem("height", 3000);
            hWindowHandle = WindowControl.HalconWindow; //图形窗口句柄，用于显示
            HOperatorSet.SetDraw(hWindowHandle, "margin");
            HOperatorSet.SetColor(hWindowHandle, "red");
            HOperatorSet.SetColored(hWindowHandle, 12);
        }
        #endregion

      
        #region  Strech全图函数
        public void Imgshow(HObject image, HTuple m_hwindowhandle, HObject disp)
        {

            // 显示全图
            HTuple width;
            HTuple height;
            HOperatorSet.GetImageSize(image, out width, out height);
            HOperatorSet.SetPart(m_hwindowhandle, 0, 0, height - 1, width - 1);
            HOperatorSet.DispObj(disp, m_hwindowhandle);
        }
        #endregion

        #region  判断HOject对象(图形)是否有效
        public bool ObjectValided(HObject Obj)
        {
            try
            {
                if (!Obj.IsInitialized())
                {
                    return false;
                }
                if (Obj.CountObj() < 1)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ImageFitWindow函数
        public void ImageFitWindow(HObject Image, HTuple hWindowHandle, bool bShowImage = true)
        {
            try
            {
                if (!ObjectValided(Image))
                {
                    MessageBox.Show("图像无效");
                    return;
                }
                //显示全图
                HTuple Width, Height;
                HOperatorSet.GetImageSize(Image, out Width, out Height);
                HOperatorSet.SetPart(hWindowHandle, 0, 0, Height - 1, Width - 1);
                if (bShowImage)
                    HOperatorSet.DispObj(Image, hWindowHandle);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region RegionFitWindow函数
        public void RegionFitWindow(HObject Image, HTuple hWindowHandle, HObject Region)
        {
            try
            {
                if (!ObjectValided(Image))
                {
                    MessageBox.Show("图像无效");
                    return;
                }
                //显示全图
                HTuple Width, Height;
                HOperatorSet.GetImageSize(Image, out Width, out Height);
                HOperatorSet.SetPart(hWindowHandle, 0, 0, Height - 1, Width - 1);             
                HOperatorSet.DispObj(Region, hWindowHandle);
            }
            catch (Exception)
            {
            }
        }
        #endregion


        public void affineTransImage(ref HObject m_image,ref HTuple image_width,ref HTuple image_height,ref HTuple char_height)
        {
            try
            {
                //图形变量 Area of text lines.
                HObject m_trans_region; 

                //数值变量
                HTuple row1, col1, row2, col2;
                HTuple m_orientationAngle, m_HomMat2D;
                row1 = image_height * 0.1;
                col1 = image_width * 0.1;
                row2 = image_height * 0.9;
                col2 = image_width * 0.9;

                HOperatorSet.GenEmptyObj(out m_trans_region);
                m_trans_region.Dispose();
                HOperatorSet.GenRectangle1(out m_trans_region, row1, col1, row2, col2);
                HOperatorSet.TextLineOrientation(m_trans_region, m_image, char_height, -0.523599, 0.523599, out m_orientationAngle);
                HOperatorSet.VectorAngleToRigid(image_height * 0.5, image_width * 0.5, m_orientationAngle, image_height * 0.5, image_width * 0.5, 0, out m_HomMat2D);
              
                HOperatorSet.AffineTransImage(m_image, out m_image, m_HomMat2D, "constant", "false");
                //释放
                m_trans_region.Dispose();
            }
            catch
            {

            }
           
        }

        /// <summary>
        /// 外部函数导入disp_message
        /// </summary>
        /// <param name="hv_WindowHandle"></param>
        /// <param name="hv_String"></param>
        /// <param name="hv_CoordSystem"></param>
        /// <param name="hv_Row"></param>
        /// <param name="hv_Column"></param>
        /// <param name="hv_Color"></param>
        /// <param name="hv_Box"></param>
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
 HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {

            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        public void DrawRectangel(HWindowControl WindowControl, HObject Image, HTuple hWindowHandle, ref HObject Region, ref HObject ImageReduced, ref HObject ImageCroped)
        {
            WindowControl.Focus();
            if (!ObjectValided(Image))
            {
                MessageBox.Show("Image Invalid");
                return;
            }

            try
            {
                HOperatorSet.GenEmptyObj(out ImageCroped);
                HOperatorSet.GenEmptyObj(out ImageReduced);     //初始化图像变量ImageReduced
                HOperatorSet.GenEmptyObj(out Region);           //初始化图像变量Region
               
                HTuple Row1, Col1, Row2, Col2;                   //初始化参数变量

                //提示信息
                disp_message(hWindowHandle, "Draw ROI，click right mouse to confirm", "window", 20, 20, "red", "true");

                //draw_rectangle1
                HOperatorSet.DrawRectangle1(hWindowHandle, out Row1, out Col1, out Row2, out Col2);

                //gen_rectangle1
                Region.Dispose();
                HOperatorSet.GenRectangle1(out Region, Row1, Col1, Row2, Col2);

                //reduce_domain
                ImageReduced.Dispose();
                ImageCroped.Dispose();
                HOperatorSet.ReduceDomain(Image, Region, out ImageReduced);
                HOperatorSet.CropDomain(ImageReduced, out ImageCroped);   //裁剪图片

                //显示
                HOperatorSet.DispObj(Image, hWindowHandle);
                HOperatorSet.DispObj(Region, hWindowHandle);
             
                disp_message(hWindowHandle, "Draw ROI complete", "window", 20, 20, "blue", "true");
                
                //生命周期释放
                //Region.Dispose();
                //ImageReduced.Dispose();
                //ImageCroped.Dispose();
            }
            catch
            {

            }

        }

        public void DialationCircle(HWindowControl WindowControl, ref HObject Image, ref HObject Region,ref HTuple Radius,HTuple hWindowHandle,out HObject RegionDilation)
        {
            try
            {             
                HOperatorSet.GenEmptyObj(out RegionDilation);
                HOperatorSet.DilationCircle(Region, out RegionDilation, Radius);

                HOperatorSet.SetDraw(hWindowHandle, "fill");
                HOperatorSet.ClearWindow(hWindowHandle);   //刷新窗体              
                RegionFitWindow(Image, hWindowHandle, RegionDilation);

            }
            catch
            {
                RegionDilation = null;               
                return;
            }
        }

    }
}
