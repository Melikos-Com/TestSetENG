import { Injectable } from '@angular/core';
/* serverProfile */
import { ServerProfile } from "../serverProfile";
/* helper */
import { HttpHelper } from "../helper/httpHelper";
/* domain */
import { HttpParameter } from '../domain/httpParameter';
/* service */
import { RootScope } from '../services/rootScope';

/*WEB API REQUEST*/
import { CallogApiReqViewModel } from '../viewModel/WebRequest';


@Injectable()
export class CallogRepository {


    constructor(private httpHelper: HttpHelper,
        private RootScope: RootScope,
        private serve: ServerProfile) { }

    /**
     * 廠商通知技師可認養
     * @param CompCd
     * @param Sn
     * @param Accounts
     * @param success
     * @param failure
     */
    VenderNotification(
        CompCd: String,
        Sn: String,
        Accounts: Array<TechnicianResultApiViewModel>,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.PostJson<Boolean>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/VenderNotification?token=${this.RootScope.get<User>('user').Token}`,
                {
                    CompCd: CompCd,
                    Sn: Sn,
                    Accounts: Accounts,
                },
            ));

    }


    /**
     * 技師認養工單
     * @param CompCd
     * @param Sn
     * @param Account
     */
    TechnicianAccept(
        CompCd: String,
        Sn: String,
        Account: String,
        IsVndAssign: Boolean,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Post<Boolean>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/TechnicianAccept?token=${this.RootScope.get<User>('user').Token}`,
                {
                    IsVndAssign: IsVndAssign,
                    CompCd: CompCd,
                    Sn: Sn,
                    AcceptedAccount: Account,
                },
            ));

    }

    /**
     * 廠商改派案件
     * @param CompCd
     * @param Sn
     * @param Account
     * @param success
     * @param failure
     */
    VenderChangeLog(
        CompCd: String,
        Sn: String,
        Account: String,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Post<Boolean>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/VendorChangeLog?token=${this.RootScope.get<User>('user').Token}`,
                {
                    CompCd: CompCd,
                    Sn: Sn,
                    AcceptedAccount: Account
                },
            ));
    }

    /**
     * 取得已認養案件清單(卡片)
     * @param CompCd    公司代號
     * @param VenderCd  廠商代號
     * @param CallSts   案件狀態
     * @param Page      當前頁數
     * @param OrderBy   排序依據
     * @param OrderType 排序方式
     * @param success   成功時呼叫的
     * @param failure   失敗時呼叫的
     */
    GetHasTechnicianCallog(
        Page: number,
        enableMask: boolean,
        success: (data: JsonResult<Array<CallogResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void): void {

        this.httpHelper.Post<Array<CallogResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/GetHasTechnicianCallog?token=${this.RootScope.get<User>('user').Token}`,
                {
                    page: Page,
                }
            ), true, enableMask);

    }

    /**
     * 取得待認養案件清單(卡片)
     * @param CompCd        公司代號
     * @param VenderCd      廠商代號
     * @param Page          當前頁數
     * @param OrderBy       排序依據
     * @param OrderType     排序方式
     * @param success       成功時執行的
     * @param failure       失敗時執行的
     */
    GetAwaitAdoptCallog(
        Page: number,
        OrderBy: String,
        OrderType: number,
        enableMask: boolean,
        success: (data: JsonResult<Array<CallogResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void): void {

        this.httpHelper.Post<Array<CallogResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/GetAwaitAdoptCallog?token=${this.RootScope.get<User>('user').Token}`,
                {
                    page: Page,
                    OrderBy: OrderBy,
                    OrderType: OrderType
                },
            ), true, enableMask);
    }

    /**
     * 取得未完修案件清單(卡片)
     * @param CompCd        公司代號
     * @param VenderCd      廠商代號
     * @param Page          當前頁數
     * @param OrderBy       排序依據
     * @param OrderType     排序方式
     * @param success       成功時執行的
     * @param failure       失敗時執行的
     */
    GetVenderManagerCallog(
        Page: number,
        Data: CallogApiReqViewModel,
        enableMask: boolean,
        success: (data: JsonResult<Array<CallogResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void): void {

        this.httpHelper.Post<Array<CallogResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/GetVenderManagerCallog?token=${this.RootScope.get<User>('user').Token}`,
                {
                    CompCd: Data.CompCd,
                    FiDateStart: Data.FiDateStart,
                    FiDateEnd: Data.FiDateEnd,
                    StoreCd: Data.StoreCd,
                    StoreName: Data.StoreName,
                    Sn: Data.Sn,
                    CallLevel: Data.CallLevel,
                    VenderCd: Data.VenderCd,
                    Timepoint: Data.Timepoint,
                    page: Page

                }
            )
            , true
            , enableMask);
    }

    /**
     * 取得案件數量資訊
     */
    GetVendorGetNewsCount(
        success: (data: JsonResult<Array<CallogCountApiViewModel>>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<CallogDetailApiViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetVendorGetNewsCount`,
            {
                token: this.RootScope.get<User>('user').Token,
            },
        ), true, false);

    }

    /**
     * 取得案件明細(待認養)
     * @param Sn        案件編號
     * @param Success   成功時呼叫的
     * @param failure   失敗時呼叫的
     */
    GetByAccept(
        Sn: String,
        success: (data: JsonResult<CallogDetailApiViewModel>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<CallogDetailApiViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetByAccept`,
            {
                token: this.RootScope.get<User>('user').Token,
                Sn: Sn
            }
        ));

    }


     /**
     * 取得案件明細(未完修)
     * @param Sn        案件編號
     * @param Success   成功時呼叫的
     * @param failure   失敗時呼叫的
     */
    GetNoFinish(
        Sn: String,
        success: (data: JsonResult<CallogDetailApiViewModel>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<CallogDetailApiViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetNoFinish`,
            {
                token: this.RootScope.get<User>('user').Token,
                Sn: Sn
            }
        ));

    }

        /**
     * 取得案件明細(已認養)
     * @param Sn        案件編號
     * @param Success   成功時呼叫的
     * @param failure   失敗時呼叫的
     */
    GetByConfirm(
        Sn: String,
        success: (data: JsonResult<CallogDetailApiViewModel>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<CallogDetailApiViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetByConfirm`,
            {
                token: this.RootScope.get<User>('user').Token,
                Sn: Sn
            }
        ));

    }
        /**
     * 取得案件明細(叫修案件查詢)
     * @param Sn        案件編號
     * @param Success   成功時呼叫的
     * @param failure   失敗時呼叫的
     */
    GetBySearch(
        Sn: String,
        success: (data: JsonResult<CallogDetailApiViewModel>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<CallogDetailApiViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetBySearch`,
            {
                token: this.RootScope.get<User>('user').Token,
                Sn: Sn
            }
        ));

    }

    /**
     * 案件列表(卡片)(叫修案件查詢)
     * @param Data      查詢條件組合的物件
     * @param success   成功時執行的
     * @param failure   失敗時執行的
     */
    GetList(
        Data: CallogApiReqViewModel,
        Page: number,
        OrderBy: String,
        OrderType: number,
        enableMask: boolean,
        success: (data: JsonResult<Array<CallogResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void): void {

        this.httpHelper.Post<CallogApiReqViewModel>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}Callog/GetList?token=${this.RootScope.get<User>('user').Token}`,
            {
                CompCd: Data.CompCd,
                FiDateStart: Data.FiDateStart,
                FiDateEnd: Data.FiDateEnd,
                StoreCd: Data.StoreCd,
                StoreName: Data.StoreName,
                Sn: Data.Sn,
                CallLevel: Data.CallLevel,
                VenderCd: Data.VenderCd,
                Timepoint: Data.Timepoint,
                page: Page,
                OrderBy: OrderBy,
                OrderType: OrderType
            }
        ), true, enableMask);


    };

    /**
     * 銷案
     * @param Sn
     * @param success
     * @param failure
     */
    VenderConfirm(
        Data: CallogDetailApiViewModel,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.PostJson<Boolean>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/VendorConfirm?token=${this.RootScope.get<User>('user').Token}`,
                {
                    CompCd: Data.CompCd,
                    Sn: Data.Sn,
                    ArriveDate: Data.ArriveDate,
                    FcDate: Data.FcDate,
                    ImgBeforeFix: Data.ImgBeforeFix,
                    ImgAfterFix: Data.ImgAfterFix,
                    Img: Data.Img,
                    TypeWorkId: Data.TypeWorkId,
                    TypeFinishId: Data.TypeFinishId,
                    TypePorcnoId: Data.TypePorcnoId,
                    FixMark : Data.FixMark,
                    CoffeeCup: Data.CoffeeCup,
                    AcceptedAccount: Data.AcceptedAccount,
                    AcceptedName: Data.AcceptedName,
                    IsStoreScanSI:Data.IsStoreScanSI,
                    ImgSignature:Data.ImgSignature,
                    IsSignature:Data.IsSignature,
                    SignatureDate:Data.SignatureDate,
                    IsStoreScanSO:Data.IsStoreScanSO,
                    LeaveDate:Data.LeaveDate,
                    Pre_Amt:Data.Pre_Amt,
                },

            ), false
        );



    }

    /**
     * 暫存
     * @param Sn
     * @param FixMark
     * @param TCALIMG
     * @param success
     * @param failure
     */
    ScratchOfVndCfm(
        CompCd: String,
        Sn: String,
        Arrivedate: String,
        IsStoreScanSI:Boolean,
        ImgBeforeFix: Array<string>,
        ImgAfterFix: Array<string>,
        Img: Array<string>,      
        AcceptedAccount: string,
        AcceptedName: string,
        FixMark : string,
        CoffeeCup: string,
        Pre_Amt:string,       
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.PostJson<Boolean>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/ScratchOfVndCfm?token=${this.RootScope.get<User>('user').Token}`,
                {
                    CompCd: CompCd,
                    Sn: Sn,
                    ArriveDate: Arrivedate,    
                    IsStoreScanSI:IsStoreScanSI,                
                    ImgBeforeFix: ImgBeforeFix,
                    ImgAfterFix: ImgAfterFix,
                    Img: Img,                   
                    AcceptedAccount: AcceptedAccount,
                    AcceptedName: AcceptedName,
                    FixMark : FixMark,
                    CoffeeCup: CoffeeCup,
                    Pre_Amt:Pre_Amt,                               
                }
            )
        );
    }

    GetRecard(
        account:string,
        success: (data: JsonResult<PushRecord[]>) => void,
        failure: (error: HttpResponse) => void) {
        this.httpHelper.Get<PushRecord[]>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/GetRecard`,
                {
                    account: account
                }
            )
        )
    };

    GetCallogHistory(
        CompCd:string,
        Sn:string,
        success: (data: JsonResult<CallogCourse[]>) => void,
        failure: (error: HttpResponse) => void) {
        this.httpHelper.Get<CallogCourse[]>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Callog/GetCallogHistory`,
                {
                    CompCd: CompCd,
                    Sn: Sn
                }
            )
        )
    };
}
