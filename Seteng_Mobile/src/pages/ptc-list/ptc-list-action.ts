import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const PTC_LIST_ACTION = {

    GETLIST: 'PTC_LIST_GETLIST',
  
};


@Injectable()
export class PtcListAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getListDispatch(items: Array<CallogResultApiViewModel>) {
        this.ngRedux.dispatch(this.getList(items));
    }


    private getList(items: Array<CallogResultApiViewModel>) {
        return {
            type: PTC_LIST_ACTION.GETLIST,
            payload: items
        };
    }


}