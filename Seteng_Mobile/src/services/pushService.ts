import { Injectable } from '@angular/core';
import { Platform, App, AlertController } from 'ionic-angular';

/* service */
import { JPushService } from 'ionic2-jpush';
import { LocalStorage } from './localStorage';

/* repository */
import { UserRepository } from '../repository/userRepository';
import { Response } from '@angular/http'

/* service */
import { FileService } from './fileService';
import { VenderConfirmPage } from '../pages/vender-confirm/vender-confirm';
import { VenderAssignPage } from '../pages/vender-assign/vender-assign';
import { VenderAcceptPage } from '../pages/vender-accept/vender-accept';

@Injectable()
export class PushService {


    constructor(private alert: AlertController,
        private app: App,
        private push: JPushService,
        private file: FileService,
        private local: LocalStorage,
        private userRepo: UserRepository,
        private platform: Platform) {

    }


    /**
     * 啟動聆聽
     */
    start() {

        this.file.WriteLog('----------啟動推播聆聽---------');

        console.log('---------- 啟動推播聆聽 -----------');

        this.platform.ready().then(() => {

            //打開推送
            this.push.openNotification()
                .subscribe(res => {

                    this.file.WriteLog('----------點開推送(openNotification)---------');

                    this.direct(res.extras.FeatureName);

                    console.log(res)
                });

            this.push.receiveNotification()
                .subscribe(res => {

                    this.file.WriteLog('----------收到推送(Notification)---------');

                    this.askDirect('收到推送', res.alert, res.extras.FeatureName);

                    console.log(res)
                });

            this.push.receiveMessage()
                .subscribe(res => {

                    this.file.WriteLog('----------收到推送(Message)---------');

                    this.askDirect('收到推送', res.alert, res.extras.FeatureName);

                    console.log(res)
                });

        })

    }

    /**
     * 初始化推播
     */
    init() {

        this.file.WriteLog('----------初始推播工具---------');

        console.log('---------- 初始推播工具 -----------');

        this.push
            .init()
            .then(res => {

                this.file.WriteLog(`---------- 初始成功 : ${res} ----------`);

                console.log(`---------- 初始成功 : ${res} ----------`);

            })
            .catch(err => {

                this.file.WriteLog(`---------- 初始失敗 : ${err} ----------`);

                console.log(`---------- 初始失敗 : ${err} ----------`);

            })
    }



    /**
     * 詢問是否導向畫面
     */
    askDirect(title: string, message: string, featureName: string) {

        if (featureName=="")
        {
            let alert = this.alert.create({
                title: title,
                message: message,
                buttons: [
                    {
                        text: '確認',
                        role: 'cancel',
                        cssClass: 'alertbuttonYes',
                        handler: () => {}
                    }
                ]
            });
            alert.present();
        }
        else
        {
            let alert = this.alert.create({
                title: title,
                message: message,
                buttons: [
                    {
                        text: '取消',
                        role: 'cancel',
                        cssClass: 'alertbuttonNo',
                        handler: () => {
    
                        }
                    },
                    {
                        text: '查看',
                        cssClass: 'alertbuttonSubmit',
                        handler: () => {
                            this.direct(featureName);
                        }
                    }
                ]
            });
            alert.present();
        }
    }

    /**
     * 導向對應功能頁
     */
    direct(FeatureName: string) {

        switch (FeatureName) {

            case 'VenderAccept':
                this.app.getActiveNav().setRoot(VenderAcceptPage);
                break;
            case 'VenderAssign':
                this.app.getActiveNav().setRoot(VenderAssignPage);
                break;
            case 'VenderConfirm':
                this.app.getActiveNav().setRoot(VenderConfirmPage);
                break;
            default:
                break;

        }


    }

    /**
    * 註冊tag , RegID取得並更新
    */
    settingRegID() {

        var user: User = this.local.get<User>('user');

        this.file.WriteLog(`---------- 開始監聽 RegisterId 回傳狀態 ----------`);

        console.log('---------- 開始監聽 RegisterId 回傳狀態 -----------');

        this.push
            .getRegistrationID()
            .then(res => {

                this.file.WriteLog(`---------- 回傳了RegID ${res} ----------`);

                console.log(` ---------- 回傳了RegID ${res} ----------`);

                this.file.WriteLog(`---------- RegisterId 存入本機緩存 ----------`);

                console.log(` ---------- RegisterId 存入本機緩存 ----------`);


                user.RegistrationID = res as string;

                this.local.set('user', user);

                this.file.WriteLog(`---------- RegisterId 上傳伺服器 ----------`);

                console.log(` ---------- RegisterId 上傳伺服器 ----------`);

                this.userRepo.UpdateRegID(
                    user.RegistrationID,
                    (data: JsonResult<Boolean>) => console.log(data.message),
                    (err: Response) => console.log(err));

            })
            .catch(err => {

                this.file.WriteLog(`----------  取得 RegID 異常 : ${err} ----------`);

                console.log(` ---------- 取得 RegID 異常 : ${err} ----------`);

            })
    }

}
