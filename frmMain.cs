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
    public partial class frmMain : Form
    {
        #region Variables

        float xvalues;  
        float yvalues;
        public static string StrPath = Application.StartupPath; //program startup path

        /// <summary>
        /// graphics variables
        /// </summary>
        HObject m_Image;       //Masterimage
        HTuple m_hWindowHandle3; 

        /// <summary>
        /// story-create variables
        /// </summary>
        string ProjectPath; //Project/Story folder
        string ImagePath;   //OCR image folder
        string OCRCodePath; //OCR code folder + OCR potential classifiers folder
        string OCRHandlePath; //OCR final selected classifier folder--has bee limited as only one classifier contained

        /// <summary>
        /// story-loading variables
        /// </summary>
        string Load_ProjectPath;
        string Load_ImgPath;
        string Load_OCRCodePath;

        /// <summary>
        /// class
        /// </summary>
        PubStore m_PubStore = PubStore.Instance();
        Tools m_Tools = PubStore.Instance().m_Tools;       
        CamParam m_camParam = PubStore.Instance().m_camParam;

      
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// frmMain load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            #region window resize
            this.Resize += new EventHandler(frmMain_Resize); //添加窗体拉伸重绘事件 windowHandle resize
            xvalues = this.Width;//记录窗体初始大小 windowHandle initialized size
            yvalues = this.Height;
            SetTag(this);
            #endregion

            #region load Halcon Classifier folder, copy all halcon classifiers inside

            try
            {
                DirectoryInfo DInfo = new DirectoryInfo(StrPath + "\\Classifier");
                if (DInfo.Exists)  //if Halcon Classifier folder does exist
                {
                    //if exists, do nothing

                }
                else  // if Halcon Classifier does not exist
                {
                    DInfo.Create();
                }

            }
            catch
            {
                MessageBox.Show("Classifier file missing!");
            }

            #endregion

            #region main winForm windowHandle      
            m_Tools.HalconInit(hWindowControl3, out m_hWindowHandle3);  //主窗体 main winForm 
            #endregion
            
            listBox1.SelectionMode = SelectionMode.One;  //Story listbox，only one can be selected
            Load_ProjectPath = StrPath + "\\Proj";
            m_Tools.LoadType(listBox1, ref Load_ProjectPath); //traverse all Story/Project

        }

        #region  窗体控件等比大小缩放 WinForm controls Resize
        private void frmMain_Resize(object sender, EventArgs e)
        {
            float newX = this.Width / xvalues;//获得比例
            float newY = this.Height / yvalues;
            SetControls(newX, newY, this);
        }
        private static void SetControls(float newX, float newY, Control cons)//Controls change size
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

        #region Load Story 加载项目click事件 Load Story click/Story choose
        private void btnLoadType_Click(object sender, EventArgs e)
        {
            try
            {
                tb_type.Text = listBox1.SelectedItem.ToString();
                txType.Text = listBox1.SelectedItem.ToString();

                #region //loading masterimage path
                Load_ImgPath = StrPath + "\\" + "Proj" + "\\" + tb_type.Text + "\\" + "Image";
                //  读双目图片（显示）
                //  判断图像是否为空  

                if ((File.Exists(Load_ImgPath + "\\" + "masterimage.bmp")))
                {
                    HOperatorSet.GenEmptyObj(out m_Image);
                    m_Image.Dispose();
                    HOperatorSet.ReadImage(out m_Image, Load_ImgPath + "\\" + "masterimage.bmp");
                    m_Tools.Imgshow(m_Image, m_hWindowHandle3, m_Image);

                }
                else
                {
                    MessageBox.Show("Image invalid!");
                }

                #endregion

                #region //laoding halcon code .txt file in WindowHandle III
                Load_OCRCodePath = StrPath + "\\" + "Proj" + "\\" + tb_type.Text + "\\" + "Code";
                //read Halcon or OpenCV .txt code, and showup in WindowHandle III, "VisionSript"
                //but this function is optional. if no .txt file saved by user, then no showup;
                string s;
                string txtCodePath = Load_OCRCodePath + "\\HCode.txt";
                //ReadStream
                m_Tools.ReadStream(ref txtCodePath, out s);
                richTextBox1.Text = s.ToString();
                #endregion

                m_PubStore.m_type = tb_type.Text; //transfer string vairalbe
                
            }
            catch
            {

            }
        }

        #endregion

        #region New story OCR 
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Contains(txType.Text))
                {
                    MessageBox.Show("name exists already!");
                }
                else
                {
                    listBox1.Items.Add(txType.Text);
                    tb_type.Text = txType.Text;
                }
                if (txType.Text == "")
                {
                    return;
                }
                
                //create project directory
                ProjectPath = StrPath + "\\" + "Proj" + "\\" + tb_type.Text;
                m_Tools.CreateAllDirectory(ProjectPath);

                //create masterimage folder
                ImagePath = ProjectPath + "\\Image";
                m_Tools.CreateAllDirectory(ImagePath);
                
                //create Halcon/other code *.txt folder
                //this can be option
                OCRCodePath = ProjectPath + "\\Code";
                m_Tools.CreateAllDirectory(OCRCodePath);

                //create OCRHandle folder for this.Story--//Code//
                OCRHandlePath = ProjectPath + "\\OCRHandle";
                m_Tools.CreateAllDirectory(OCRHandlePath);

                //save masterimage--//Image//
                HOperatorSet.WriteImage(m_Image, "bmp", 0, ImagePath + "\\" + "masterimage");

            }
            catch
            {
                MessageBox.Show("Story name invalid!");
            }
        }
        #endregion

        #region Open frmExe window
        /// <summary>
        /// sub windowForm for masterimage OCR detail operation
        /// after operation save story and ready for loading for volume OCR
        /// Open frmExe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                frmExe m_frmExe = new frmExe();
                m_frmExe.Show();
              
            }
            catch
            {
             
            }
           
        }
        #endregion
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tb_type.Text = listBox1.SelectedItem.ToString();
            }
            catch
            {

            }
        }

        /// <summary>
        /// if no camera grab image;
        /// Open masterimage from local file;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
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
                    m_Tools.Imgshow(m_Image, m_hWindowHandle3, m_Image);
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

        /// <summary>
        /// Save_Img
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImagePath!=null)
                {
                    //save image
                    HOperatorSet.WriteImage(m_Image, "bmp", 0, ImagePath + "\\" + "masterimage");
                    MessageBox.Show("Master image saved complete");
                }
                else
                {
                    MessageBox.Show("Check story file");
                    return;
                }
               
            }
            catch
            {
                MessageBox.Show("Check image or story file");
                return;
            }
         
        }

        #region Remove Story
        /// <summary>
        /// Remove story file,picture path and everything
        /// remove from listBox1 project list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelType_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\" + "Proj" + "\\" + listBox1.SelectedItem;
                if (!Directory.Exists(path))
                {
                    return;
                }
                Directory.Delete(path, true);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);         
                HOperatorSet.ClearWindow(m_hWindowHandle3);
               
            }
            catch
            {
                MessageBox.Show("story delete falied");
            }
        }
        #endregion
    }
}
