(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }

    var confirmButtonColor = '#3085d6';
    var cancelButtonColor = '#d33';
    var confirmButtonText = "確定";
    var cancelButtonText = "取消"

    window.PTC = window.PTC || {};

    window.PTC.alertPop = window.PTC.alertPop || {};

    window.PTC.alertError = function (message) {
        var $elem = $('<div role="alert" class="alert alert-danger alert-dismissible fade in">' +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '<span class="glyphicon glyphicon-remove-sign" aria-hidden="true"></span>' +
            '<span class="sr-only">Error:</span>' +
            '<strong>ERROR</strong> ' + message +
            '</div>');

        show($elem);
    };

    window.PTC.alertWarning = function (message) {
        var $elem = $('<div role="alert" class="alert alert-warning alert-dismissible fade in">' +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>' +
            '<span class="sr-only">Warning:</span>' +
            '<strong>警告</strong> ' + message +
            '</div>');

        show($elem);
    };

    window.PTC.alertInfo = function (message) {
        var $elem = $('<div role="alert" class=" alert alert-info alert-dismissible fade in">' +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '<strong>一般</strong> ' + message +
            '</div>');

        show($elem);
    };

    window.PTC.alertNotice = function (message) {
        var $elem = $('<div role="alert" class="alert alert-warning alert-dismissible fade in">' +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '<strong>注意</strong> ' + message +
            '</div>');

        show($elem);
    };

    window.PTC.alertPrompt = function (message, confirm, decline) {
        var $alert = $('<div role="alert" class="alert alert-success alert-dismissible fade in"></div>');
        var $dismiss = $('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
        var $msg = $('<p><span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span>' +
            '<span class="sr-only">Prompt:</span>' +
            '<strong>確認</strong> ' + message +
            '</p>');
        var $btns = $('<p>');
        var $confirm = $('<button class="btn btn-sm btn-success">是</button>');
        $confirm.click(function () {
            confirm();
            $alert.remove();
        });
        var $decline = $('<button class="btn btn-sm">否</button>');
        $decline.click(function () {
            decline();
            $alert.remove();
        });

        $btns.append($confirm);
        $btns.append($decline);
        $alert.append($dismiss);
        $alert.append($msg);
        $alert.append($btns);

        show($alert);
    };

    window.PTC.alertSuccess = function (message, confirm) {
        var $alert = $('<div role="alert" class="alert alert-success alert-dismissible fade in"></div>');
        var $dismiss = $('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
        var $msg = $('<p>' +
             message +
            '</p>');
        var $btns = $('<p>');
        var $confirm = $('<button class="btn btn-sm btn-success text-left">確認</button>');
        $confirm.click(function () {
            confirm();
            $alert.remove();
        });


        $btns.append($confirm);
        $alert.append($dismiss);
        $alert.append($msg);
        $alert.append($btns);

        show($alert);
    };

    window.PTC.alertPop.basic = function (options) {

        swal(options.message);
    }

    window.PTC.alertPop.normal = function (options) {

        var $title = options.title;

        var $message = options.message;

        var $type = options.type;


        swal($title, $message, $type);
    }

    window.PTC.alertPop.timer = function (options, confirm, decline) {

        var $title = options.title;
        var $message = options.message;
        var $type = options.type;
        var $timer = options.timer;
        var $data = options.data;

        swal({
            title: $title,
            text: $message,
            type: $type,
            timer: $timer,
        }).then(
            function () {

                confirm($data);
            },
            function (dismiss) {
                //if (dismiss === 'timer') //<==condition dissmiss from timer;

                decline($data);
            }
        )
    }

    window.PTC.alertPop.feature = function (options, confirm, decline) {

        var $title = options.title;
        var $message = options.message;
        var $type = options.type;
        var $data = options.data;
        var $okText = options.okText || confirmButtonText;
        var $noText = options.noText || cancelButtonText;

        swal({
            title: $title,
            text: $message,
            type: $type,
            showLoaderOnConfirm: true,
            showCancelButton: true,
            confirmButtonColor: confirmButtonColor,
            cancelButtonColor: cancelButtonColor,
            confirmButtonText: $okText,
            cancelButtonText: $noText,
        }).then(function () {

            confirm($data);


        }, function (dismiss) {
            // dismiss can be 'cancel', 'overlay',
            // 'close', and 'timer'
            if (dismiss === 'cancel') {
                decline($data);
            }
        })
    }

    window.PTC.alertPop.promise = function (options, confirm, decline) {


        var $title = options.title;
        var $message = options.message;
        var $type = options.type;
        var $data = options.data;
        var $okText = options.okText || confirmButtonText;
        var $noText = options.noText || cancelButtonText;


        swal.queue([{
            title: $title,
            text: $message,
            type: $type,
            showCancelButton: true,
            showLoaderOnConfirm: true,
            confirmButtonText: $okText,
            cancelButtonText: $noText,
            preConfirm: function () {
                return new Promise(function (resolve) {
                    confirm($data);
                })
            }
        }]).then(function () { },
                 function () { decline($data); })

    }

    window.PTC.Loading = function (isShow) {

        if (isShow)
        {

            var html = '<div class="loading">' +
                        '<div>' +
                        '<div class="load-icon"></div>' +
                        '<span>資料載入中...</span>' +
                        '</div>' +
                        '</div>'

            $('body').append(html);
                 
        } else {
            $('.loading').remove();
        }

    }

    function show(alertElem) {
        var $existing = $(document.body).children('.alert');

        if ($existing.length <= 0) {
            $(document.body).prepend(alertElem);
        }
        else {
            $existing.last().after(alertElem);
        }
    }

})(jQuery);