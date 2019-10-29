import { Component } from '@angular/core';
import { NavController, NavParams, AlertController,ToastController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { LocalStorage } from "../../services/localStorage";

/* helper */
import { LogHelper } from "../../helper/logHelper";

/* NgStore */
import { PtcDetailAction } from '../../pages/ptc-detail/ptc-detail-action'


@Component({
    selector: 'vender-accept-detail',
    templateUrl: 'vender-accept-detail.html'
})
export class VenderAcceptDetailPage {

    public item: any = {};

    public Sn: string;

    constructor(private nav: NavController,
        private toast:ToastController,
        private log: LogHelper,
        private local: LocalStorage,
        private action: PtcDetailAction,
        private navParam: NavParams,
        private callogRepo: CallogRepository,
        private alertCtrl: AlertController) {

        this.Sn = navParam.get('Sn');
    }

    /**
     * 當頁面載入完畢時執行
     */
    ngOnInit() {

        this.log.info(`--目前位置(廠商認養工單-明細)=>[vendorAcceptDetail.ts]`);

        this.get();

    }

    /**
     * 取得案件明細
     */
    get() {

        this.log.info(`--準備取得待認養案件明細`);

        this.callogRepo.GetByAccept(
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

            this.action.getDispatch(data.element);
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
    }
}
