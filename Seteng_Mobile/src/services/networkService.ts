import { Injectable } from '@angular/core';
import { Network } from 'ionic-native';

/* service */
import { FileService } from './fileService';



@Injectable()
export class NetworkService {

    private timer: number = 60000;

    connectSubscription: any;

    disconnectSubscription: any;

    constructor(private file: FileService) { }

    /**
     * 網路監聽開啟
     * @param OnConnect 
     * @param DisConnect 
     */
    OnConnectStateListener(OnConnect: () => void,
                           DisConnect: () => void) {

        this.connectSubscription = Network.onConnect().subscribe(() => {

            console.log('----------網路連線正常----------');

            this.file.WriteLog('-----------網路連線正常-----------');

            //每分鐘在有網路的情況下,執行該事件
            setTimeout(() => {

                console.log('----------網路連線正常,並執行服務----------');

                this.file.WriteLog('-----------網路連線正常,並執行服務-----------');

                OnConnect();

            }, this.timer);



        });


        this.disconnectSubscription = Network.onDisconnect().subscribe(() => {

            console.log('----------網路連線中斷----------');

            this.file.WriteLog('-----------網路連線中斷-----------');

            DisConnect();
        });

    }

    /**
     * 網路監聽關閉
     */
    OffConnectStateListener() {

        console.log('----------關閉網路監聽----------');

        this.connectSubscription.unsubscribe();

        this.disconnectSubscription.unsubscribe();

    }





}