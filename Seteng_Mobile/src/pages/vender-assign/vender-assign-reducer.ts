import { VENDER_ASSIGN_ACTION   } from './vender-assign-action';

export interface IVenderAssignState {
    items?: Array<CallogResultApiViewModel>,
    item?: CallogDetailApiViewModel
    
}

const INIT_STATE: IVenderAssignState = {
    items: [] as Array<CallogResultApiViewModel>,
    item: {} as CallogDetailApiViewModel
};

export function VenderAssignReducer(state = INIT_STATE, action: any): IVenderAssignState {

    switch (action.type) {
        case VENDER_ASSIGN_ACTION.GETLIST:
            return {
                items: action.payload,             
            }
        case VENDER_ASSIGN_ACTION.GET:
            return {
                item: action.payload,
            }
        default:
            return state;
    }
}