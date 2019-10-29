import { Injectable } from '@angular/core';

/* serverProfile */
import { ServerProfile } from "../serverProfile"
/* helper */
import { HttpHelper } from "../helper/httpHelper"
/* domain */
import { HttpParameter } from '../domain/httpParameter';
/* service */
import { RootScope } from '../services/rootScope';

@Injectable()
export class TechnicianRepository {

    constructor(private httpHelper: HttpHelper,
        private RootScope: RootScope,
        private serve: ServerProfile) {

    }

    /**
     * 取得技師清單
     * @param Page
     * @param keyword
     * @param success
     * @param failure
     */
    GetList(Page: number,
        keyword: String,
        success: (data: JsonResult<Array<TechnicianResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Post<Array<TechnicianResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Technician/GetList?token=${this.RootScope.get<User>('user').Token}`,
                {
                    Page: Page,
                    keyword: keyword,
                },
            )
        );

    }

    /**
    * 取得技師清單
    * @param Page
    * @param keyword
    * @param success
    * @param failure
    */
    GetGroupList(Page: number,
        keyword: String,
        success: (data: JsonResult<Array<TechnicianGPResultApiViewModel>>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Post<Array<TechnicianGPResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Technician/GetGroupList?token=${this.RootScope.get<User>('user').Token}`,
                {
                    Page: Page,
                    keyword: keyword,
                },
            )
        );

    }

    /**
     * 合併技師清單
     * @param Groups
     * @param Accounts
     * @param success
     */
    MergeTechnician(Groups: Array<TechnicianGPResultApiViewModel>,
                    Accounts: Array<TechnicianResultApiViewModel>,
                    success: (data: JsonResult<Array<TechnicianResultApiViewModel>>)=>void,
                    failure: (error: HttpResponse) => void){

        this.httpHelper.PostJson<Array<TechnicianResultApiViewModel>>(
            new HttpParameter(
                success,
                failure,
                `${this.serve.ApiUri}Technician/MergeTechnician?token=${this.RootScope.get<User>('user').Token}`,
                {
                    Groups: Groups,
                    Accounts: Accounts,
                },
            )
        );
    }

}
