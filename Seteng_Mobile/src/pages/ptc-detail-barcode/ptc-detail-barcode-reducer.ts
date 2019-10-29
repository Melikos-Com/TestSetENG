import { PTC_DETAIL_BARCODE_ACTION } from './ptc-detail-barcode-action';

export interface IPtcDetailBarcodeState {
    item?: CallogDetailApiViewModel,
    slide?: Array<any>

}

const INIT_STATE: IPtcDetailBarcodeState = {
    item: {} as CallogDetailApiViewModel,
    slide: []

};

export function PtcDetailBarcodeReducer(state = INIT_STATE, action: any): IPtcDetailBarcodeState {

    switch (action.type) {
        case PTC_DETAIL_BARCODE_ACTION.GET:
            return {
                item: action.payload,
                slide: state.slide,
            }

        case PTC_DETAIL_BARCODE_ACTION.SLIDECHANGE:
            return {
                item: state.item,
                slide: action.payload,
            }

        default:
            return state;
    }
}
