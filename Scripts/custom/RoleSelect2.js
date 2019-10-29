(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }

    window.PTC.Select2.Roles = function (elem, url, func) {

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入權限";


        window.PTC.SingleSelect2(option);

    }

})(jQuery);







