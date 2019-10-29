import { Injectable } from '@angular/core';
/* serverProfile */
import { ServerProfile } from "../serverProfile"
import { LocalStorage } from '../services/localStorage'
/* helper */
import { HttpHelper } from "../helper/httpHelper"
/* domain */
import { HttpParameter } from '../domain/httpParameter';
import { RootScope } from '../services/rootScope';
import { File } from 'ionic-native';
import { LoadingController } from 'ionic-angular';

@Injectable()
export class UserRepository {

    constructor(private httpHelper: HttpHelper,
        private loading: LoadingController,
        private local: LocalStorage,
        private RootScope: RootScope,
        private serve: ServerProfile) { }

    /**
     * 檢查版本號
     * @param Version 
     * @param success 
     * @param failure 
     */
    IsVersionValid(Version: String,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {
        this.httpHelper.Get<boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/CheckVersion`,
            {
                Version: Version
            }
        ));
    }
    /**
     * 登入
     * @param Account
     * @param Password
     */
    Login(Account: string,
        Password: string,
        UUID: string,
        success: (data: JsonResult<User>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<User>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/VendorLogin`,
            {
                UUID: UUID,
                Account: Account,
                Password: Password
            }
        ));


    }

    /**
     * 登出
     * @param Account
     * @param Password
     * @param success
     * @param failure
     */
    Logout(token: string,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        return this.httpHelper.Get<Boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/VendorLogout`,
            {
                token: token
            }
        ));


    }

    /**
     * 刷新推播碼
     * @param Account
     * @param Password
     * @param RegId
     * @param success
     * @param failure
     */
    UpdateRegID(RegId: string,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {

        this.httpHelper.Get<Boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/UpdateRegID`,
            {
                token: this.RootScope.get<User>('user').Token,
                RegId: RegId
            }
        ));


    }

    /**
     * 清除設備識別值
     * @param Account
     * @param Password
     * @param success
     * @param failure
     */
    ClearUUID(Account: string,
        Password: string,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {


        this.httpHelper.Get<Boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/ClearUUID`,
            {
                Account: Account,
                Password: Password,
            }
        ));


    }

    /**
     * 驗證token 有效性
     * @param token
     * @param success
     * @param failure
     */
    IsTokenValid(token: string,
        success: (data: JsonResult<Boolean>) => void,
        failure: (error: HttpResponse) => void) {


        this.httpHelper.Get<Boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/IsTokenValid`,
            {
                Token: token,
            }
        ));
    };
    /**
     * 取得更新連結
     * @param index 
     * @param success 
     * @param failure 
     */
    AppLink(index: string,
        success: (data: JsonResult<boolean>) => void,
        failure: (error: HttpResponse) => void) {
        this.httpHelper.Get<boolean>(new HttpParameter(
            success,
            failure,
            `${this.serve.ApiUri}User/AppLink`,
            {
                index: index,
            }
        ));
    };
}
