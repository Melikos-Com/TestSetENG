using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication
{
    public class AuthData
    {

        public AuthData()
        {
            DataRangeList = new List<AuthData>();
        }

        public AuthData(string id, string parentID, string Name, int level = 0, bool isDeny = false)
        {
            DataRangeList = new List<AuthData>();
            this.isDeny = isDeny;
            this.id = id;
            this.parentID = parentID;
            this.Name = Name;
            this.level = level;
            this.isDeny = isDeny;
        }


        public string id { get; set; }

        public string parentID { get; set; }
        public List<AuthData> DataRangeList { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 目前階層
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 拒絕
        /// </summary>
        public bool isDeny { get; set; }

        public bool HasChild
        {
            get
            {
                return this.DataRangeList == null ? false : this.DataRangeList.Count >= 1;
            }
        }


        public List<AuthData> ToList()
        {
            List<AuthData> retColl = new List<AuthData>();

            retColl.Add(this);

            foreach (AuthData item in this.DataRangeList)
            {
                retColl.Add(item);
                var ret = RecursiveDataRangeChild(item, null);
                if (ret != null && ret.Count>=1)
                {
                    retColl.AddRange(ret);
                }
            }

            return retColl;
        }


        private List<AuthData> RecursiveDataRangeChild(
            AuthData currentDataRange,
            AuthData parentDataRange)
        {
            //準備回傳的
            List<AuthData> retColl = new List<AuthData>();


            if (currentDataRange.DataRangeList != null && currentDataRange.DataRangeList.Count >= 1)
            {
                //底下還有資料
                foreach (AuthData childItem in currentDataRange.DataRangeList)
                {

                    var childRetColl = RecursiveDataRangeChild(childItem, currentDataRange);
                    if (childRetColl != null)
                        retColl.AddRange(childRetColl);
                }
            }
            else
            {
                return new List<AuthData>();
            }


            return retColl;

        }


        //public AuthData GetRoundDataRange(List<AuthData> allDataRange)
        //{

        //    //AuthData currentAuthData = allDataRange.First(x => x.id == this.id);

        //    //var ret = RecursiveDataRangeChild(currentAuthData, null, this.DataRangeList, 0);

        //    //return ret;


        //    ////整理後合併的權限





        //    int level = 0;
        //    foreach (AuthData authItem in this.DataRangeList)
        //    {
        //        AuthData currentAuthData = allDataRange.First(x => x.id == authItem.id);

        //        var currentAuthItem = RecursiveDataRangeChild(currentAuthData, null, this.DataRangeList, level + 1);
        //        if (currentAuthItem != null)
        //            RoundDataRange.Add(currentAuthItem);
        //    }

        //    //整理結果
        //    return RoundDataRange;
        //}

        //private AuthData RecursiveDataRangeChild(
        //    AuthData currentDataRange,
        //    AuthData parentDataRange,
        //    List<AuthData> userAuthItemColl,
        //    int level)
        //{
        //    //準備回傳的

        //    if (userAuthItemColl.Any(x => x.id == currentDataRange.id) == false)
        //        return null;

        //    AuthData currentAuthItem = new AuthData(
        //        currentDataRange.id,
        //        parentDataRange == null ? string.Empty : parentDataRange.id, currentDataRange.Name, level);


        //    if (currentDataRange.DataRangeList != null && currentDataRange.DataRangeList.Count >= 1)
        //    {
        //        //底下還有資料
        //        foreach (AuthData childItem in currentDataRange.DataRangeList)
        //        {
        //            List<AuthData> userSettingRange = new List<AuthData>();
        //            var userSetting = userAuthItemColl.FirstOrDefault(x => x.id == currentDataRange.id);
        //            if (userSetting.DataRangeList != null)
        //                userSettingRange.AddRange(userSetting.DataRangeList);

        //            var ret = RecursiveDataRangeChild(childItem, currentDataRange, userSettingRange, level + 1);
        //            if (ret != null)
        //                currentAuthItem.DataRangeList.Add(ret);



        //        }

        //        //全選就不記錄
        //        if (currentAuthItem.DataRangeList.Count == currentDataRange.DataRangeList.Count)
        //        {
        //            bool isClear = true;
        //            foreach (var item in currentAuthItem.DataRangeList)
        //            {
        //                if (item.DataRangeList?.Count() >= 1)
        //                    isClear = false;

        //            }
        //            if (isClear)
        //                currentAuthItem.DataRangeList = new List<AuthData>();
        //        }
        //        return currentAuthItem;
        //    }
        //    else
        //    {
        //        return currentAuthItem;
        //    }

        //}


        //public List<string> toGroupLevelString(int GroupLevenl)
        //{
        //    //整理後合併的權限
        //    List<string> RoundDataRange = new List<string>();

        //    int level = 0;
        //    foreach (AuthData authItem in this.DataRangeList)
        //    {
        //        var currentAuthItem = RecursiveDataRangeString(authItem, null, level + 1);
        //        if (currentAuthItem != null)
        //            RoundDataRange.AddRange(currentAuthItem);
        //    }

        //    //整理結果
        //    return RoundDataRange;

        //}

        public List<DataRangeString> toGroupLevelString()
        {
            List<DataRangeString> ret = new List<DataRangeString>();
            int level = 0;
            string authString = toGroupLevelString(this, this.id, level + 1);

            if (string.IsNullOrEmpty(authString))
            {
                authString = this.id;
            }

            ret.Add(new DataRangeString(level, authString, this.id));

            return ret;
        }


        public string toGroupLevelString(
         AuthData currentDataRange,
         string rootId,
         int level)
        {
            if (currentDataRange.DataRangeList == null || currentDataRange.DataRangeList.Count == 0)
                return string.Empty;

            var retColl = new List<DataRangeString>();

            //底下還有資料

            string authString = string.Empty;
            foreach (AuthData childItem in currentDataRange.DataRangeList)
            {
                if (childItem.DataRangeList == null || childItem.DataRangeList.Count == 0)
                {
                    authString += childItem.id + ",";
                }
                else
                {
                    authString += toGroupLevelString(childItem, rootId, level + 1);



                    //if (ret.Count == 0)
                    //{
                    //    if (childItem.DataRangeList.Count >= 1)
                    //    {
                    //        retColl.Add(new DataRangeString(level, string.Join(",", childItem.DataRangeList.Select(x => x.id)), rootId));

                    //    }
                    //}
                    //else
                    //{
                    //    retColl.AddRange(ret);
                    //}
                }
            }


            //準備回傳的
            return authString;
        }


        /// <summary>
        /// 比對用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as AuthData;
            if (other == null)
            {
                return false;
            }
            return this.id == other.id;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
