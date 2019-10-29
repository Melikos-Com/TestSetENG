/*
    案件狀態
*/
declare enum CallSts {

    //處理中
    process = 0,

    //門市了結
    storeEnd = 1,

    //廠商確認了結
    vendorCfm = 2,

    //門市確認了結
    storeCfm = 3,

    //審核人確認了結
    FMCfm = 5,

    //主管確認了結
    TLCfm = 6,

    //案件結算
    Billing = 9,
}






