import { Injectable } from '@angular/core';


@Injectable()
export class RootScope {



    private RootData: Array<{ key: string, data: any }> = [];

    public set(key: string, data: any) {

        console.log("----------Set RootScope  ----------");
        console.log(`KEY:${key}`);
        console.log(`DATA:${JSON.stringify(data)}`);


        if (this.RootData.some(x => x.key == key)) {

            this.RootData.find(x => x.key == key).data = data;

            return;
        };

        this.RootData.push({ key: key, data: data });

    }

    public get<T>(key: string): T {

        var value = (!this.RootData.some(x => x.key == key)) ? {} :
            this.RootData.find(x => x.key == key).data

        console.log("---------- Get RootScope ----------");
        console.log(`KEY:${key}`);
        console.log(`DATA:${value}`);


        return <T>value;

    }

    public clear(): void {

        console.log("---------- Clear RootScope ----------");

        this.RootData = [];

    }




}
