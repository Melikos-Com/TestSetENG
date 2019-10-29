import { Component, ViewChild } from '@angular/core';
import { Slides, NavParams, ViewController } from 'ionic-angular';
 
 
 

@Component({
    selector: 'modal-image',
    templateUrl: 'modal-image.html'
}) 

export class ModalImagePage {


    public imageUrl: string;
    public listImg:string[];
    public index:number;

    @ViewChild(Slides) slides: Slides;

    constructor(private view : ViewController,
                private navParam: NavParams, ) {

         this.listImg = navParam.get('ListImg');
         this.index = navParam.get('Index'); 
         
    }
    
    ionViewDidEnter(){ 
         this.slides.slideTo(this.index);
    }

     /**
    * 關閉modal
    */
    closeModal() {
        this.view.dismiss();
    }
    /**
     * 頁面載入時執行
     */
    ngOnInit() { }

}
