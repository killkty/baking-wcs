using GlobalsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flow
{
    public class FlowWaterOut
    {
        int tim_WaterOut = 30;     //3秒
        public void _tim_WaterOut()  // 
        {
            for (int i = 0; i < tim_WaterOut; i++)
            {
                if (Omron.STOP == true)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }

        public void ThreadProcFlow(object str)
        {
            do
            {
                WaterOutCall();
                _tim_WaterOut();
            }
            while (Omron.STOP == false);

        }

        public void WaterOutCall()
        {
            //水含量上传
            //1, 查询水含量电脑水含量电芯条码  
            //2，保存一个本地水含量电芯条码
            //3，修改水含量电脑电芯允许查询标志 为不可查询
            //4，通过水含量电芯 查询本地数据库的3个托盘条码
            //5, 通过托盘条码查询出所有的电芯条码
            //6，将

            string SFC = "";
            bool hotP_bool = false;
            string[] hotP_ss = new string[4];

            string status = "";
            string message_web = "";
            WebMesH.WebHelper.Web_WaterSfc(SFC, ref status, ref message_web, hotP_bool, hotP_ss);

            //lb_code.Text = status.ToString();
            //lb_message.Text = message_web;

        }




    }
}
