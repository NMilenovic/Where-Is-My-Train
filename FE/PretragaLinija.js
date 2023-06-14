export class PretragaLinija
{
    constructor(id,DP, PS, KS, DD,prevoznikId,prevoznikUser,korisnikUser,pocetnaId,krajnjaId){
        this.id = id;
        this.kontejner = null;
        this.datumPolaska = DP;
        this.datumDolaska = DD;
        this.pocetnaStanica = PS;
        this.krajnjaStanica = KS;
        this.prevoznikId = prevoznikId;
        this.prevoznikUser = prevoznikUser;
        this.korisnikUser = korisnikUser;
        this.pocetnaId = pocetnaId;
        this.krajnjaId = krajnjaId;

    }

    crtaj(host)
    {
        if(!host)
            throw new Error("Host nije unet.");

        //while(host.firstChild != null)
            //host.removeChild(host.firstChild);

        this.kontejner = document.createElement("div");
        host.appendChild(this.kontejner);

        let linijaDiv = document.createElement("div");
        linijaDiv.className = "linijaDiv";
        this.kontejner.appendChild(linijaDiv);


        let divPS = document.createElement("div");
        divPS.classList.add("divPS");
        divPS.classList.add("linDiv");
        linijaDiv.appendChild(divPS);

        let labelaPS = document.createElement("label");
        labelaPS.className = "labelaPS";
        labelaPS.innerHTML = Window.lang.text.PolaznaStanica;
        divPS.appendChild(labelaPS);

        let labelaImeStanice = document.createElement("label");
        labelaImeStanice.innerHTML = this.pocetnaStanica;
        divPS.appendChild(labelaImeStanice);

        let divDPolaska = document.createElement("div");
        divDPolaska.classList.add("divDPolaska");
        divDPolaska.classList.add("linDiv");
        linijaDiv.appendChild(divDPolaska);

        let labelaDPolaska = document.createElement("label");
        labelaDPolaska.className = "labelaDPolaska";
        labelaDPolaska.innerHTML = Window.lang.text.VremePolaska;
        divDPolaska.appendChild(labelaDPolaska);

        let labelaDPolaskaVreme = document.createElement("label");
        labelaDPolaskaVreme.innerHTML = this.datumPolaska;
        divDPolaska.appendChild(labelaDPolaskaVreme);

        //---------------------------------------------------------------------- 

        let prevoznikDiv = document.createElement("div");
        prevoznikDiv.classList.add("prevDiv");
        prevoznikDiv.classList.add("linDiv");
        linijaDiv.appendChild(prevoznikDiv);

        let prevoznikLab = document.createElement("label");
        prevoznikLab.innerHTML = Window.lang.text.Prevoznik;
        prevoznikLab.className="prevoznikLab1";
        prevoznikDiv.appendChild(prevoznikLab);

        let prevoznikNam = document.createElement("label");
        prevoznikNam.innerHTML = this.prevoznikUser;
        prevoznikDiv.appendChild(prevoznikNam);

        divPS.onclick = (ev) =>{
            //POLJE ZA RECENZIJU
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

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.station_reviews + this.pocetnaStanica;
            formDiv.appendChild(naslov);

            let inputFld = document.createElement("textarea");
            inputFld.className = "inputFld";
            formDiv.appendChild(inputFld);

            let submitBtn = document.createElement("button");
            submitBtn.innerHTML = Window.lang.text.Submit;
            submitBtn.classList.add("dugme");
            formDiv.appendChild(submitBtn);

            submitBtn.onclick = (ev) =>{
                if(this.korisnikUser != "" && this.pocetnaId != "" && inputFld.value != ""){
                fetch(`http://localhost:5001/Recenzije/NovaRecenzijaStanice/${this.korisnikUser}/${this.pocetnaId}/${inputFld.value}`,{method: "POST"}).then(p=>{
                    if(p.status == 200){
                        alert(Window.lang.text.RencezijaPoslata);
                    }else{
                        alert(Window.lang.text.DosloDoGreske);
                    }
                    kont.removeChild(dodajDiv);
                });
            }else{
                alert(Window.lang.text.NijeUnetText);
            }
            }


            let ponistiBtn = document.createElement("button");
            ponistiBtn.classList.add("dugme");
            ponistiBtn.innerHTML = Window.lang.text.back;
            formDiv.appendChild(ponistiBtn);
          

            //--------------------------
            let recenzijeBtn = document.createElement("button");
            recenzijeBtn.innerHTML = Window.lang.text.sve_recenzijeS;
            recenzijeBtn.classList.add("dugme");
            formDiv.appendChild(recenzijeBtn);
            
            recenzijeBtn.onclick = ev =>{
                while(formDiv.firstChild != null)
                    formDiv.removeChild(formDiv.firstChild);

                fetch("http://localhost:5001/Recenzije/VratiRecenzijeStanice/"+this.pocetnaStanica).then(p=>{
                    if(p.status == 200){
                        p.json().then(p=>{
                            if(p != ""){
                            let ns = document.createElement("h3");
                            ns.innerHTML = Window.lang.text.reviews;
                            formDiv.appendChild(ns);
                            p.forEach(el => {
                                
                                let divv = document.createElement("div");
                                divv.className = "divv";
                                formDiv.appendChild(divv);

                                let lab = document.createElement("label");
                                lab.innerHTML = el.autor.username + ": " + el.sadrzaj;
                                divv.appendChild(lab);

                            });

                            let nz = document.createElement("button");
                            nz.className = "dugme";
                            nz.innerHTML = Window.lang.text.back;
                            formDiv.appendChild(nz);
                            nz.onclick = (ev) =>{
                
                                kont.removeChild(dodajDiv);
                                }
                            }else{
                                let nema = document.createElement("h3");
                                nema.innerHTML = Window.lang.text.nema_recenzijaS;
                                formDiv.appendChild(nema);
                                let btnN = document.createElement("button");
                                btnN.className = "dugme";
                                btnN.innerHTML = Window.lang.text.back;
                                btnN.onclick = (ev) =>{
                
                                    kont.removeChild(dodajDiv);
                                    }
                                
                                formDiv.appendChild(btnN);
                            }

                        })
                    }else{
                        alert(Window.lang.text.error);
                        kont.removeChild(dodajDiv);
                    }
                })
            }

            ponistiBtn.onclick = (ev) =>{
                
                kont.removeChild(dodajDiv);
                }

            }
        }

        prevoznikDiv.onclick = (ev) =>{
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

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.RecenzijaPrevoznika + this.prevoznikUser;
            formDiv.appendChild(naslov);

            let inputFld = document.createElement("textarea");
            inputFld.className = "inputFld";
            formDiv.appendChild(inputFld);

            let submitBtn = document.createElement("button");
            submitBtn.innerHTML = Window.lang.text.Submit;
            submitBtn.classList.add("dugme");
            formDiv.appendChild(submitBtn);

            submitBtn.onclick = (ev) =>{
                if(this.korisnikUser != "" && this.prevoznikId != "" && inputFld.value != ""){
                fetch(`http://localhost:5001/Recenzije/NovaRecenzijaPrevoznika/${this.korisnikUser}/${this.prevoznikId}/${inputFld.value}`,{method: "POST"}).then(p=>{
                    if(p.status == 200){
                        alert(Window.lang.text.RencezijaPoslata);
                    }else{
                        alert(Window.lang.text.DosloDoGreske);
                    }
                    kont.removeChild(dodajDiv);
                });
            }else{
                alert(Window.lang.text.NijeUnetText);
            }
            }


            let ponistiBtn = document.createElement("button");
            ponistiBtn.classList.add("dugme");
            ponistiBtn.innerHTML = Window.lang.text.back;
            formDiv.appendChild(ponistiBtn);

            ponistiBtn.onclick = (ev) =>{
                
                kont.removeChild(dodajDiv);
                }

                let recenzijeBtn = document.createElement("button");
                recenzijeBtn.innerHTML = Window.lang.text.sve_recenzijeP;
                recenzijeBtn.classList.add("dugme");
                formDiv.appendChild(recenzijeBtn);
                
                recenzijeBtn.onclick = ev =>{
                    while(formDiv.firstChild != null)
                        formDiv.removeChild(formDiv.firstChild);
    
                    fetch("http://localhost:5001/Recenzije/VratiRecenzijePrevoznika/"+this.prevoznikUser).then(p=>{
                        if(p.status == 200){
                            p.json().then(p=>{
                                if(p != ""){
                                let ns = document.createElement("h3");
                                ns.innerHTML = Window.lang.text.reviews;
                                formDiv.appendChild(ns);
                                p.forEach(el => {
                                    
                                    let divv = document.createElement("div");
                                    divv.className = "divv";
                                    formDiv.appendChild(divv);
    
                                    let lab = document.createElement("label");
                                    lab.innerHTML = el.autor.username + ": " + el.sadrzaj;
                                    divv.appendChild(lab);
    
                                });
    
                                let nz = document.createElement("button");
                                nz.className = "dugme";
                                nz.innerHTML = Window.lang.text.back;
                                formDiv.appendChild(nz);
                                nz.onclick = (ev) =>{
                    
                                    kont.removeChild(dodajDiv);
                                    }
                                }else{
                                    let nema = document.createElement("h3");
                                    nema.innerHTML = Window.lang.text.nema_recenzijaP;
                                    formDiv.appendChild(nema);
                                    let btnN = document.createElement("button");
                                    btnN.className = "dugme";
                                    btnN.innerHTML = Window.lang.text.back;
                                    btnN.onclick = (ev) =>{
                    
                                        kont.removeChild(dodajDiv);
                                        }
                                    
                                    formDiv.appendChild(btnN);
                                }
    
                            })
                        }else{
                            alert(Window.lang.text.error);
                            kont.removeChild(dodajDiv);
                        }
                    })
                }

            }
        }
        //------------------------------------------------

        let divDStanica = document.createElement("div");
        divDStanica.classList.add("divDStanica");
        divDStanica.classList.add("linDiv");

        divDStanica.onclick = (ev) =>{
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

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.station_reviews + this.krajnjaStanica;
            formDiv.appendChild(naslov);

            let inputFld = document.createElement("textarea");
            inputFld.className = "inputFld";
            formDiv.appendChild(inputFld);

            let submitBtn = document.createElement("button");
            submitBtn.innerHTML = Window.lang.text.Submit;
            submitBtn.classList.add("dugme");
            formDiv.appendChild(submitBtn);

            submitBtn.onclick = (ev) =>{
                if(this.korisnikUser != "" && this.krajnjaId != "" && inputFld.value != ""){
                fetch(`http://localhost:5001/Recenzije/NovaRecenzijaStanice/${this.korisnikUser}/${this.krajnjaId}/${inputFld.value}`,{method: "POST"}).then(p=>{
                    if(p.status == 200){
                        alert(Window.lang.text.RencezijaPoslata);
                    }else{
                        alert(Window.lang.text.DosloDoGreske);
                    }
                    kont.removeChild(dodajDiv);
                });
            }else{
                alert(Window.lang.text.NijeUnetText);
            }
            }


            let ponistiBtn = document.createElement("button");
            ponistiBtn.classList.add("dugme");
            ponistiBtn.innerHTML = Window.lang.text.back;
            formDiv.appendChild(ponistiBtn);

            ponistiBtn.onclick = (ev) =>{
                
                kont.removeChild(dodajDiv);
                
            }

            let recenzijeBtn = document.createElement("button");
            recenzijeBtn.innerHTML = Window.lang.text.sve_recenzijeS;
            recenzijeBtn.classList.add("dugme");
            formDiv.appendChild(recenzijeBtn);
            
            recenzijeBtn.onclick = ev =>{
                while(formDiv.firstChild != null)
                    formDiv.removeChild(formDiv.firstChild);
                if(this.krajnjaStanica != "")
                fetch("http://localhost:5001/Recenzije/VratiRecenzijeStanice/"+this.krajnjaStanica).then(p=>{
                    if(p.status == 200){
                        p.json().then(p=>{
                            if(p != ""){
                            let ns = document.createElement("label");
                            ns.innerHTML = Window.lang.text.reviews;
                            let nsDiv = document.createElement("div");
                            nsDiv.appendChild(ns);
                            formDiv.appendChild(nsDiv);
                            p.forEach(el => {
                                
                                let divv = document.createElement("div");
                                divv.className = "divv";
                                formDiv.appendChild(divv);

                                let lab = document.createElement("label");
                                lab.innerHTML = el.autor.username + ": " + el.sadrzaj;
                                divv.appendChild(lab);

                            });

                            let nz = document.createElement("button");
                            nz.className = "dugme";
                            nz.innerHTML = Window.lang.text.back;
                            formDiv.appendChild(nz);
                            nz.onclick = (ev) =>{
                
                                kont.removeChild(dodajDiv);
                                }
                            }else{
                                let nema = document.createElement("h3");
                                nema.innerHTML = Window.lang.text.nema_recenzijaS;
                                formDiv.appendChild(nema);
                                let btnN = document.createElement("button");
                                btnN.className = "dugme";
                                btnN.innerHTML = Window.lang.text.back;
                                btnN.onclick = (ev) =>{
                
                                    kont.removeChild(dodajDiv);
                                    }
                                
                                formDiv.appendChild(btnN);
                            }

                        })
                    }else{
                        alert(Window.lang.text.error);
                        kont.removeChild(dodajDiv);
                    }
                })
            }


            

        }

            
    }

        let labelaDStanica = document.createElement("label");
        labelaDStanica.className = "labelaDStanica";
        labelaDStanica.innerHTML =  Window.lang.text.DolaznaStanica;
        divDStanica.appendChild(labelaDStanica);

        let labelaImeStaniceD = document.createElement("label");
        labelaImeStaniceD.innerHTML = this.krajnjaStanica;
        divDStanica.appendChild(labelaImeStaniceD);

        let divDDolaska = document.createElement("div");
        divDDolaska.classList.add("divDDolaska");
        divDDolaska.classList.add("linDiv");
        linijaDiv.appendChild(divDDolaska);

        linijaDiv.appendChild(divDStanica);

        let labelaDDolaska = document.createElement("label");
        labelaDDolaska.className = "labelaDDolaska";
        labelaDDolaska.innerHTML = Window.lang.text.VremeDolaska;
        divDDolaska.appendChild(labelaDDolaska);

        let labelaDDolaskaVreme = document.createElement("label");
        labelaDDolaskaVreme.innerHTML = this.datumDolaska;
        divDDolaska.appendChild(labelaDDolaskaVreme);

        let btnKupiKartu = document.createElement("button");
        btnKupiKartu.innerHTML = Window.lang.text.KupiKartu;
        btnKupiKartu.className = "dugme";
        linijaDiv.appendChild(btnKupiKartu);
        btnKupiKartu.onclick = (ev)=>{
            fetch(`http://localhost:5001/Karta/KupiKartu/${this.korisnikUser}/${this.id}`,{method: "PUT"}).then(p=>{
                if(p.status == 200){
                    alert( Window.lang.text.KartaKupljena);
                }else{
                    //console.log(p.text);
                    alert(Window.lang.text.KartaJeVecKupljena);
                }
            });
        }


        let greskBtn = document.createElement("button");
        greskBtn.innerHTML = Window.lang.text.PrijaviGresku;
        greskBtn.className = "dugme";
        linijaDiv.appendChild(greskBtn);

        greskBtn.onclick = (ev) =>{
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

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.PrijaviGresku;
            formDiv.appendChild(naslov);

            let inputFld = document.createElement("textarea");
            inputFld.className = "inputFld";
            formDiv.appendChild(inputFld);

            let submitBtn = document.createElement("button");
            submitBtn.innerHTML = Window.lang.text.Submit;
            submitBtn.classList.add("dugme");
            formDiv.appendChild(submitBtn);

            submitBtn.onclick = (ev) =>{
                if(inputFld.value != ""){
                fetch(`http://localhost:5001/Greska/NapisiGresku/${this.id}/${inputFld.value}`,{method: "POST"}).then(p=>{
                    if(p.status == 200){
                        alert(Window.lang.text.GreskaPoslata);
                    }else{
                        alert(Window.lang.text.GreskaNijePoslata);
                    }
                    kont.removeChild(dodajDiv);
                });
            }else{
                alert(Window.lang.text.NijeUnetText);
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
        }

        
    }
}