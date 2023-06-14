export class PrevoznikZahtev{

    constructor(id,voznjaId,polaznaStanica,krajnjaStanica,prevoznik,datumVremePolazak,datumvremeDolazak,parent){
        this.id = id;
        this.voznjaId = voznjaId;
        this.polaznaStanica = polaznaStanica;
        this.krajnjaStanica = krajnjaStanica;
        this.prevoznik = prevoznik;
        this.kontejner = null;
        this.datumPolazak = datumVremePolazak.split("T")[0];
        this.datumDolazak = datumvremeDolazak.split("T")[0];
        this.vremePolazak = datumVremePolazak.split("T")[1];
        this.vremeDolazak = datumvremeDolazak.split("T")[1];
        this.parent = parent;
    }

    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");
        
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("column");
        host.appendChild(this.kontejner);

        let initialDiv = document.createElement("div");
        initialDiv.classList.add("korisnik");
        this.kontejner.appendChild(initialDiv);

        let ps = document.createElement("label");
        ps.innerHTML = Window.lang.text.departure_station+": " + this.polaznaStanica;
        initialDiv.appendChild(ps);

        let prev = document.createElement("label");
        prev.innerHTML = Window.lang.text.transporter+": " + this.prevoznik;
        initialDiv.appendChild(prev);

        let ks = document.createElement("label");
        ks.innerHTML = Window.lang.text.arrival_station+": " + this.krajnjaStanica;
        initialDiv.appendChild(ks);

        let btn = document.createElement("button");
        btn.classList.add("dugme");
        btn.innerHTML = Window.lang.text.allow;
        initialDiv.appendChild(btn);
        btn.onclick = async (ev) =>{

            let kont = document.querySelector(".kontejner");

            let cenaDiv = document.createElement("div");
            cenaDiv.classList.add("dodajDiv");
            cenaDiv.classList.add("z1");
            cenaDiv.classList.add("center");
            kont.appendChild(cenaDiv);

            let unosDiv = document.createElement("div");
            unosDiv.classList.add("unosDiv");
            unosDiv.classList.add("z1");
            unosDiv.classList.add("center");
            cenaDiv.appendChild(unosDiv);

            let cenaNaslov = document.createElement("h3");
            cenaNaslov.innerHTML = Window.lang.text.insert_card_price+":";
            unosDiv.appendChild(cenaNaslov);

            let cenaIn = document.createElement("input");
            cenaIn.classList.add("inp");
            cenaIn.placeholder = Window.lang.text.price;
            cenaIn.type = "number";
            cenaIn.step = 50;
            unosDiv.appendChild(cenaIn);

            let krajBtn = document.createElement("button");
            krajBtn.innerHTML = Window.lang.text.add;
            krajBtn.classList.add("dugme");
            unosDiv.appendChild(krajBtn);

            krajBtn.onclick = async (ev) =>{
                if(cenaIn.value.length > 0 && cenaIn.value > 0){
                await fetch(`http://localhost:5001/Voznja/DodajPrevoznika/${this.voznjaId}/${this.prevoznik}`,{method: "PUT"});
                await fetch(`http://localhost:5001/Karta/DodajKartu/${this.voznjaId}/${cenaIn.value}`,{method: "POST"});
                let c =document.querySelector(".placeholder");
                this.parent[1](c);
                kont.removeChild(cenaDiv);
                }else{
                    alert(Window.lang.text.invalid_price);
                }
            }

            let odustaniBtn = document.createElement("button");
            odustaniBtn.classList.add("dugme");
            odustaniBtn.innerHTML = Window.lang.text.back;
            unosDiv.appendChild(odustaniBtn);

            odustaniBtn.onclick = (ev) =>{
                kont.removeChild(cenaDiv);
            }

            //await fetch(`http://localhost:5001/Voznja/DodajPrevoznika/${this.voznjaId}/${this.prevoznik}`,{method: "PUT"});
            //let c =document.querySelector(".placeholder");
            //this.parent[1](c);
        }
        

    }

}