import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NavController } from 'ionic-angular';

/* NgStore */
import { NgRedux, select } from '@angular-redux/store';
import { Observable } from 'rxjs/Rx';
import { IAppState } from '../../rootReducer';

/**
 * 時間軸
 */
@Component({
    selector: 'wizard',
    templateUrl: 'wizard.html'
})
export class Wizard implements OnInit {

    @select(['ptcDetail', 'slide']) items$: Observable<Array<any>>;

    @Output() slideDirect = new EventEmitter();

    constructor(private nav: NavController,
                private ngRedux: NgRedux<IAppState>) {}


    /**
     * 頁面早載入完畢時
     */
    ngOnInit() { };


}
