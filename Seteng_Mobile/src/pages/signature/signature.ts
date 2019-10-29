
import { Component, ViewChild } from '@angular/core';
import { NavParams, ViewController } from 'ionic-angular';

import { SignaturePad } from 'angular2-signaturepad/signature-pad';
@Component({
  selector: 'signature',
  templateUrl: 'signature.html'
})


export class signature {

  
  constructor(private view: ViewController,
    private navParam: NavParams, ) {
    
  }

  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  /**
  * 頁面載入時
  */
  ngOnInit() {
    console.log("");
  }
  saveBtn() {
    //將圖片的URL匯出成LOG Base64string模式
    console.log(this.signaturePad.toDataURL("image/jpeg"));

    //關閉modal
    this.closeModal();
  } 
  /**
  * 關閉modal
  */
  closeModal() {
    this.signaturePad.clear(); 
    // this.view.dismiss();
  } 
  saveModal() {
    
    var signature: string ="";
    signature = this.signaturePad.toDataURL("image/jpeg");  
    this.view.dismiss(signature);
  }
  // 畫筆的初始值設定
  private signaturePadOptions: Object = { // passed through to szimek/signature_pad constructor
    'minWidth': 2,
  };


  // 畫面載入後要清空畫面可繪製的區塊
  ngAfterViewInit() {
    var wrapper = document.getElementById('forPad')
    this.signaturePad.set('canvasWidth', wrapper.clientWidth);
    this.signaturePad.set('canvasHeight', wrapper.clientHeight-100);   
    this.signaturePad.set('backgroundColor','rgb(255,255,255)');
    this.signaturePad.clear(); // invoke functions from szimek/signature_pad API
   
  }

  


  //畫筆放開的時間點
  drawComplete() {
    // will be notified of szimek/signature_pad's onEnd event
    // console.log(this.signaturePad.toDataURL());
  }
  //畫筆畫下去的時間點
  drawStart() {
    // will be notified of szimek/signature_pad's onBegin event
    // console.log('begin drawing');
  }




}