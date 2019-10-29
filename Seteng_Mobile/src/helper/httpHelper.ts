import { Http, Headers, RequestOptions, URLSearchParams, Response } from '@angular/http'
import { Injectable } from '@angular/core';
import { LoadingController, AlertController, App } from 'ionic-angular';
import * as Rx from 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { LocalStorage } from "../services/localStorage";

/* domain */
import { HttpParameter } from '../domain/httpParameter';

/* service  */
import { FileService } from '../services/fileService';

/* service  */
import { LogHelper } from './logHelper';

import { WelcomePage } from '../pages/welcome/welcome';

/**
 * 進行http傳輸
   -----------------
    Post<T>
    Get<T>
 */
@Injectable()
export class HttpHelper {

    constructor(private Http: Http,
        private app: App,
        private Loading: LoadingController,
        private log: LogHelper,
        private Alert: AlertController,
        private local: LocalStorage,) { }

    /**
     * Get Mode
     * @param param
     */
    public Get<T>(param: HttpParameter, enableErrorStatus: Boolean = true, enableMask: Boolean = true): Observable<HttpResponse> {


        let loadingPopup = this.Loading.create({ content: '加載中...' });

        try {

            this.log.info('----------[Start] Http Get----------')

            let currentUri: string = `${param.uri}?${this.CommandStr(param.params)}`;

            this.log.info(`target : ${currentUri}`);
            this.log.info(`params : ${JSON.stringify(param.params)}`)

            //啟用遮罩
            if (enableMask)
                loadingPopup.present();


            var http = this.Http
                .get(currentUri)
                .map(res => {

                    if (!this.StatusSuccess(res.status))
                        throw new RangeError(`Server Status Error : ${res.status}`);

                    return <JsonResult<T>>res.json();

                })
                //when connect error to do ..
                .catch((error, obs) => {

                    this.log.error(`[Error] Http Get => 連線發生錯誤: ${error}`);
                        

                    switch (error.status) {

                        case 0:  //連線發生錯誤
                            if (enableErrorStatus) {
                                this.ShowAlert("連線發生錯誤");
                            }
                            param.failure(error);
                            break

                        case 400: //badRequest
                            param.failure(error);
                            break;
                        case 401: // 權限驗證異常
                            param.failure(error);
                            break;
                        case 404: // API無法連接
                            this.ShowAlert("無法連線至伺服器");
                            break;
                        default:
                            this.ShowAlert("連線發生錯誤!!");
                            this.direct();
                            break;

                    }


                    loadingPopup.dismiss();

                    return Rx.Observable.empty();

                });

            http.subscribe(
                //when response change to do ..
                (data) => {

                    this.log.info('-----------[Success] End Http Get-----------');

                    param.success(data);

                    loadingPopup.dismiss();
                },
                //when Complete to do..
                (error) => {

                    this.log.error(`[Error] Http Get =>  ${error}`);

                    param.failure(error);

                    loadingPopup.dismiss();

                    this.ShowAlert(error);
                },
                //when Complete to do..
                () => {

                    this.log.info('-----------[Complete] End Http Get-----------');

                    loadingPopup.dismiss();

                });

            return http;


        } catch (e) {

            this.log.error(`[Exception] Http Get =>  ${(<Error>e).message}`);

            loadingPopup.dismiss();

            this.ShowAlert((<Error>e).message);

        }
    }

    /**
     * Post Mode
     * @param param
     */
    public Post<T>(param: HttpParameter, enableErrorStatus: Boolean = true, enableMask: Boolean = true): Observable<Response> {

        let loadingPopup = this.Loading.create({ content: '加載中...' });

        try {

            this.log.info('----------[Start] Http Post----------');

            var header = param.header || { 'Content-Type': 'application/x-www-form-urlencoded' };
            let currentUri = param.uri;
            let body = this.CommandStr(param.params);
            let headers = new Headers(header);
            let options = new RequestOptions({ headers: headers });

            this.log.info(`target : ${currentUri}`);
            this.log.info(`params : ${body}`)

            //啟用遮罩
            if (enableMask)
                loadingPopup.present();


            var http = this.Http
                .post(currentUri, body, options)
                .map(res => {

                    if (!this.StatusSuccess(res.status))
                        throw new RangeError(`Server Status Error : ${res.status}`);

                    return <JsonResult<T>>res.json();

                })
                //when connect error to do ..
                .catch((error: Response, obs) => {

                    this.log.error(`[Error] Http Post => 連線發生錯誤:${error}`);
                    switch (error.status) {

                        case 0:  //連線發生錯誤
                            if (enableErrorStatus) {
                                this.ShowAlert("連線發生錯誤");
                            }
                            param.failure(error);
                            break
                        case 400: //badRequest
                            param.failure(error);
                            break;
                        case 401: // 權限驗證異常
                            param.failure(error);
                            break;
                        case 404: // API無法連接
                            this.ShowAlert("無法連線至伺服器");
                            break;
                        default:
                            this.ShowAlert("連線發生錯誤!!");
                            this.direct();
                            break;

                    }

                    loadingPopup.dismiss();

                    return Rx.Observable
                        .empty();

                })

            http.subscribe(
                //when response change to do ..
                (data) => {

                    this.log.info('-----------[Success] Http Post -----------');

                    param.success(data);

                    loadingPopup.dismiss();

                },
                //when Complete to do..
                (error) => {

                    this.log.error(`[Error] Http Post => ${error}`);

                    param.failure(error);

                    loadingPopup.dismiss();

                    this.ShowAlert(error);

                },
                //when Complete to do..
                () => {

                    this.log.info('-----------[Complete] Http Post-----------');

                    loadingPopup.dismiss();


                });

            return http;

        } catch (e) {


            this.log.error(`[Exception] Http Post => ${(<Error>e).message}`);

            loadingPopup.dismiss();

            this.ShowAlert((<Error>e).message);

        }


    }

    /**
     * post Mode (Json)
     * @param param
     */
    public PostJson<T>(param: HttpParameter, enableErrorStatus: Boolean = true, enableMask: Boolean = true): Observable<Response> {

        let loadingPopup = this.Loading.create({ content: '加載中...' });

        try {

            this.log.info('----------[Start] Http Post----------');

            var header = { 'Content-Type': 'application/json' };
            let currentUri = param.uri;
            let body = JSON.stringify(param.params);
            let headers = new Headers(header);
            let options = new RequestOptions({ headers: headers });

            this.log.info(`target : ${currentUri}`);
            this.log.info(`params : ${body}`)

            //啟用遮罩
            if (enableMask)
                loadingPopup.present();

            var http = this.Http
                .post(currentUri, body, options)
                .map(res => {

                    if (!this.StatusSuccess(res.status))
                        throw new RangeError(`Server Status Error : ${res.status}`);

                    return <JsonResult<T>>res.json();

                })
                //when connect error to do ..
                .catch((error: Response, obs) => {

                    this.log.error(`[Error] Http Post => 連線發生錯誤:${error}`);

                    switch (error.status) {

                        case 0:  //連線發生錯誤
                            if (enableErrorStatus) {
                                this.ShowAlert("連線發生錯誤");
                            }
                            param.failure(error);
                            break
                        case 400: //badRequest
                            param.failure(error);
                            break;
                        case 401: // 權限驗證異常
                            param.failure(error);
                            break;
                        case 404: // API無法連接
                            this.ShowAlert("無法連線至伺服器");
                            break;
                        default:
                            this.ShowAlert("連線發生錯誤!!");
                            this.direct();
                            break;

                    }

                    loadingPopup.dismiss();

                    return Rx.Observable
                        .empty();

                });


            http.subscribe(
                //when response change to do ..
                (data) => {

                    this.log.info('-----------[Success] Http Post -----------');

                    param.success(data);

                    loadingPopup.dismiss();

                },
                //when Complete to do..
                (error) => {

                    this.log.error(`[Error] Http Post => ${error}`);

                    param.failure(error);

                    loadingPopup.dismiss();

                    this.ShowAlert(error);

                },
                //when Complete to do..
                () => {

                    this.log.info('-----------[Complete] Http Post-----------');

                    loadingPopup.dismiss();


                })

            return http;

        } catch (e) {

            this.log.error(`[Exception] Http Post => ${(<Error>e).message}`);

            loadingPopup.dismiss();

            this.ShowAlert((<Error>e).message);

        }


    }

    /**
     * post Mode (Multipart-form-data)
     * @param param
     */
    public PostMultipart(param: HttpParameter) {


        try {

            this.log.info('----------[Start] Http Post----------');

            this.log.info(`target : ${param.uri}`);
            this.log.info(`params : ${param.params}`)

            var request = new XMLHttpRequest();
            request.open('POST', param.uri);
            request.send(param.params);

        } catch (e) {

            this.log.error(`[Exception] Http Post => ${(<Error>e).message}`);

            this.ShowAlert((<Error>e).message);
        }


    }


    //判斷statusCode,是否為正常
    StatusSuccess = (status: any): Boolean => {

        return !(status < 200 || status >= 300);

    }

    //[HttpPost]根據Data,產生querystring
    CommandStr(options: Object): String {
        if (!options) { return ''; }
        var params = new URLSearchParams();

        Object.getOwnPropertyNames(options)
            .forEach((val, idx, array) => {
                if (options[val] instanceof Date) {
                    params.set(val, options[val].toISOString());
                } else {
                    params.set(val, options[val]);
                }



            });
        return params.toString();
    }

    //產生提示視窗
    ShowAlert(errorMsg: String): void {
        let alert = this.Alert.create({
            title: '錯誤',
            subTitle: `${errorMsg}`,
            buttons: [
                {
                    text: 'OK',
                    handler: data => {
                        console.log('Cancel clicked');
                    }
                }
            ]
        });
        alert.present();
    }
    /**
 * 導向對應功能頁
 */
    direct() {
        this.local.set('user', null);
        this.app.getActiveNav().push(WelcomePage);
    };

}
