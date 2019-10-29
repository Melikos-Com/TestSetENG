using System.ComponentModel;

namespace Ptc.Seteng
{
    public enum RecordType
    {
        /// <summary>
        /// 到店時間(SI)
        /// </summary>
        [Description("實際到店時間")]
        ArriveDate = 1,

        /// <summary>
        /// 離店時間(SO)
        /// </summary>
        [Description("實際離店時間")]
        LeaveDate = 2,

        /// <summary>
        /// 到店時間(備查)
        /// </summary>
        [Description("備查到店時間")]
        ArriveDateTemp = 3,
        /// <summary>
        /// 離店時間(備查) 
        /// </summary>
        [Description("備查離店時間")]
        LeaveDateTemp = 4,

        /// <summary>
        /// 門市拒絕刷讀時間(到店)
        /// </summary>
        [Description("門市拒絕刷讀時間(到店)")]
        ArriveDateNotScan = 5,

        /// <summary>
        /// 門市拒絕刷讀時間(離店)
        /// </summary>
        [Description(" 門市拒絕刷讀時間(離店)")]
        LeaveDateNotScan = 6,

        /// <summary>
        /// 門市簽名時間
        /// </summary>
        [Description("門市簽名時間")]
        Signature = 7,

        /// <summary>
        /// 門市拒絕簽名時間
        /// </summary>
        [Description("門市拒絕簽名時間")]
        SignatureNot = 8,
    }
}
