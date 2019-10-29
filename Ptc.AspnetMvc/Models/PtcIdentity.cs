
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Ptc.AspnetMvc.Models
{
    [Serializable]
    public class PtcIdentity : IIdentity, IIdentityAuth
    {
        private IIdentity _orgIdentity;//登入基本識別
        private UserBase _currentUser;//目前登入使用者完整資訊
      

        private AuthNodeType? _groupActionOperation;
        private AuthItem _currentUserGroupAction;

        public PtcIdentity(
            IIdentity identity,
            UserBase currentUser,
            string GroupName,
            MenuNodeAttribute actionAuthDefine)
        {
            //原始ID
            _orgIdentity = identity;

            //目前使用者
            _currentUser = currentUser;

        
            //action沒設定，表示全部有效
            if (actionAuthDefine == null)
            {
                _groupActionOperation = AuthNodeType.All;
                return;
            }

            //取得使用者的定義
             if (_currentUser.UserPageAuth.ContainsKey(GroupName))
            {
                _currentUserGroupAction = _currentUser.UserPageAuth[GroupName];
                _groupActionOperation = _currentUserGroupAction.AuthType;

                //使用者沒有設定操作操限
                if (_currentUserGroupAction.AuthType == null)
                {
                    throw new Exception("沒設定功能的操作權限(CRUD)");
                }

                //操作權限不符合
                if (_currentUserGroupAction.AuthType.Value.HasFlag(actionAuthDefine.AuthType) == false)
                {
                    throw new Exception("功能的操作權限不符合(CRUD)");
                }
            }
            else
            {
                //找出使用者權限設定項目
                throw new Exception("沒該功能權限");
            }
        }

        /// <summary>
        /// 授權方式
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return _orgIdentity.AuthenticationType;
            }
        }

        /// <summary>
        /// 已認證
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return _orgIdentity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 登入使用者
        /// </summary>
        public string Name
        {
            get
            {
                return _orgIdentity.Name;
            }
        }

        /// <summary>
        /// 使用者資訊
        /// </summary>
        public UserBase currentUser
        {
            get
            {
                return _currentUser;
            }
        }

        /// <summary>
        /// 使用者權限清單
        /// </summary>
        public Dictionary<string, AuthItem> AuthItemColl
        {
            get
            {
                return _currentUser.UserPageAuth;
            }
            set
            {

            }
        }

        /// <summary>
        /// 本次使用的GroupAction
        /// </summary>
        public AuthItem CurrentGroupAction
        {
            get
            {
                return _currentUserGroupAction;
            }
            private set
            {
                _currentUserGroupAction = value;
            }
        }
    }
}