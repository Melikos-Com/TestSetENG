import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const VENDER_ASSIGN_ACTION = {
    GETLIST: 'VENDER_ASSIGN_GETLIST',
    GET: 'VENDER_ASSIGN_GET',
};


@Injectable()
export class VenderAssignAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getListDispatch(items: Array<CallogResultApiViewModel>) {
        this.ngRedux.dispatch(this.getList(items));
    }

    getDispatch(item: CallogDetailApiViewModel) {
        this.ngRedux.dispatch(this.get(item));
    }


    private getList(items: Array<CallogResultApiViewModel>) {
        return {
            type: VENDER_ASSIGN_ACTION.GETLIST,
            payload: items
        };
    }

    private get(item: CallogDetailApiViewModel) {
        return {
            type: VENDER_ASSIGN_ACTION.GET,
            payload: item
        };
    }
   


}