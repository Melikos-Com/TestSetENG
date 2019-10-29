(function ($) {
    'use strict';

    window.PTC = window.PTC || {};

    window.PTC.Enum = {};

    //設備分類
    window.PTC.Enum.AssetKindType =
    {
        LargeCategory  : 1,             //大分類
        MiddleCategory : 2,             //中分類
        SmallCategory  : 3,             //小分類
    }

    window.PTC.Enum.CallLevelType = {

        general: 1,             //一般
        warning: 2,             //緊急
        keepAging1: 3,          //保留時效1
        keepAging2: 4,          //保留時效2

    }

      window.PTC.Enum.CaseCancelType = {

        General: 0,        //一般
        CC: 1,             //CC誤叫修
        Store: 2,          //門市誤叫修
     

    }
  
    //提示彈出視窗的圖示
    window.PTC.Enum.AlertPopType = {

        question: 'question',           //問號
        error   : 'error',              //叉叉
        success : 'success',            //勾勾    
        warning : 'warning',            //驚嘆號

    }



})(jQuery);