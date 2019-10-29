
using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TreeViewNode
    {

        private List<TreeViewNode> _node;

        private AuthNodeType? _authType;
       


        public TreeViewNode()
        {

        }
        public TreeViewNode(string id, string name, bool isTail, bool isCheckbox)
        {

            this.isTail = isTail;
            this.id = id;
            //this.selectable = true;
            this.state = new State();
            this.state.@checked = isCheckbox;
            this.name = name;
            this.text = name;
            if (this.isTail)
            {
                //this.selectable = true;
                this.icon = "fa fa-square-o";
                this.selectedIcon = "fa fa-check-square";
            }


        }


        public TreeViewNode(string id, string name, bool isTail, AuthNodeType? authType)
        {
            _authType = authType;
            this.isTail = isTail;
            this.id = id;
            this.name = name;
            this.text = name;
            //this.selectable = true;
            this.state = new State();


            //預設顯示checkbox圖型，父節點不顯示
            if (this.isTail)
            {
                this.state.@checked = true;

                if (_authType.HasValue)
                {
                    this.state.authCreate = _authType.Value.HasFlag(AuthNodeType.Create);
                    this.state.authDelete = _authType.Value.HasFlag(AuthNodeType.Delete);
                    this.state.authEdit = _authType.Value.HasFlag(AuthNodeType.Edit);
                    this.state.authExport = _authType.Value.HasFlag(AuthNodeType.Export);
                    this.state.authRead = _authType.Value.HasFlag(AuthNodeType.Read);
                    this.state.authReport = _authType.Value.HasFlag(AuthNodeType.Report);
                }


                System.Text.StringBuilder _textAppend = new System.Text.StringBuilder();
                _textAppend.Append("<a class='lbl' onclick='SelectAll(this)'>");
                _textAppend.Append(name);
                _textAppend.Append("</a>");
                _textAppend.Append(" <label class='pull-right'> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Create' class='ace' " + (this.state.authCreate ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>新增</span> ");
                _textAppend.Append(" </label> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Delete' class='ace' " + (this.state.authDelete ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>刪除</span> ");
                _textAppend.Append(" </label> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Edit' class='ace' " + (this.state.authEdit ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>修改</span> ");
                _textAppend.Append(" </label> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Read' class='ace' " + (this.state.authRead ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>查詢</span> ");
                _textAppend.Append(" </label> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Export' class='ace' " + (this.state.authExport ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>匯出</span> ");
                _textAppend.Append(" </label> ");

                _textAppend.Append(" <label class='pos-rel'> ");
                _textAppend.Append(" <input type='checkbox' name='Report' class='ace' " + (this.state.authReport ? "checked" : "") + " /> ");
                _textAppend.Append(" <span class='lbl'>報表</span> ");
                _textAppend.Append(" </label> ");
                _textAppend.Append(" </label> ");

                text = _textAppend.ToString();
            }

        }





        public string id { get; set; }

        public bool isTail { get; set; }

        public string name { get; set; }
        public string text { get; set; }
        public string href { get; set; }
        public bool selectable { get; set; }

        public string icon { get; set; }

        public string selectedIcon { get; set; }

        public List<string> tags { get; set; }

        public State state { get; set; }

        public AuthNodeType? AuthType { get; set; }

        public List<TreeViewNode> nodes
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;

                //有子節點表示本身為父節點
                if (_node.Count >= 1)
                {
                    this.icon = string.Empty;
                    this.selectedIcon = string.Empty;
                }
                else
                {
                    this.icon = "fa fa-square-o";
                    this.selectedIcon = "fa fa-check-square";

                }
            }
        }

        public class State
        {
            public bool @checked { get; set; }
            public bool expanded { get; set; }
            public bool selected { get; set; }

            public bool authCreate { get; set; }

            public bool authDelete { get; set; }

            public bool authEdit { get; set; }

            public bool authRead { get; set; }

            public bool authExport { get; set; }

            public bool authReport { get; set; }

        }
    }


}








