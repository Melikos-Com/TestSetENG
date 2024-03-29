;(function($){
/**
 * jqGrid Chinese Translation
 * 咖啡兔 yanhonglei@gmail.com
 * http://www.kafeitu.me 
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
    defaults : {
        recordtext: "{0} - {1}\u3000共 {2} 條", // 共字前是全角空格
        emptyrecords: "無資料",
        loadtext: "讀取中...",
        pgtext : " {0} 共 {1} 頁"
    },
    search : {
        caption: "查詢...",
        Find: "找尋",
        Reset: "重置",
        odata: [{ oper:'eq', text:'等於\u3000\u3000'},{ oper:'ne', text:'不等\u3000\u3000'},{ oper:'lt', text:'小於\u3000\u3000'},{ oper:'le', text:'小於等於'},{ oper:'gt', text:'大於\u3000\u3000'},{ oper:'ge', text:'大於等於'},{ oper:'bw', text:'开始於'},{ oper:'bn', text:'不开始於'},{ oper:'in', text:'属於\u3000\u3000'},{ oper:'ni', text:'不属於'},{ oper:'ew', text:'结束於'},{ oper:'en', text:'不结束於'},{ oper:'cn', text:'包含\u3000\u3000'},{ oper:'nc', text:'不包含'},{ oper:'nu', text:'不存在'},{ oper:'nn', text:'存在'}],
        groupOps: [ { op: "AND", text: "所有" },    { op: "OR",  text: "任一" } ],
		operandTitle : "Click to select search operation.",
		resetTitle : "Reset Search Value"
    },
    edit : {
        addCaption: "添加紀錄",
        editCaption: "編輯紀錄",
        bSubmit: "送出",
        bCancel: "取消",
        bClose: "關閉",
        saveData: "資料已改變，是否儲檔？",
        bYes : "是",
        bNo : "否",
        bExit : "取消",
        msg: {
            required:"此字段為必填",
            number:"請輸入有效數字",
            minValue:"數值必須大於等於 ",
            maxValue: "數值必須小於等於 ",
            email: "這不是有效的e-mail地址",
            integer: "請輸入有效整數",
            date: "請輸入有效時間",
            url: "無效網址。前方必須為 ('http://' 或 'https://')",
            nodefined : " 未定義！",
            novalue : " 需要返回值！",
            customarray : "自訂函數需要返回值！",
            customfcheck: "必須有自訂函數!"
        }
    },
    view : {
        caption: "查看紀錄",
        bClose: "關閉"
    },
    del : {
        caption: "刪除",
        msg: "刪除所選紀錄？",
        bSubmit: "刪除",
        bCancel: "取消"
    },
    nav : {
        edittext: "",
        edittitle: "編輯所選紀錄",
        addtext:"",
        addtitle: "添加新紀錄",
        deltext: "",
        deltitle: "刪除所選紀錄",
        searchtext: "",
        searchtitle: "查找",
        refreshtext: "",
        refreshtitle: "刷新表格",
        alertcap: "注意",
        alerttext: "請選擇紀錄",
        viewtext: "",
        viewtitle: "查看所選紀錄"
    },
    col : {
        caption: "選擇列",
        bSubmit: "確定",
        bCancel: "取消"
    },
    errors : {
        errcap : "錯誤",
        nourl : "没有設置url",
        norecords: "没有要處理的紀錄",
        model : "colNames 和 colModel 長度不等！"
    },
    formatter : {
        integer : {thousandsSeparator: ",", defaultValue: '0'},
        number : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, defaultValue: '0.00'},
        currency : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, prefix: "", suffix:"", defaultValue: '0.00'},
        date : {
            dayNames:   [
                "日", "一", "二", "三", "四", "五", "六",
                "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六",
            ],
            monthNames: [
                "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二",
                "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"
            ],
            AmPm : ["am","pm","上午","下午"],
            S: function (j) {return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th';},
            srcformat: 'Y-m-d',
            newformat: 'Y-m-d',
            parseRe : /[#%\\\/:_;.,\t\s-]/,
            masks : {
                // see http://php.net/manual/en/function.date.php for PHP format used in jqGrid
                // and see http://docs.jquery.com/UI/Datepicker/formatDate
                // and https://github.com/jquery/globalize#dates for alternative formats used frequently
                // one can find on https://github.com/jquery/globalize/tree/master/lib/cultures many
                // information about date, time, numbers and currency formats used in different countries
                // one should just convert the information in PHP format
                ISO8601Long:"Y-m-d H:i:s",
                ISO8601Short:"Y-m-d",
                // short date:
                //    n - Numeric representation of a month, without leading zeros
                //    j - Day of the month without leading zeros
                //    Y - A full numeric representation of a year, 4 digits
                // example: 3/1/2012 which means 1 March 2012
                ShortDate: "n/j/Y", // in jQuery UI Datepicker: "M/d/yyyy"
                // long date:
                //    l - A full textual representation of the day of the week
                //    F - A full textual representation of a month
                //    d - Day of the month, 2 digits with leading zeros
                //    Y - A full numeric representation of a year, 4 digits
                LongDate: "l, F d, Y", // in jQuery UI Datepicker: "dddd, MMMM dd, yyyy"
                // long date with long time:
                //    l - A full textual representation of the day of the week
                //    F - A full textual representation of a month
                //    d - Day of the month, 2 digits with leading zeros
                //    Y - A full numeric representation of a year, 4 digits
                //    g - 12-hour format of an hour without leading zeros
                //    i - Minutes with leading zeros
                //    s - Seconds, with leading zeros
                //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                FullDateTime: "l, F d, Y g:i:s A", // in jQuery UI Datepicker: "dddd, MMMM dd, yyyy h:mm:ss tt"
                // month day:
                //    F - A full textual representation of a month
                //    d - Day of the month, 2 digits with leading zeros
                MonthDay: "F d", // in jQuery UI Datepicker: "MMMM dd"
                // short time (without seconds)
                //    g - 12-hour format of an hour without leading zeros
                //    i - Minutes with leading zeros
                //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                ShortTime: "g:i A", // in jQuery UI Datepicker: "h:mm tt"
                // long time (with seconds)
                //    g - 12-hour format of an hour without leading zeros
                //    i - Minutes with leading zeros
                //    s - Seconds, with leading zeros
                //    A - Uppercase Ante meridiem and Post meridiem (AM or PM)
                LongTime: "g:i:s A", // in jQuery UI Datepicker: "h:mm:ss tt"
                SortableDateTime: "Y-m-d\\TH:i:s",
                UniversalSortableDateTime: "Y-m-d H:i:sO",
                // month with year
                //    Y - A full numeric representation of a year, 4 digits
                //    F - A full textual representation of a month
                YearMonth: "F, Y" // in jQuery UI Datepicker: "MMMM, yyyy"
            },
            reformatAfterEdit : false
        },
        baseLinkUrl: '',
        showAction: '',
        target: '',
        checkbox : {disabled:true},
        idName : 'id'
    }
});
})(jQuery);
