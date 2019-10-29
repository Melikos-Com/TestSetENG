import { Injectable } from '@angular/core';



@Injectable()
export class LocalStorage {

    constructor() { };

    set(key: string, data: any) {

        console.log("---------- Set LocalStorage ----------");
        console.log(`KEY:${key}`);
        console.log(`DATA:${JSON.stringify(data)}`);

        window.localStorage.setItem(key, JSON.stringify(data));

    }

    get<T>(key: string): T {

        var value = window.localStorage.getItem(key);

        console.log("---------- Get LocalStorage ----------");
        console.log(`KEY:${key}`);   
        console.log(`DATA:${value}`);

        return <T>(JSON.parse(value)) ;

    }
  

    clear(): void {

        console.log("---------- Clear LocalStorage ----------");

        window.localStorage.clear();

    }

}
