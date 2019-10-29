import { Component, OnInit } from '@angular/core';
import { NavController, NavParams, AlertController, ToastController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/* NgStore */
import { PtcDetailAction } from '../../pages/ptc-detail/ptc-detail-action'

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { LocalStorage } from "../../services/localStorage";

/* helper */
import { LogHelper } from "../../helper/logHelper";


@Component({
    selector: 'vender-assign-detail',
    templateUrl: 'vender-assign-detail.html'
})
export class VenderAssignDetailPage implements OnInit {

    public item: any = {};

    public Sn: string;

    constructor(private toast: ToastController,
        private nav: NavController,
        private navParam: NavParams,
        private log: LogHelper,
        private local: LocalStorage,
        private action: PtcDetailAction,
        private callogRepo: CallogRepository,
        private alertCtrl: AlertController) {

        this.Sn = navParam.get('Sn');

    }

    /**
    * 頁面載入完畢時執行
    */
    ngOnInit() {

        this.log.info(`--目前位置(廠商轉派工單-明細)=>[vendorAcceptDetail.ts]`);

        this.get();

    };
    /**
  * 取得案件明細
  */
    get() {

        this.log.info(`--準備取得轉派案件明細`);

        this.callogRepo.GetNoFinish(
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
