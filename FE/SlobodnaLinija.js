import { lang } from "./lang.js";

export class SlobodnaLinija
{
    constructor(id,DP, PS, KS, DD){
        this.id = id;
        this.kontejner = null;
        this.datumPolaska = DP;
        this.datumDolaska = DD;
        this.pocetnaStanica = PS;
        this.krajnjaStanica = KS;
        this.ime = document.cookie.split("next")[0];
        Window.lang = new lang(document.cookie.split("next")[2]);

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
        divPS.classList.add("linDiv");
        //divPS.classList.add("divPS");
        linijaDiv.appendChild(divPS);

        let labelaPS = document.createElement("label");
        labelaPS.className = "labelaPS";
        labelaPS.innerHTML = Window.lang.text.first_station;
        divPS.appendChild(labelaPS);

        let labelaImeStanice = document.createElement("label");
        labelaImeStanice.innerHTML = this.pocetnaStanica;
        divPS.appendChild(labelaImeStanice);

        let divDPolaska = document.createElement("div");
        divDPolaska.classList.add("linDiv");
        divDPolaska.classList.add("divDPolaska");
        linijaDiv.appendChild(divDPolaska);

        let labelaDPolaska = document.createElement("label");
        labelaDPolaska.className = "labelaDPolaska";
        labelaDPolaska.innerHTML = Window.lang.text.departure_time;
        divDPolaska.appendChild(labelaDPolaska);

        let labelaDPolaskaVreme = document.createElement("label");
        labelaDPolaskaVreme.innerHTML = this.datumPolaska;
        divDPolaska.appendChild(labelaDPolaskaVreme);

        //---------------------------------------------------------------------

        let divDStanica = document.createElement("div");
        divDStanica.classList.add("linDiv");
        //divDStanica.classList.add("divDStanica");

        let labelaDStanica = document.createElement("label");
        labelaDStanica.className = "labelaDStanica";
        labelaDStanica.innerHTML = Window.lang.text.last_station;
        divDStanica.appendChild(labelaDStanica);

        let labelaImeStaniceD = document.createElement("label");
        labelaImeStaniceD.innerHTML = this.krajnjaStanica;
        divDStanica.appendChild(labelaImeStaniceD);

        let divDDolaska = document.createElement("div");
        divDDolaska.classList.add("linDiv");
        divDDolaska.classList.add("divDDolaska");
        linijaDiv.appendChild(divDDolaska);
        linijaDiv.appendChild(divDStanica);

        let labelaDDolaska = document.createElement("label");
        labelaDDolaska.className = "labelaDDolaska";
        labelaDDolaska.innerHTML = Window.lang.text.arrival_time;
        divDDolaska.appendChild(labelaDDolaska);

        let labelaDDolaskaVreme = document.createElement("label");
        labelaDDolaskaVreme.innerHTML = this.datumDolaska;
        divDDolaska.appendChild(labelaDDolaskaVreme);

        let b = 0;

        let btnPrijajviSe = document.createElement("button");
        btnPrijajviSe.innerHTML = Window.lang.text.sign_up_ft;
        btnPrijajviSe.className = "dugme";
        linijaDiv.appendChild(btnPrijajviSe);
        btnPrijajviSe.onclick = (ev)=>{
            fetch(`http://localhost:5001/Voznja/VratiSveZahteve`, {
                method: "GET"}).then(s=>{
                    if(s.ok)
                    {
                        
                        s.json().then(o=>{
                            
                            o.forEach(el=>{
                                if(this.id == el.listaVoznji.id)
                                {
                                    b = b + 1;
                                }
                            })
                            if(b==0)
                            {
                                fetch(`http://localhost:5001/Voznja/PosaljiZahtev/${this.ime}/${this.id}`, {
                                    method: "POST"}).then(p=>{
                                        if(p.ok)
                                        {
                                            alert(Window.lang.text.request_sent);
                                        }
                                })
                            }
                            else
                            {
                                alert(Window.lang.text.transporter_exists);
                            }
                        })
                    } 
                    btnPrijajviSe.disabled = true;
                })


        };
    }
}