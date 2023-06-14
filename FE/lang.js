import { langEN } from "./lang.en.js";
import { langSR } from "./lang.sr.js";
export class lang{

    text;

    constructor(jezik){
        if(jezik == "en")
        this.text = new langEN();
        if(jezik == "sr")
        this.text = new langSR();
    }
    zameni(jezik){

        if(jezik == "en"){
            this.text = new langEN();
        }
        if(jezik == "sr"){
            this.text = new langSR();
        }
    }

}