import { Component } from '@angular/core';
import { ViewController } from 'ionic-angular';


/*
 Generated class for the LoginPage page.

 See http://ionicframework.com/docs/v2/components/#navigation for more info on
 Ionic pages and navigation.
 */
@Component({
  selector: 'page-tab-attribute',
  templateUrl: 'tab-attribute.html'
})
export class TabAttributePage {
  // all attributes
  public attr = {
    category: 1,
    sleeve: '',
    fabric: ''
  }

  constructor(public viewCtrl: ViewController) {
  }

  // close modal
  closeModal() {

    this.viewCtrl.dismiss(true);
  }
}
