import { Component } from '@angular/core';
import { NavController, AlertController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/* service */
import { LocalStorage } from "../../services/localStorage";

/*helper */
import { LogHelper } from "../../helper/logHelper";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* NgStore */
import { NgRedux } from '@angular-redux/store';

import { PtcListAction } from '../../pages/ptc-list/ptc-list-action'
import { IAppState } from '../../rootReducer';



/**
 * 廠商執行案件
 */
@Component({
    selector: 'vender-confirm',
    templateUrl: 'vender-confirm.html'
})
export class VenderConfirmPage {

    public user: User;

    //資料集合
    public items: Array<CallogResultApiViewModel> = [];

    //資料總筆數
    public totalCount: number = 0;

    //續載參數
    public InfiniteEnabled: Boolean = true;

    //功能分類
    public features = 0;

    //當前頁碼
    public pageIndex = 0;


    constructor(private nav: NavController,
        private local: LocalStorage,
        private log: LogHelper,
        private callogRepo: CallogRepository,
        private ngRedux: NgRedux<IAppState>,
        private action: PtcListAction,
        private alertCtrl: AlertController) { }

    /**
     * 頁面即將載入時,呼叫的method
     */
    ionViewWillEnter($event) {

        this.pageIndex = 0;

        this.items = [];

        this.log.info(`--目前位置(技師執行工單)=>[vendorConfirm.ts]`);

        this.getList($event);
    };

    /**
     * 重載資料
     */
    doRefresh($event) {

        this.pageIndex = 0
        this.items = [];

        this.InfiniteEnabled = true;

        this.getList($event);


    }

    /**
        * scroll 持續載入
        * @param $event
        */
    continuousLoading($event) {

        this.pageIndex++;

        this.getList($event);

    }

    /**
     * 取得待銷案件清單
     */
    getList($event) {


        this.log.info(`--準備取得待銷案件清單`);

        this.callogRepo.GetHasTechnicianCallog(
            this.pageIndex, 
            $event== null,
            success.bind(this),
            failure.bind(this),
        );


        function success(data: JsonResult<Array<CallogResultApiViewModel>>): void {

            this.log.info(data);

            var awaitUploads = (this.local.get('logs') || []) as Array<CallogDetailApiViewModel>;

            data.element.forEach(x => {

                x.IsWait = awaitUploads.some(y => y.Sn == x.Sn);

                this.items.push(x)

            });

            this.action.getListDispatch(this.items);

            this.totalCount = data.totalCount;

            //註銷事件
            if ($event) $event.complete();

            //如果回傳總數小於等於目前容器內的,就停用InfiniteScroll
            if ((<JsonResult<Array<CallogResultApiViewModel>>>(data)).totalCount <= this.items.length)
                this.InfiniteEnabled = false;

        };

        function failure(error: HttpResponse): void {

            //如果使用者驗證失效
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


}
