import { VENDER_ACCEPT_ACTION   } from './vender-accept-action';

export interface IVenderAcceptState {
    items?: Array<CallogResultApiViewModel>,
    item?: CallogDetailApiViewModel
    
}

const INIT_STATE: IVenderAcceptState = {
    items: [] as Array<CallogResultApiViewModel>,
    item: {} as CallogDetailApiViewModel
};

export function VenderAcceptReducer(state = INIT_STATE, action: any): IVenderAcceptState {

    switch (action.type) {
        case VENDER_ACCEPT_ACTION.GETLIST:
            return {
                items: action.payload,             
            }
        case VENDER_ACCEPT_ACTION.GET:
            return {
                item: action.payload,
            }
        default:
            return state;
    }
}