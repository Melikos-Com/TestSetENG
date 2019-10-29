/**
 * 全域變數
 */
var GlobalVariable = (function () {
    function GlobalVariable() {
        // public logPath: string='';
        //  public logPath: string =  cordova.file.externalApplicationStorageDirectory + '/files/';
        //頁面長度
        this.pageSize = 10;
        //log 上傳路徑
        this.logUploadApiPath = 'File/SaveLog?token=';
    }
    return GlobalVariable;
}());
export { GlobalVariable };
//# sourceMappingURL=globalVariable.js.map