using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ptc.AspnetMvc.Models
{
    /// <remarks>登入後回傳給Mobile資訊</remarks>
    public class MobileInfo
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="message">回傳訊息</param>
        /// <param name="isSuccess">資訊正常否</param>
        /// <param name="data">回傳物件</param>
        public MobileInfo(string message = "", bool isSuccess = false, Object data = null)
        {
            Message = message;
            IsSuccess = isSuccess;
            ReturnData = data;
        }



        /// <summary>
        /// 資訊正常OR 不正常
        /// </summary>
        public bool IsSuccess { get; set; }


        private string _message;

        /// <summary>
        /// 要顯示的資訊
        /// </summary>
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                    return string.Empty;

                return _message.Replace("'", string.Empty);
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 回傳資料
        /// </summary>
        public Object ReturnData { get; set; }

        public void AddError(Exception ex)
        {
            IsSuccess = false;
            if (string.IsNullOrEmpty(ex.HelpLink))
                Message = ex.Message;
            else
                Message = ex.HelpLink;
        }

    }
}

