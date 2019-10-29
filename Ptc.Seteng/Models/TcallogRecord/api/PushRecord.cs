using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class PushRecord
    {
        public PushRecord()
        {

        }
        public PushRecord(TcallogRecord data)
        {
            this.PushRecard = data.RecordRemark;
            this.PushTime = data.RecordDatetime.ToString("yyyy/MM/dd HH:mm:ss");
        }
        //推播紀錄
        public string PushRecard { get; set; }
        //推播時間
        public string PushTime { get; set; }
    }
}