/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: ZH (Chinese; 銝剜�� (Zh�㤔gw蒙n), 瘙㕑祗, 瞍Ｚ��)
 * Region: TW (Taiwan)
 */
(function ($) {
    $.extend($.validator.messages, {
        required: "*此欄位為必填",
        remote: "*Please fix this field",
        email: "*請輸入合法的email格式",
        url: "*請輸入合法的url格式",
        date: "*請輸入合法的日期格式",
        dateISO: "*請輸入合法的日期格式(ISO).",
        number: "*必須輸入合法的數字(負數，小數)",
        digits: "*必須輸入整數",
        creditcard: "*請輸入正確的信用卡號",
        equalTo: "*請再次輸入相同的值",
        accept: "*輸入擁有合法後綴名的字符串",
        maxlength: $.validator.format("*長度不得大於{0}"),
        minlength: $.validator.format("*長度不得小於{0}"),
        rangelength: $.validator.format("*請輸入 {0} 到 {1} 個字"),
        range: $.validator.format("*請輸入 {0} 到 {1} 的數字."),
        max: $.validator.format("*請輸入<={0}的數字"),
        min: $.validator.format("*請輸入>={0}的數字")
    });
}(jQuery));
