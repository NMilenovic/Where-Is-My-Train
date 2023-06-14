import { Login } from "./Login.js";
import { AdminView } from "./AdminView.js";
import { KorisnikView } from "./KorisnikView.js";
import { PrevoznikView } from "./PrevoznikView.js";
import { GostView } from "./GostView.js";

if(document.cookie == ""){
    Window.lan = "en";
var login = new Login();
//login.CrtajAdminLogin(document.body);
login.Crtaj(document.body);
}else{
    let tip = document.cookie.split("next")[1];
    //console.log(new Date(Date.now() - 100000).toUTCString()); <-- brise cookie
    switch(tip){
        case "prevoznik":
            let prevoznik = new PrevoznikView();
            prevoznik.Crtaj(document.body);
            break;
        case "korisnik":
            let korisnik = new KorisnikView();
            korisnik.Crtaj(document.body);
            break;
        case "admin":
            let admin = new AdminView();
            admin.Crtaj(document.body);
            break;
        case "gost":
            let gost = new GostView();
            gost.Crtaj(document.body);
            break;
    }
}
    
