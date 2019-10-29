import { Component } from '@angular/core';
import { AlertController, ViewController, NavParams } from 'ionic-angular';

/* repository */
import { TechnicianRepository } from '../../repository/technicianRepository';

/*helper */
import { LogHelper } from '../../helper/logHelper';

@Component({
    selector: 'modal-technician',
    templateUrl: 'modal-technician.html'
})
export class ModalTechnicianPage {

    //案件編號
    public Sn: string = '';
    //門市名稱
    public StoreName: string = '';
    //受理人
    public AcceptedName: string = '';
    //受理人帳號
    public AcceptedAccount: string = '';
    //設備名稱
    public AssetName: string = '';

    //推撥對象
    public pushUser: TechnicianResultApiViewModel;

    //關鍵字
    public keyword = '';

    //當前頁碼
    public pageIndex = 0;

    //資料總筆數
    public totalCount: number = 0;

    //資料集合
    public items: Array<TechnicianResultApiViewModel> = [];


    constructor(private navParam: NavParams,
        private log: LogHelper,
        private viewCtrl: ViewController,
        private technicianRepo: TechnicianRepository,
        private alert: AlertController) {

        this.Sn = navParam.get('Sn');
        this.AssetName = navParam.get('AssetName');
        this.AcceptedName = navParam.get('AcceptedName');
        this.StoreName = navParam.get('StoreName');
        this.AcceptedAccount = navParam.get('AcceptedAccount');
    }

    /**
    * 頁面載入完畢時執行
    */
    ngOnInit() {

        this.getList();

    };

    /**
    * 按下查詢時執行
    */
    filter() {

        this.items = [];
        this.pageIndex = 0;

        this.getList();
    }

    /**
    * 取得技師清單
    */
    getList() {

        this.items = [];
        
        this.technicianRepo.GetList(
            this.pageIndex,
            this.keyword,
            success.bind(this),
            failure.bind(this),
        );


        function success(data: JsonResult<Array<TechnicianResultApiViewModel>>): void {

            this.log.info(data);

            if (!data.element) return;

            data.element.filter(x => x.Account != this.AcceptedAccount).forEach(x => this.items.push(x));


        };

        function failure(error: string): void {

            this.log.error(error);

        };

    }


    /**
     * 選擇技師
     * @param data
     */
    check(data: TechnicianResultApiViewModel) {
        this.items.forEach(x => x.IsCheck = false);

        this.pushUser = data;
        this.items.filter(x => x == data).forEach(x => x.IsCheck = true);

    }

    /**
     * 關閉modal
     */
    closeModal() {
        this.viewCtrl.dismiss();
    }
    /**
         * 選擇技師
         * @param data
         */
    confirm() {

        if (!this.pushUser) return;

        let alert = this.alert.create({
            title: '確認指派技師',
            message: `<div>案件編號:${this.Sn}</div>
                      <div>門市:${this.StoreName}</div>
                      <div>設備:${this.AssetName}</div>
                      <div>技師:${this.pushUser.Name}</div>`,
            buttons: [
                {
                    text: '取消',
                    role: 'cancel',
                    cssClass: 'alertbuttonNo',
                    handler: () => {

                    }
                },
                {
                    text: '確認',
                    cssClass: 'alertbuttonSubmit',
                    handler: () => {
                        this.viewCtrl.dismiss(this.pushUser);
                    }
                }
            ]
        });
        alert.present();
    }

    /**
    * scroll 持續載入
    * @param $event
    */
    continuousLoading($event) {

        this.pageIndex++;

        this.getList();

        $event.complete();

    }



}
