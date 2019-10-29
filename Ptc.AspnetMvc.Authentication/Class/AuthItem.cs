using System;

namespace Ptc.AspnetMvc.Authentication
{
    [Serializable]
    public class AuthItem
    {
        /// <summary>
        /// 權限名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 權限
        /// </summary>
        public Nullable<AuthNodeType> AuthType { get; set; }
        /// <summary>
        /// 拒絕
        /// </summary>
        public bool isDeny { get; set; }
        /// <summary>
        /// 比對用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as AuthItem;
            if (other == null)
            {
                return false;
            }
            return this.GroupName == other.GroupName ;
        }

        public override int GetHashCode()
        {
            return 0;//this.GroupName.GetHashCode();
        }
    }
}