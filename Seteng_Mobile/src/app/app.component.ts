import { Component } from '@angular/core';
import { Platform, AlertController, IonicApp,ToastController } from 'ionic-angular';
import { ViewChild } from '@angular/core';
import { StatusBar, Splashscreen } from 'ionic-native';

/* NgStore */
import { NgRedux, select } from '@angular-redux/store';
import { Observable } from 'rxjs/Rx';
import { rootReducer, IAppState } from '../rootReducer';
import { WelcomeAction } from '../pages/welcome/welcome-action';
import { IWelcomeState } from '../pages/welcome/welcome-reducer';

/* pages */
import { HomePage } from '../pages/home/home';
import { WelcomePage } from '../pages/welcome/welcome';
import { VenderSearchPage } from '../pages/vender-search/vender-search';
import { Record } from '../pages/Record/Record';

/* repository */
import { CallogRepository } from '../repository/callogRepository';

/* service */
import { LocalStorage } from '../services/localStorage';
import { UserService } from '../services//userService';
import { PushService } from '../services/pushService';
import { BackgroundService } from '../services/backgroundService';
import { NetworkService } from '../services/networkService';

import { LogHelper } from '../helper/logHelper';

@Component({
    templateUrl: 'app.html',
    queries: {
        nav: new ViewChild('content')
    }
})
export class MyApp {

    private user: any = {};

    @select(['welcome', 'item']) user$: Observable<User>;

    public Sticker = '';

    public uuid: string = 'none';

    public rootPage: any;

    public nav: any;

    public pages = [
        {
            title: '主頁',
            feature: 'home',
            icon: 'hammer',
            count: 0,
            component: HomePage
        },
        {
            title: '案件查詢',
            feature: 'search',
            icon: 'search',
            count: 0,
            component: VenderSearchPage
        },
        {
            title: '推播紀錄查詢',
            feature: 'Record',
            icon: 'search',
            count: 0,
            component: Record
        },
        {
            title: '登出',
            feature: 'logout',
            icon: 'log-out',
            count: 0,
            component: WelcomePage
        },

    ];

    constructor(private platform: Platform,
        private log: LogHelper,
        private local: LocalStorage,
        private network: NetworkService,
        private action: WelcomeAction,
        private background: BackgroundService,
        private alert: AlertController,
        private callogRepo: CallogRepository,
        private ionicApp: IonicApp,
        private push: PushService,
        private userService: UserService,
        private ngRedux: NgRedux<IAppState>,
        private toast: ToastController,) {

        ngRedux.configureStore(rootReducer, {});

        this.rootPage = WelcomePage;

        platform.ready().then(() => {
            Splashscreen.hide();
            StatusBar.styleDefault();
            // var filePath = '';
            // if (cordova) {
            //     if (this.platform.is('ios')) {
            //         filePath = cordova.file.applicationStorageDirectory + '/files/';
            //     }
            //     if (this.platform.is('android')) {
            //         filePath = cordova.file.externalApplicationStorageDirectory + '/files/';
            //     }
            // }
            // console.info(filePath);
            // this.local.set('logPath', filePath);
            if (this.platform.is('android')) {
                this.platform.registerBackButtonAction(() => {

                    var viewCtrl = this.nav.getActive();

                    let activePortal = this.ionicApp._loadingPortal.getActive() ||
                        this.ionicApp._modalPortal.getActive() ||
                        this.ionicApp._toastPortal.getActive() ||
                        this.ionicApp._overlayPortal.getActive();

                    //關閉popup
                    if (activePortal) {

                        activePortal.dismiss();
                        return;
                    }
                    //關閉一般頁面
                    if (viewCtrl.component.name != 'WelcomePage') {
                        this.nav.pop();
                        return;
                    }

                    //起始頁需要詢問
                    let alert = this.alert.create({
                        title: '是否離開程式?',
                        buttons: [
                            {
                                text: '否',
                                handler: () => {
                                    console.log('Cancel clicked');
                                }
                            },
                            {
                                text: '是',
                                handler: () => {
                                    navigator['app'].exitApp();
                                }
                            }
                        ]
                    });
                    alert.present();

                });
            }
            //初始化網路監聽工具
            this.network.OnConnectStateListener(
                uploadTempCase.bind(this),
                () => { });

            //初始化背景程序
            this.background.Enable();

            //初始化推播工具
            this.push.init();

            //並開始聆聽
            this.push.start();

            //取得裝置代號
            this.uuid = "132";

            //放入暫存
            this.local.set('uuid', this.uuid);

        });



        this.user = this.userService.GetUser();

        /**
         * 上傳暫存案件
         */
        function uploadTempCase() {

            var logs = this.local.get('logs');

            if (!logs || logs.length == 0) {
                this.log.info("並無暫存案件可上傳")
                return;
            }

            logs.forEach((log: CallogDetailApiViewModel, index: number) => {


                this.callogRepo.VenderConfirm(
                    log,
                    success.bind(this),
                    failure.bind(this))

                function success(data: JsonResult<Boolean>) {

                    this.log.info(`上傳暫存案件成功,案件編號:${log.Sn}`)

                    logs.splice(index);

                    this.local.set('logs', logs);

                }

                function failure(error: string) {
                    this.log.info(`上傳暫存案件失敗,案件編號:${log.Sn},原因為:${error}`)
                    this.toast(`上傳暫存案件失敗,案件編號:${log.Sn},原因為:${error}`,false)
                }

            });

        }

    }



    openPage(page) {

        this.log.info('--------開啟page----------')


        if (page.feature !== 'logout') {
            this.nav.setRoot(page.component);
            return;
        }

        this.userService.Logout(
            success.bind(this),
            failure.bind(this));

        function success() {

            this.log.info(`登出成功`)
            //retry init reducer.
            this.action.getDispatch({} as User);
            //導向起始頁
            this.nav.setRoot(WelcomePage);

        }

        function failure() {

            this.log.info(`登出失敗`)
            //導向起始頁
            this.nav.setRoot(WelcomePage);
        }

    }

    /**
 * 暫時訊息
 * @param msg
 */
    showToast(msg: string, isSuccess: boolean) {

        let toast = this.toast.create({
            message: msg,
            duration: 3000,
            position: 'middle',
            cssClass: isSuccess ? 'detail-toast-success' : 'detail-toast-fail'
        });
        toast.present();
    };

}


