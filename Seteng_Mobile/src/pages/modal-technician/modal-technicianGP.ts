import { Component } from '@angular/core';
import { ViewController, ModalController } from 'ionic-angular';

/* repository */
import { TechnicianRepository } from '../../repository/technicianRepository';

/* modal */
import { ModalTechnicianResultPage } from './modal-technician-result'

/* helper */
import { LogHelper } from '../../helper/logHelper'


@Component({
    selector: 'modal-technicianGP',
    templateUrl: 'modal-technicianGP.html'
})
export class ModalTechnicianGPPage {

    //查詢方式
    public type: number = 0;

    //資料集合(群組)
    public groups: Array<TechnicianGPResultApiViewModel> = [];

    //資料集合(技師)
    public technicians: Array<TechnicianResultApiViewModel> = [];

    //當前頁碼(群組的)
    public pageIndexGP = 0;

    //當前頁碼(技師的)
    public pageIndexTH = 0;

    //關鍵字查詢(群組的)
    public keywordGP: string = '';

    //關鍵字查詢(技師的)
    public keywordTH: string = '';


    public selectedAllGP: Boolean = false;

    public selectedAllTH: Boolean = false;

    constructor(private log : LogHelper,
        private modal: ModalController,
        private viewCtrl: ViewController,
        private technicianRepo: TechnicianRepository) { }

    /**
    * 頁面載入完畢時執行
    */
    ngOnInit() {

        this.getGroupList();
        this.getTechnicianList();

    };


    /**
     * 按下查詢時執行(群組的)
     */
    filterGP() {

        this.groups = [];
        this.pageIndexGP = 0;

        this.getGroupList();
    }

    /**
    * 按下查詢時執行(技師的)
    */
    filterTH() {

        this.technicians = [];
        this.pageIndexTH = 0;

        this.getTechnicianList();
    }

    /**
     * 取得群組
     */
    getGroupList() {

        this.groups = [];

        this.technicianRepo.GetGroupList(
            this.pageIndexGP,
            this.keywordGP,
            success.bind(this),
            failure.bind(this),
        )
        
        function success(data: JsonResult<Array<TechnicianGPResultApiViewModel>>) {

            this.log.info(data);

            if (!data.element) return;

            data.element.forEach(x => this.groups.push(x));
        }

        function failure(error: string) {

            this.log.error(error);

        }
    }

    /**
     * 取得技師
     */
    getTechnicianList() {

        this.technicians=[];

        this.technicianRepo.GetList(
            this.pageIndexTH,
            this.keywordTH,
            success.bind(this),
            failure.bind(this),
        )

        function success(data: JsonResult<Array<TechnicianResultApiViewModel>>) {

            this.log.info(data);

            if (!data.element) return;

            data.element.forEach(x => this.technicians.push(x));
        }

        function failure(error: string) {

            this.log.error(error);

        }

    }

    /**
    * scroll 持續載入(群組的)
    * @param $event
    */
    continuousLoadingGP($event) {

        this.pageIndexGP++;

        this.groups=[];

        this.getGroupList();

        $event.complete();

    }
    /**
    * scroll 持續載入(技師的)
    * @param $event
    */
    continuousLoadingTH($event) {

        this.pageIndexTH++;

        this.technicians=[];

        this.getTechnicianList();

        $event.complete();

    }

    /**
     * 關閉modal
     */
    closeModal() {

        this.viewCtrl.dismiss();
    }

    /**
     * 確認選擇(篩選技師,並導向確認推播頁面)
     */
    confirm() {

        var technicians: Array<TechnicianResultApiViewModel> = [];

        this.technicians
            .filter(x => x.IsCheck)
            .forEach(x => technicians.push(x));

        var groups: Array<TechnicianGPResultApiViewModel> = [];

        this.groups
            .filter(x => x.IsCheck)
            .forEach(x => groups.push(x));

        this.technicianRepo.MergeTechnician(
            groups,
            technicians,
            success.bind(this),
            failure.bind(this)
        );


        function success(data: JsonResult<Array<TechnicianResultApiViewModel>>) {

            // show modal
            let modal = this.modal.create(ModalTechnicianResultPage, {
                groups: this.groups.filter(x => x.IsCheck),
                technicians: data.element,
            });

            // listen for modal close
            modal.onDidDismiss(data => {

                if (!data) return;

                this.viewCtrl.dismiss(data);

            });

            modal.present();
        }

        function failure(error: string) {
            
          this.log.error(error);

        }

    }

    allCheckTH = () => this.selectedAllTH = !this.selectedAllTH;

    allCheckGP = () => this.selectedAllGP = !this.selectedAllGP;
}