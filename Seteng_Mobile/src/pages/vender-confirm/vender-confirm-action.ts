import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const VENDER_CONFIRM_ACTION = {
    GETLIST: 'VENDER_CONFIRM_GETLIST',
    GET: 'VENDER_CONFIRM_GET',
};


@Injectable()
export class VenderConfirmAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getListDispatch(items: Array<CallogResultApiViewModel>) {
        this.ngRedux.dispatch(this.getList(items));
    }

    getDispatch(item: CallogDetailApiViewModel) {
        this.ngRedux.dispatch(this.get(item));
    }


    private getList(items: Array<CallogResultApiViewModel>) {
        return {
            type: VENDER_CONFIRM_ACTION.GETLIST,
            payload: items
        };
    }

    private get(item: CallogDetailApiViewModel) {
        return {
            type: VENDER_CONFIRM_ACTION.GET,
            payload: item
        };
    }
   


}