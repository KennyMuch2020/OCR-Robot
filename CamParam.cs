using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRTrainee
{
    public class CamParam
    {
        #region 字段
        public HTuple m_AcqHandle;          //相机类的对象指针
        public HTuple m_Name;               //相机名
        public bool m_bTrigger;             //触发模式：true为外触
        public HTuple m_ExposureTime;       //相机曝光
        public HTuple m_Gain;               //相机增益
        public bool m_bCamIsOK;              //相机是否存在
        public bool m_bBusy;                 //相机工作标志位
        #endregion

        public CamParam(string Name = "ccd1")
        {
            m_AcqHandle = -1;
            m_Name = Name;
            m_bTrigger = false;
            m_ExposureTime = 10000;
            m_Gain = 5;
            m_bBusy = false;
        }
        ~CamParam()
        {
            CloseCam();
        }

        //设置触发
        public void SetTriggerMode(bool bTrigger)
        {
            try
            {
                if (bTrigger)
                    HOperatorSet.SetFramegrabberParam(m_AcqHandle, "TriggerMode", "On");
                else
                    HOperatorSet.SetFramegrabberParam(m_AcqHandle, "TriggerMode", "Off");
            }
            catch (Exception ex)
            {
            }
        }
        //设置曝光
        public void SetExposureTime(HTuple Value)
        {
            try
            {
                if (m_AcqHandle > -1)
                    HOperatorSet.SetFramegrabberParam(m_AcqHandle, "ExposureTime", Value);
            }
            catch (Exception ex)
            {

            }
        }
        //设置增益
        public void SetGain(HTuple Value)
        {
            try
            {
                if (m_AcqHandle > -1)
                    HOperatorSet.SetFramegrabberParam(m_AcqHandle, "Gain", Value);
            }
            catch (Exception ex)
            {

            }
        }

        //关闭相机
        public void CloseCam()
        {
            try
            {
                //停止相机连续采集
                m_bBusy = false;
                if (m_AcqHandle > -1)
                {
                    HOperatorSet.CloseFramegrabber(m_AcqHandle);
                    m_AcqHandle = -1;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

