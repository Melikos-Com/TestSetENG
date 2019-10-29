import { Component } from '@angular/core';
import { NavParams, ViewController } from 'ionic-angular';

/*WEB API REQUEST*/
import { CallogApiReqViewModel } from '../../viewModel/WebRequest';



@Component({
    selector: 'modal-conditions-assign',
    templateUrl: 'modal-conditions-assign.html'
})
export class ModalConditionsAssignPage {

    public item: CallogApiReqViewModel;

    public FiDateStart: string;

    public FiDateEnd: string;

    constructor(private navParam: NavParams,
        private viewCtrl: ViewController,
        private params: NavParams) {
        this.item = params.get('condition');
    }

    /**
    * 頁面載入完畢時執行
    */
    ngOnInit() {

        //預設開始結束
        var currentSDate = this.item.FiDateStart ||  new Date();
        var currentEDate = this.item.FiDateEnd ||  new Date();
        
        //先扣掉時區
        currentSDate.setMinutes(currentSDate.getMinutes() - currentSDate.getTimezoneOffset());
        currentEDate.setMinutes(currentEDate.getMinutes() - currentEDate.getTimezoneOffset()); 

        //如果沒有既有的,代表給預設的,開始日就要扣掉三個月
         if(!this.item.FiDateStart && !this.item.FiDateEnd)
            currentSDate.setMonth(currentSDate.getMonth() - 3);     

        this.FiDateStart = currentSDate.toISOString();   
        this.FiDateEnd = currentEDate.toISOString();
        
    }

    /**
     * 確認並查詢
     */
    search() {

        if (this.FiDateStart) {
            var sDate = new Date(this.FiDateStart.split('T')[0]).setHours(0, 0, 0);
            this.item.FiDateStart = new Date(sDate);
        }
        if (this.FiDateEnd) {
            var eDate = new Date(this.FiDateEnd.split('T')[0]).setHours(0, 0, 0);
            this.item.FiDateEnd = new Date(eDate); 

        }

        this.viewCtrl.dismiss(this.item);
    }


    /**
    * 關閉modal
    */
    closeModal() {
        this.viewCtrl.dismiss();
    }
}
