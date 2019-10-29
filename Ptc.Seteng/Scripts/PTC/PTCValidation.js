(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }
    $.validator.addMethod("alphabetnumber", function (value, element) {
      
        var re = /^[\x21-\x7e]+$/g;
        return value.match(re);
    }, "*账号只能输入(英文、数字、特殊符号)");

    $.validator.addMethod("noSpace", function (value, element) {
        return value.indexOf(" ") < 0 && value != "";
    }, "*不允许输入空字符");

    $.fn.PTCValidation = function (option) {

        if (!$(this) || typeof $(this) !== 'object') { throw 'elem is not form .';}

        $(this).validate({
            ignore: [],
            rules:option.rules,
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            errorElement: 'span',
            errorClass: 'help-block',
            errorPlacement: function (error, element) {
        
             
                if (element["0"].type == "file") { 
          
                    error.insertAfter(element.parent().parent().parent());
                }
                else if (element["0"].type == "radio") { 

                    error.insertAfter(element.siblings("text").last());
                }
                else if(element.is('select')){
         
                    error.insertAfter(element.next());
                }
                else {
                  if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.insertAfter(element);
                    }
                }


                
            }
        });

    }
})(jQuery);


