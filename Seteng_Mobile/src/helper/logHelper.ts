import { Http, Headers, RequestOptions, URLSearchParams, Response } from '@angular/http'
import { Injectable } from '@angular/core';
import { LoadingController } from 'ionic-angular';
import * as Rx from 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { HttpParameter } from '../domain/httpParameter';
import { FileService } from '../services/fileService';



@Injectable()
export class LogHelper {


    constructor(private file: FileService) {


    }

    info(...messages: any[]): void {

        messages.forEach(message => {

            console.log(message);

            this.file.WriteLog(message);
        });

    }
    error(...messages: any[]): void {

        messages.forEach(message => {

            console.error(message);

            this.file.WriteLog(message);
        });

    }
    debug(...messages: any[]): void {

        messages.forEach(message => {

            console.debug(message);

            this.file.WriteLog(message);
        });

    }
    warn(...messages: any[]): void {

        messages.forEach(message => {

            console.warn(message);

            this.file.WriteLog(message);
        });

    }
    trace(...messages: any[]): void {

        messages.forEach(message => {

            console.trace(message);

            this.file.WriteLog(message);
        });

    }
}
