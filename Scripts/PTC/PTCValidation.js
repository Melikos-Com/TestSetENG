(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


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
        
                if (element["0"].attributes[2].nodeValue == "file")
                {
                    debugger;
                    error.insertAfter(element.parent().parent().parent());
                }
                else if (element["0"].type == "file") { //forIE
                    debugger;
                    error.insertAfter(element.parent().parent().parent());
                }
                else if (element["0"].type == "radio") { 
                    debugger;
              
                   
                    error.insertAfter(element.siblings("text").last());
                }
                else if(element.is('select')){
                    debugger;
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


