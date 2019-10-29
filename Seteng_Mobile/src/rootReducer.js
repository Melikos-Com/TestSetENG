import { combineReducers } from 'redux';
import { VenderConfirmReducer } from './pages/vender-confirm/vender-confirm-reducer';
import { VenderSearchReducer } from './pages/vender-search/vender-search-reducer';
import { VenderAcceptReducer } from './pages/vender-accept/vender-accept-reducer';
import { VenderAssignReducer } from './pages/vender-assign/vender-assign-reducer';
import { PtcDetailReducer } from './pages/ptc-detail/ptc-detail-reducer';
import { PtcDetailBarcodeReducer } from './pages/ptc-detail-barcode/ptc-detail-barcode-reducer';
import { PtcListReducer } from './pages/ptc-list/ptc-list-reducer';
import { WelcomeReducer } from './pages/welcome/welcome-reducer';
var IAppState = (function () {
    function IAppState() {
    }
    return IAppState;
}());
export { IAppState };
;
export var rootReducer = combineReducers({
    venderConfirm: VenderConfirmReducer,
    venderSearch: VenderSearchReducer,
    venderAccept: VenderAcceptReducer,
    venderAssign: VenderAssignReducer,
    ptcDetail: PtcDetailReducer,
    ptcDetailBarcode: PtcDetailBarcodeReducer,
    ptcList: PtcListReducer,
    welcome: WelcomeReducer
});
//# sourceMappingURL=rootReducer.js.map
