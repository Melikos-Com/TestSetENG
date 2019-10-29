import { VENDER_CONFIRM_ACTION  } from './vender-confirm-action';

export interface IVenderConfirmState {
    items?: Array<CallogResultApiViewModel>,
    item?: CallogDetailApiViewModel
    
}

const INIT_STATE: IVenderConfirmState = {
    items: [] as Array<CallogResultApiViewModel>,
    item: {} as CallogDetailApiViewModel
};

export function VenderConfirmReducer(state = INIT_STATE, action: any): IVenderConfirmState {

    switch (action.type) {
        case VENDER_CONFIRM_ACTION.GETLIST:
            return {
                items: action.payload,      
                item:  state.item,
            }
        case VENDER_CONFIRM_ACTION.GET:
            return {
                items : state.items,
                item: action.payload,
            }
        default:
            return state;
    }
}