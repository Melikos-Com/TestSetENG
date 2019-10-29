import { PTC_DETAIL_ACTION } from './ptc-detail-action';

export interface IPtcDetailState {
    item?: CallogDetailApiViewModel,
    slide?: Array<any>

}

const INIT_STATE: IPtcDetailState = {
    item: {} as CallogDetailApiViewModel,  
    slide: []

};

export function PtcDetailReducer(state = INIT_STATE, action: any): IPtcDetailState {

    switch (action.type) {
        case PTC_DETAIL_ACTION.GET:
            return {
                item: action.payload,
                slide: state.slide,
            }

        case PTC_DETAIL_ACTION.SLIDECHANGE:
            return {
                item: state.item,
                slide: action.payload,
            }
     
        default:
            return state;
    }
}