using cn.jpush.api;
using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.push.mode;
using Ptc.Logger;
using Ptc.Logger.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    /// <summary>
    /// 極光推播
    /// </summary>
    public class JPushRepository : IPushRepository
    {
        private readonly ISystemLog _logger;

        public JPushRepository(ISystemLog Logger)
        {
            _logger = Logger;
        }

        /// <summary>
        /// 依照個人推播
        /// </summary>
        /// <param name="regIds"></param>
        /// <returns></returns>
        public Boolean Push(string msg, IDictionary<string, string> extras = null, params string[] regIds)
        {
            if (regIds == null || regIds.Count() == 0)
            {
                _logger.Info($"[INFO]=>實際執行推播時,並未給入RegID");
                return false;
            }

            var notification = new Notification();
            notification.AndroidNotification = new cn.jpush.api.push.notification.AndroidNotification();
            notification.AndroidNotification.setAlert(msg)
                                            .setTitle(msg);


            notification.IosNotification = new cn.jpush.api.push.notification.IosNotification();
            notification.IosNotification.setAlert(msg);
            notification.IosNotification.incrBadge(0);
            notification.IosNotification.setSound("default");

            extras?.ForEach(x =>
            {
                notification.AndroidNotification.AddExtra(x.Key, x.Value);
                notification.IosNotification.AddExtra(x.Key, x.Value);
            });


            PushPayload pushPayload = new PushPayload()
            {
                platform = Platform.all(),
                audience = Audience.s_registrationId(regIds),
                notification = notification
            };
            //apns_production是設定IOS的推播管道，預設false->推到開發環境，true->推播至正式環境
            //pushPayload.options.apns_production = true;
            //20180302 wei modify 改為從Web.config檔中判斷
            pushPayload.options.apns_production = Convert.ToBoolean(ServerProfile.GetInstance().PushToOfficial);
            return this.Excute(pushPayload);
        }
        /// <summary>
        /// 執行推播
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        private Boolean Excute(PushPayload payload)
        {
            try
            {
                JPushClient client = new JPushClient(
                    ServerProfile.GetInstance().JPUSH_APP_KEY,
                    ServerProfile.GetInstance().JPUSH_APP_SECRET
                );

                var result = client.SendPush(payload);
            }
            catch (APIRequestException ex)
            {
                _logger.Info("Error response from JPush server. Should review and fix it. ");
                _logger.Info("HTTP Status: " + ex.Status);
                _logger.Info("Error Code: " + ex.ErrorCode);
                _logger.Info("Error Message: " + ex.ErrorMessage);
                return false;
            }
            catch (APIConnectionException ex)
            {
                _logger.Info(ex.Message);
                return false;
            }

            return true;
        }

    }
}
