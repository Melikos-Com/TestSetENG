

/*查詢分頁的基底*/
export class PagingViewModel {
    keyword: string;
    OrderBy: string;
    OrderType: number;
    page: number;
    pageSize: number;
}


/*
    查詢條件
*/
export class CallogApiReqViewModel extends PagingViewModel {

    //公司代號
    CompCd: string;

    //叫修編號
    Sn: string;
    
    //廠商代號
    VenderCd: string;

    //門市代號
    StoreCd : string;

    //受理日期(開始)
    FiDateStart?:Date;

    //受理日期(結束)
    FiDateEnd?:Date;

    //門市名稱
    StoreName: string;

    //緊急程度
    CallLevel : string;

    //查詢的時間種類
    DateType : string;

    //案件狀態
    Timepoint : string;
}



