(function ($) {
    'use strict';

    if (typeof $ === 'undefined') { return; }

    /**
     * @description
     * 套用DataTable至<table>元素，結合查詢表單，自動以AJAX方式取得資料(傳送資料格式
     * 請見: https://datatables.net/manual/server-side )，且在每次查詢、排序、換頁等
     * 動作會產生歷史紀錄
     *
     * @param {object}  option             設定
     * @param {array}   option.colmDefs    欄位設定(參考:https://datatables.net/reference/option/columnDefs)
     * @param {string}  option.form        查詢表單(Form)的jQuery選擇語法
     * @param {func}    option.refill      回填表單資訊function
     * @param {func}    option.validate    送出查詢前驗證function
     * @param {object}  option.responsive  欄位顯示自動調整選項(參考:https://datatables.net/reference/option/responsive)
     * @returns {object} dataTables object
     */
    $.fn.PTCDataTable = function (option) {

        var historyLock = true;     // 確保歷史紀錄不會無端產生
        var elem,                   // 要套用DataTable的<table>
            $form,                  // 查詢表單jQuery物件
            $searchBtn;             // 查詢表單中的submit按鈕
        var action,                 // 遠端資料來源url
            method;                 // HTTP method (e.g. GET, POST ...)
        var table;                  // DataTable物件
        var duration = 7200;        // DataTable狀態保留時間長度(單位:秒)
        var submitHandler;

        if (this.length <= 0) { return; }

        if (typeof option !== 'object' || typeof option.form !== 'string') {
            return;
        }

        // 預設開啟自動隨版面寬度調整欄位顯示
        option.responsive || (option.responsive = true);

        elem = this.first();
        if (!elem.is('table')) {
            throw 'selector is not a <table>';
        }

        $form = $(option.form);
        if ($form.length <= 0 || !$form.is('form')) {
            throw '"form" is not a <form>';
        }

        $searchBtn = $form.find('button:submit');
        if ($searchBtn.length <= 0) {
            throw '"form" does not have submit button';
        }

        action = $form.attr('action');
        method = $form.attr('method');

        table = elem.DataTable(
        {

            lengthMenu: [[10, 20, 50, 100], [10, 20, 50, 100]],     // 設定項目選項
            pageLength: 20,                                         // 預設顯示的頁數
            searchDelay: 2000,                                      // 允許ajax timeout,單位:毫秒
            responsive: true,		                                // 開啟自動隨版面寬度調整欄位顯示
            autoWidth: false,		                                // 關閉自動欄寬自動設定
            paging: true,			                                // 開啟分頁列
            info: true,                                             // 顯示當前比數與總筆數
            searching: false,		                                // 關閉搜尋功能
            processing: false,		                                // 開啟"處理中"指示
            columnDefs: option.colmDefs,                            // 套用欄位設定
            language: dataTables_lang.zhTW,                         // 設定翻譯擋
            serverSide: true,                                       // 啟用遠端資料來源
            deferLoading: 0,                                        // 預設顯示的資料筆數
            preDrawCallback: function (settings) {
                //if (typeof option.validate === 'function' && !option.validate()) {
                //    $searchBtn.button('reset');
                //    return false;
                //}
            },
            ajax: {
                url: action,
                type: method,
                data: function (d) {
                    window.PTC.Loading(true); //add lodaing...
                    var extendData = option.singleCondition ? $form.serializeObject() : GetSheetCriteria($form);
                    return $.extend({}, d, {
                        'criteria': extendData
                    });
                }
            },
            stateSave: true,                                        // 啟用狀態儲存
            stateDuration: duration,                                // 狀態記錄保留一天
            stateSaveParams: function (settings, data) {
                // 新增自訂的狀態儲存參數(查詢條件)
                var extendData = option.singleCondition ? $form.serializeObject() : GetSheetCriteria($form);

                //data.criteria = $form.serializeObject();
                data.criteria = extendData
            },
            stateLoaded: function (settings, data) {
                // 回復表格狀態後回填查詢條件
                if (typeof option.refill === 'function') {
                    option.refill(data.criteria);
                }
                //history.pushState(data, '設備資料維護');
                //history.replaceState(data, '設備資料維護');
            },
            //針對每個列作尋覽
            fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {

                $(nRow).each(function () {

                    var rData = GetdtRowData($(this));

                    $(this).attr('title', $(this).innerText);

                    $(this).on("taphold", function () {

                        if (option.rowTabholdOK && option.rowTabholdNO) {

                            PTC.alertPop.feature({
                                title: "請選擇動作",
                                type: PTC.Enum.AlertPopType.warning,
                                data: rData,
                                okText: option.rowTabholdOKText || '確認',
                                noText: option.rowTabholdNOText || '取消',
                            },
                            option.rowTabholdOK,
                            option.rowTabholdNO)

                        }
                    });

                    if (option.trMethod && typeof option.trMethod === 'function') {
                        option.trMethod(rData, this);
                    }



                });

            },
            // 自訂footer樣式
            "sDom": "<'row'<'col-sm-6'l><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row no-margin'<'col-sm-5'i><'col-sm-7'p>>"
        });

        // 去掉dataTables套件自動加上的col-sm-12以使表格兩側對齊外框 
        elem.parent().removeClass();
        // 自動調整寬度
        $('.dataTables_wrapper').addClass("table-responsive");
        // 套用 float bar
        elem.floatTableFooter();

        // 當DataTable遠端要完資料
        table.on('xhr.dt', function (e, settings, json, xhr) {

            var state;

            $('.price-flag').html('');

            if (json !== null && json.error !== null) {

                PTC.alertPop.normal({
                    title: "查詢發生錯誤",
                    message: '原因:' +json.error,
                    type: PTC.Enum.AlertPopType.error
                });

            }
            else {
                state = table.ajax.params();    // 取得DataTable ajax時送出的資料

                if (!historyLock) {
                    // 當進行查詢、換頁等會產生新歷史紀錄時
                    //history.pushState(state, '設備資料維護');
                }
                else {
                    // 當初次載入或返回上/下頁等未產生新歷史紀錄時
                    historyLock = false;
                    //history.replaceState(state, '設備資料維護');
                    debugger;
           
                }
            }
            window.PTC.Loading(false);      //cancel lodaing...

            //if (json.extend) {

                //$('.price-flag').append(`<span class='select-item'>總成本:${json.extend.TotalCost}</span>`);

            //}

            $searchBtn.button('reset');     // 還原查詢按鈕
        });

        $.fn.dataTable.ext.errMode = 'none';

        //當遠端傳輸過程發生錯誤
        elem.on('error.dt', function (e, settings, techNote, errMsg) {

            window.PTC.Loading(false);      //cancel lodaing...

            console.error('An error has been reported by DataTables: ', errMsg);
        });

        KeyboardCtrl();

        // 當瀏覽器上/下一頁時...
        window.onpopstate = function (event) {
            var state = event.state;
            var page;
            var $prev;

            if (state) {
                // 回填查詢表單
                if (typeof option.refill === 'function') {
                    option.refill(state.criteria);
                }

                historyLock = true;

                page = state.start / state.length;


                // 回填頁數、單頁項目數量後更新內容(包含遠端重取資料)
                table.page(page).page.len(state.length).draw(false);
            }
        };

        submitHandler = function (e) {
            e.preventDefault();

            if (typeof option.validate === 'function' && !option.validate()) {
                return false;
            }

            // 停用查詢按鈕(避免查詢未完成前重複點擊)
            $searchBtn.button('loading');

            //判斷螢幕大小,做出不同反應
            InitScreenSizeAction();

            // DataTable更新內容(包含遠端重取資料)
            table.draw();
        };

        $form.submit(submitHandler);

        elem.on('destroy.dt', function (e, settings) {
            $form.off('submit', submitHandler);
            window.onpopstate = null;
        });

        //全選事件
        elem.find('#checkAll').change(function () {
            if ($('#checkAll').prop('checked')) {
                $('#resultTable input[name=cbx]').each(function () {
                    $(this).prop('checked', 'checked');
                });
            } else {
                $('#resultTable input[name=cbx]').each(function () {
                    $(this).prop('checked', '');
                });
            }
        })

        //放入資料列DOM,取得資料
        function GetdtRowData(rows) {

            var object = [];

            rows.each(function () {

                var rowData = {};

                var index = 0;

                $(this).find('td').each(function () {
                    var value = $(this).text();
                    var columName = option.colmDefs[index].id;
                    rowData[columName] = value;

                    index++;
                })
                object.push(rowData);
            });

            return object;
        }

        //鍵盤監聽
        function KeyboardCtrl() {
            $('html').keyup(function (e) {

                switch (e.keyCode) {
                    case 46:
                        console.log('delete');
                        break;
                    case 85 || 117:
                        console.log('update');
                        break;
                    case 97 || 65:
                        console.log('add');

                        break;
                }
            });
        }

        //匯出excel
        $('#dt-exportXml').click(function () {

            window.PTC.Loading(true);

            var obj = GetSheetCriteria($form);

            $.ajax({
                url: option.CreateXMLDataUrl,
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                traditional: true,
                cache: false,
                data: JSON.stringify(obj),
                statusCode: {
                    400: function () {

                        window.PTC.alertError("Sorry! We cannot process you request");

                        window.PTC.Loading(false);


                    },
                    200: function () {

                        window.location.href = option.CreateXMLFileUrl

                        window.PTC.Loading(false);
                    }
                }
            });


        });


        return table;

    };

    //取得多重條件,組合成陣列物件
    function GetSheetCriteria($form) {
        var data = [];

        $form.find('.tab-pane')
             .each(function () {
                 var obj = {};
                 var block = $(this).find('select')
                                    .each(function () {
                                        var name = $(this).attr('name');
                                        var value = $(this).val();

                                        if (value == 'null') { return; }

                                        obj[name] = value;
                                    })
                                    .end()
                                    .find("input[type='text']")
                                    .each(function () {

                                        var name = $(this).attr('name');
                                        var value = $(this).val();

                                        if (value == 'null') { return; }

                                        obj[name] = value;
                                    })
                                    .end()
                                    .find("input[type='radio']:checked")
                                    .each(function () {

                                        var name = $(this).attr('name');
                                        var value = $(this).val();

                                        if (value == 'null') { return; }

                                        obj[name] = value;
                                    })
                                    .end()
                                    .find("input[type='checkbox']:checked")
                                    .each(function () {

                                        var name = $(this).attr('name');
                                        var value = $(this).val();

                                        if (value == 'null') { return; }

                                        obj[name] = value;
                                    })
                                    .end()
                                    .find("input[type='hidden']")
                                    .each(function () {

                                        var name = $(this).attr('name');
                                        var value = $(this).val();

                                        if (value == 'null') { return; }

                                        obj[name] = value;
                                    })
                 data.push(obj);
             });


        return data;

    }

    //根據螢幕不同尺寸,設定方法
    function InitScreenSizeAction() {

        if ($(window).width() < 768) {

            if ($('.ptc-conditions').is(":visible")) {
                $('.ptc-dismiss').click()
            }
        }
        else if ($(window).width() >= 768 && $(window).width() <= 992) {
            // do something for medium screens
        }
        else if ($(window).width() > 992 && $(window).width() <= 1200) {
            // do something for big screens
        }
        else {
            // do something for huge screens
        }



    }




})(jQuery);