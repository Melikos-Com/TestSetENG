//(function ($) {
//    'use strict';

//    if ($ === null || typeof $ === 'undefined') {
//        console.error('jQuery required not found.');
//        return;
//    }

//    //enum for template
//    var $templateType = {
//        Image: { value: "image" },
//        Pdf: { value: "pdf" },
//        Object: { value: "object" },
//    }

//    var $maxFileCount = 3;
//    var $initialPreviewAsData = true;
//    var $overwriteInitial = true;
//    var $allowedFileExtensions = ["XLSX", "XLS", "DOC", "DOCX", "PDF", "JPEG", "PNG", "JPG", ];
//    var $maxFileSize = 25600;
   
   
//    $.fn.PTCFileInput = function (options) {

//        if (!element.is('input')) { throw 'this element is not input.'}

//        if (!options) { throw 'options is null.'; }

//        var files = options.files;
//        var prevDeleteUrl = options.prevDeleteUrl;
//        var prevUploadUrl = options.prevUploadUrl;
//        var prevDeleteParam = options.prevDeleteParam;
//        var prevUploadParam = options.prevUploadParam;
//        var orgDeleteUrl = options.orgDeleteUrl;
//        var orgUploadUrl = options.orgUploadUrl;
//        var orgDeleteParam = options.orgDeleteParam;
//        var orgUploadParam = options.orgUploadParam;


//        var initialPreview = CombinationInitPreview(options);
//        var initialPreviewConfig = CombinationInitPreviewConfig(options)

//        var initSet = {

//            initialPreviewAsData: $initialPreviewAsData,
//            overwriteInitial: $overwriteInitial, 
//            initialPreview: initialPreview,
//            initialPreviewConfig: initialPreviewConfig,
//            allowedFileExtensions: $allowedFileExtensions,
//            uploadUrl: `${orgDeleteUrl}${FormatObject(orgDeleteParam)}`,
//            deleteUrl: `${orgUploadParam}${FormatObject(orgUploadParam)}`,
//            maxFileSize :$maxFileSize ,  
//            maxFilePreviewSize: $maxFileSize, 
//            browseClass: "btn",
//            uploadClass: "btn btn-file",
//            removeClass: "btn btn-file",
//            captionClass:"text-left",
//            maxFileCount: $maxFileSize, 
//            validateInitialCount :true ,
//            uploadAsync : false ,
//            showRemove : false ,
//            showUpload : true,
//            showClose : false ,
//            showCancel : false ,
//            showCaption : true,
//            dropZoneEnabled : true,
//            showBrowse :  true,
//            previewFileIcon: PreviewFileIcon(),
//            layoutTemplates :LayoutTemplates(),
//            otherActionButtons: OtherActionButtons(),
//            previewFileIconSettings: PreviewFileIconSettings(),
//            previewFileExtSettings: PreviewFileExtSettings(),
//            msgSizeTooLarge: fileinput_lang.msgSizeTooLarge,
//            msgFilesTooMany: fileinput_lang.msgFilesTooMany,
//            dropZoneTitle: fileinput_lang.dropZoneTitle,
//            browseLabel: fileinput_lang.browseLabel,
//            uploadLabel: fileinput_lang.uploadLabel,
//            msgSelected: fileinput_lang.msgSelected,
//            msgZoomModalHeading: fileinput_lang.msgZoomModalHeading,
//            msgInvalidFileExtension: fileinput_lang.msgInvalidFileExtension,
//            fileActionSettings: fileActionSettings()

//        }


//        $(this).fileinput(initSet);




//    }


//    function fileActionSettings() {

//        return {
//            showZoom: false,
//            showRemove:  true,
//            removeTitle: "移除檔案",
//            uploadTitle: "上傳檔案",
//            indicatorNewTitle: "檔案尚未上傳",
//        }

//    }

//    function CombinationInitPreview(options) {

//        var initialPreview = [];
//        var files = options.files;

//        if (!files || !Array.isArray(files))
//        { throw 'files is null or files is not array.' }

//        files.forEach(function (item) {

//            initialPreview.push(`${options.prevUploadUrl}${FormatObject(item.data)}`);

//        })


//    }
//    function CombinationInitPreviewConfig(options) {
//        var initialPreviewConfig = [];
//        var files = options.files;

//        if (!files || !Array.isArray(files))
//        { throw 'files is null or files is not array.' }

//        files.forEach(function (item) {

//            var object = {
//                caption: item.FileName,
//                type: FormatTemplateType(item.ExtensionFileName),
//                url: `${options.prevDeleteUrl}${FormatObject(item.data)}`
//            }

//        })

//    }

//    function FormatTemplateType(ExtensionFileName) {

//        switch (ExtensionFileName) {

//            case ExtensionFileName.match(/(PNG|jpe?g)/i):
//                return $templateType.Image.value;

//            case ExtensionFileName.match(/pdf/i):
//                return $templateType.Pdf.value;

//            default:
//                return $templateType.Object.value;

//        }

//    }

//    function FormatObject(object) {

//        var paramStr = '';

//        Object.getOwnPropertyNames(object)
//              .forEach(function (value, index, array) {

//                  paramStr += `&${object[value]}=${value}`;

//              });

//        return paramStr.replace('&', '?');

//    }

//    function PreviewFileIcon() {

//        return '<i class="fa fa-file"></i>';

//    }

//    function LayoutTemplates() {


//        return {

//            actionDelete: '<button type="button"  class="kv-file-remove {removeClass}" title="移除檔案"{dataUrl}{dataKey}>{removeIcon}</button>\n'

//        }

//    }

//    function OtherActionButtons() {

//        return '<button type="button" ' +
//               'onclick="download(this);"' +
//               'class="kv-file-download btn btn-xs btn-default" ' +
//               'title="下載檔案" {dataKey}>\n' +
//               '<i class="glyphicon glyphicon-download-alt text-success"></i>\n' +
//               '</button>\n'

//    }
 
//    function PreviewFileIconSettings() {

//        return {

//            doc: '<i class="fa fa-file-word-o text-primary"></i>',
//            xls: '<i class="fa fa-file-excel-o text-success"></i>'

//        }


//    }

//    function previewFileExtSettings() {

//        return {

//            doc: function (ext) { return ext.match(/(doc|docx)$/i); },
//            xls: function (ext) { return ext.match(/(xls|xlsx)$/i); },

//        }

//    }

  
//    $(this).on('filepredelete', function (event, key) {
//        console.log('filepredelete');

//    });

//    function download(ele) {
//        console.log('download');
//        console.log(ele);
//    }

//    $.fn.fileCtrlDestroy = function () {
//        $(this).fileinput('destroy');
//    };

//    $.fn.fileCtrlUnlock = function () {
//        $(this).fileinput('unlock');
//    };




//})(jQuery);