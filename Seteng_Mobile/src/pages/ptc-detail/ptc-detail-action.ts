import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const PTC_DETAIL_ACTION = {
    GET: 'PTC_DETAIL_GETLIST',
    SLIDECHANGE: 'PTC_DETAIL_SLIDE_CHANGE',
};


@Injectable()
export class PtcDetailAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getDispatch(item: CallogDetailApiViewModel) {
        this.ngRedux.dispatch(this.get(item));
    }

    getSlideDispatch(slide: Array<any>) {

        this.ngRedux.dispatch(this.getSlide(slide));
    }

    private get(item: CallogDetailApiViewModel) {
        return {
            type: PTC_DETAIL_ACTION.GET,
            payload: item
        };
    }

    private getSlide(slide: Array<any>) {
        return {
            type: PTC_DETAIL_ACTION.SLIDECHANGE,
            payload: slide

        }


    }

}