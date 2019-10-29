import { Component } from '@angular/core';
import { RootScope } from "../../services/rootScope";
/* Repository */
import { CallogRepository } from "../../repository/callogRepository";

@Component({
    selector: 'Record',
    templateUrl: 'Record.html'
})

export class Record {

    public items : Array<{PushRecord}> = [];

    public user: User;

    constructor(private CallRepo: CallogRepository,
        private rootScope: RootScope){

    }

     /**
     * 頁面載入時
     */
    ngOnInit() {
        this.GetRecard();
    }
    /**
     * 取得工程聯絡資訊 
     */
    GetRecard() {
        this.user = this.rootScope.get<User>('user');
        this.CallRepo.GetRecard(
                this.user.Account,
                success.bind(this),
                failure.bind(this));
        function success(data: JsonResult<PushRecord[]>): void {
            data.element.forEach(x=> this.items.push(x));
        }
        function failure(error: HttpResponse): void {
                 
        }
    };

}
