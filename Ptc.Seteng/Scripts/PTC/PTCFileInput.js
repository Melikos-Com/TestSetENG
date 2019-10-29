(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


    $.fn.PTCFileInput = function (option) {

        $(this).fileinput({
            initialPreview: option.initialPreview,
            initialPreviewConfig: option.initialPreviewConfig,
            allowedFileExtensions: option.allowedFileExtensions,
            maxFileCount: option.maxFileCount,
            maxFileSize: option.maxFileSize,
            previewSettings: option.previewSettings,
            previewZoomSettings: option.previewZoomSettings,
            browseLabel: "上传",
            maxFilePreviewSize: 8192,        
            msgSizeTooLarge: '档案 "{name}" ({size} KB) 超过允许 {maxSize} KB. 请重新上传!',
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
            deleteUrl: '',
            browseClass: "btn",
            uploadClass: "btn btn-file",
            removeClass: "btn btn-file",
            initialPreviewAsData: true,
            overwriteInitial: true,
            showUpload: false,
            showRemove: false,
            uploadAsync: false,
   
        });

    }
})(jQuery);


