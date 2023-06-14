import { lang } from "./lang.js";
export class Login{

    constructor(main){
        this.kontejner = null;
        this.main = main;
    }
    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");
        this.lang = new lang(Window.lan);


        while(host.firstChild != null)
            host.removeChild(host.firstChild);
        
        this.kontejner = document.createElement("div");
        this.kontejner.className = "logKontejner";
        host.appendChild(this.kontejner);
        
        let logDiv = document.createElement("div");
        logDiv.className = "loginDiv";
        this.kontejner.appendChild(logDiv);
        

        //---------------------------
        let loginNaslov = document.createElement("h2");
        loginNaslov.className = "loginNaslov";
        loginNaslov.innerHTML = this.lang.text.login;
        logDiv.appendChild(loginNaslov);

        let passUserDiv = document.createElement("div");
        passUserDiv.className = "passUserDiv";
        logDiv.appendChild(passUserDiv);

        let emailIn = document.createElement("input");
        emailIn.className = "loginInU";
        emailIn.placeholder = this.lang.text.username;
        passUserDiv.appendChild(emailIn);

        let passIn = document.createElement("input");
        passIn.type = "password";
        passIn.className = "loginInP";
        passIn.placeholder = this.lang.text.password;
        passUserDiv.appendChild(passIn);
        //-----------tip korisnika
        let typeDiv = document.createElement("div");
        typeDiv.className = "typeDiv";
        logDiv.appendChild(typeDiv);

        let kLab = document.createElement("label");
        kLab.innerHTML = this.lang.text.user;
        typeDiv.appendChild(kLab);
        let typeKIn = document.createElement("input");
        typeKIn.value = "korisnik";
        typeKIn.type = "radio";
        typeKIn.name = "type";
        typeKIn.checked = "checked";
        typeDiv.appendChild(typeKIn);

        let tLab = document.createElement("label");
        tLab.innerHTML = this.lang.text.transporter;
        typeDiv.appendChild(tLab);
        let typeTIn = document.createElement("input");
        typeTIn.value = "prevoznik";
        typeTIn.type = "radio";
        typeTIn.name = "type";
        typeDiv.appendChild(typeTIn);

        //---------------
        let logBtn = document.createElement("button");
        logBtn.innerHTML = this.lang.text.login;
        logBtn.className = "logBtn";
        logBtn.type = "submit";
        logDiv.appendChild(logBtn);

        logBtn.onclick = (ev) =>{
            let typeSelected = this.kontejner.querySelector("input[type=radio]:checked");
            if(emailIn.value == "" || passIn.value == ""){
            alert(this.lang.text.invalid_data);
            }else{
           
            if(typeSelected.value == "korisnik"){
                //fetch korisnik

                fetch(`http://localhost:5001/Korisnik/VratiKorisnika/${emailIn.value}/${passIn.value}`,{method: "GET"})
            .then(p=>{
                if(p.status == 200){
                    p.json().then(p =>{
                        if(p.ban != 1){
                            document.cookie = `${emailIn.value}next${typeSelected.value}next${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()}next${p.id};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
                            location.reload();
                        }else{
                            alert(Window.lang.text.KorisnikBanovan);
                        }
                    })
                }else{
                    alert(this.lang.text.login_failed);
                }

                
            });
                
            }else if(typeSelected.value == "prevoznik"){
                //fetch prevoznik

                fetch(`http://localhost:5001/Prevoznik/VratiPrevoznika/${emailIn.value}/${passIn.value}`,{method: "GET"})
            .then(p=>{
                if(p.status == 200){
                    p.json().then(p =>{
                        if(p.ban != 1){
                            document.cookie = `${emailIn.value}next${typeSelected.value}next${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()}next${p.id};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
                            location.reload();
                        }else{
                            alert(Window.lang.text.KorisnikBanovan);
                        }
                    })
                }else{
                    alert(this.lang.text.login_failed);
                }

                
            });
            }else{
                alert(Window.lang.text.error);
            }
            
            //document.cookie = "username=;"+"expires="+new Date(Date.now() - 1000).toUTCString()+";";
            
            //document.cookie = `${emailIn.value}next${typeSelected.value}next${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
            //location.reload();
            }
        }
        let gostLogBtn = document.createElement("button");
        gostLogBtn.innerHTML = this.lang.text.continue_as_guest;
        gostLogBtn.className = "logBtnGuest";
        gostLogBtn.type = "submit";
        logDiv.appendChild(gostLogBtn);

        gostLogBtn.onclick = (ev) =>{
            document.cookie = `${emailIn.value}nextgostnext${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
            location.reload();
        }

        let reg = document.createElement("label");
        reg.innerHTML = this.lang.text.click_here_to_create_account;
        reg.className = "reg";

        reg.onclick = (ev) =>{
            this.CrtajRegister(host);
        }
        //--------------
        let jezikDiv = document.createElement("div");
        jezikDiv.className = "jezikDiv";
        logDiv.appendChild(jezikDiv);

       /* let enLab = document.createElement("lab");
        enLab.innerHTML = "EN";
        jezikDiv.appendChild(enLab);*/
        let en2=document.createElement("img");
        en2.src="engZastava.jpg";
        en2.className="zastave";
        jezikDiv.appendChild(en2);

        let jezikLab = document.createElement("label");
        jezikLab.className = "switch";
        jezikDiv.appendChild(jezikLab);

        let jezikIn = document.createElement("input");
        jezikIn.classList.add("jezikIn");
        jezikIn.type = "checkbox";
        jezikLab.appendChild(jezikIn);

        let jezikSp = document.createElement("span");
        jezikSp.className = "slider";
        jezikLab.appendChild(jezikSp);
        
        /*let srLab = document.createElement("lab");
        srLab.innerHTML = "SR";
        jezikDiv.appendChild(srLab);*/
        let srb2=document.createElement("img");
        srb2.src="srbZastava.jpg";
        srb2.className="zastave";
        jezikDiv.appendChild(srb2);


        if(Window.lan == "sr")
            jezikIn.checked = true;

        jezikIn.onclick = (ev) =>{
            if(jezikIn.checked){
                Window.lan = "sr";
            }else{
                Window.lan = "en";
            }
            this.lang.zameni(Window.lan);
            this.Crtaj(document.body);
        }

        logDiv.appendChild(reg);
        //---------------------------///
        
    }

    CrtajRegister(host){
        if(!host)
            throw new Error("nije unet host");

        let logDiv = this.kontejner.querySelector(".loginDiv");
        while(logDiv.firstChild != null)
            logDiv.removeChild(logDiv.firstChild);
        
        let regNaslov = document.createElement("h2");
        regNaslov.innerHTML = this.lang.text.register;
        logDiv.appendChild(regNaslov);

        //----------------------------

        let inDiv = document.createElement("div");
        inDiv.className = "inDiv";
        logDiv.appendChild(inDiv);

        let userIn = document.createElement("input");
        userIn.placeholder = this.lang.text.username;
        userIn.className = "loginIn";
        inDiv.appendChild(userIn);

        let passIn = document.createElement("input");
        passIn.placeholder = this.lang.text.password;
        passIn.className = "loginIn";
        passIn.type = "password";
        inDiv.appendChild(passIn);

        let passRepIn = document.createElement("input");
        passRepIn.placeholder = this.lang.text.repeat_password;
        passRepIn.className = "loginIn";
        passRepIn.type = "password";
        inDiv.appendChild(passRepIn);

        let btn = document.createElement("button");
        btn.innerHTML = this.lang.text.register;
        btn.className = "logBtn";
        

        let radioInDiv = document.createElement("div");
        logDiv.appendChild(radioInDiv);

        let korLab = document.createElement("label");
        korLab.innerHTML = this.lang.text.user + ": ";
        radioInDiv.appendChild(korLab);

        let korIn = document.createElement("input");
        korIn.type = "radio";
        korIn.name = "rad";
        korIn.value = "korisnik";
        korIn.checked = true;
        korIn.className = "rad";
        radioInDiv.appendChild(korIn);

        let prevLab = document.createElement("label");
        prevLab.innerHTML = this.lang.text.transporter + ": ";
        radioInDiv.appendChild(prevLab);

        let prevIn = document.createElement("input");
        prevIn.type = "radio";
        prevIn.name = "rad";
        prevIn.value = "prevoznik";
        prevIn.className = "rad";
        radioInDiv.appendChild(prevIn);

        btn.onclick = (ev) =>{
            if(userIn.value == "")
            alert(this.lang.text.invalid_username);
            if(passIn.value != passRepIn.value)
            alert(this.lang.text.passwords_do_not_match);

            
            var data2 = JSON.stringify({
                "username": userIn.value
            })
            var data = JSON.stringify({
                "username": userIn.value
            })
            let checked = this.kontejner.querySelector("input[type='radio'][name='rad']:checked");
            if(checked.value == "korisnik")
            {
            fetch("http://localhost:5001/Korisnik/DodajKorisnika/"+passIn.value,{
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: data

            }).then(p => {
                if(p.ok)
                    alert(Window.lang.text.KorisnikDodat);
                else
                    alert(Window.lang.text.error);
            })
            }
            else if(checked.value == "prevoznik")
            {
                fetch("http://localhost:5001/Prevoznik/DodajPrevoznika/"+passIn.value,{
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: data2

            }).then(p => {
                if(p.ok)
                    alert(Window.lang.text.PrevoznikDodat);
                else
                    alert(Window.lang.text.error);
            })
            }else{
                alert(this.lang.text.insertion_error);
            }
            this.Crtaj(host);
        }
        logDiv.appendChild(btn);

        let log = document.createElement("label");
        log.innerHTML = this.lang.text.click_here_to_login;
        log.className = "reg";

        log.onclick = (ev) =>{
            this.Crtaj(host);
        }


        logDiv.appendChild(log);
    }


    CrtajAdminLogin(host){
        this.Crtaj(host);
        this.kontejner.querySelector(".loginNaslov").innerHTML = this.lang.text.admin_login;
        this.kontejner.querySelector(".loginDiv").removeChild(this.kontejner.querySelector(".typeDiv"));
        this.kontejner.querySelector(".loginDiv").removeChild(this.kontejner.querySelector(".reg"));
        this.kontejner.querySelector(".loginDiv").removeChild(this.kontejner.querySelector(".logBtnGuest"));


        let btn = this.kontejner.querySelector(".logBtn");
        btn.onclick = (ev) =>{
            
            let typeSelected = "admin";
            let emailIn = this.kontejner.querySelector(".loginInU");
            let passIn = this.kontejner.querySelector(".loginInP");
            if(emailIn.value == "" || passIn.value == ""){
            alert(this.lang.text.enter_all_data);
            }else{
                //document.cookie = `${emailIn.value}next${typeSelected}next${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
            fetch(`http://localhost:5001/Administrator/VratiAdministratora/${emailIn.value}/${passIn.value}`,{method: "GET"})
            .then(p=>{
                if(p.status == 200){
                    p.json().then(p =>{
                        document.cookie = `${emailIn.value}next${typeSelected}next${Window.lan}next${new Date(Date.now() + 15*60000).toUTCString()};expires=${ new Date(Date.now() + 15*60000).toUTCString()};path=/FE;`;
                        location.reload();
                    })
                }else{
                    alert(this.lang.text.login_failed);
                }

                
            });
            
            //document.cookie = "username=;"+"expires="+new Date(Date.now() - 1000).toUTCString()+";";
            //document.cookie = `${emailIn.value},${typeSelected};expires=${ new Date(Date.now() + 15*60000).toUTCString()}; path=/;`;
            //location.reload();
            }


        }

        let jezikIn = this.kontejner.querySelector(".jezikIn");
            jezikIn.onclick = (ev) =>{
                if(jezikIn.checked){
                    Window.lan = "sr";
                }else{
                    Window.lan = "en";
                }
                this.lang.zameni(Window.lan);
                this.CrtajAdminLogin(document.body);
            }
    }

}