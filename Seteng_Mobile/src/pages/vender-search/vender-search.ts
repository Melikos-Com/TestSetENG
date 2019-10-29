import { Component, OnInit } from '@angular/core';
import { NavController, ActionSheetController, ModalController, AlertController } from 'ionic-angular';
import { WelcomePage } from "../welcome/welcome";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { RootScope } from "../../services/rootScope";
import { LocalStorage } from '../../services/localStorage';

/*WEB API REQUEST*/
import { CallogApiReqViewModel } from '../../viewModel/WebRequest';

/* NgStore */
import { PtcListAction } from '../../pages/ptc-list/ptc-list-action'

/*helper */
import { LogHelper } from "../../helper/logHelper";

/* pages */
import { ModalConditionsPage } from "../modal-conditions/modal-conditions";

/**
 * 廠商案件查詢
 */
@Component({
    selector: 'vender-search',
    templateUrl: 'vender-search.html'
})
export class VenderSearchPage implements OnInit {

    public user: User;

    //資料集合
    public items: Array<CallogResultApiViewModel> = [];

    //查詢條件
    public condition: CallogApiReqViewModel;

    //續載參數
    public InfiniteEnabled: Boolean = true;

    //資料總筆數
    public totalCount: string = '-';

    //當前頁碼
    public pageIndex = 0;

    //功能分類
    public features = 2;

    //排序條件
    public orderText = '排序條件';
    public orderBy = '';

    //排序方式
    public orderTypeText = '排序方式';
    public orderType: number = 0;

    constructor(private nav: NavController,
        private log: LogHelper,
        private rootScope: RootScope,
        private local: LocalStorage,
        private modal: ModalController,
        private actionSheetCtrl: ActionSheetController,
        private callogRepo: CallogRepository,
        private action: PtcListAction,
        private alertCtrl: AlertController) {

        this.user = rootScope.get<User>('user');

    }


    /**
     * 頁面載入完畢時執行 
     */
    ngOnInit() {

        this.action.getListDispatch([]);
        this.items = [];

        this.log.info(`--目前位置(技師查詢工單)=>[vendorSearch.ts]`);

        this.condition = new CallogApiReqViewModel();
        this.condition.CompCd = this.user.CompCd;
        this.condition.VenderCd = this.user.VenderCd;
        this.setConditions();
    };

    /**
     * 資料重載
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
     * 取得案件清單
     */
    getList($event) {

        this.log.info(`--準備取得案件清單`);

        this.callogRepo.GetList(
            this.condition,
            this.pageIndex,
            this.orderBy,
            this.orderType,
            $event==null,
            success.bind(this),
            failure.bind(this)
        )

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
     * 查詢彈跳視窗
     */
    setConditions() {

        this.log.info(`--取得案件清單,彈出查詢條件popup`);

        // show modal
        let modal = this.modal.create(ModalConditionsPage,
            { condition: this.condition });

        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) return;

            //清空舊資料
            this.items = [];
            this.pageIndex = 0;

            this.InfiniteEnabled = true;

            //查詢清單
            this.getList(null);

        });

        modal.present();

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
                        this.log.info(`--排序待案件清單[依照案件編號]`);
                        this.orderText = '案件編號';
                        this.orderBy = 'Sn';
                        this.doRefresh(null);

                    }
                },
                {
                    text: '門市代號',
                    handler: () => {
                        this.log.info(`--排序待案件清單[依照門市編號]`);
                        this.orderText = '門市編號';
                        this.orderBy = 'Store_Name';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '維修金額',
                    handler: () => {
                        this.log.info(`--排序待案件清單[依照維修金額]`);
                        this.orderText = '維修金額';
                        this.orderBy = 'Vnd_Total_Cost';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '設備名稱',
                    handler: () => {
                        this.log.info(`--排序待案件清單[依照設備名稱]`);
                        this.orderText = '設備名稱';
                        this.orderBy = 'Asset_Name';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '受理日期',
                    handler: () => {
                        this.log.info(`--排序待案件清單[依照受理日期]`);
                        this.orderText = '受理日期';
                        this.orderBy = 'Rcv_Datetime';
                        this.doRefresh(null);
                    }
                },
                {
                    text: '完修日期',
                    handler: () => {
                        this.log.info(`--排序待案件清單[依照完修日期]`);
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
                        this.log.info(`--排序案件清單[依照${this.orderText},由小到大]`);
                        this.orderTypeText = '由小到大';
                        this.orderType = 0;
                        this.doRefresh(null);

                    }
                },
                {
                    text: '由大到小',
                    handler: () => {
                        this.log.info(`--排序案件清單[依照${this.orderText},由大到小]`);
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
