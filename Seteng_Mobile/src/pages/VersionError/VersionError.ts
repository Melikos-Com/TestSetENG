import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { InAppBrowser } from 'ionic-native';
/* repository */
import { UserRepository } from '../../repository/userRepository';
@Component({
    selector: 'page-VersionError',
    templateUrl: 'VersionError.html'
})

export class VersionErrorPage {

    constructor(private platform: Platform,
                private userRepo: UserRepository) {

    };

    leave() {
        if (this.platform.is('ios')) {
            this.AppLink("1");
        };
        if (this.platform.is('android')) {
            this.AppLink("2");
        }
    };

    AppLink(index: string) {


        this.userRepo.AppLink(
            index,
            success.bind(this),
            failure.bind(this)

        )
        function success(data) {
            let browser = new InAppBrowser(data.message, '_blank')
        }
        function failure(error) {

        }
    }
}

