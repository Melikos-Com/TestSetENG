/*
   Serialize Object
*/
(function ($) {
    $.fn.serializeObject = function () {
        var o = {};

        //overwrite serializeArray 
        var proxy = $.fn.serializeArray;
        $.fn.serializeArray = function () {
            var inputs = this.find(':disabled');
            inputs.prop('disabled', false);
            var serialized = proxy.apply(this, arguments);
            inputs.prop('disabled', true);
            return serialized.filter(function (object) {
                return object.value &&  object.value != 'null';
            });
        };

        var a = this.serializeArray();

        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);


/*
  Direct Url
*/
function DirectToUrl(url, submitObj) {
    debugger;
    var paramterStr = "";

    if (submitObj) {

        $.each(submitObj, function (key, value) {
            paramterStr += "&" + key + "=" + value

        });
    }
    window.location.href = (url + "?" + paramterStr);

}

/*
  String Format
*/
String.format = function () {
    var s = arguments[0];
    if (s == null) return "";
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = getStringFormatPlaceHolderRegEx(i);
        s = s.replace(reg, (arguments[i + 1] == null ? "" : arguments[i + 1]));
    }
    return cleanStringFormatResult(s);
}

function getStringFormatPlaceHolderRegEx(placeHolderIndex) {
    return new RegExp('({)?\\{' + placeHolderIndex + '\\}(?!})', 'gm')
}

function cleanStringFormatResult(txt) {
    if (txt == null) return "";
    return txt.replace(getStringFormatPlaceHolderRegEx("\\d+"), "");
}

function padLeft(str, length, sign) {
    if (str.length >= length) return str;
    else return padLeft(sign + str, length, sign);
}

function padRight(str, length, sign) {
    if (str.length >= length) return str;
    else return padRight(str + sign, length, sign);
}
