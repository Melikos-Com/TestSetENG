import { Component, Input, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';


/**
 * 案件-清單內容
 */
@Component({
    selector: 'ptc-item',
    templateUrl: 'ptc-item.html'
})
export class PtcItem implements OnInit {


    @Input() item: CallogResultApiViewModel;

    @Input() class: string = 'list';

     /* 時間軸 */
     public TimePointName = [
        { name: '立案', class: '' },
        { name: '派工', class: '' },
        { name: '受理', class: '' },
        { name: '到店', class: '' },
        { name: '完成', class: '' },
    ];

    constructor(private nav: NavController) { 
        // this.TimePointName.forEach(element => {
        //     if (this.item.Timepoint=="0")
        //             element.class = '';
        // });
        
    }


    /**
     * 頁面早載入完畢時
     */
    ngOnInit() {
        if (this.item.Enable == true)
        {
            switch(this.item.Timepoint)
            {
                case '0' : 
                    this.TimePointName[0].class ='completed';
                    break;
                case '1' : 
                    this.TimePointName[0].class ='completed';
                    this.TimePointName[1].class ='completed';
                    break;
                case '2' : 
                    this.TimePointName[0].class ='completed';
                    this.TimePointName[1].class ='completed';
                    this.TimePointName[2].class ='completed';
                    break;
                case '3' : 
                    this.TimePointName[0].class ='completed';
                    this.TimePointName[1].class ='completed';
                    this.TimePointName[2].class ='completed';
                    this.TimePointName[3].class ='completed';
                    break;
                case '4' : 
                    this.TimePointName[0].class ='completed';
                    this.TimePointName[1].class ='completed';
                    this.TimePointName[2].class ='completed';
                    this.TimePointName[3].class ='completed';
                    this.TimePointName[4].class ='completed';
                    break;
            }
        }
     };
}
