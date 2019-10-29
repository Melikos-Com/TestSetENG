(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


    window.PTC = window.PTC || {};
    var $pageSize = 10;          //頁面長度
    var $delayTime = 250;        //傳輸daley時間(ms)
    var $defaultPageIndex = 0;   //初始頁面數
    var $dataType = 'JSON';      //資料回傳格式
    var $transType = 'POST';     //傳輸格式
    var $cache = false;          //暫存


    /*

            PAMAMETER:
     ------------------------       
        elementName
        url
        placeholder
        getTransData
           
    */
    window.PTC.HierarchyAjaxSelect2 = function (options) {
    
        Restructuring(options).forEach(function (val, index, array) {

            Initial(val, GetByIndex(index, array, 1));

        })

    }

    window.PTC.SingleSelect2 = function (options) {

        var elementName = options.name;

        var placeholder = options.placeholder;
        var url = options.url;

        $(elementName).select2({
            width: '100%',
            allowClear: true,
            minimumInputLength: 0,
            placeholder: placeholder,
            ajax: {
                url: url,
                type: $transType,
                cache: $cache,
                delay: $delayTime,
                dataType: $dataType,
                data: function (param) {

                    var object = options.func();
                    object.page = param.page;
                    object.keyword = param.term;
                    object.rows = $pageSize;


                    return object;

                },
                success: function (data) {


                },
                initSelection: function (element, callback) {

                    callback({ "text": elementText, "id": elementText });
                },
                processResults: function (data, param) {

                    param.page = param.page || $defaultPageIndex;

                    data.items = data.items.map(function (item) {
                        return {
                            id: item.id,
                            text: item.text,
                            addition: item.data
                        };
                    });

                    return {
                        results: data.items,
                        pagination: {
                            more: (param.page * $pageSize) < data.totalCount
                        }
                    }
                }
            }


        });


    }

    //物件重組
    function Restructuring(options) {

        var newer = [];

        if (!options || !Array.isArray(options)) { throw 'Restructuring error , options is not array.' };

        options.forEach(function (val, index, array) {

            newer.push({
                index: index,
                elementName: val.elementName,
                select2Param: {
                    width: '100%',
                    allowClear: true,
                    minimumInputLength: 0,
                    placeholder: val.placeholder,
                    ajax: CombinationAjaxParam(val, GetByIndex(index, array, 1)),
                },
                changeCallBack: {

                    getJson: GetByIndex(index, array, 1) ? GetJson(val, GetByIndex(index, array, 1)) : function () { },

                }
            });
        });

        return newer;

    }

    //初始化事件、UI
    function Initial(newerItem, prev) {

        $(newerItem.elementName).select2(newerItem.select2Param);
        $(newerItem.elementName).change(function () {

            var value = $(this).val();

            if (!value && prev) {
                Unlock(prev.elementName);
                return;
            }

            newerItem.changeCallBack.getJson();

        });

    }

    //組合AJAX物件
    function CombinationAjaxParam(item, prev) {

        if (!item.url || typeof item.url !== 'string')
        { throw 'CombinationAjaxParam error , url has not value or not string. ' }

        return {
            url: item.url,
            type: $transType,
            cache: $cache,
            delay: $delayTime,
            dataType: $dataType,
            data: function (param) {

                var object = item.getTransData(IsReverse(item, prev, 'CombinationAjaxParam'));
                object.page = param.page;
                object.keyword = param.term;
                object.rows = $pageSize;


                return object;

            },
            success: function (data) {


            },
            processResults: function (data, param) {

                param.page = param.page || $defaultPageIndex;

                data.items = data.items.map(function (item) {
                    return {
                        id: item.id,
                        text: item.text,
                        addition: item.data
                    };
                });

                return {
                    results: data.items,
                    pagination: {
                        more: (param.page * $pageSize) < data.totalCount
                    }
                }
            }


        }

    }

    //回填上層UI
    function ReverseFillUI(data, prev) {

        if (!data ||
            !data.items ||
            !Array.isArray(data.items) ||
            data.items.length == 0) {

            //TODO:因為現況沒有設備分類三,之後得討論是否加上這段
            Lock(prev.elementName, {
                data: {
                    id: 'null',
                    text: "找不到對應的資訊",
                    addition: null,
                }
            });

            return;

        }

        Lock(prev.elementName, {
            data: {
                id: data.items[0].id,
                text: data.items[0].text,
                addition: data.items[0].data,
            }
        });


    }

    //找尋資料來源
    function GetJson(item, prev) {

        return function () {

            $.ajax({
                type: $transType,
                dataType: $dataType,
                url: prev.url,
                data: prev.getTransData(IsReverse(item, prev, 'GetJson')),
                success: function (data) {

                    ReverseFillUI(data, prev)

                },
                error: function (error) { }
            });
        }


    }

    //鎖定UI
    function Lock(element, value) {

        $(element).select2("trigger", "select", value);
        $(element).prop('disabled', true);
        return;

    }

    //解鎖UI
    function Unlock(element) {
        $(element).prop('disabled', false);
        return;
    }

    //取得前/後的UI資訊
    function GetByIndex(index, array, minus) {

        if (!array || !Array.isArray(array))
        { throw 'GetByIndex error , input is null or  input is not array' }


        if (!array[index - minus])
        { return false }

        return array[index - minus];

    }

    //判斷是正向/反向
    function IsReverse(item, prev, funcName) {

        if (!prev) { return false; }

        //if (!$(prev.elementName).val())
        //{ return true; }


        if (funcName == 'GetJson') {
            if (!$(prev.elementName).val() || $(item.elementName).val())
            { return true; }

        } else {
            if (!$(prev.elementName).val())
            { return true; }
        }


        return false;

    }



    $.fn.InitSelect2Data = function (option) {

        var $elem = $(this);

        $.ajax({
            type: $transType,
            dataType: $dataType,
            url: option.url,
            data: option.data,
            success: function (data) {

                $elem.select2("trigger", "select", { data: { id: data.items[0].id, text: data.items[0].text, addition: data.items[0].data } });
                

            },
        });
    }


})(jQuery);