import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';

/* pages  */
import { VenderConfirmPage } from "../vender-confirm/vender-confirm";
import { VenderAcceptPage } from "../vender-accept/vender-accept";
import { VenderAssignPage } from "../vender-assign/vender-assign";

/* service */
import { RootScope } from "../../services/rootScope";

/* helper */
import { LogHelper } from "../../helper/logHelper";

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage {

    /**使用者資訊 */
    public user: User;

    /**強制stype設定 */
    public itemHeigh: SafeStyle;

    /**使用者可檢視menu item */
    public mainMenus: Array<any> = [];

    /* 案件數量 */
    public item: any = {};

    /**主選單項目 */
    private pages = [
        {
            title: '待認養案件',
            feature: 'Accept',
            icon: 'search',
            count: 0,
            component: VenderAcceptPage,
            IsVendor: false,
            background: this.sanitizer.bypassSecurityTrustStyle("url('./assets/img/accepted.jpg')")
        },
        {
            title: '已認養案件',
            feature: 'Confirm',
            icon: 'body',
            count: 0,
            component: VenderConfirmPage,
            IsVendor: false,
            background: this.sanitizer.bypassSecurityTrustStyle("url('./assets/img/finish.jpg')")
        },
        {
            title: '未完修案件管理',
            feature: 'Assign',
            icon: 'logo-buffer',
            count: 0,
            component: VenderAssignPage,
            IsVendor: true,
            background: this.sanitizer.bypassSecurityTrustStyle("url('./assets/img/push.jpg')")
        },
       
    ];


    constructor(private nav: NavController,
        private log: LogHelper,
        private callogRepo: CallogRepository,
        private rootScope: RootScope,
        private sanitizer: DomSanitizer) {

        this.user = rootScope.get<User>('user')

        //計算每個項目的高
        if (!this.user.IsVendor) {
            this.pages.forEach((val, idx, array) => {
                if (val.IsVendor == false)
                    this.mainMenus.push(val);
            });
        } else {
            this.mainMenus = this.pages;
        }

        //項目-高
        this.itemHeigh = this.sanitizer.bypassSecurityTrustStyle(`calc(91vh/${this.mainMenus.length})`);
    }


    /**
    * 頁面載入完畢時執行
    */
    //ngOnInit() {
      ionViewWillEnter() {
        this.log.info(`--目前位置(首頁)=>[home.ts]`);

        this.Get();

    };

    /**
     * 暫存頁面再次被叫醒
     */
    ionViewDidEnter(){

        this.log.info(`--喚回目前位置(首頁)=>[home.ts]`);

        this.Get();
    }

    /**
     * 開啟page
     */
    openPage(page) {

        this.log.info(`--openPage ${page.title}`);
        this.nav.push(page.component);


    }

    /**
     * 取得案件相關數量
     */
    Get() {

        this.log.info(`--準備取得案件數量資訊`);

        this.callogRepo.GetVendorGetNewsCount(success.bind(this), failure.bind(this));

        function success(data: JsonResult<CallogCountApiViewModel>): void {

            this.log.info(data);

            this.item = data;

            for (let page of this.pages) {
                switch (page.feature) {
                    case 'Accept':
                        page.count = data.element.AwaitAdoptCount;
                        break;

                    case 'Confirm':
                        page.count = data.element.HasTechnicianCount;
                        break;

                    case 'Assign':
                        page.count = data.element.AwaitAssignCount;
                        break;
                    // case 'Pending':
                    //     page.count = 6;
                    //     break;
                }
            }


        };

        function failure(error: HttpResponse): void {

            this.log.info(error.statusText);

        };

    }

}
