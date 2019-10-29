import { Component } from '@angular/core';
import { NavParams, ViewController } from 'ionic-angular';
/* Repository */
import { CallogRepository } from "../../repository/callogRepository";

@Component({
    selector: 'calloghistory',
    templateUrl: 'calloghistory.html'
})

export class calloghistory {
    
        public items : Array<{CallogCourse}> = [];
        public CompCd: string;
        public Sn: string;
    
        constructor(private view : ViewController,
            private CallRepo: CallogRepository,
            private navParam: NavParams,){
                this.CompCd = navParam.get('CompCd');
                this.Sn = navParam.get('Sn');
        }
    
         /**
         * 頁面載入時
         */
        ngOnInit() {
            this.GetCallogHistory();
        }
         /**
         * 關閉modal
         */
         closeModal() {
          this.view.dismiss();
         }
        /**
         * 取得工程聯絡資訊 
         */
        GetCallogHistory() {
            this.CallRepo.GetCallogHistory(
                    this.CompCd,
                    this.Sn,
                    success.bind(this),
                    failure.bind(this));
            function success(data: JsonResult<CallogCourse[]>): void {
                data.element.forEach(x=> this.items.push(x));
            }
            function failure(error: HttpResponse): void {
                     
            }
        };
    
    }