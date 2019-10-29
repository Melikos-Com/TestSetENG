/*
    使用者資訊
*/
declare interface User {

    //公司代號
    CompCd: string,

    //廠商編號
    VenderCd: string,

    //技師帳號
    Account: string,

    //廠商名稱
    VenderName: string,

    //公司名稱
    CompName: string,

    //是否啟用
    Enable: boolean,

    //是否總部確認過了
    IsHQCheck: boolean,

    //是否透過qrcode登入
    IsManually: boolean,

    //是否為廠商
    IsVendor: boolean

    //大頭貼路徑
    ImagePath: string

    //身分證ID
    ID: string,

    //技師名稱
    Name: string,

    //技師密碼
    Password: string,

    //推播碼
    RegistrationID: string,

    //設備編號
    DeviceID: string;

    //token
    Token: string;

    //大頭貼照
    Sticker: string;

    //本機暫存的路徑
    TempSticker :string;
}
