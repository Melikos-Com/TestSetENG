import { PTC_LIST_ACTION } from './ptc-list-action';

export interface IPtcListState {
    items?: Array<CallogResultApiViewModel>,
   
}

const INIT_STATE: IPtcListState = {
    items: [] as Array<CallogResultApiViewModel>,
   
};

export function PtcListReducer(state = INIT_STATE, action: any): IPtcListState {

    switch (action.type) {
        case PTC_LIST_ACTION.GETLIST:
            return {
                items: action.payload,              
            }
      
        default:
            return state;
    }
}