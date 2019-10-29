import { Component, Input, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';

/* NgStore */
import { select } from '@angular-redux/store';
import { Observable } from 'rxjs/Rx';

/* pages */
import { VenderConfirmDetailPage } from '../../pages/vender-confirm/vender-confirm-detail';
import { VenderAcceptDetailPage } from '../../pages/vender-accept/vender-accept-detail';
import { VenderSearchDetailPage } from '../../pages/vender-search/vender-search-detail';
import { VenderAssignDetailPage } from '../../pages/vender-assign/vender-assign-detail';

/**
 * 案件-清單外框
 */
@Component({
    selector: 'ptc-list',
    templateUrl: 'ptc-list.html'
})
export class PtcList implements OnInit {

    @select([ 'ptcList', 'items']) items$: Observable<Array<CallogResultApiViewModel>>;

    @Input() features: number;

    constructor(private nav: NavController) {}


    /**
     * 頁面早載入完畢時
     */
    ngOnInit() { };

    /**
     * 頁面結束時,停止訂閱
     */
    ngOnDestroy() {
       
    }

    /**
     * 導向案件明細
     * @param Sn
     */
    viewDetail(item: CallogResultApiViewModel): void {


        switch (this.features) {
              
            case 3:
                this.nav.push(VenderAssignDetailPage, { Sn: item.Sn });
                break
            case 2:
                this.nav.push(VenderSearchDetailPage, { Sn: item.Sn });
                break
            case 1:
                this.nav.push(VenderAcceptDetailPage, { Sn: item.Sn });
                break;
            case 0:
                this.nav.push(VenderConfirmDetailPage, { Sn: item.Sn,IsSISOVender:item.IsSISOVender });
                break;
        }



    }

}
