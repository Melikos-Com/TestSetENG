import { Component } from '@angular/core';
import { NavController, AlertController, ModalController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/*WEB API REQUEST*/
import { CallogApiReqViewModel } from '../../viewModel/WebRequest';

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { RootScope } from "../../services/rootScope";
import { LocalStorage } from "../../services/localStorage";

/* helper */
import { LogHelper } from "../../helper/logHelper";

/* NgStore */
import { PtcListAction } from '../../pages/ptc-list/ptc-list-action'

/* pages */
import { ModalConditionsAssignPage } from "../modal-conditions/modal-conditions-assign";

/**
 * 廠商分派技師
 */
@Component({
    selector: 'vender-assign',
    templateUrl: 'vender-assign.html'
})
export class VenderAssignPage {
    public user: User;

    //資料集合
    public items: Array<CallogResultApiViewModel> = [];

    //資料總筆數
    public totalCount: number = 0;

    //續載參數
    public InfiniteEnabled: Boolean = true;

    //功能分類
    public features = 3;

    //當前頁碼
    public pageIndex = 0;

    //查詢條件
    public condition: CallogApiReqViewModel = new CallogApiReqViewModel();


    constructor(private nav: NavController,
        private local: LocalStorage,
        private log: LogHelper,
        private modal: ModalController,
        private callogRepo: CallogRepository,
        private action: PtcListAction,
        private alertCtrl: AlertController,
        private rootScope: RootScope) {
        
        this.user = rootScope.get<User>('user');
    }

   

    /**
      * 頁面即將載入時,呼叫的method
      */
    ionViewWillEnter($event) {

        this.pageIndex = 0;

        this.items = [];

        this.log.info(`--目前位置(廠商案件管理)=>[vendorAssign.ts]`);
        this.condition.CompCd = this.user.CompCd;
        this.condition.VenderCd = this.user.VenderCd;
        this.getVenderManagerCallog($event);
    };

    /**
     * 重新載入
     */
    doRefresh($event) {

        this.pageIndex = 0
        this.items = [];

        this.InfiniteEnabled = true;
        this.condition.CompCd = this.user.CompCd;
        this.condition.VenderCd = this.user.VenderCd;
        this.getVenderManagerCallog($event);
    }


    /**
  * scroll 持續載入
  * @param $event
  */
    continuousLoading($event) {

        this.pageIndex++;

        this.getVenderManagerCallog($event);


    }
    /**
     * 取得清單
     */
    getVenderManagerCallog($event) {

        this.callogRepo.GetVenderManagerCallog(
            this.pageIndex,
            this.condition, 
            $event== null,
            success.bind(this),
            failure.bind(this),
        )

        /**
        * 成功時執行的
        * @param data
        */
        function success(data: JsonResult<Array<CallogResultApiViewModel>>): void {

            this.log.info(data);

            data.element.forEach(x => this.items.push(x));

            this.action.getListDispatch(this.items);

            this.totalCount = data.totalCount;

            //註銷事件
            if ($event) $event.complete();

            //如果回傳總數小於等於目前容器內的,就停用InfiniteScroll
            if ((<JsonResult<Array<CallogResultApiViewModel>>>(data)).totalCount <= this.items.length)
                this.InfiniteEnabled = false;


        };


        /**
         * 失敗時執行的
         * @param error
         */
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

    /**
    * 查詢彈跳視窗
    */
    setConditions() {

        this.log.info(`--取得案件清單,彈出查詢條件popup`);

        // show modal
        let modal = this.modal.create(ModalConditionsAssignPage, { condition: this.condition });

        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) return;

            this.condition = data;


            //清空舊資料
            this.items = [];
            this.pageIndex = 0;
            this.InfiniteEnabled = true;
            this.getVenderManagerCallog(null);

        });


        modal.present();

    }
}
