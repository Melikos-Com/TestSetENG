
/**
 * 進行http行為所需要的物件
   ----------------------
   Success => when success callback.
   Failure => when fail callback.
   Uri     => target uri.
   Params  => transation object.
   Header  => transation Header.
 */
export class HttpParameter {

    constructor(
        private Success: (data: any) => void,
        private Failure: (error: HttpResponse) => void,
        private Uri: string,
        private Params: any,
        private Header?: any) {

        this.success = Success;
        this.failure = Failure;
        this.uri = Uri;
        this.params = Params;
        this.header = Header;
    }

    /*執行成功*/
    public success(data: any): void { };

    /*執行異常*/
    public failure(error: HttpResponse): void { };

    //呼叫位址
    public uri: string;

    //相關參數
    public params: any;

    //傳輸header
    public header: any;

}
