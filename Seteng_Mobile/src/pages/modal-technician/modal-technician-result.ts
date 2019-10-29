import { Component } from '@angular/core';
import { AlertController, ViewController, NavParams } from 'ionic-angular';

@Component({
    selector: 'modal-technician-result',
    templateUrl: 'modal-technician-result.html'
})
export class ModalTechnicianResultPage {

    //查詢方式
    public type: number = 0;

    //資料集合(群組)
    public groups: Array<TechnicianGPResultApiViewModel> = [];

    //資料集合(技師)
    public technicians: Array<TechnicianResultApiViewModel> = [];


    constructor(private navParam: NavParams,
                private viewCtrl: ViewController,
                private alert: AlertController) {

        this.groups = navParam.get('groups');
        this.technicians = navParam.get('technicians');
      
    }

    /**
    * 頁面載入完畢時執行
    */
    ngOnInit() {};


    /**
     * 關閉modal
     */
    closeModal() {
        this.viewCtrl.dismiss();
    }

    /**
     * 確認選擇
     */
    confirm() {
        if (this.groups.length==0 && this.technicians.length == 0) {
            this.ShowAlert("請選擇技師或群組");
            return;
        }
        this.viewCtrl.dismiss({
            groups: this.groups,
            technicians: this.technicians
        });

    }
    //產生提示視窗
    ShowAlert(errorMsg: String): void {
        let alert = this.alert.create({
            title: '錯誤',
            subTitle: `${errorMsg}`,
            buttons: [
                {
                    text: 'OK',
                    handler: data => {
                        console.log('Cancel clicked');
                    }
                }
            ]
        });
        alert.present();
    }

}