import { combineReducers } from 'redux';
import { IVenderConfirmState, VenderConfirmReducer } from './pages/vender-confirm/vender-confirm-reducer';
import { IVenderSearchState, VenderSearchReducer } from './pages/vender-search/vender-search-reducer';
import { IVenderAcceptState, VenderAcceptReducer } from './pages/vender-accept/vender-accept-reducer';
import { IVenderAssignState, VenderAssignReducer } from './pages/vender-assign/vender-assign-reducer';
import { IPtcDetailState, PtcDetailReducer } from './pages/ptc-detail/ptc-detail-reducer';
import { IPtcDetailBarcodeState, PtcDetailBarcodeReducer } from './pages/ptc-detail-barcode/ptc-detail-barcode-reducer';
import { IPtcListState, PtcListReducer } from './pages/ptc-list/ptc-list-reducer';
import { IWelcomeState, WelcomeReducer } from './pages/welcome/welcome-reducer';
export class IAppState {

    venderConfirm?: IVenderConfirmState;
    venderSearch?: IVenderSearchState;
    venderAccept?: IVenderAcceptState;
    venderAssign?: IVenderAssignState;
    ptcDetail?: IPtcDetailState;
    ptcList?: IPtcListState;
    welcome?:IWelcomeState;
    ptcDetailBarcode?: IPtcDetailBarcodeState;
};

export const rootReducer = combineReducers<IAppState>({
    venderConfirm: VenderConfirmReducer,
    venderSearch: VenderSearchReducer,
    venderAccept: VenderAcceptReducer,
    venderAssign: VenderAssignReducer,
    ptcDetail: PtcDetailReducer,
    ptcDetailBarcode: PtcDetailBarcodeReducer,
    ptcList: PtcListReducer,
    welcome:WelcomeReducer
});
