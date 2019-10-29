import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const VENDER_SEARCH_ACTION = {
    GETLIST: 'VENDER_SEARCH_GETLIST',
    GET: 'VENDER_SEARCH_GET',
};


@Injectable()
export class VenderSearchAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getListDispatch(items: Array<CallogResultApiViewModel>) {
        this.ngRedux.dispatch(this.getList(items));
    }

    getDispatch(item: CallogDetailApiViewModel) {
        this.ngRedux.dispatch(this.get(item));
    }


    private getList(items: Array<CallogResultApiViewModel>) {
        return {
            type: VENDER_SEARCH_ACTION.GETLIST,
            payload: items
        };
    }

    private get(item: CallogDetailApiViewModel) {
        return {
            type: VENDER_SEARCH_ACTION.GET,
            payload: item
        };
    }
   


}