import { Injectable } from '@angular/core';
import { GlobalVariable } from '../globalVariable';


const DBname = 'SetengStorage';
const win: any = window;

@Injectable()
export class SqliteService {

    private _db: any;

    constructor(private global: GlobalVariable) {

        if (!win.sqlitePlugin) {

            console.warn('Storage: SQLite plugin not installed, falling back to WebSQL. Make sure to install cordova-sqlite-storage in production!');

            this._db = win.openDatabase(DBname, '1.0', 'database', 5 * 1024 * 1024);

        } else {

            this._db = win.sqlitePlugin.openDatabase({
                name: DBname,
                location: 2,
                createFromLocation: 0
            });
        }
        this.initTable();

    }

    /**
     * init table
     */
    public initTable() {

        

        this.query(this.TbUser())
            .catch(err => {
                console.error('Storage: Unable to create initial storage tables', err.tx, err.err);
            });

        this.query(this.TbFinishCase())
            .catch(err => {
                console.error('Storage: Unable to create initial storage tables', err.tx, err.err);
            });
    }

    /**
     * 單一取得
     * @param tbName
     * @param key
     */
    public Get<T>(tbName: string, key: string): Promise<T> {

        let query = `SELECT * FROM ${tbName} WHERE key = ?`;

        return this.query(query, [key])
            .then(data => {

                let item = <T>data.res.rows.item(0);

                return item;
            });
    }

    /**
     * 取得清單
     * @param tbName
     */
    public GetList<T>(tbName: string): Promise<T[]> {

        let query = `SELECT * FROM ${tbName}`;

        let items: T[];

        return this.query(query)
            .then(data => {

                for (let i = 0; i < data.res.rows.length; i++) {
                    var row = <T>data.res.rows.item(i);
                    items.push(row);
                }

                return items;
            });
    }

    /**
     * 新增使用者紀錄
     * @param user
     */
    public AddUser(user: User) {


        //let query = "INSERT INTO USER VALUES(? , ? , ? , ? , ? , ? , ? , ? , ? , ? )";
      
        //return this.query(query,
        //       [user.ID,
        //        user.CompCd,
        //        user.VenderCd,
        //        user.CompName,
        //        user.VenderName,
        //        user.deviceID,
        //        user.registrationID,
        //        user.IsHQCheck,
        //        user.LastLoginTime])
        //    .then(data => {
        //    console.log(data.res);
        //});
        return;

    }

   /**
    * 更新使用者紀錄
    * @param user
    */
    public UpdateUser(user: User) {

        //let query = 'UPDATE USER SET Name = ? , CompName = ? ,  VenderName = ? , DeviceID = ? , RegistrationID = ? , IsHQCheck = ? , LastLoginTime = ? , WHERE id = ?';
        //return this.query(query,
        //    [user.Name,
        //     user.CompName,
        //     user.VenderName,
        //     user.deviceID,
        //     user.registrationID,
        //     user.IsHQCheck,
        //     user.LastLoginTime,
        //     user.ID]);

        return;
    };

    /**
     * excute
     * @param query
     * @param params
     */
    private query(query: string, params: any[] = []): Promise<any> {
        return new Promise((resolve, reject) => {
            try {
                this._db.transaction((tx: any) => {
                    tx.executeSql(query, params,
                        (tx: any, res: any) => resolve({ tx: tx, res: res }),
                        (tx: any, err: any) => reject({ tx: tx, err: err }));
                },
                    (err: any) => reject({ err: err }));
            } catch (err) {
                reject({ err: err });
            }
        });
    }


    private TbUser(): string {

        var tbStr: string = '';

        tbStr += " CREATE TABLE IF NOT EXISTS USER ( ";
        tbStr += " Key text primary key ,"; //id
        tbStr += " Name text ,";
        tbStr += " CompCd text ,";
        tbStr += " VenderCd text ,";
        tbStr += " CompName text ,";
        tbStr += " VenderName text ,";
        tbStr += " DeviceID text ,";
        tbStr += " RegistrationID text ,";
        tbStr += " IsHQCheck boolean  , "
        tbStr += " LastLoginTime datetime ) ";

        return tbStr;
    }

    private TbFinishCase(): string {

        var tbStr: string = '';

        tbStr += " CREATE TABLE IF NOT EXISTS FCASE ( ";
        tbStr += " Key text primary key ,";   //Sn
        tbStr += " VndArtificialCost text ,";
        tbStr += " FixMark text ,";
        tbStr += " VndCloseUser text ) ";

        return tbStr;
    }

}
