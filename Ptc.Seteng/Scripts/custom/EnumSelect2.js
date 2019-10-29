(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


    window.PTC.Select2 = window.PTC.Select2 || {};

   
   
    window.PTC.Select2.CallKind = function (option) {

    var currentData = [];

        $.post(option.url)
        .done(function (items) {

        items.forEach(function(data,index,array){
        
         currentData.push({
                    id:data.Id,
                    text: data.Name
                });
        
        });

            $(option.elem).select2({
                width: '100%',
                placeholder: '請選擇案件類型',
                allowClear: true,
                data: currentData
            });
        });


    }
  



})(jQuery);