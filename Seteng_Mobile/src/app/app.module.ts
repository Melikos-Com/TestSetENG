import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule } from 'ionic-angular';
import { MyApp } from './app.component';

/*ng store*/
import { NgReduxModule, NgRedux } from '@angular-redux/store';

/* common */
import { GlobalVariable } from '../globalVariable';
import { ServerProfile } from '../serverProfile';

/* service */
import { SqliteService } from '../services/sqliteService';
import { RootScope } from '../services/rootScope';
import { LocalStorage } from '../services/localStorage';
import { PushService } from '../services/pushService';
import { FileService } from '../services/fileService';
import { BackgroundService } from '../services/backgroundService';
import { NetworkService } from '../services/networkService';
import { UserService } from '../services/userService';

/* helper */
import { HttpHelper } from '../helper/httpHelper';
import { TransferHelper } from '../helper/transferHelper';
import { LogHelper } from '../helper/logHelper';

/* repository */
import { CallogRepository } from '../repository/callogRepository';
import { UserRepository } from '../repository/userRepository';
import { TechnicianRepository } from '../repository/technicianRepository';

/* pages */
import { HomePage } from '../pages/home/home';
import { ModalFilterPage } from '../pages/modal-filter/modal-filter';
import { TabAttributePage } from '../pages/tab-attribute/tab-attribute';
import { TabFilterPage } from '../pages/tab-filter/tab-filter';
import { WelcomePage } from '../pages/welcome/welcome';
import { WelcomeAction } from '../pages/welcome/welcome-action';
import { VenderConfirmPage } from '../pages/vender-confirm/vender-confirm';
import { VenderConfirmDetailPage } from '../pages/vender-confirm/vender-confirm-detail';
import { VenderConfirmAction } from '../pages/vender-confirm/vender-confirm-action';
import { VenderAcceptPage } from '../pages/vender-accept/vender-accept';
import { VenderAcceptDetailPage } from '../pages/vender-accept/vender-accept-detail';
import { VenderAcceptAction } from '../pages/vender-accept/vender-accept-action';
import { VenderSearchPage } from '../pages/vender-search/vender-search';
import { VenderSearchDetailPage } from '../pages/vender-search/vender-search-detail';
import { VenderSearchAction } from '../pages/vender-search/vender-search-action';
import { VenderAssignPage } from '../pages/vender-assign/vender-assign';
import { VenderAssignDetailPage } from '../pages/vender-assign/vender-assign-detail';
import { VenderAssignAction } from '../pages/vender-assign/vender-assign-action';
import { VersionErrorPage } from '../pages/VersionError/VersionError';
import { Record } from '../pages/Record/Record';
import { calloghistory } from '../pages/calloghistory/calloghistory';
import { signature } from '../pages/signature/signature';

/* modal */
import { ModalConditionsPage } from '../pages/modal-conditions/modal-conditions';
import { ModalConditionsAssignPage } from '../pages/modal-conditions/modal-conditions-assign';
import { ModalTechnicianPage } from '../pages/modal-technician/modal-technician';
import { ModalTechnicianGPPage } from '../pages/modal-technician/modal-technicianGP';
import { ModalTechnicianResultPage } from '../pages/modal-technician/modal-technician-result';
import { ModalImagePage } from '../pages/modal-image/modal-image';
/* components */
import { Wizard } from '../pages/wizard/wizard';
import { WizardBarcode } from '../pages/wizard-barcode/wizard-barcode';
import { PtcList } from '../pages/ptc-list/ptc-list';
import { PtcItem } from '../pages/ptc-item/ptc-item';
import { PtcDetail } from '../pages/ptc-detail/ptc-detail';
import { PtcDetailAction } from '../pages/ptc-detail/ptc-detail-action';
import { PtcListAction } from '../pages/ptc-list/ptc-list-action';
import { PtcDetailBarcode } from '../pages/ptc-detail-barcode/ptc-detail-barcode';
import { PtcDetailBarcodeAction } from '../pages/ptc-detail-barcode/ptc-detail-barcode-action';

/* plugins */
import { IonJPushModule } from 'ionic2-jpush'
import { MyErrorHandler } from '../misc/ErrorHandler';
import { Md5 } from 'ts-md5/dist/md5';

import { SignaturePadModule } from 'angular2-signaturepad';

@NgModule({
    declarations: [
        MyApp,
        HomePage,
        ModalFilterPage,
        TabAttributePage,
        TabFilterPage,
        WelcomePage,
        VenderConfirmPage,
        VenderConfirmDetailPage,
        VenderAcceptPage,
        VenderAcceptDetailPage,
        VenderSearchPage,
        VenderSearchDetailPage,
        VenderAssignPage,
        VenderAssignDetailPage,
        ModalConditionsPage,
        ModalTechnicianPage,
        ModalTechnicianGPPage,
        ModalTechnicianResultPage,
        ModalConditionsAssignPage,
        ModalImagePage,
        Wizard,
        WizardBarcode,
        PtcList,
        PtcItem,
        PtcDetail,
        VersionErrorPage,
        Record,
        calloghistory,
        signature,
        PtcDetailBarcode
    ],
    imports: [
        IonicModule.forRoot(MyApp,{
            scrollAssist: true,    // Valid options appear to be [true, false]
            autoFocusAssist: false
        }),
        NgReduxModule,
        IonJPushModule,
        SignaturePadModule,
    ],
    bootstrap: [IonicApp],
    entryComponents: [
        MyApp,
        HomePage,
        ModalFilterPage,
        TabAttributePage,
        TabFilterPage,
        WelcomePage,
        VenderConfirmPage,
        VenderConfirmDetailPage,
        VenderAcceptPage,
        VenderAcceptDetailPage,
        VenderSearchPage,
        VenderSearchDetailPage,
        VenderAssignPage,
        VenderAssignDetailPage,
        ModalConditionsPage,
        ModalTechnicianPage,
        ModalTechnicianGPPage,
        ModalTechnicianResultPage,
        ModalConditionsAssignPage,
        ModalImagePage,
        VersionErrorPage,
        Record,
        calloghistory,
        signature

    ],
    providers: [
        ServerProfile,
        GlobalVariable,
        HttpHelper,
        TransferHelper,
        LogHelper,
        CallogRepository,
        UserRepository,
        TechnicianRepository,
        SqliteService,
        PushService,
        FileService,
        BackgroundService,
        NetworkService,
        RootScope,
        LocalStorage,
        WelcomeAction,
        VenderConfirmAction,
        VenderSearchAction,
        VenderAssignAction,
        VenderAcceptAction,
        PtcDetailAction,
        PtcListAction,
        UserService,
        Md5,
        PtcDetailBarcodeAction,
        //門市未消案件查尋
        // StorePendingAction,
        { provide: ErrorHandler, useClass: MyErrorHandler }
    ]
})
export class AppModule { }
