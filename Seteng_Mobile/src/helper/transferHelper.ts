import { Injectable } from '@angular/core';
import { Transfer, FileUploadOptions } from 'ionic-native';
import { LoadingController, AlertController } from 'ionic-angular';

/* global */
import { GlobalVariable } from '../globalVariable';
import { ServerProfile } from '../serverProfile';
import { File } from 'ionic-native';

/* service */
import { LogHelper } from './logHelper';


@Injectable()
export class TransferHelper {


    private trans: Transfer;

    constructor(private Loading: LoadingController,
        private log: LogHelper,
        private global: GlobalVariable,
        private server: ServerProfile) {

        this.trans = new Transfer();
    }


    /**
     * 傳輸檔案
     */
    Upload(filePath: string, fileName: string, user: User) {

        let loadingPopup = this.Loading.create({ content: '加載中...' });

        this.log.info('----------準備傳輸檔案----------');

        this.log.info(`目錄:${filePath}`);
        this.log.info(`檔名:${fileName}`);

        loadingPopup.present();


        this.trans.upload(filePath + fileName,
            this.server.ApiUri + this.global.logUploadApiPath + user.Token,
            {
                fileKey: 'files',
                fileName: fileName,
                chunkedMode: false,
                mimeType: "multipart/form-data",
            })
            .then((success) => {

                this.log.info(`----------傳輸成功:${success}----------`);

                loadingPopup.dismiss()
            })
            .catch((error) => {

                this.log.error(`----------傳輸失敗:${error}----------`);

                loadingPopup.dismiss();

            })
    }


}