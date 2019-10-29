import { Injectable } from '@angular/core';
import { File, Transfer } from 'ionic-native';
import { LoadingController } from 'ionic-angular';
import { LocalStorage } from './localStorage';


@Injectable()
export class FileService {

    constructor(
        private Loading: LoadingController,
        private local: LocalStorage) { }


    /**
     * 寫入log
     * @param msg
     */
    WriteLog(msg: string) {

        console.log('----------準備寫入LOG----------');

        console.log(`LOG內容為:${msg}`);

        //取得目前時間
        var today = new Date();

        var todayStr: string = today.toISOString().substring(0, 10);

        console.log('----------準備讀取目錄----------');

        File.resolveDirectoryUrl(this.local.get<string>('logPath') || '').then(dir => {

            console.log('----------準備取得檔案----------');

            dir.getFile(`${todayStr}Log.txt`,
                {
                    create: true
                },
                function (success) {

                    var logOb = success;

                    var log = `:: ${new Date().toISOString()} ::  [${msg}]\n`;

                    console.log('----------準備寫入內容----------');

                    logOb.createWriter(function (fWriter) {

                        fWriter.seek(fWriter.length);

                        var blob = new Blob([log], { type: 'text/plain' });

                        fWriter.write(blob);

                    }, function (error) {

                        console.log(`----------寫入內容失敗,原因=>${error}----------`);

                    });

                }, function (error) {

                    console.log(`----------讀取檔案失敗,原因=>${error}----------`);

                })


        }).catch(error => {

            console.log(`----------讀取目錄失敗,原因=>${error.message}----------`);

        });


    }

    /**
     * 刪除檔案
     * @param filePath
     * @param fileNames
     */
    Remove(filePath: string, fileNames: Array<string>) {

        let loadingPopup = this.Loading.create({ content: '加載中...' });

        console.log(`---------- 準備刪除檔案 ----------`)

        loadingPopup.present();

        File.resolveDirectoryUrl(filePath).then(dir => {

            fileNames.forEach(filename => {

                console.log(`--準備刪除檔案,檔名:${filename}`);

                dir.getFile(filename,
                    {
                        create: true
                    },
                    function (success) {

                        success.remove(() => {

                            console.log(`--刪除檔案成功,檔名:${filename}`);

                        }, (error) => {

                            console.log(`--刪除檔案失敗,原因:${error}`);

                            this.WriteLog(`--刪除檔案失敗,原因:${error}`);
                        });


                    }, function (error) {

                        console.log(`----------刪除檔案失敗,原因=>${error}----------`);

                        this.WriteLog(`--刪除檔案失敗,原因=>${error}----------`);
                    })
            });

            loadingPopup.dismiss();

        }).catch(error => {

            console.log(`----------讀取目錄失敗,原因=>${error}----------`);

            this.WriteLog(`--讀取目錄失敗,原因=>${error}----------`);

            loadingPopup.dismiss();

        });


    }

    /**
     * 下載檔案
     * @param url 
     */
    // Download(url: string , filename:string):Promise<Entry> {

    // var fullPath =   (this.local.get<string>('logPath') || '') + filename;

    // console.log(`準備下載檔案,url為:${url}`);
    // console.log(`準備下載檔案,目標路徑為:${fullPath}`);

    // return new Transfer().download(url, fullPath);
        

    // }


}