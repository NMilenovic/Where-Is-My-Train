import { Greska } from "./Greska.js";
import { Korisnik } from "./Korisnik.js";
import { LinijaIzmena } from "./LinijaIzmena.js";
import { Recenzija } from "./Recenzija.js";
import { PrevoznikZahtev } from "./PrevoznikZahtev.js";
import { lang } from "./lang.js";

let spRec = 0;


export class AdminView{

    
    constructor(){
        this.kontejner = null;
        this.ime = document.cookie.split("next")[0];
        this.tip = document.cookie.split("next")[1];
        this.lng = document.cookie.split("next")[2];
        Window.lang = new lang(document.cookie.split("next")[2]);
        }

    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");

        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        this.kontejner = document.createElement("div");
        host.appendChild(this.kontejner);

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

        let enLab = document.createElement("lab");
        enLab.innerHTML = "EN";
        jezikDiv.appendChild(enLab);

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
        
        let srLab = document.createElement("lab");
        srLab.innerHTML = "SR";
        jezikDiv.appendChild(srLab);

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
        logoutBtn.classList.add("dugme");
        logoutBtn.innerHTML = Window.lang.text.logout;
        logDiv.appendChild(logoutBtn);

        logoutBtn.onclick = (ev) =>{
            document.cookie = `l;expires = ${new Date(Date.now() - 100000).toUTCString()};path:/`;
            location.reload();
        }


        

        
        //++++++++++++++++++++++++

        let opcijeDiv = document.createElement("div");
        opcijeDiv.classList.add("opcijeDiv");
        opcijeDiv.classList.add("center");
        this.kontejner.appendChild(opcijeDiv);

        let opcije = [Window.lang.text.banning,Window.lang.text.allowing_transporters,Window.lang.text.reviews,Window.lang.text.errors,Window.lang.text.lines];
        let opcijeFun = [this.CrtajBanovanje,this.CrtajOdobravanje,this.CrtajRecenzije,this.CrtajGreske,this.CrtajLinije];

        opcije.forEach((op,index) =>{
            let opcijaBtn = document.createElement("button");
            opcijaBtn.className = "opcijaBtn";
            opcijaBtn.innerHTML = op;
            opcijeDiv.appendChild(opcijaBtn);
            
            opcijaBtn.onclick = (ev) =>{
                opcijeFun[index](placeholder);
            }
        })
        let placeholder = document.createElement("div");
        placeholder.classList.add("placeholder");
        //placeholder.classList.add("center");
        this.kontejner.appendChild(placeholder);

        let dobrodosli = document.createElement("h1");
        dobrodosli.className = "dobrodosli";
        dobrodosli.innerHTML = Window.lang.text.welcome;
        placeholder.appendChild(dobrodosli);


    }

    CrtajBanovanje(host){
        if(!host)
            throw new Error("nije unet host");

        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        var listaKorisnika = [];
        
        let searchDiv = document.createElement("div");
        searchDiv.classList.add("srcDiv");
        host.appendChild(searchDiv);

        let searchIn = document.createElement("input");
        searchIn.className = "inp";
        searchIn.placeholder = Window.lang.text.username;
        searchDiv.appendChild(searchIn);

        let searchBtn = document.createElement("button");
        searchBtn.classList.add("dugme");
        searchBtn.innerHTML = Window.lang.text.search;
        searchDiv.appendChild(searchBtn);
        searchBtn.onclick = async (ev) =>{
        if(searchIn.value.length > 3){
        await fetch("http://localhost:5001/Korisnik/PronadjiKorisnike/"+searchIn.value).then(p=>{
                p.json().then(p=>{
                    if(p != ""){
                        listaKorisnika = [];
                        while(phBody.firstChild != null)
                                phBody.removeChild(phBody.firstChild);
                        p.forEach(el=>{
                            let k = new Korisnik(el.id,el.username,el.ban,el.datumBanovanja,this);
                            listaKorisnika.push(k);
                        });

                        listaKorisnika.forEach(el=>{
                            let kDiv = document.createElement("div");
                            phBody.appendChild(kDiv);
                            el.Crtaj(kDiv);
                            
                        });
                    }else{
                        while(phBody.firstChild != null)
                                phBody.removeChild(phBody.firstChild);

                        let msg = document.createElement("h2");
                        msg.classList.add("phh");
                        msg.innerHTML = Window.lang.text.no_resoults_found;

                        phBody.appendChild(msg);
                    }
                })
            })

        }else{
            alert(Window.lang.text.username_must_be_longer_than_3);
        }
    }


        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);

        //-----------
        let dobrodosli = document.createElement("h1");
        dobrodosli.className = "dobrodosli";
        dobrodosli.innerHTML = Window.lang.text.enter_search_parameters;
        phBody.appendChild(dobrodosli);

    }

    CrtajOdobravanje(host){
        if(!host)
            throw new Error("nije unet host");

        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        let listaZahteva = [];
        
        fetch("http://localhost:5001/Voznja/VratiSveZahteve").then(p=>{
            p.json().then(p=>{
                if(p != ""){
                listaZahteva = [];
                p.forEach(el=>{
                    let a = new PrevoznikZahtev(el.id,
                        el.listaVoznji.id,
                        el.listaVoznji.pocetnaStanica.mesto,
                        el.listaVoznji.krajnaStanica.mesto,
                        el.usernamePrevoznik,
                        el.listaVoznji.termin,
                        el.listaVoznji.stize,this);
                    listaZahteva.push(a);
                });
                while(phBody.firstChild != null)
                    phBody.removeChild(phBody.firstChild);
                listaZahteva.forEach(el=>{
                    el.Crtaj(phBody);
                });
            }else{
                let msg = document.createElement("h2");
                msg.classList.add("phh");
                        msg.innerHTML = Window.lang.text.no_requests;
                phBody.appendChild(msg);
            }
            })
        });

        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);

        listaZahteva.forEach(el=>{
            el.Crtaj(phBody);
        });
    }

    CrtajRecenzije(host){
        if(!host)
            throw new Error("nije unet host");
        
        let listaRecenzija = [];
        
        if(spRec == 0){
            //console.log(super.spRec);
        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        let searchDiv = document.createElement("div");
        searchDiv.classList.add("srcDiv");
        host.appendChild(searchDiv);

        let searchIn = document.createElement("input");
        searchIn.classList.add("inp");
        searchIn.placeholder = Window.lang.text.station;
        searchDiv.appendChild(searchIn);

        let chBtn = document.createElement("button");
        chBtn.classList.add("dugme");
        chBtn.innerHTML = Window.lang.text.transporter_reviews;
        searchDiv.appendChild(chBtn);
        chBtn.onclick = (ev) =>{
            spRec = 1;
            this[2](host);
        }

        let dropDiv = document.createElement("div");
        dropDiv.className = "dropDiv";
        searchDiv.appendChild(dropDiv);

        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);

        let dobrodosli = document.createElement("h1");
        dobrodosli.className = "dobrodosli";
        dobrodosli.innerHTML = Window.lang.text.enter_search_parameters;
        phBody.appendChild(dobrodosli);

        searchIn.onkeyup = (ev) =>{
            
            //fetch SearchIn
            if(searchIn.value != ""){
            fetch("http://localhost:5001/Stanica/PretragaStanice/"+searchIn.value).then(p=>{
                p.json().then(p=>{
                    while(dropDiv.firstChild != null)
                        dropDiv.removeChild(dropDiv.firstChild);
                        if(p != ""){
                            p.forEach(el =>{
                                let opcija = document.createElement("div");
                                opcija.className = "opcija";
                                let lab = document.createElement("label");
                                lab.innerHTML = el.mesto;
                                lab.value = el.id;
                                opcija.appendChild(lab);
                                dropDiv.appendChild(opcija);
                                //-----
                                
                                opcija.onclick = (ev) =>{
                                    
                                    fetch("http://localhost:5001/Recenzije/VratiRecenzijeStanice/"+lab.innerHTML).then(p=>{
                                    if(p.status == 200){
                                    p.json().then(p=>{
                                        while(phBody.firstChild != null)
                                            phBody.removeChild(phBody.firstChild);
                                        listaRecenzija = [];
                                        if(p != ""){
                                            p.forEach(el=>{
                                                let a = new Recenzija(el.id,el.stanica.mesto,el.sadrzaj,el.autor.username,0);
                                                listaRecenzija.push(a);
                                            });
                                            listaRecenzija.forEach(el=>{
                                                el.Crtaj(phBody);
                                            });
                                        }else{
                                            let msg = document.createElement("h2");
                                            msg.classList.add("phh");
                                            msg.innerHTML = Window.lang.text.no_reviews;
                                            phBody.appendChild(msg);
                                        }
                                    })
                                }else{
                                    alert(Window.lang.text.error);
                                }

                                })

                                searchIn.value = "";
                                while(dropDiv.firstChild != null)
                                    dropDiv.removeChild(dropDiv.firstChild);
                                }
                            });
                        }else{

                        }
                })
            })
        }else{
            while(dropDiv.firstChild != null)
                        dropDiv.removeChild(dropDiv.firstChild);
                    
        }

        }
    }else{

        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        let searchDiv = document.createElement("div");
        searchDiv.classList.add("srcDiv");
        host.appendChild(searchDiv);

        let searchIn = document.createElement("input");
        searchIn.classList.add("inp");
        searchIn.placeholder = Window.lang.text.transporter;
        searchDiv.appendChild(searchIn);

        let chBtn = document.createElement("button");
        chBtn.classList.add("dugme");
        chBtn.innerHTML = Window.lang.text.station_reviews;
        searchDiv.appendChild(chBtn);
        chBtn.onclick = (ev) =>{
            spRec = 0;
            this[2](host);
        }

        let dropDiv = document.createElement("div");
        dropDiv.className = "dropDiv";
        searchDiv.appendChild(dropDiv);

        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);

        let dobrodosli = document.createElement("h1");
        dobrodosli.className = "dobrodosli";
        dobrodosli.innerHTML = Window.lang.text.enter_search_parameters;
        phBody.appendChild(dobrodosli);

        searchIn.onkeyup = (ev) =>{
            
            //fetch SearchIn
            if(searchIn.value != ""){
            fetch("http://localhost:5001/Prevoznik/PretragaPrevoznika/"+searchIn.value).then(p=>{
                p.json().then(p=>{
                    while(dropDiv.firstChild != null)
                        dropDiv.removeChild(dropDiv.firstChild);
                        if(p != ""){
                            p.forEach(el =>{
                                let opcija = document.createElement("div");
                                opcija.className = "opcija";
                                let lab = document.createElement("label");
                                lab.innerHTML = el.username;
                                lab.value = el.id;
                                opcija.appendChild(lab);
                                dropDiv.appendChild(opcija);
                                //-----
                                
                                opcija.onclick = (ev) =>{
                                    
                                    fetch("http://localhost:5001/Recenzije/VratiRecenzijePrevoznika/"+lab.innerHTML).then(p=>{
                                    if(p.status == 200){
                                    p.json().then(p=>{
                                        while(phBody.firstChild != null)
                                            phBody.removeChild(phBody.firstChild);
                                        listaRecenzija = [];
                                        if(p != ""){
                                            p.forEach(el=>{
                                                let a = new Recenzija(el.id,el.prevoznik.username,el.sadrzaj,el.autor.username,1);
                                                listaRecenzija.push(a);
                                            });
                                            listaRecenzija.forEach(el=>{
                                                el.Crtaj(phBody);
                                            });
                                        }else{
                                            let msg = document.createElement("h2");
                                            msg.classList.add("phh");
                                            msg.innerHTML = Window.lang.text.no_reviews;
                                            phBody.appendChild(msg);
                                            //"http://localhost:5001/Karta/DodajKartu/1/500"
                                        }
                                    })
                                }else{
                                    alert(Window.lang.text.error);
                                }

                                })

                                searchIn.value = "";
                                while(dropDiv.firstChild != null)
                                    dropDiv.removeChild(dropDiv.firstChild);
                                }
                            });
                        }else{

                        }
                })
            })
        }else{
            while(dropDiv.firstChild != null)
                        dropDiv.removeChild(dropDiv.firstChild);
                    
        }

        }
    }
}

    async CrtajGreske(host){
        if(!host)
            throw new Error("nije unet host");
        
        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        let listaGresaka = [];
        
        await fetch("http://localhost:5001/Greska/VratiSveGreske").then(p=>{
            p.json().then(p=>{
                if(p != ""){
                    p.forEach(el => {
                        let a = new Greska(el.id,el.opis,this);
                        listaGresaka.push(a);
                    });
                    listaGresaka.forEach(el=>{
                        el.Crtaj(phBody);
                    });
                }else{
                    let msg = document.createElement("h2");
                    msg.classList.add("phh");
                    msg.innerHTML = Window.lang.text.no_errors;
                    phBody.appendChild(msg);
                }
            })
        })
        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);
        
    }

    CrtajLinije(host){
        if(!host)
            throw new Error("nije unet host");
            
        while(host.firstChild != null)
            host.removeChild(host.firstChild);

        let pretragaDiv = document.createElement("div");
        pretragaDiv.classList.add("srcDiv");
        host.appendChild(pretragaDiv);

        let pStan = document.createElement("input");
        pStan.classList.add("inp");
        pStan.placeholder = Window.lang.text.first_station;
        pretragaDiv.appendChild(pStan);

        let zameniBtn = document.createElement("button");
        zameniBtn.classList.add("dugme");
        zameniBtn.innerHTML = "><";
        pretragaDiv.appendChild(zameniBtn);
        zameniBtn.onclick = (ev) =>{
            let a = pStan.value;
            pStan.value = kStan.value;
            kStan.value = a;
        }

        let kStan = document.createElement("input");
        kStan.classList.add("inp");
        kStan.placeholder = Window.lang.text.last_station;
        pretragaDiv.appendChild(kStan);

        let vp = document.createElement("input");
        vp.classList.add("inp");
        vp.type = "date";
        pretragaDiv.appendChild(vp);

        let listaLinija = [];

        let pretragaBtn = document.createElement("button");
        pretragaBtn.classList.add("dugme");
        pretragaBtn.innerHTML = Window.lang.text.search;
        pretragaBtn.onclick = (ev) =>{
            if(pStan.value != "" && kStan.value != "" && vp.value != ""){
            fetch(`http://localhost:5001/Voznja/VratiVoznje/${pStan.value}/${kStan.value}/${vp.value}`,{method: "GET"}).then(p=>{
                p.json().then(p=>{
                    if(p != ""){
                        listaLinija = [];
                        while(phBody.firstChild != null)
                            phBody.removeChild(phBody.firstChild);
                        p.forEach(el=>{
                            let a = new LinijaIzmena(el.id,el.pocetnaStanica.mesto,el.krajnaStanica.mesto,el.prevoznik.username,
                            el.termin,el.stize);
                            listaLinija.push(a);
                        });
                        listaLinija.forEach(el=>{
                            let izDiv = document.createElement("div");
                            phBody.appendChild(izDiv);
                            el.Crtaj(izDiv);
                        })
                    }else{
                        //nema rezultata
                    }
                })
            })
        }else{
            alert(Window.lang.text.enter_search_parameters);
        }
    }
        pretragaDiv.appendChild(pretragaBtn);

        //--------------- dodaj liniju

        let dodaj = document.createElement("button");
        dodaj.innerHTML = Window.lang.text.add_line;
        dodaj.classList.add("dugme");
        pretragaDiv.appendChild(dodaj);
        dodaj.onclick = (ev) =>{
            
            let kont = document.querySelector(".kontejner");
            

            if(document.querySelector(".dodajDiv") == null){

            let dodajDiv = document.createElement("div");
            dodajDiv.classList.add("center");
            dodajDiv.classList.add("dodajDiv");
            dodajDiv.classList.add("z1");
            kont.appendChild(dodajDiv);

            //dDiv = dodajDiv;

            let formDiv = document.createElement("div");
            formDiv.classList.add("formDiv");
            formDiv.classList.add("z1");
            dodajDiv.appendChild(formDiv);

            //-----------

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.add_line;
            formDiv.appendChild(naslov);

            let staniceDiv = document.createElement("div");
            formDiv.appendChild(staniceDiv);

            let polazakIn = document.createElement("input");
            polazakIn.classList.add("inp");
            polazakIn.placeholder = Window.lang.text.first_station;
            staniceDiv.appendChild(polazakIn);

            let zameniBtn = document.createElement("button");
            zameniBtn.classList.add("dugme");
            zameniBtn.innerHTML = "><";
            staniceDiv.appendChild(zameniBtn);
            zameniBtn.onclick = (ev) =>{
                let a = polazakIn.value;
                polazakIn.value = dolazakIn.value;
                dolazakIn.value = a;
            }

            let dolazakIn = document.createElement("input");
            dolazakIn.classList.add("inp");
            dolazakIn.placeholder = Window.lang.text.last_station;
            staniceDiv.appendChild(dolazakIn);

            //-------
            let datumDiv = document.createElement("div");
            formDiv.appendChild(datumDiv);

            let dp = document.createElement("input");
            dp.classList.add("inp");
            dp.type = "date";
            datumDiv.appendChild(dp);
            
            let dd = document.createElement("input");
            dd.classList.add("inp");
            dd.type = "date";
            datumDiv.appendChild(dd);

            //--

            let vremeDiv = document.createElement("div");
            formDiv.appendChild(vremeDiv);

            let vp = document.createElement("input");
            vp.classList.add("inp");
            vp.type = "time";
            vp.placeholder = Window.lang.text.departure_time;
            vremeDiv.appendChild(vp);
            
            let vd = document.createElement("input");
            vd.classList.add("inp");
            vd.type = "time";
            vd.placeholder = Window.lang.text.arrival_time;
            vremeDiv.appendChild(vd);

            //----------

            let dodajBtn = document.createElement("button");
            dodajBtn.classList.add("dugme");
            dodajBtn.innerHTML = Window.lang.text.add_line;
            formDiv.appendChild(dodajBtn);
            dodajBtn.onclick = (ev) =>{
                if(polazakIn.value != "" && dolazakIn.value != "" && dp.value != "" && vp.value != "" && dd.value != "" && vd.value != ""){
                fetch(`http://localhost:5001/Voznja/DodajVoznju/${polazakIn.value}/${dolazakIn.value}/${dp.value+"T"+vp.value+":00"}/${dd.value+"T"+vd.value+":00"}`,{method: "POST"}).then(p=>{
                    if(p.status == 200){
                        alert(Window.lang.text.line_added);
                        kont.removeChild(dodajDiv);

                    }else{
                        alert(Window.lang.text.error_while_adding_line);
                    }
                });
            }else{
                alert(Window.lang.text.enter_all_data);
            }
            }

            let ponistiBtn = document.createElement("button");
            ponistiBtn.classList.add("dugme");
            ponistiBtn.innerHTML = Window.lang.text.back;
            formDiv.appendChild(ponistiBtn);

            ponistiBtn.onclick = (ev) =>{
                kont.removeChild(dodajDiv);
            }
        }
            //-----------
        }

        //------------------

        let phBody = document.createElement("div");
        phBody.classList.add("phBody");
        phBody.classList.add("canter");
        host.appendChild(phBody);

        let dobrodosli = document.createElement("h1");
        dobrodosli.className = "dobrodosli";
        dobrodosli.innerHTML = Window.lang.text.enter_search_parameters;
        phBody.appendChild(dobrodosli);
    }

}