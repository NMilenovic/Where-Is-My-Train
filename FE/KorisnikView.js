import { lang } from "./lang.js";

import { PretragaLinija } from "./PretragaLinija.js";

export class KorisnikView{

    constructor(){
        this.kontejner = null;
        this.lng = document.cookie.split("next")[2];
        Window.lang = new lang(document.cookie.split("next")[2]);
        this.ime = document.cookie.split("next")[0];
        this.tip = document.cookie.split("next")[1];
    }
    
    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");
        
        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        
        this.kontejner = document.createElement("div");
        this.kontejner.className = "kontejner";
        host.appendChild(this.kontejner);

        let headerDiv = document.createElement("div");
        headerDiv.className = "head";
        this.kontejner.appendChild(headerDiv);

        let logo = document.createElement("h1");
        logo.innerHTML = Window.lang.text.where_is_my_train;
        logo.className = "logo";
        headerDiv.appendChild(logo);

        let jezLogDiv = document.createElement("div");
        headerDiv.appendChild(jezLogDiv);
        jezLogDiv.className = "jezLog";

        //-----------------

        let jezikDiv = document.createElement("div");
        jezikDiv.className = "jezikDiv";
        jezLogDiv.appendChild(jezikDiv);

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

        if(this.lng == "sr")
            jezikIn.checked = true;

        jezikIn.onclick = (ev) =>{
            if(jezikIn.checked){
                document.cookie = `${this.ime}next${this.tip}nextsrnext${document.cookie.split("next")[3]};expires=${document.cookie.split("next")[3]};path:/;`;
                this.lng = "sr";
            }else{
                document.cookie = `${this.ime}next${this.tip}nextennext${document.cookie.split("next")[3]};expires=${document.cookie.split("next")[3]};path:/;`;
                this.lng = "en";
            }
            Window.lang.zameni(this.lng);
            //console.log(document.cookie)
            this.Crtaj(host);
        }

        //--------------------

        let logDiv = document.createElement("div");
        logDiv.className = "logDiv";
        jezLogDiv.appendChild(logDiv);

        let username = document.createElement("label");
        username.innerHTML = Window.lang.text.username+": " + this.ime;
        logDiv.appendChild(username);

        let logoutBtn = document.createElement("button");
        logoutBtn.className="btn btn-primary";
        logoutBtn.innerHTML = Window.lang.text.logout;
        logDiv.appendChild(logoutBtn);

        logoutBtn.onclick = (ev) =>{
            document.cookie = `l;expires = ${new Date(Date.now() - 100000).toUTCString()};path:/`;
            location.reload();
        }

        //-------------------------------
        let searchDiv = document.createElement("div");
        searchDiv.className = "searchDiv";
        this.kontejner.appendChild(searchDiv);

        let polStan = document.createElement("input");
        polStan.placeholder = Window.lang.text.PolaznaStanica;
        polStan.className = "form-control rounded";
        searchDiv.appendChild(polStan);

        let dolStan = document.createElement("input");
        dolStan.placeholder = Window.lang.text.DolaznaStanica;
        dolStan.className = "form-control rounded";
        searchDiv.appendChild(dolStan);

        let datumIn = document.createElement("input");
        datumIn.className = "form-control rounded";
        datumIn.type = "date";
        searchDiv.appendChild(datumIn);

        let pbody = document.createElement("div");
        pbody.className = "pbody";
        this.kontejner.appendChild(pbody);

        let srcBtn = document.createElement("button");
        srcBtn.innerHTML = Window.lang.text.search;
        srcBtn.className="btn btn-primary";
        srcBtn.onclick=(ev)=>
        {        
        if(polStan.value != "" && dolStan.value != "" && datumIn.value != ""){
        fetch(`http://localhost:5001/Voznja/VratiVoznje/${polStan.value}/${dolStan.value}/${datumIn.value}`,{
            method: "GET"}).then(s=>{
                if(s.ok)
                {
                    s.json().then(s=>{
                        while(pbody.firstChild != null)
                            pbody.removeChild(pbody.firstChild);
                        if(s != ""){
                        s.forEach(el => {
                             let a = new PretragaLinija(el.id,convertFromStringToDate(el.termin),el.pocetnaStanica.mesto,el.krajnaStanica.mesto,convertFromStringToDate(el.stize),el.prevoznik.id,el.prevoznik.username,this.ime,el.pocetnaStanica.id,el.krajnaStanica.id);
                            
                             function convertFromStringToDate(responseDate) { 
                                let prviDatum=   new Date(Date.parse(responseDate));
                                let DatumtoS=prviDatum.toString();
                                let k =DatumtoS.slice(0,25);                     
                                return(k)      
                                                                   
                                                     
                            }
                            a.crtaj(pbody);
                        });
                        }else{
                            let v = document.createElement("h3");
                            v.className = "phh";
                            v.innerHTML = "Nema pronadjenih rezultata";
                            pbody.appendChild(v);
                        }
                    })
                
                }
                else
                {
                    alert(Window.lang.text.DosloDoGreske);
                }
            })
        }else{
            alert(Window.lang.text.NisuUnetiPodaci);
        }

        }
        searchDiv.appendChild(srcBtn);


        //----------------------------
        

    }
    
}