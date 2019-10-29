import { NgModule, ErrorHandler } from '@angular/core';
import { IonicErrorHandler } from 'ionic-angular';

export class MyErrorHandler implements ErrorHandler {
  handleError(err: any): void {
    // do something with the error
    //alert(err);

   
  }
}