using System;

namespace Model
{
    public class Model_SFC_Water
    {
        //  1 ID, 2 拉线号, 3 A/B线,4 炉腔编号 ,5 托盘1编号,6 托盘2编号
        // ,7 托盘3编号,8 测试条码,9 BK开始时间,10 BK结束时间,11 BK时长,
        //  12 BK温度报警,13 BK真空报警,14 上传数据库时间,15 水含量值,16 水含量结果,17 上传数据库标志,18 上传Mes时间,19 上传Mes标志,


        private int _ID;             //1,ID
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        private string _BkLine;      //2,拉线号
        private string _BkAB;        //3,A/B线
        private string _BkNumber;    //4,炉腔编号
        private string _Tray1;       //5,托盘1编号

        /// <summary>
        /// 拉线号
        /// </summary>
        public string BkLine
        {
            set { _BkLine = value; }
            get { return _BkLine; }
        }

        /// <summary>
        /// A/B线
        /// </summary>
        public string BkAB
        {
            set { _BkAB = value; }
            get { return _BkAB; }
        }

        /// <summary>
        /// 炉腔编号
        /// </summary>
        public string BkNumber
        {
            set { _BkNumber = value; }
            get { return _BkNumber; }
        }

        /// <summary>
        /// 托盘1编号
        /// </summary>
        public string Tray1
        {
            set { _Tray1 = value; }
            get { return _Tray1; }
        }

        private string _Tray2;       //6,托盘2编号
        private string _Tray3;       //7,托盘3编号
        private string _SfcTest;     //8,测试条码
        private string _BkTimeStart; //9,BK开始时间

        /// <summary>
        /// 托盘2编号
        /// </summary>
        public string Tray2
        {
            set { _Tray2 = value; }
            get { return _Tray2; }
        }

        /// <summary>
        /// 托盘3编号
        /// </summary>
        public string Tray3
        {
            set { _Tray3 = value; }
            get { return _Tray3; }
        }

        /// <summary>
        /// 测试条码
        /// </summary>
        public string SfcTest
        {
            set { _SfcTest = value; }
            get { return _SfcTest; }
        }

        /// <summary>
        /// BK开始时间
        /// </summary>
        public string BkTimeStart
        {
            set { _BkTimeStart = value; }
            get { return _BkTimeStart; }
        }

        private string _BkTimeEnd;   //10,BK结束时间   
        private string _BkTime;      //11,BK时长
        private string _TAlarm;      //12,BK温度报警
        private string _KPaAlarm;    //13,BK真空报警
        /// <summary>
        /// BK结束时间
        /// </summary>
        public string BkTimeEnd
        {
            set { _BkTimeEnd = value; }
            get { return _BkTimeEnd; }
        }

        /// <summary>
        /// BK时长
        /// </summary>
        public string BkTime
        {
            set { _BkTime = value; }
            get { return _BkTime; }
        }

        /// <summary>
        /// BK温度报警
        /// </summary>
        public string TAlarm
        {
            set { _TAlarm = value; }
            get { return _TAlarm; }
        }

        /// <summary>
        /// BK真空报警
        /// </summary>
        public string KPaAlarm
        {
            set { _KPaAlarm = value; }
            get { return _KPaAlarm; }
        }

        private string _DataTime; //14,上传数据库时间
        private string _CWCValue;    //15,水含量值
        private string _CWCResult;   //16,水含量结果
        private string _DataFlag;    //17,上传数据库标志  "false", "true",""

        /// <summary>
        /// 上传数据库时间
        /// </summary>
        public string DataTime
        {
            set { _DataTime = value; }
            get { return _DataTime; }
        }

        /// <summary>
        /// 水含量值
        /// </summary>
        public string CWCValue
        {
            set { _CWCValue = value; }
            get { return _CWCValue; }
        }

        /// <summary>
        /// 水含量结果
        /// </summary>
        public string CWCResult
        {
            set { _CWCResult = value; }
            get { return _CWCResult; }
        }

        /// <summary>
        /// 上传数据库标志
        /// </summary>
        public string DataFlag
        {
            set { _DataFlag = value; }
            get { return _DataFlag; }
        }

        private string _MesTime;     //18,上传Mes时间
        private string _MesFlag;     //19,上传Mes标志  "OK" "NG"  ""
        private string _BkResource;  //20,设备资源号Resource

        /// <summary>
        /// 上传Mes时间
        /// </summary>
        public string MesTime
        {
            set { _MesTime = value; }
            get { return _MesTime; }
        }

        /// <summary>
        /// 上传Mes标志
        /// </summary>
        public string MesFlag
        {
            set { _MesFlag = value; }
            get { return _MesFlag; }
        }

        /// <summary>
        /// 设备资源号Resource
        /// </summary>
        public string BkResource
        {
            set { _BkResource = value; }
            get { return _BkResource; }
        }


    }
}
