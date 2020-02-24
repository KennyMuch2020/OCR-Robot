using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace OCRTrainee
{
    public partial class frmExe : Form
    {
        #region Variables
        /// <summary>
        /// C# variables
        /// </summary>
        float xvalues, yvalues;
        private string load_imagePath; //
        private string load_codePath;  //
        private string load_handlePath; 
        private string load_classifPath; //directory _path for halcon OCRHandle
        public static string StrPath = Application.StartupPath; //program start _path
        string m_Exetype; //中间变量，从frmMain传入
        string sortType; //

        /// <summary>
        /// Halcon variables
        /// HObject
        /// </summary>
        HObject m_Image;       //image from Camera, or local disk  
        HObject m_Region;
        HObject m_imageReduced;      //ROI from imageReduced
        HObject m_imageCroped;     //ROI from imageCroped, for display result; make display bigger for user       
        HObject ho_RegionDilation, ho_ConnectedRegions;
        HObject ho_RegionDilation_C, ho_ConnectedRegions_C;

        HObject ho_RegionIntersection, ho_Characters;
        HObject ho_RegionIntersection_C;
        HObject ho_Characters_C;

        HObject T_Region;    //Threshold Region
        HObject C_Region;    //Threshold CropRegion for display 

        HObject SRegion_T;  //select shape region
        HObject SRegion_C;
        HObject ho_CharCandidates;
        HObject ho_CharCandidates_C;

        /// <summary>
        /// HTuple 
        /// </summary>
        HTuple OCRHandle; //OCRHandle 
        HTuple ResultString = new HTuple();
        HTuple hv_imageWidth = new HTuple(),hv_imageHeight = new HTuple();
        HTuple hv_UsedThreshold1;
        HTuple hv_RadiusDialate = new HTuple(); //Dialation radius
        HTuple hv_Length = new HTuple(), hv_Classes = new HTuple();
        HTuple hv_J = new HTuple(), hv_CharacterNames = new HTuple();
        HTuple hv_CharacterCount = new HTuple(), hv_OCRHandle = new HTuple();
        HTuple hv_Error = new HTuple(), hv_ErrorLog = new HTuple();
        HTuple hv_TrainingDocument = new HTuple();
            //hv_UsedThreshold1 = new HTuple();
        HTuple hv_Class = new HTuple();
        HTuple hv_Confidence = new HTuple(), hv_Area = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
         //Rotate image
        HTuple hv_charHeight;
        HTuple hv_OrientationAngle = new HTuple();
        HTuple hv_HomMat2D=new HTuple();

        /// <summary>
        /// WindowHandles
        /// </summary>
        HTuple m_hWindowHandle1;
        HTuple m_hWindowHandle2;
        HTuple m_hWindowHandle3;

        /// <summary>
        /// class instantiation
        /// Singleton Pattern
        /// </summary>
        PubStore m_PubStore = PubStore.Instance();        
        Tools m_Tools = PubStore.Instance().m_Tools;
        Threshold m_Thres = PubStore.Instance().m_Thres;
        SelectShape m_Select = PubStore.Instance().m_Select;
        ShapeModel m_ShapeM = PubStore.Instance().m_shapeM;
        
        #endregion

        #region Open Img
        private void btnOpenImage_Click(object sender, EventArgs e)
        {          
            ofdImage.Filter = "(*.bmp,*.png;*.jpg;*.jpeg;*.tif)|*.bmp;*.png;*.jpg;*.tif";
            ofdImage.Multiselect = false;
            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    HOperatorSet.GenEmptyObj(out m_Image);
                    m_Image.Dispose();
                    HOperatorSet.ReadImage(out m_Image, ofdImage.FileName);
                    
                    m_Tools.Imgshow(m_Image, m_hWindowHandle1, m_Image);
                }
                catch (Exception)
                {
                    MessageBox.Show("Image Invalid!");
                }
                finally
                {
                    ofdImage.Dispose();
                }
               
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Threshold HScrollBar
        private void sb_ThresU_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                sb_ThresU.Focus();
                tb_ThresU.Text = sb_ThresU.Value.ToString();
                HOperatorSet.GenEmptyObj(out T_Region);
                HOperatorSet.GenEmptyObj(out C_Region);
                T_Region.Dispose();
                C_Region.Dispose();
                
                HTuple minGray, maxGray;
                minGray = sb_ThresL.Value;
                maxGray = sb_ThresU.Value;
                if (sb_ThresL.Value >= sb_ThresU.Value)
                {
                    sb_ThresL.Value = sb_ThresU.Value; 
                    tb_ThresL.Text = sb_ThresL.Value.ToString();
                }
                
                m_Thres.HThreshold(hWindowControl2, ref m_imageReduced,ref m_imageCroped,m_hWindowHandle2, minGray, maxGray, ref T_Region,ref C_Region);
  
            }
            catch
            {

            }
        }
       

        private void sb_ThresL_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {            
                sb_ThresL.Focus();
                tb_ThresL.Text = sb_ThresL.Value.ToString();
                HOperatorSet.GenEmptyObj(out T_Region);
                T_Region.Dispose();

                HTuple minGray, maxGray;
                minGray = sb_ThresL.Value;
                maxGray = sb_ThresU.Value;
                if (sb_ThresU.Value <= sb_ThresL.Value)
                {
                    sb_ThresU.Value = sb_ThresL.Value;
                    tb_ThresU.Text = sb_ThresU.Value.ToString();
                }
               
                m_Thres.HThreshold(hWindowControl2, ref m_imageReduced, ref m_imageCroped,m_hWindowHandle2, minGray, maxGray, ref T_Region,ref C_Region);
                 
            }
            catch
            {

            }
        }
        #endregion

        #region Select_Shape HScrollBar
        private void sb_ShapSelU_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                sb_ShapSelU.Focus();
                tb_SelShU.Text = sb_ShapSelU.Value.ToString();

                if (sb_ShapSelU.Value <= sb_ShapSelL.Value)
                {
                    sb_ShapSelU.Value = sb_ShapSelL.Value;
                    tb_SelShU.Text = sb_ShapSelU.Value.ToString();
                }

                HTuple min, max;
                min = sb_ShapSelL.Value;
                max = sb_ShapSelU.Value;

                HOperatorSet.GenEmptyObj(out SRegion_T);
                HOperatorSet.GenEmptyObj(out SRegion_C);
                SRegion_T.Dispose();
                SRegion_C.Dispose();
                m_Select.selectShape(hWindowControl2,ref m_imageCroped, ref T_Region, ref C_Region, m_hWindowHandle2, min, max, out SRegion_T, out SRegion_C);

                

            }
            catch
            {

            }
        }

        private void sb_ShapSelL_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                sb_ShapSelL.Focus();
                tb_SelShL.Text = sb_ShapSelL.Value.ToString();

                if (sb_ShapSelU.Value <= sb_ShapSelL.Value)
                {
                    sb_ShapSelU.Value = sb_ShapSelL.Value;
                    tb_SelShL.Text = sb_ShapSelL.Value.ToString();
                }

                HTuple min, max;
                min = sb_ShapSelL.Value;
                max = sb_ShapSelU.Value;

                HOperatorSet.GenEmptyObj(out SRegion_T);
                HOperatorSet.GenEmptyObj(out SRegion_C);
                SRegion_T.Dispose();
                SRegion_C.Dispose();
                m_Select.selectShape(hWindowControl2, ref m_imageCroped, ref T_Region, ref C_Region, m_hWindowHandle2, min, max, out SRegion_T, out SRegion_C);

            }
            catch
            {

            }
        }

        #endregion

        #region Classifier Load for final classifier working
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string selectFile = load_codePath + "\\" + lb_ClassStory.SelectedItem.ToString(); //select the final classifier
                string[] Dirs = Directory.GetFiles(load_handlePath);  //Directory
                lb_Handle.Items.Clear();
                foreach (string TypePath in Dirs) //step1: clear the former classifer in the \\OCRHandle
                {
                    try
                    {
                        File.Delete(TypePath);
                        
                    }
                    catch 
                    {
                        
                    }
                }

                //step 2: only one classifier can exist in the  \\OCRHandle
                //copy the new classifier into \\OCRHandle
                //第二部，把选定的分类器添加/复制到load_handlePath文件夹里。每次load_handlePath里只有一个分类器
                File.Copy(selectFile, load_handlePath + "\\" + lb_ClassStory.SelectedItem.ToString());
                m_Tools.LoadFile(lb_Handle, ref load_handlePath);
                MessageBox.Show("Classifer updated done");


            }
            catch
            {

                MessageBox.Show("Classifer loaded error");
            }
           
        }
        #endregion

        #region ROI rectangular
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.GenEmptyObj(out m_Region);
                HOperatorSet.GenEmptyObj(out m_imageReduced);
                HOperatorSet.GenEmptyObj(out m_imageCroped);
                m_Region.Dispose();
                m_imageReduced.Dispose();
                m_imageCroped.Dispose(); //display for windowHandle2
                
                m_Tools.DrawRectangel(hWindowControl1, m_Image, m_hWindowHandle1, ref m_Region, ref m_imageReduced,ref m_imageCroped);
                m_Tools.ImageFitWindow(m_imageCroped,m_hWindowHandle2);  //display for windowHandle2          
                m_Region.Dispose();
               
                //Show execute result
                label1.Text = "ROI done";
            }
            catch
            {

            }
        }
        #endregion

     

        /// <summary>
        /// RD value change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nd_RD_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                hv_RadiusDialate = Convert.ToDouble(nd_RD.Value);

                HOperatorSet.GenEmptyObj(out ho_RegionDilation);
                HOperatorSet.GenEmptyObj(out ho_RegionDilation_C);
                ho_RegionDilation.Dispose();
                ho_RegionDilation_C.Dispose();
                if (cb_shape.Checked)
                {
                    HOperatorSet.DilationCircle(SRegion_T, out ho_RegionDilation, hv_RadiusDialate);
                    m_Tools.DialationCircle(hWindowControl3, ref m_imageCroped, ref SRegion_C, ref hv_RadiusDialate, m_hWindowHandle3, out ho_RegionDilation_C);
                }
                else
                {
                    HOperatorSet.DilationCircle(T_Region, out ho_RegionDilation, hv_RadiusDialate);
                    m_Tools.DialationCircle(hWindowControl3, ref m_imageCroped, ref C_Region, ref hv_RadiusDialate, m_hWindowHandle3, out ho_RegionDilation_C);
                }
                
                
                
            }
            catch
            {
               
            }
            finally
            {

            }
            
        }
        

        #region Classifier Open, Save, Select, Delect
        private void cb_classi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cb_classi.SelectedIndex)
                {
                    case 0:
                        ofdOMC.Filter = "(*.omc)|*.omc";
                        ofdOMC.Multiselect = false;
                        if (ofdOMC.ShowDialog() == DialogResult.OK)
                        {
                            //复制classifier文件到指定文件夹里
                            
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 1:
                      
                        break;              
                }
            }
            catch
            {

            }
        }


        #endregion

        #region halcon ocr copy
        /// <summary>
        /// copy halcon OCR to Story to replace story OCR
        /// put name still as Classifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_Classifiers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Dialation and Connection for ho_RegionDilation
        private void btn_dialate_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDilation, out ho_ConnectedRegions);

                //for display 
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions_C);
                ho_ConnectedRegions_C.Dispose();
                HOperatorSet.Connection(ho_RegionDilation_C, out ho_ConnectedRegions_C);
                
                // full window display           
                HOperatorSet.ClearWindow(m_hWindowHandle3); //refresh window
                m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_ConnectedRegions_C);

                //check Objest amount after connection, this is important to go to next step.
                //make sure for segment for characters is correct
                HTuple charsNum;
                HOperatorSet.CountObj(ho_ConnectedRegions,out charsNum);
                tb_charsNum.Text = charsNum.ToString();
              
            }
            catch
            {

            }
        }

        #endregion

        #region Partition charblocks
        private void btn_parti_Click(object sender, EventArgs e)
        {
            try
            {
                //define partition value, width and height 
                HTuple partiWidth, partiHeight;
                partiWidth = Convert.ToDouble(nud_partW.Value.ToString());
                partiHeight = Convert.ToDouble(nud_partH.Value.ToString());
              
                HOperatorSet.GenEmptyObj(out ho_CharCandidates);
                HOperatorSet.GenEmptyObj(out ho_RegionIntersection);

                ho_CharCandidates.Dispose();
                ho_RegionIntersection.Dispose();
                m_Tools.Partition(ref partiWidth, ref partiHeight, ref ho_ConnectedRegions, ref T_Region, out ho_CharCandidates, out ho_RegionIntersection);

                //display result on the screen, put image/region bigger to see
                HOperatorSet.GenEmptyObj(out ho_CharCandidates_C);
                HOperatorSet.GenEmptyObj(out ho_RegionIntersection_C);
                ho_CharCandidates_C.Dispose();
                ho_RegionIntersection_C.Dispose();
                m_Tools.Partition(ref partiWidth, ref partiHeight, ref ho_ConnectedRegions_C, ref C_Region, out ho_CharCandidates_C, out ho_RegionIntersection_C);
                 
                HOperatorSet.SetDraw(m_hWindowHandle3, "margin");
                HOperatorSet.ClearWindow(m_hWindowHandle3); //Refresh window
                //m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_RegionIntersection_C); //Option 1
                m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_ConnectedRegions_C);     //Option 2
                m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_CharCandidates_C);
                 
            }
            catch
            {

            }

        }


        #endregion

        #region Sort Region Check
        private void btn_sort_Click(object sender, EventArgs e)
        {
            try
            {
                sortType = cb_sort.SelectedItem.ToString(); //SortRegion的方式

                HOperatorSet.GenEmptyObj(out ho_Characters);
                HOperatorSet.GenEmptyObj(out ho_Characters_C);

                ho_Characters.Dispose();
                HOperatorSet.SortRegion(ho_RegionIntersection, out ho_Characters, "character",
                    "true", sortType);

                //放大显示排序
                ho_Characters_C.Dispose();
                HOperatorSet.SortRegion(ho_RegionIntersection_C, out ho_Characters_C, "character",
                   "true", sortType);
                
                HTuple number, Row_i, Column_i, hv_Area_i;
                HObject ho_ObjectSelected_i = null;
                HOperatorSet.ClearWindow(m_hWindowHandle3);
                HOperatorSet.GenEmptyObj(out ho_ObjectSelected_i);

                HOperatorSet.CountObj(ho_Characters_C, out number);
                for (int i = 1; i < number + 1; i++)
                {
                    ho_ObjectSelected_i.Dispose();
                    HOperatorSet.SelectObj(ho_Characters_C, out ho_ObjectSelected_i, i);         
                    HOperatorSet.AreaCenter(ho_ObjectSelected_i, out hv_Area_i, out Row_i,
                        out Column_i);
                    m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_ObjectSelected_i);
                    //display at HwindowControl3
                    m_Tools.disp_message(m_hWindowHandle3, i, "window", Row_i, Column_i,
                        "black", "true");
                    m_Tools.RegionFitWindow(m_imageCroped, m_hWindowHandle3, ho_ObjectSelected_i);

                }

            }
            catch
            {

            }
        }


        #endregion

        #region Refresh refresh classifier list of this.Story
        private void btn_refClass_Click(object sender, EventArgs e)
        {
            try
            {
                m_Tools.LoadFileExtensionOMC(lb_ClassStory, ref load_codePath);
                m_Tools.LoadFile(lb_Handle,ref load_handlePath);
            }
            catch
            {

            }
        }
        #endregion

        #region do OCR manual
        private void btn_OCRM_Click(object sender, EventArgs e)
        {
            try
            {
                rb_result.Clear(); //clearbox of result history
                sortType = cb_sort.SelectedItem.ToString(); //SortRegion type row/colume

                //read OCR calssifer
                //OCRHandle.Dispose();
                string[] Dirs = Directory.GetFiles(load_handlePath);  //Directory类的获取文件

                HOperatorSet.ReadOcrClassMlp(Dirs, out OCRHandle);
                //Classification
                hv_Class.Dispose(); hv_Confidence.Dispose();
                HOperatorSet.DoOcrMultiClassMlp(ho_Characters, m_Image, OCRHandle, out hv_Class,
                    out hv_Confidence);

                //
                //Display results
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                HOperatorSet.AreaCenter(ho_RegionIntersection, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.DispObj(m_Image, m_hWindowHandle1);

                ResultString = hv_Class;

                for (int i = 0; i < hv_Class.Length; i++)
                {
                    m_Tools.disp_message(m_hWindowHandle1, ResultString[i], "window", hv_Row[i] + 3, hv_Column[i] + 8, "blue", "true");
                    rb_result.AppendText(ResultString[i]);
                }

                #region  //release all HObject      
                T_Region.Dispose();     //Threshold
                C_Region.Dispose();     //Threshold
                ho_RegionDilation.Dispose();        // Splite and dialate
                ho_RegionDilation_C.Dispose();      // Splite and dialate
                ho_ConnectedRegions.Dispose();      // Splite and dialate
                ho_ConnectedRegions_C.Dispose();    // Splite and dialate
                ho_RegionIntersection.Dispose();
                ho_Characters.Dispose();            // Sort
                ho_Characters_C.Dispose();          // Sort
                OCRHandle.Dispose();                                        // OCR Manual
                hv_Class.Dispose(); hv_Confidence.Dispose();                // OCR Manual
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();   // OCR Manual
                m_Region.Dispose();                     //ROI rectangular
                m_imageCroped.Dispose();                //ROI rectangular
                m_imageReduced.Dispose();               //ROI rectangular
              
                //ho_CharBlocks.Dispose();            //Partition
                ho_CharCandidates.Dispose();        //Partition
                ho_RegionIntersection.Dispose();    //Partition

               // ho_CharBlocks_C.Dispose();          //Partition
                ho_CharCandidates_C.Dispose();      //Partition
                ho_RegionIntersection_C.Dispose();  //Partition
                #endregion
                
            }
            catch
            {

            }
        }
        
        #endregion

        #region Rotate Image if image text line has a degree 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.GetImageSize(m_Image,out hv_imageWidth,out hv_imageHeight);
                hv_charHeight = Convert.ToDouble(nud1.Value.ToString());            
                //affineTransImamge
                m_Tools.affineTransImage(ref m_Image,ref hv_imageWidth,ref hv_imageHeight,ref hv_charHeight);                     
                m_Tools.Imgshow(m_Image, m_hWindowHandle1, m_Image);              
            }
            catch
            {

            }
        }
        #endregion

        #region Execution OCR
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {   
                rb_result.Clear(); //清空一下box
                sortType = cb_sort.SelectedItem.ToString(); //SortRegion的方式

               
                HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
                HOperatorSet.GenEmptyObj(out ho_Characters);

                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDilation, out ho_ConnectedRegions);


                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_ConnectedRegions, T_Region, out ho_RegionIntersection);
                ho_Characters.Dispose();
                HOperatorSet.SortRegion(ho_RegionIntersection, out ho_Characters, "character",
                    "true", sortType);
                
                //disp_SortRegion
                //CropImage for display
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions_C);
                HOperatorSet.GenEmptyObj(out ho_RegionIntersection_C);
                HOperatorSet.GenEmptyObj(out ho_Characters_C);

                ho_ConnectedRegions_C.Dispose();
                HOperatorSet.Connection(ho_RegionDilation_C, out ho_ConnectedRegions_C);
                ho_RegionIntersection_C.Dispose();
                HOperatorSet.Intersection(ho_ConnectedRegions_C, C_Region, out ho_RegionIntersection_C);
                ho_Characters_C.Dispose();
                HOperatorSet.SortRegion(ho_RegionIntersection_C, out ho_Characters_C, "character",
                   "true", sortType);
            
                HTuple number,Row_i,Column_i, hv_Area_i;
                HObject ho_ObjectSelected_i = null;
                HOperatorSet.ClearWindow(m_hWindowHandle3);
                HOperatorSet.GenEmptyObj(out ho_ObjectSelected_i);
              
                HOperatorSet.CountObj(ho_Characters_C, out number);
                for (int i = 1; i < number+1; i++)
                {
                    ho_ObjectSelected_i.Dispose();
                    HOperatorSet.SelectObj(ho_Characters_C, out ho_ObjectSelected_i,i);            
                    HOperatorSet.AreaCenter(ho_ObjectSelected_i, out hv_Area_i, out Row_i,
                        out Column_i);

                    //display at HwindowControl3
                    m_Tools.disp_message(m_hWindowHandle3, i, "window", Row_i, Column_i,
                        "black", "true");

                }

                //
                //读取分类器
                //OCRHandle.Dispose();
                HOperatorSet.ReadOcrClassMlp(load_codePath + "\\Classifier.omc", out OCRHandle);
                //Classification
                hv_Class.Dispose(); hv_Confidence.Dispose();
                HOperatorSet.DoOcrMultiClassMlp(ho_Characters, m_Image, OCRHandle, out hv_Class,
                    out hv_Confidence);

                //
                //Display results
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                HOperatorSet.AreaCenter(ho_RegionIntersection, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.DispObj(m_Image, m_hWindowHandle1);

                ResultString = hv_Class;
              
                for (int i = 0; i < hv_Class.Length; i++)
                {

                    m_Tools.disp_message(m_hWindowHandle1, ResultString[i], "window", hv_Row[i] + 3, hv_Column[i] + 8, "blue", "true");                              
                    rb_result.AppendText(ResultString[i]);
                }

                label3.Text = "OCR exe done";

                //释放HObject                            
                ho_RegionIntersection_C.Dispose();
                ho_Characters_C.Dispose();
                T_Region.Dispose();
                C_Region.Dispose();
                ho_RegionDilation.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionIntersection.Dispose();
                ho_Characters.Dispose();
                OCRHandle.Dispose();
                hv_Class.Dispose(); hv_Confidence.Dispose();
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                m_Region.Dispose();
                m_imageCroped.Dispose();
                m_imageReduced.Dispose();
                ho_RegionDilation_C.Dispose();
                ho_ConnectedRegions_C.Dispose();


            }
            catch
            {
                MessageBox.Show("OCR process failed");
            }
            finally
            {
               
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// 把图片文件夹与代码文件夹写入
        /// </summary>
        public frmExe()
        {
            InitializeComponent();
            m_Exetype = m_PubStore.m_type; //中间变量从frmMain传入
            load_imagePath = StrPath + "\\" + "Proj" + "\\" + m_Exetype + "\\Image"; //图片文件夹
            load_codePath= StrPath + "\\" + "Proj" + "\\" + m_Exetype + "\\Code"; //代码文件夹，story分类器文件夹
            load_handlePath= StrPath + "\\" + "Proj" + "\\" + m_Exetype + "\\OCRHandle"; //story工作的分类器文件夹

            load_classifPath = StrPath + "\\" + "Classifier"; //系统全部分类器
          
        }

        /// <summary>
        /// 第二界面，操作界面Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExe_Load(object sender, EventArgs e)
        {
            try
            {
                #region 加载Story的分类器
                lb_ClassStory.SelectionMode = SelectionMode.One;  //只能选择一项    
                m_Tools.LoadFileExtensionOMC(lb_ClassStory,ref load_codePath);
                #endregion

                #region 所有控件大小缩放
                this.Resize += new EventHandler(frmExe_Resize); //添加窗体拉伸重绘事件
                xvalues = this.Width;//记录窗体初始大小
                yvalues = this.Height;
                SetTag(this);
                #endregion

                #region SortRegion选择
                this.cb_sort.Items.Add("row");
                this.cb_sort.Items.Add("col");
                this.cb_sort.SelectedIndex = 0;
                #endregion

                #region Classifier增删改查            
                this.cb_classi.Items.Add("Check Classifier");
              
                #endregion

                #region 窗体句柄
                m_Tools.HalconInit(hWindowControl1, out m_hWindowHandle1);  //窗体1
                m_Tools.HalconInit(hWindowControl2, out m_hWindowHandle2);  //窗体2
                m_Tools.HalconInit(hWindowControl3, out m_hWindowHandle3);  //窗体2

                #endregion

                #region 加载图片
                //  读双目图片（显示）
                //  判断图像是否为空       
                if ((File.Exists(load_imagePath + "\\" + "masterimage.bmp")))
                {
                    HOperatorSet.GenEmptyObj(out m_Image);
                    m_Image.Dispose();
                    HOperatorSet.ReadImage(out m_Image, load_imagePath + "\\" + "masterimage.bmp");
                    m_Tools.Imgshow(m_Image, m_hWindowHandle1, m_Image);

                }
                else
                {
                    MessageBox.Show("Image invalid!");
                }
                #endregion

                #region 加载halcon自带分类器
                lb_Classifiers.SelectionMode = SelectionMode.One;  //只能选择一项    
                m_Tools.LoadFile(lb_Classifiers, ref load_classifPath);    //加载遍历分类器文件halcon自带
                #endregion

                // 加载story最终工作分类器
                m_Tools.LoadFile(lb_Handle, ref load_handlePath);

                #region lb_Classifiers listbox显示
                lb_Classifiers.HorizontalScrollbar = true;   //水平方向可显示
                lb_Classifiers.ScrollAlwaysVisible = true;   //垂直方向可显示
                #endregion

                //最后工作用分类器
                lb_Handle.ScrollAlwaysVisible = false;  //垂直方向不可显示

                #region sb_Thres 滚动条设置
                sb_ThresU.Minimum = 1;
                sb_ThresU.Maximum = 255;
                sb_ThresU.LargeChange = 1;
                sb_ThresU.Value = 10;

                sb_ThresL.Minimum = 1;
                sb_ThresL.Maximum = 255;
                sb_ThresL.LargeChange = 1;
                sb_ThresL.Value = 1;

                tb_ThresU.Text = sb_ThresU.Value.ToString();
                tb_ThresL.Text = sb_ThresL.Value.ToString();
                #endregion

                #region sb_SelectShape 滚动条设置
                sb_ShapSelU.Minimum = 1;
                sb_ShapSelU.Maximum = 5000;
                sb_ShapSelU.LargeChange = 1;
                sb_ShapSelU.Value = 10;

                sb_ShapSelL.Minimum = 1;
                sb_ShapSelL.Maximum =5000;
                sb_ShapSelL.LargeChange = 1;
                sb_ShapSelL.Value = 1;

                tb_SelShU.Text = sb_ShapSelU.Value.ToString();
                tb_SelShL.Text = sb_ShapSelL.Value.ToString();
                #endregion

                nd_RD.Value = 1;              
                nud1.Value = 75; //CharHeight Roate image
                nud_partW.Value = 20;  //Partition rectangle1 Width
                nud_partH.Value = 70;  //Partition rectangle1 Height
            }
            catch
            {

            }
            
        }

        #region  窗体控件等比大小缩放方法
        private void frmExe_Resize(object sender, EventArgs e)
        {
            float newX = this.Width / xvalues;//获得比例
            float newY = this.Height / yvalues;
            SetControls(newX, newY, this);
        }

        private static void SetControls(float newX, float newY, Control cons)//改变控件的大小
        {
            foreach (Control con in cons.Controls)
            {
                try
                {
                    if (con.Tag == null) continue;
                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                    float a = Convert.ToSingle(mytag[0]) * newX;
                    con.Width = (int)a;
                    a = Convert.ToSingle(mytag[1]) * newY;
                    con.Height = (int)a;
                    a = Convert.ToSingle(mytag[2]) * newX;
                    con.Left = (int)a;
                    a = Convert.ToSingle(mytag[3]) * newY;
                    con.Top = (int)a;
                    Single currentSize = Convert.ToSingle(mytag[4]) * (newY - 1);
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count >= 0)
                    {
                        SetControls(newX, newY, con);
                    }
                }
                catch
                {

                }
            }
        }
        
        /// <summary>
        /// 遍历窗体中控件函数
        /// </summary>
        /// <param name="cons"></param>
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)  //遍历窗体中的控件,记录控件初始大小
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }
        
        #endregion

        #region Mouse operate WindowHandle
        /// <summary>
        /// mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            #region hwindowcontrol控件中的图像移动，缩放
            HTuple width, height;
            Rectangle part_rectangle;
            Rectangle inirectangle;
            Point mouseDown_point = new Point();
            Point curPoint = new Point();
            #endregion
            if (MouseButtons == MouseButtons.Left)
            {
                inirectangle = hWindowControl1.ImagePart;
                int x = (int)e.X;
                int y = (int)e.Y;
                mouseDown_point = new Point(x, y);
            }
            if (MouseButtons == MouseButtons.Right)
            {
                HOperatorSet.GetImageSize(m_Image, out width, out height);
                // part_rectangle = new Rectangle(0, 0, width, height);
                hWindowControl1.HalconWindow.ClearWindow();
                // hWindowControl1.ImagePart = part_rectangle;
                hWindowControl1.HalconWindow.DispObj(m_Image);
            }
        }
        
        /// <summary>
        /// mouse wheel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (!m_Tools.ObjectValided(m_Image))
            {
                return;
            }
            Rectangle rec = new Rectangle();
            rec = hWindowControl1.ImagePart;
            if (e.Delta > 0)   //放大图片
            {
                rec.Width = (int)(rec.Width * 0.8);
                rec.Height = (int)(rec.Height * 0.8);
                int org_x = (int)((double)rec.X + (e.X - (double)rec.X) * 0.2);
                int org_y = (int)((double)rec.Y + (e.Y - (double)rec.Y) * 0.2);
                rec.X = org_x;
                rec.Y = org_y;
                hWindowControl1.ImagePart = rec;
            }
            else if (e.Delta < 0)   //缩小图片
            {
                rec.Width = (int)(rec.Width * 1.2);
                rec.Height = (int)(rec.Height * 1.2);
                int org_x = (int)((double)rec.X - (e.X - (double)rec.X) * 0.2);
                int org_y = (int)((double)rec.Y - (e.Y - (double)rec.Y) * 0.2);
                rec.X = org_x;
                rec.Y = org_y;
                hWindowControl1.ImagePart = rec;
            }

            hWindowControl1.HalconWindow.ClearWindow();
            hWindowControl1.HalconWindow.DispObj(m_Image);
            hWindowControl1.Refresh();
        }

      
        /// <summary>
        /// 鼠标拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            //if (!m_Tools.ObjectValided(m_Image))
            //{
            //    return;
            //}

            //if (MouseButtons == MouseButtons.Left)
            //{
            //    try
            //    {
            //        int x = (int)e.X;
            //        int y = (int)e.Y;
            //        curPoint = new Point(x, y);
            //        int dx = curPoint.X - mouseDown_point.X;
            //        int dy = curPoint.Y - mouseDown_point.Y;
            //        if (part_rectangle != null)
            //        {
            //            int row1 = inirectangle.X - dx;
            //            int col1 = inirectangle.Y - dy;
            //            Size size = inirectangle.Size;
            //            part_rectangle = new Rectangle(new Point(row1, col1), size);

            //            hWindowControl1.HalconWindow.ClearWindow();
            //            hWindowControl1.ImagePart = part_rectangle;
            //            hWindowControl1.HalconWindow.DispObj(m_Image);
            //            hWindowControl1.Refresh();
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        #endregion

        #region GC
        /// <summary>
        /// GC
        /// </summary>
        ~frmExe()
        {

            m_Image.Dispose();
            m_Region.Dispose();
            m_imageReduced.Dispose();
            m_imageCroped.Dispose();
            ho_RegionDilation.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionDilation_C.Dispose();
            ho_ConnectedRegions_C.Dispose();
            ho_RegionIntersection.Dispose();
            ho_Characters.Dispose();
            ho_RegionIntersection_C.Dispose();
            ho_Characters_C.Dispose();
            T_Region.Dispose();
            C_Region.Dispose();

        }
        #endregion





    }
}
