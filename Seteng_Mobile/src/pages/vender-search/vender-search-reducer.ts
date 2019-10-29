import {VENDER_SEARCH_ACTION   } from './vender-search-action';

export interface IVenderSearchState {
    items?: Array<CallogResultApiViewModel>,
    item?: CallogDetailApiViewModel
    
}

const INIT_STATE: IVenderSearchState = {
    items: [] as Array<CallogResultApiViewModel>,
    item: {} as CallogDetailApiViewModel
};

export function VenderSearchReducer(state = INIT_STATE, action: any): IVenderSearchState {

    switch (action.type) {
        case VENDER_SEARCH_ACTION.GETLIST:
            return {
                items: action.payload,             
            }
        case VENDER_SEARCH_ACTION.GET:
            return {
                item: action.payload,
            }
        default:
            return state;
    }
}