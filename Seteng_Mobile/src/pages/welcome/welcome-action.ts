import { NgRedux } from '@angular-redux/store';
import { Injectable } from '@angular/core';



export const WELCOME_ACTION = {
    GET: 'WELCOME_SET_USER',
  
};


@Injectable()
export class WelcomeAction {
    constructor(private ngRedux: NgRedux<any>) { }

    getDispatch(item: User) {
        this.ngRedux.dispatch(this.get(item));
    }


    private get(item: User) {
        return {
            type: WELCOME_ACTION.GET,
            payload: item
        };
    }

}