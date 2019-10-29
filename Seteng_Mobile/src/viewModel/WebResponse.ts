/*
   案件明細
*/
declare interface CallogDetailApiViewModel {

    //-------------
    // 立案當下產生的欄位
    //-------------

    //公司別
    CompCd: string;

    //門市地址
    StoreAddress: string;

    //門市電話
    StoreTel: string;

    //案件編號
    Sn: string;

    //門市名稱
    StoreName: string;

    //門市編號
    StoreCd: string;

    //設備代號
    AssetCd: string;

    //設備名稱
    AssetName: string;

    //叫修備註
    CallDesc: string;

    //補充備註
    RemarkAdd: string;

    //故障內容
    DamageDesc: string;

    //廠商名稱
    VenderName: string;

    //廠商代號
    VenderCd: string;

    //是否等待上傳
    IsWait: Boolean;

    //叫修時間
    FiDate: string;

    //完修時間
    FcDate: string;

    //應到店時間
    FaDate: string;

    //到店時間
    ArriveDate: string;

    //派工時間
    FvDate: string;

    //應完成時間
    FdDate: string;

    //-------------
    // 技師維修完畢後產生的欄位
    //-------------

    //認養人帳號
    AcceptedAccount: string;

    //認養人名稱
    AcceptedName: string;

    //是否受理
    IsAccept: boolean;

    //是否催修
    IsRcvDate: boolean;

    //維修說明
    FixMark: string;

    //案件圖片(修理前)
    ImgBeforeFix: Array<string>;

    //案件圖片(修理後)
    ImgAfterFix: Array<string>;

    //案件圖片(工單)
    Img: Array<string>;

    //案件圖片(門市簽名)
    ImgSignature: Array<string>;

    //門市是否簽名
    IsSignature: boolean;

    ///門市(拒絕)簽名時間      
    SignatureDate: string;

    //工作類型
    TypeWorkId: string;

    //銷案類型
    TypeFinishId: string;

    //處理類型
    TypePorcnoId: string;

    //工作類型(全部)
    Work: Array<Twrkknd>;

    //銷案類型(全部)
    Finish: Array<Tfinish>;

    //處理類型(全部)
    Proc: Array<Tdammtn>;

    //設施是否為咖啡設施
    IsCoffeeAsset: boolean;

    //咖啡杯數
    CoffeeCup: string;

    //工作類型名稱
    WorkName: string;

    //銷案類型名稱
    Finish_Name: string;

    //處理類型名稱
    Mtn_Desc: string;

    //***************SISO新增欄位項目***************
    //門市是否掃描到店時間
    IsStoreScanSI: Boolean;
    //門市是否掃描離店時間
    IsStoreScanSO: Boolean;
    //離店時間
    LeaveDate: string;
    //***************SISO新增欄位項目***************

    //SAP財邊
    SapAssetNo: string;
   
    //預估金額
    Pre_Amt:string;
}

/**
 * 案件數量資訊
 */
declare interface CallogCountApiViewModel {


    //待銷案數
    HasTechnicianCount: number;

    //待認養數
    AwaitAdoptCount: number;

    //待派工數
    AwaitAssignCount: number;

}

/*
   案件簡易資訊
*/
declare interface CallogResultApiViewModel {

    //案件編號
    Sn: string;

    //門市代號
    StoreCd: string;

    //門市名稱
    StoreName: string;

    //設備名稱
    AssetName: string;

    //故障原因
    DamageDesc: string;

    //門市地址
    StoreAddress: string;

    //緊急度
    CallLevel: string

    //維修描述
    FixMark: string;

    //叫修時間
    FiDate: string;

    //完修時間
    FcDate: string;

    //應到店時間
    FaDate: string;

    //派工時間
    FvDate: string;

    //應完成時間
    FdDate: string;

    //受理時間
    AcceptDatetime: string;

    //案件類型
    CallKind: CallKind;

    //案件圖片
    Img: Array<string>;

    //是否等待上傳
    IsWait: Boolean;

    //敘述
    TimeDescription: String;

    //受理人
    AcceptedName: String;

    //判斷是否逾時
    Overtime: boolean;

    //叫修人
    CallName: string;

    //案件狀態
    Timepoint: string;

    // 卡片是否顯示
    Enable: Boolean;

    //是否導入SISO流程廠商
    IsSISOVender: Boolean;

    VenderName: String;


}

/*
   技師主檔
*/
declare interface TechnicianResultApiViewModel {

    //廠商代號
    VenderCd: string;

    //公司代號
    CompCd: string;

    //廠商名稱
    VenderName: string;

    //公司名稱
    CompName: string;

    //技師帳號
    Account: string;

    //技師名稱
    Name: string;

    //畫面上選擇的
    IsCheck: Boolean;

}

/*
   技師群組主檔
*/
declare interface TechnicianGPResultApiViewModel {

    CompCd: string;

    GroupName: string;

    VendorCd: string;

    Seq: number;

    Count: number;

    Technicians: Array<TechnicianResultApiViewModel>;

    //畫面上選擇的
    IsCheck: Boolean;
}


/**
 * 轉換Angular Response
 */
declare interface HttpResponse {
    /**
     *  傳輸型態 "basic", "cors", "default", "error", or "opaque"
     * default 2, 網路異常 3
     */
    type: number
    /**
     * Status code returned by serve
     * default 200
     */
    status: number
    /**
     * Status文字描述
     */
    statusText: string
}

/**
 * 工作類型
 */
declare interface Twrkknd {
    //公司別
    CompCd: string
    /// 類別代碼
    WorkId: string
    /// 類別內容
    WorkDesc: string
    //狀態碼
    Worksts: string
}

/**
 * 銷案類型
 */
declare interface Tfinish {
    //公司別
    CompCd: string
    /// 類別代碼
    FinishId: string
    /// 類別內容
    FinishName: string
    /// 有效叫修案件碼
    CountSts: string
    /// 廠商銷案碼
    VenderSts: string
    /// 狀態碼
    FinishSts: string

}
/**
 * 處理類型
 */
declare interface Tdammtn {
    /// 公司別
    CompCd: string
    /// 設備代號
    AssetCd: string
    /// 故障處理代碼
    DamageProcNo: string
    /// 處理內容
    MtnDesc: string
    /// 有效叫修案件碼
    CountSts: string
}

declare interface PushRecord {
    //推播紀錄
    PushRecard: string
    //推播時間
    PushTime: string
}

declare interface CallogCourse {
    //指派者
    Assignor: string
    //受理者
    Admissibility: string
    //推播時間
    DateTime: Date
}