import { Component, Input, ViewChild, OnInit } from '@angular/core';
import { NavController, Slides, ModalController, ToastController, AlertController } from 'ionic-angular';
import { ResponseType } from '@angular/http'
import { Camera } from 'ionic-native';
/* pages */
import { WelcomePage } from "../welcome/welcome";

/* form */
import { Validators } from '@angular/forms';

/* NgStore */
import { select } from '@angular-redux/store';
import { Observable } from 'rxjs/Rx';
import { PtcDetailBarcodeAction } from './ptc-detail-barcode-action';

/* modal */
import { ModalImagePage } from '../modal-image/modal-image';
import { ModalTechnicianPage } from '../modal-technician/modal-technician';
import { ModalTechnicianGPPage } from '../modal-technician/modal-technicianGP';
import { calloghistory } from '../calloghistory/calloghistory';

/* repository */
import { CallogRepository } from "../../repository/callogRepository";

/* service */
import { LocalStorage } from "../../services/localStorage";
import { RootScope } from "../../services/rootScope";

/*helper */
import { LogHelper } from "../../helper/logHelper";
import { retry } from 'rxjs/operator/retry';
import { signature } from '../signature/signature';

import JsBarcode from 'jsbarcode';
import { dateDataSortValue } from 'ionic-angular/umd/util/datetime-util';

// declare var cordova; // after import's
/**
 * 案件-明細內容
 */
@Component({
    selector: 'ptc-detail-barcode',
    templateUrl: 'ptc-detail-barcode.html'
})
export class PtcDetailBarcode implements OnInit {

    @ViewChild(Slides) slides: Slides;

    @select(['ptcDetail', 'item']) item$: Observable<CallogDetailApiViewModel>;

    @Input() viewType: string;

    public item: any = {};

    public nowStr: string = '';
    //是否已到店  點選後顯示QRCODE  true=點選顯示   false=尚未顯示
    public isInStore: boolean = false;
    //是否離店店  點選後顯示QRCODE  true=點選顯示   false=尚未顯示
    public isOutStore: boolean = false;
    // //是否點選拒絕簽名確認
    // public isRejectignCheck: boolean = false;



    private subscription;

    //是否註銷可認養按鈕
    isAcceptDisable: boolean = false;
    //是否可註銷改派按鈕
    isChangeDisable: boolean = false;

    public isShowReasionPopup: boolean = true;

    /* 時間軸 */
    public wizzard = [
        { name: '資訊', class: 'completed' },
        { name: '到店', class: '' },
        { name: '拍照', class: '' },
        { name: '維修', class: '' },
        { name: '確認', class: '' },
        { name: '離店', class: '' },
    ];

    public user: User;

    constructor(
        private local: LocalStorage,
        private log: LogHelper,
        private action: PtcDetailBarcodeAction,
        private nav: NavController,
        private toast: ToastController,
        private callogRepo: CallogRepository,
        private modal: ModalController,
        private alertCtrl: AlertController,
        private rootScope: RootScope) {

        this.user = rootScope.get<User>('user');
    }

    ngOnInit() {
        this.subscription = this.item$.subscribe(data => {
            if (!data) return;
            this.item = data;
            this.action.getSlideDispatch(this.wizzard);
            if (this.slides) {
                if (data.IsWait) {
                    this.slides.slideTo(5);
                    this.slides.lockSwipes(true);

                }
            }
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    };

    /**
     * 導向slide
     */
    slideDirect = (index: number) => this.slides.slideTo(index);

    /**
     * 當slide變更時
     */
    slideChanged() {
        let currentIndex = this.slides.getActiveIndex();

        var detail = this.item as CallogDetailApiViewModel;
        //slide滑到第二頁，判斷有沒有到達時間，不然回到第一頁
        if (currentIndex >= 2) {
            if (detail.ArriveDate == null) {
                this.slides.slideTo(1);
                this.showToast("請掃描條碼或選擇無法掃描", false);
                return;
            }
        }

        let content;
        switch (currentIndex) {
            case 1:
                content = "38" + detail.Sn + "1";
                JsBarcode("#arriveBarCode", content, { displayValue: true, format: "CODE39", width: 1 });
                //若已經有到店時間，條碼自動顯示
                if (detail.IsStoreScanSI == true && detail.ArriveDate != null) {
                    this.isInStore = true;
                }

                break;
            case 4:
                //銷案類型 找出對應的名稱
                var finish = detail.Finish.filter(x => x.FinishId == this.item.TypeFinishId);
                if (finish.length == 0)
                    detail.Finish_Name = "";
                else
                    detail.Finish_Name = finish[0].FinishName;

                //工作類型 找出對應的名稱
                var work = detail.Work.filter(x => x.WorkId == this.item.TypeWorkId);
                if (work.length == 0)
                    detail.WorkName = "";
                else
                    detail.WorkName = work[0].WorkDesc;

                //處理類型 找出對應的名稱
                var proc = this.item.Proc.filter(x => x.DamageProcNo == this.item.TypePorcnoId);
                if (proc.length == 0)
                    detail.Mtn_Desc = "";
                else
                    detail.Mtn_Desc = proc[0].MtnDesc;

                break;
            case 5:
                content = "38" + this.item.Sn + "2";
                JsBarcode("#leaveBarCode", content, { displayValue: true, format: "CODE39", width: 1 });
                //若已經有到店時間，條碼自動顯示
                if (detail.IsStoreScanSO == true && detail.LeaveDate != null) {
                    this.isOutStore = true;
                }

                break;
            default: break;
        }

        //變更時間軸
        this.wizzard.forEach((element, index) =>
            element.class = (index <= currentIndex) ? 'completed' : '');

        //更新 wizard component
        this.action.getSlideDispatch(this.wizzard);


        //顯示目前時間
        this.nowStr = (currentIndex == 2) ? new Date().toLocaleString('zh-Hans-CN', { hour12: false }) : '';

        //如果是確認銷案sheet ,就disable swipe ,以防按鈕被變換
        this.slides.lockSwipeToNext((currentIndex == 5));


    };
    /**
     * 維修前照片上傳
     */
    uploadBeforefix(event) {
        if ((this.item as CallogDetailApiViewModel).ImgBeforeFix != null) {
            if ((this.item as CallogDetailApiViewModel).ImgBeforeFix.length >= 3) {
                this.showToast("圖片(修理前)最多3張", false);
                return;
            }
        }
        else {
            (this.item as CallogDetailApiViewModel).ImgBeforeFix = new Array<string>();
        }

        Camera.getPicture({
            sourceType: 1,
            destinationType: Camera.DestinationType.DATA_URL,
            targetWidth: 512,
            targetHeight: 512,
            quality: 100,
        }).then((imageData) => {

            let base64Image = 'data:image/jpeg;base64,' + imageData;

            (this.item as CallogDetailApiViewModel).ImgBeforeFix.push(base64Image);

        }, (err) => {
            this.showToast(err, false);
        });
    };
    /**
     * 維修後照片上傳
     */
    uploadAfterfix() {
        if ((this.item as CallogDetailApiViewModel).ImgAfterFix != null) {
            if ((this.item as CallogDetailApiViewModel).ImgAfterFix.length >= 3) {
                this.showToast("圖片(修理後)最多3張", false);
                return;
            }
        }
        else {
            (this.item as CallogDetailApiViewModel).ImgAfterFix = new Array<string>();
        }

        Camera.getPicture({
            sourceType: 1,
            destinationType: Camera.DestinationType.DATA_URL,
            targetWidth: 512,
            targetHeight: 512,
            quality: 100,
        }).then((imageData) => {

            let base64Image = 'data:image/jpeg;base64,' + imageData;

            (this.item as CallogDetailApiViewModel).ImgAfterFix.push(base64Image);

        }, (err) => {

            this.showToast(err, false);

        });
    };
    /**
    * 工單或店章
    */
    uploadPic(event) {
        if ((this.item as CallogDetailApiViewModel).Img != null) {
            if ((this.item as CallogDetailApiViewModel).Img.length >= 1) {
                this.showToast("圖片(工單或店章)最多1張", false);
                return;
            }
        }
        else {
            (this.item as CallogDetailApiViewModel).Img = new Array<string>();
        }


        Camera.getPicture({
            sourceType: 1,
            destinationType: Camera.DestinationType.DATA_URL,
            targetWidth: 512,
            targetHeight: 512,
            quality: 100,
        }).then((imageData) => {

            let base64Image = 'data:image/jpeg;base64,' + imageData;

            (this.item as CallogDetailApiViewModel).Img.push(base64Image);

        }, (err) => {

            this.showToast(err, false);

        });
    };

    // /**
    //  * 刪除圖片
    //  */
    // removePic = (event, index) => {
    //     (this.item as CallogDetailApiViewModel).Img.splice(index, 1);
    // }

    /**
    * 刪除維修前圖片
    */
    removeBeforeImg = (event, index) => {
        (this.item as CallogDetailApiViewModel).ImgBeforeFix.splice(index, 1);
    };
    /**
    * 刪除維修後圖片
    */
    removeAfterImg = (event, index) => {
        (this.item as CallogDetailApiViewModel).ImgAfterFix.splice(index, 1);
    };
    /**
    * 刪除工單圖片
    */
    removePic = (event, index) => {
        (this.item as CallogDetailApiViewModel).Img.splice(index, 1);
    };

    /**
     * 查看維修前大圖
     */
    viewLargerBefore(index) {
        // show modal
        let modal = this.modal.create(ModalImagePage,
            {
                Index: index,
                ListImg: (this.item as CallogDetailApiViewModel).ImgBeforeFix
            });

        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) { return; }

            if (typeof data === 'string') {
                this.showToast(data, false);
                return;
            }

        });
        modal.present();
    };
    /**
    * 查看維修前大圖
    */
    viewLargerAfter(index) {
        // show modal
        let modal = this.modal.create(ModalImagePage,
            {
                Index: index,
                ListImg: (this.item as CallogDetailApiViewModel).ImgAfterFix
            });
        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) { return; }

            if (typeof data === 'string') {
                this.showToast(data, false);
                return;
            }
        });
        modal.present();
    };
    /**
     * 查看工單大圖
     */
    viewLarger(index) {
        // show modal
        let modal = this.modal.create(ModalImagePage,
            {
                Index: index,
                ListImg: (this.item as CallogDetailApiViewModel).Img
            });

        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) { return; }

            if (typeof data === 'string') {
                this.showToast(data, false);
                return;
            }

        });
        modal.present();
    };

    /**
     * 查看案件歷程
     */
    showHistory(index) {
        var detail = this.item as CallogDetailApiViewModel
        let modal = this.modal.create(calloghistory,
            {
                CompCd: detail.CompCd,
                Sn: detail.Sn,
            });


        modal.present();
    };

    /**
    * 門市簽名
    */
    public signaturURL: string = "";
    showSignature(index) {
        var detail = this.item as CallogDetailApiViewModel
        let modal = this.modal.create(signature,
            {

            });

        modal.onDidDismiss(data => {

            if (!data) { return; }

            if (typeof data === 'string') {
                this.signaturURL = data;

                detail.ImgSignature = new Array<string>();
                detail.ImgSignature.push(data);

                var date = new Date();
                var strMonth = "";
                var Year = date.getFullYear();
                var Month = date.getMonth();
                Month = Month + 1;
                if (Month.toString().length == 1)
                    strMonth = "0" + Month.toString();
                else
                    strMonth = Month.toString();
                var day = date.getDate().toString();
                if (day.length == 1)
                    day = "0" + day;
                var hour = date.getHours().toString();
                if (hour.length == 1)   //長度不足補0
                    hour = "0" + hour;
                var Min = date.getMinutes().toString();
                if (Min.length == 1)
                    Min = "0" + Min;
                var Sec = date.getSeconds().toString();
                if (Sec.length == 1)
                    Sec = "0" + Sec;
                detail.SignatureDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec;
                return;
            }
        });
        modal.present();
        modal.present();
    };


    /**
     * 暫時訊息
     * @param msg
     */
    showToast(msg: string, isSuccess: boolean) {

        let toast = this.toast.create({
            message: msg,
            duration: 3000,
            position: 'middle',
            cssClass: isSuccess ? 'detail-toast-success' : 'detail-toast-fail'
        });
        toast.present();
    };
    /**
     * 撥打電話
     */
    callPhone(mobNumber: string) {
        window.open("tel:" + mobNumber);
    }

    /**
     * 按下認養
     */
    accept() {

        var user = this.local.get<User>('user');

        this.TechnicianAccept(user.Account, false);

    }
    /**
     * 按下指派
     */
    assign() {

        var detail = this.item as CallogDetailApiViewModel

        // show modal
        let modal = this.modal.create(ModalTechnicianPage, {
            Sn: detail.Sn,
            AssetName: detail.AssetName,
            StoreName: detail.StoreName,
        });

        // listen for modal close
        modal.onDidDismiss(data => {

            //確認並執行指派
            if (!data) return;

            var technician = data as TechnicianResultApiViewModel;

            this.TechnicianAccept(technician.Account, true);

        });

        modal.present();

    }

    /**
    * 按下改派
    */
    change() {

        var detail = this.item as CallogDetailApiViewModel

        // show modal
        let modal = this.modal.create(ModalTechnicianPage, {
            Sn: detail.Sn,
            AssetName: detail.AssetName,
            StoreName: detail.StoreName,
            AcceptedName: detail.AcceptedName,
            AcceptedAccount: detail.AcceptedAccount
        });

        // listen for modal close
        modal.onDidDismiss(data => {

            //確認並執行改派
            if (!data) return;

            var technician = data as TechnicianResultApiViewModel;

            this.VenderChangeLog(technician.Account);

        });

        modal.present();

    }

    /**
     * 按下通知
     */
    notify() {

        var detail = this.item as CallogDetailApiViewModel

        // show modal
        let modal = this.modal.create(ModalTechnicianGPPage, {
            Sn: detail.Sn,
        });

        // listen for modal close
        modal.onDidDismiss(data => {

            if (!data) return;

            this.venderNotify(data.technicians);

        });

        modal.present();

    }
    /**
     * 廠商通知技師認養
     */
    venderNotify(accountInfos: Array<TechnicianResultApiViewModel>) {

        if (!accountInfos || accountInfos.length == 0) {
            this.showToast('請選擇技師或群組', false);
            return;
        }

        this.log.info("----------準備進行廠商通知----------")

        var detail = this.item as CallogDetailApiViewModel

        this.callogRepo.VenderNotification(
            detail.CompCd,
            detail.Sn,
            accountInfos,
            success.bind(this),
            failure.bind(this)
        )


        function success(data: JsonResult<Boolean>) {

            this.log.info(data);

            this.showToast(data.message, data.isSuccess);

            this.isAcceptDisable = !data.isSuccess;

            this.nav.pop(); //返回前一頁
        }

        function failure(error: HttpResponse) {

            this.showToast('通知失敗', false);

            this.log.error(`---------- 廠商通知失敗,原因為:${error} ----------`);

        }

    }

    isInt(value) {

        var x;

        return isNaN(value) ? !1 : (x = parseFloat(value), (0 | x) === x);
    }

    /**
    * 銷案
    */
    venderConfirm() {
        var detail = this.item as CallogDetailApiViewModel;

        if (detail.ArriveDate == null) {
            this.showToast("請點擊到店時間", false);
            return;
        };
        if (detail.TypeWorkId == null) {
            this.showToast("請選擇工作類型", false);
            return;
        };
        if (detail.TypeFinishId == null) {
            this.showToast("請選擇銷案類型", false);
            return;
        };
        if (detail.TypePorcnoId == null) {
            this.showToast("請選擇處理類型", false);
            return;
        };
        if (detail.IsCoffeeAsset) {
            if (detail.CoffeeCup == null) {
                this.showToast("請輸入咖啡杯數", false);
                return;
            }
            if (detail.CoffeeCup == "") {
                this.showToast("咖啡杯數不為空值或非數字", false);
                return;
            }
            if (Number(detail.CoffeeCup) < 0) {
                this.showToast("咖啡杯數不能為負數", false);
                return;
            }
            if (!(this.isInt(Number(detail.CoffeeCup)))) {
                this.showToast("咖啡杯數要為整數", false);
                return;
            }
            if (Number(detail.CoffeeCup) > 99999999) {
                this.showToast("咖啡杯數不得超過九位數", false);
                return;
            }
        };
        if (detail.LeaveDate == null) {
            this.showToast("離店請掃描條碼或選擇無法掃描", false);
            return;
        };

        if (detail.IsSignature == true && (detail.ImgSignature == null || detail.ImgSignature.length == 0)) {
            this.showToast("請門市人員簽名或勾選拒絕門市拒絕簽名", false);
            return;
        }


        this.log.info("----------準備進行廠商了結----------")

        this.callogRepo.VenderConfirm(
            detail,
            success.bind(this),
            failure.bind(this)
        );

        function success(data: JsonResult<Boolean>): void {

            this.log.info(data);

            this.showToast(data.message, data.isSuccess);

            this.slides.lockSwipes(true);

            this.nav.pop(); //返回前一頁
        };

        function failure(error: HttpResponse): void {

            this.log.error(error);

            if (error.type = ResponseType.Error) {


                switch (error.status) {

                    case 0:  //連線發生錯誤
                        this.showToast('網路連線異常，案件暫存成功。', false);
                        this.log.error(`---------- 廠商了結失敗,所以暫存於本機,原因為:${error} ----------`);
                        this.slides.lockSwipes(true);

                        var logs = this.local.get('logs') as Array<CallogDetailApiViewModel> || [];

                        if (logs.some(x => x.Sn == detail.Sn)) {
                            this.log.error(`----------手機緩存已經有既有的案件編號:${detail.Sn}----------`);
                            return;
                        }

                        logs.push(detail)

                        this.local.set('logs', logs);
                        break

                    case 400: //badRequest
                        this.showToast('銷案失敗', false);
                        break;
                    case 401: // 權限驗證異常
                        this.local.set('user', null);
                        let alert = this.alertCtrl.create({
                            title: '身份驗証',
                            subTitle: '身份資訊失效，請重新登入',
                            buttons: ['OK']
                        });
                        alert.present();

                        this.nav.setRoot(WelcomePage);

                        break;

                    default:
                        this.showToast('銷案失敗', false);
                        break;

                }

            }

        };

    }
    /**
     * 廠商資料暫存
     */
    scratchOfVndCfm() {

        this.log.info("----------準備進行資料暫存----------");

        var detail = this.item as CallogDetailApiViewModel;

        this.callogRepo.ScratchOfVndCfm(
            detail.CompCd,
            detail.Sn,
            detail.ArriveDate,
            detail.IsStoreScanSI,
            detail.ImgBeforeFix,
            detail.ImgAfterFix,
            detail.Img,            
            detail.AcceptedAccount,
            detail.AcceptedName,
            detail.FixMark,
            detail.CoffeeCup,
            detail.Pre_Amt,
            success.bind(this),
            failure.bind(this)
        );


        function success(data: JsonResult<Boolean>): void {

            this.log.info(data);
            this.showToast(data.message, data.isSuccess);
        }
        function failure(error: HttpResponse): void {

            this.showToast('紀錄失敗', false);

            this.log.error(`---------- 廠商資料暫存失敗,原因為:${error.statusText} ----------`);

        };
    }
    /**
     * 技師認養案件
     */
    TechnicianAccept(account: string, isVndAssign: Boolean) {

        this.log.info("----------準備進行技師認養案件----------")

        var detail = this.item as CallogDetailApiViewModel;

        this.callogRepo.TechnicianAccept(
            detail.CompCd,
            detail.Sn,
            account,
            isVndAssign,
            success.bind(this),
            failure.bind(this)
        );


        function success(data: JsonResult<Boolean>): void {

            this.log.info(data);
            this.showToast(data.message, data.isSuccess)
            this.isAcceptDisable = true;
            this.nav.pop(); //返回前一頁

        };

        function failure(error: any): void {

            this.log.error(`---------- 技師認養案件失敗 ----------`);

        };

    }
    /**
     * 廠商改派案件
     * @param Account
     */
    VenderChangeLog(account: string) {

        this.log.info("----------準備進行廠商改派案件----------")

        var detail = this.item as CallogDetailApiViewModel;

        this.callogRepo.VenderChangeLog(
            detail.CompCd,
            detail.Sn,
            account,
            success.bind(this),
            failure.bind(this)
        );


        function success(data: JsonResult<Boolean>): void {

            this.log.info(data);

            this.showToast(data.message, data.isSuccess);

            this.isChangeDisable = !data.isSuccess;

            this.nav.pop(); //返回前一頁
        };

        function failure(error: string): void {

            this.showToast('廠商改派案件失敗', false);

            this.log.error(`---------- 廠商改派案件失敗,原因為:${error} ----------`);

        };

    }

    /**
     * 到店時間
     */
    SetArriveDate(check: boolean) {
        if (this.item.IsStoreScanSI == false) {
            // this.isInStoreNoScreen = false;
            return;
        } else {
            let alert = this.alertCtrl.create({
                title: '確認勾選',
                subTitle: '若勾選，則會由系統自動發送訊息，通知相關人員。',
                buttons: [
                    {
                        text: '取消',
                        handler: data => {
                            this.item.IsStoreScanSI = true;
                            console.log('取消');
                        }
                    },
                    {
                        text: '確認',
                        handler: () => {
                            this.item.IsStoreScanSI = false;
                            var detail = this.item as CallogDetailApiViewModel;

                            var date = new Date();
                            var strMonth = "";
                            var Year = date.getFullYear();
                            var Month = date.getMonth();
                            Month = Month + 1;
                            if (Month.toString().length == 1)
                                strMonth = "0" + Month.toString();
                            else
                                strMonth = Month.toString();
                            var day = date.getDate().toString();
                            if (day.length == 1)
                                day = "0" + day;
                            var hour = date.getHours().toString();
                            if (hour.length == 1)   //長度不足補0
                                hour = "0" + hour;
                            var Min = date.getMinutes().toString();
                            if (Min.length == 1)
                                Min = "0" + Min;
                            var Sec = date.getSeconds().toString();
                            if (Sec.length == 1)
                                Sec = "0" + Sec;
                            detail.ArriveDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec

                            this.scratchOfVndCfm();
                            console.log('確認');
                        }
                    }
                ]
            });
            alert.present();
        }
    };


    /**
     * 離店時間
     */
    SetLeaveDate() {
        if (this.item.IsStoreScanSO == false) {
            // this.isInStoreNoScreen = false;
            return;
        } else {
            let alert = this.alertCtrl.create({
                title: '確認勾選',
                subTitle: '若勾選，則會由系統自動發送訊息，通知相關人員。',
                buttons: [
                    {
                        text: '取消',
                        handler: data => {
                            this.item.IsOutStoreNoScreen = true;
                            console.log('取消');
                        }
                    },
                    {
                        text: '確認',
                        handler: () => {
                            var detail = this.item as CallogDetailApiViewModel;
                            if (detail.ArriveDate == null) {
                                this.slides.slideTo(2);
                                this.showToast("請先刷讀條碼或選擇拒絕刷讀", false);
                                return;
                            };
                            this.item.IsStoreScanSO = false;

                            var date = new Date();
                            var strMonth = "";
                            var Year = date.getFullYear();
                            var Month = date.getMonth();
                            Month = Month + 1;
                            if (Month.toString().length == 1)
                                strMonth = "0" + Month.toString();
                            else
                                strMonth = Month.toString();
                            var day = date.getDate().toString();
                            if (day.length == 1)
                                day = "0" + day;
                            var hour = date.getHours().toString();
                            if (hour.length == 1)   //長度不足補0
                                hour = "0" + hour;
                            var Min = date.getMinutes().toString();
                            if (Min.length == 1)
                                Min = "0" + Min;
                            var Sec = date.getSeconds().toString();
                            if (Sec.length == 1)
                                Sec = "0" + Sec;
                            detail.LeaveDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec;

                        }
                    }

                ]
            });
            alert.present();
        }
    };

    //點選門市人員拒絕
    SetRejectignCheck() {

        if (this.item.IsSignature == false) {
            this.item.IsSignature = true;
            return;
        } else {
            let alert = this.alertCtrl.create({
                title: '確認勾選',
                subTitle: '若勾選，則會由系統自動發送訊息，通知相關人員，並清除既有的簽名。',
                buttons: [
                    {
                        text: '取消',
                        handler: data => {
                            this.item.IsSignature = true;
                            console.log('取消');
                        }
                    },
                    {
                        text: '確認',
                        handler: () => {
                            var detail = this.item as CallogDetailApiViewModel;

                            this.item.IsSignature = false;

                            var date = new Date();
                            var strMonth = "";
                            var Year = date.getFullYear();
                            var Month = date.getMonth();
                            Month = Month + 1;
                            if (Month.toString().length == 1)
                                strMonth = "0" + Month.toString();
                            else
                                strMonth = Month.toString();
                            var day = date.getDate().toString();
                            if (day.length == 1)
                                day = "0" + day;
                            var hour = date.getHours().toString();
                            if (hour.length == 1)   //長度不足補0
                                hour = "0" + hour;
                            var Min = date.getMinutes().toString();
                            if (Min.length == 1)
                                Min = "0" + Min;
                            var Sec = date.getSeconds().toString();
                            if (Sec.length == 1)
                                Sec = "0" + Sec;
                            detail.SignatureDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec;

                            //清除既有的簽名檔
                            if (detail.ImgSignature != null || detail.ImgSignature.length > 0) {

                                detail.ImgSignature.splice(0);
                                this.signaturURL = "";
                            }
                        }
                    }

                ]
            });
            alert.present();
        }
    }
    //點選到店顯示BarCode
    ShowBarCodeSI() {
        this.isInStore = true;

        var detail = this.item as CallogDetailApiViewModel;
        if (detail.ArriveDate == null) {
            var date = new Date();
            var strMonth = "";
            var Year = date.getFullYear();
            var Month = date.getMonth();
            Month = Month + 1;
            if (Month.toString().length == 1)
                strMonth = "0" + Month.toString();
            else
                strMonth = Month.toString();
            var day = date.getDate().toString();
            if (day.length == 1)
                day = "0" + day;
            var hour = date.getHours().toString();
            if (hour.length == 1)   //長度不足補0
                hour = "0" + hour;
            var Min = date.getMinutes().toString();
            if (Min.length == 1)
                Min = "0" + Min;
            var Sec = date.getSeconds().toString();
            if (Sec.length == 1)
                Sec = "0" + Sec;
            detail.ArriveDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec;
            detail.IsStoreScanSI = true;

            this.scratchOfVndCfm();
        }


    }
    //點選離店顯示BarCode
    ShowBarCodeSO() {
        this.isOutStore = true;
        var detail = this.item as CallogDetailApiViewModel;
        if (detail.LeaveDate == null) {
            var date = new Date();
            var strMonth = "";
            var Year = date.getFullYear();
            var Month = date.getMonth();
            Month = Month + 1;
            if (Month.toString().length == 1)
                strMonth = "0" + Month.toString();
            else
                strMonth = Month.toString();
            var day = date.getDate().toString();
            if (day.length == 1)
                day = "0" + day;
            var hour = date.getHours().toString();
            if (hour.length == 1)   //長度不足補0
                hour = "0" + hour;
            var Min = date.getMinutes().toString();
            if (Min.length == 1)
                Min = "0" + Min;
            var Sec = date.getSeconds().toString();
            if (Sec.length == 1)
                Sec = "0" + Sec;
            detail.LeaveDate = Year + "/" + strMonth + "/" + day + " " + hour + ":" + Min + ":" + Sec;
            detail.IsStoreScanSO = true;
        }
    }

    //
}
