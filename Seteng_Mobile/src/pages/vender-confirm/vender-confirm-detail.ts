import { Component } from '@angular/core';
import { NavController, NavParams, AlertController, ToastController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { LocalStorage } from "../../services/localStorage";

/* NgStore */
import { PtcDetailAction } from '../../pages/ptc-detail/ptc-detail-action'

/*helper */
import { LogHelper } from "../../helper/logHelper";



@Component({
    selector: 'vender-confirm-detail',
    templateUrl: 'vender-confirm-detail.html'
})
export class VenderConfirmDetailPage {

    public item: any = {};

    public Sn: string;

    public IsSISOVender: Boolean;

    constructor(private nav: NavController,
        private toast: ToastController,
        private local: LocalStorage,
        private log: LogHelper,
        private navParam: NavParams,
        private action: PtcDetailAction,
        private callogRepo: CallogRepository,
        private alertCtrl: AlertController) {

        this.Sn = navParam.get('Sn');
        this.IsSISOVender = navParam.get('IsSISOVender');
    }


    /**
     * 當頁面載入完畢時執行
     */
    ngOnInit() {

        this.log.info(`--目前位置(技師執行工單明細)=>[vendorConfirmDetail.ts]`);

        this.get();
    }


    /**
     * 取得案件明細
     */
    get() {


        this.log.info(`--準備取得技師執行工單明細`);

        this.callogRepo.GetByConfirm(
            this.Sn,
            success.bind(this),
            failure.bind(this)
        );


        function success(data: JsonResult<CallogDetailApiViewModel>): void {

            this.log.info(data);

            if (!data.isSuccess) {
                this.showToast(data.message, false);
                this.nav.pop();
                return;
            }

            var awaitUploads = (this.local.get('logs') || []) as Array<CallogDetailApiViewModel>;

            var temp = awaitUploads.find(y => y.Sn == data.element.Sn);

            if (temp) {
                temp.IsWait = true;
                this.action.getDispatch(temp)
            } else {
                this.action.getDispatch(data.element)
            }
        };

        function failure(error: HttpResponse): void {

            //如果執行API TOKEN無效
            if (error.status == 401) {
                this.local.set('user', null);
                let alert = this.alertCtrl.create({
                    title: '身份驗証',
                    subTitle: '身份資訊失效，請重新登入',
                    buttons: ['OK']
                });
                alert.present();

                this.nav.setRoot(WelcomePage);


            }

        };
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
    };
}
