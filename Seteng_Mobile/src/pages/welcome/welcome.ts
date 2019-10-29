import { Component, AfterViewInit } from '@angular/core';
import { NavController, ToastController, AlertController, Platform, MenuController } from 'ionic-angular';
import { BarcodeScanner, Keyboard } from 'ionic-native';
import { AppVersion } from 'ionic-native';
/* form */
import { FormBuilder, Validators } from '@angular/forms';

/* repository */
import { UserRepository } from '../../repository/userRepository';

/* service */
import { LocalStorage } from '../../services/localStorage';
import { PushService } from '../../services/pushService';
import { UserService } from '../../services/userService';

/* helper  */
import { LogHelper } from '../../helper/logHelper';

/* pages */
import { HomePage } from '../home/home';
import { VersionErrorPage } from '../VersionError/VersionError';
/* NgStore */
import { WelcomeAction } from '../../pages/welcome/welcome-action'

@Component({
    selector: 'page-welcome',
    templateUrl: 'welcome.html'
})
export class WelcomePage implements AfterViewInit {


    public user: any = {};

    public version: string = 'none';


    constructor(private toast: ToastController,
        private menu: MenuController,
        private log: LogHelper,
        private platform: Platform,
        private push: PushService,
        private alert: AlertController,
        private local: LocalStorage,
        private fb: FormBuilder,
        private action: WelcomeAction,
        private nav: NavController,
        private userRepo: UserRepository,
        private userService: UserService) {



        this.platform.ready().then(() => {

           Keyboard.disableScroll(false);

            AppVersion.getVersionNumber().then(version => {
                //取得版本代號
                this.version = version;
                this.log.info("APP版本:" + this.version);
                this.CheckVersion(this.version);
            });

        });

        //鎖定menu
        this.menu.swipeEnable(false);

    }

    //欄位驗證機制
    public form = this.fb.group({
        Account: ['', Validators.compose([Validators.maxLength(20), Validators.required])],
        Password: ['', Validators.compose([Validators.maxLength(20), Validators.required])]
    });

    /**
        * 畫面剛載入完畢時
        */
    ngAfterViewInit() {

    }
    
    /**
     * 按下登入
     */
    login(event) {

        this.log.info(`--準備執行手動登入`);

        var input = this.user as User;

        if (!this.form.valid) {
            this.showToast("欄位驗証不通過", false);
            return;
        }

        this.userRepo.IsVersionValid(this.version,
        success.bind(this),
        failure.bind(this))

        function success(data) {
            console.log(data)
            if (data.isSuccess==true)
            {
                this.excute(input.Account, input.Password);
            } 
            else
            {
                 this.nav.setRoot(VersionErrorPage);
            } 
        }
        function failure(error) {

        }

    }

    /**
     * 使用者登入
     * @param account
     * @param password
     */
    excute(account: string,
        password: string) {

        this.log.info(`--準備執行實際登入動作`);

        this.userRepo.Login(
            account,
            password,
            this.local.get<string>('uuid') || 'none',
            success.bind(this),
            failure.bind(this));

        function success(data) {

            this.log.info(data);

            if (data.isSuccess == false) {
                this.showToast(data.message, false);
                return;
            }

            if (data.element.clearUUID) {
                this.showPrompt(account,
                    password,
                    data.message);
                return;
            }


            this.user = data.element;

            this.userService.Login(data.element);

            this.push.settingRegID();

            this.action.getDispatch(data.element)

            this.nav.setRoot(HomePage);

            this.menu.swipeEnable(true);

        }

        function failure(error: HttpResponse) {

            this.showToast("登入失敗");

            this.log.error(error);

        };

    }

    /**
    * 強制登入
    */
    forceLogin(account: string, password: string) {

        this.log.info(`--準備執行強制登入動作`);

        this.userRepo.ClearUUID(
            account,
            password,
            success.bind(this),
            failure.bind(this)
        );

        function success(data) {

            this.log.info(data);

            this.excute(account, password);

        }

        function failure(error) {


            this.log.error(error);

        }


    }


    /**
     * 暫時訊息
     */
    showToast(msg: string, isSuccess: boolean) {

        let toast = this.toast.create({
            message: msg,
            duration: 3000,
            position: 'middle',
            cssClass: isSuccess ? 'detail-toast-success' : 'detail-toast-fail'
        });
        toast.present();
    }

    /**
     * 是否強制登入?
     */
    showPrompt(account: string, password: string, msg: string) {

        this.log.info(`--詢問是否要強制登入?`);

        let prompt = this.alert.create({
            title: '提示',
            message: msg,

            buttons: [
                {
                    text: '否',
                    handler: data => {
                        this.log.info(`--選擇:『否』`);
                    }
                },
                {
                    text: '是',
                    handler: data => {
                        this.log.info(`--選擇:『是』`);
                        this.forceLogin(account, password);

                    }
                }
            ]
        });
        prompt.present();
    }
     /**
     * 檢查APP版本
     */
    CheckVersion(msg: String) {
        this.userRepo.IsVersionValid(msg,
        success.bind(this),
        failure.bind(this))

        function success(data) {
            console.log(data)
            if (data.isSuccess==true)
            {
                this.IsTokenValid();
            } 
            else
            {
                 this.nav.setRoot(VersionErrorPage);
            } 
        }
        function failure(error) {
        }
    }
    /**
     * 驗證token
     */
    IsTokenValid() {
        this.log.info(`--目前位置(登入)=>[welcome.ts]`);

        var temp = this.local.get<User>('user');

        if (!temp) return;

        if (!temp.Token) return;

        this.log.info(`--驗證token是否合法`);

        //驗證token是否合法
        this.userRepo.IsTokenValid(temp.Token,
            success.bind(this),
            failure.bind(this))

        //成功就自動登入
        function success(data) {

            this.log.info(data);

            this.excute(temp.Account, temp.Password);
        }

        //失敗就清除本機緩存
        function failure(error) {

            this.log.error(error);

            this.local.set('user', null);

        }
    }

}
