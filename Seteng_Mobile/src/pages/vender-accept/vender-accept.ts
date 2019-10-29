import { Component } from '@angular/core';
import { NavController, ActionSheetController, AlertController } from 'ionic-angular';

/* pages */
import { WelcomePage } from "../welcome/welcome";

/* service */
import { LocalStorage } from "../../services/localStorage";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* helper */
import { LogHelper } from "../../helper/logHelper";

/* NgStore */
import { NgRedux } from '@angular-redux/store';
import { PtcListAction } from '../../pages/ptc-list/ptc-list-action'

/**
 * 廠商受理案件
 */
@Component({
    selector: 'vender-accept',
    templateUrl: 'vender-accept.html'
})
export class VenderAcceptPage {

    public user: User;

    //資料集合
    public items: Array<CallogResultApiViewModel> = [];

    //資料總筆數
    public totalCount: number = 0;

    //當前頁碼
    public pageIndex = 0;

    //續載參數
    public InfiniteEnabled: Boolean = true;

    //功能分類
    public features = 1;

    //排序條件
    public orderText = '排序條件';
    public orderBy = '';

    //排序方式
    public orderTypeText = '排序方式';
    public orderType: number = 0;

    constructor(private nav: NavController,
        private log: LogHelper,
        private local: LocalStorage,
        private actionSheetCtrl: ActionSheetController,
        private callogRepo: CallogRepository,
        private ngRedux: NgRedux<Array<CallogResultApiViewModel>>,
        private action: PtcListAction,
        private alertCtrl: AlertController) { }


    /**
      * 頁面即將載入時,呼叫的method
      */
    ionViewWillEnter($event) {

        this.pageIndex = 0;

        this.items = [];

        this.log.info(`--目前位置(技師認養工單)=>[vendorAccept.ts]`);

        this.getList($event);
    };

    /**
     * 重新載入
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
     * 取得待認養案件清單
     */
    getList($event) {

        this.log.info(`--準備取得待認養案件清單`);

        this.callogRepo.GetAwaitAdoptCallog(
            this.pageIndex,
            this.orderBy,
            this.orderType, 
            $event==null,
            success.bind(this),
            failure.bind(this),
        );

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
    * 排序條件清單
    */
    chooseSortBy() {

        let actionSheet = this.actionSheetCtrl.create({
            buttons: [
                {
                    text: '案件編號',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照案件編號]`);
                        this.orderText = '案件編號';
                        this.orderBy = 'Sn';
                        this.doRefresh(null);

                    }
                },
                {
                    text: '門市名稱',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照門市名稱]`);
                        this.orderText = '門市名稱';
                        this.orderBy = 'Store_Name';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '維修金額',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照維修金額]`);
                        this.orderText = '維修金額';
                        this.orderBy = 'Vnd_Total_Cost';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '設備名稱',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照設備名稱]`);
                        this.orderText = '設備名稱';
                        this.orderBy = 'Asset_Name';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '受理日期',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照受理日期]`);
                        this.orderText = '受理日期';
                        this.orderBy = 'Rcv_Datetime';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '完修日期',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照完修日期]`);
                        this.orderText = '完修日期';
                        this.orderBy = 'Fc_Date';
                        this.doRefresh(null);
                    }
                }
            ]
        });
        actionSheet.present();
    }

    /**
     * 排序方式
     */
    chooseSortTypeBy() {

        let actionSheet = this.actionSheetCtrl.create({
            buttons: [
                {
                    text: '由小到大',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照${this.orderText},由小到大]`);
                        this.orderTypeText = '由小到大';
                        this.orderType = 0;
                        this.doRefresh(null);

                    }
                },
                {
                    text: '由大到小',
                    handler: () => {
                        this.log.info(`--排序待認養案件清單[依照${this.orderText},由大到小]`);
                        this.orderTypeText = '由大到小';
                        this.orderType = 1;
                        this.doRefresh(null);
                    }
                },

            ]
        });
        actionSheet.present();


    }

}
