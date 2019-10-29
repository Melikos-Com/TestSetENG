import { Injectable } from '@angular/core';
import * as Rx from 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { UserRepository } from '../repository/userRepository';
import { RootScope } from './rootScope';
import { LocalStorage } from '../services/localStorage';


@Injectable()
export class UserService {
    constructor(private rootScope: RootScope,
        private userRepo: UserRepository,
        private local: LocalStorage, ) {
    }

    /*取得使用者資訊*/
    GetUser(): User {
        return this.rootScope.get<User>('user');
    }


    /*
    登出處理
    */
    Logout(success: () => void,
        failure: () => void) {

        var user = this.rootScope.get<User>('user');

        if (!user) {
            return Rx.Observable.empty();
        }


        var http = this.userRepo.Logout(
            user.Token,
            (data: JsonResult<Boolean>) => {

                //清除手機緩存
                this.local.set('user', null);
                this.rootScope.set('user', null);
                success();

            },
            (err: HttpResponse) => {
                this.local.set('user', null);
                this.rootScope.set('user', null);
                console.log(err);
                failure();

            });

        // http.subscribe(x => {

        //     //清除手機緩存
        //     this.local.set('user', null);
        //     this.rootScope.set('user', null);
        //     success();

        // }, error => {

        //     //清除手機緩存
        //     this.local.set('user', null);
        //     this.rootScope.set('user', null);
        //     success();

        // });


    }



    Login(user: User): void {

        this.local.set('user', user);
        this.rootScope.set('user', user);
    }

}
