import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const VENDER_ACCEPT_ACTION = {
    GETLIST: 'VENDER_ACCEPT_GETLIST',
    GET: 'VENDER_ACCEPT_GET',
};


@Injectable()
export class VenderAcceptAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getListDispatch(items: Array<CallogResultApiViewModel>) {
        this.ngRedux.dispatch(this.getList(items));
    }

    getDispatch(item: CallogDetailApiViewModel) {
        this.ngRedux.dispatch(this.get(item));
    }


    private getList(items: Array<CallogResultApiViewModel>) {
        return {
            type: VENDER_ACCEPT_ACTION.GETLIST,
            payload: items
        };
    }

    private get(item: CallogDetailApiViewModel) {
        return {
            type: VENDER_ACCEPT_ACTION.GET,
            payload: item
        };
    }
   


}