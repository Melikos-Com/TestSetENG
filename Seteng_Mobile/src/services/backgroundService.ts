import { Injectable } from '@angular/core';
import { BackgroundMode } from 'ionic-native';

/* service */
import { FileService } from './fileService';

@Injectable()
export class BackgroundService {


    private timer: number = 10000;

    constructor(private file: FileService) {

        BackgroundMode.setDefaults({

            title: '工程保修系統',
            text: 'Executing background tasks.',
            silent: true

        })

    }


    public Onactivate = (Onactivate: () => void) => {

        this.file.WriteLog('----------設定背景服務內容---------');

        BackgroundMode.onactivate().subscribe(x => {

             this.file.WriteLog('----------準備執行背景服務---------');

              Onactivate();

        })


    }

    /**
     * 啟用
     */
    public Enable() {

        this.file.WriteLog('----------啟用背景服務---------');

        console.log('----------啟用背景服務---------');

        BackgroundMode.enable();
    }

    /**
     * 停用
     */
    public Disable() {

        this.file.WriteLog('----------停用背景服務---------');

        console.log('----------停用背景服務---------');

        BackgroundMode.disable();
    }

    /**
     * 是否已開啟
     */
    public IsActive = (): Boolean => BackgroundMode.isActive();

    /**
   * 背景程序是否已啟用
   */
    public IsEnabled = (): Boolean => BackgroundMode.isEnabled();

}