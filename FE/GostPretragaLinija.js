export class GostPretragaLinija
{
    constructor(id,DP, PS, KS, DD,prevoznikUser){
        this.id = id;
        this.kontejner = null;
        this.datumPolaska = DP;
        this.datumDolaska = DD;
        this.pocetnaStanica = PS;
        this.krajnjaStanica = KS;
        this.prevoznikUser = prevoznikUser;

    }

  /*  crtaj(host)
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
        divPS.className = "divPS";
        linijaDiv.appendChild(divPS);

        let labelaPS = document.createElement("label");
        labelaPS.className = "labelaPS";
        labelaPS.innerHTML = "Polazna stanica: ";
        divPS.appendChild(labelaPS);

        let labelaImeStanice = document.createElement("label");
        labelaImeStanice.innerHTML = this.pocetnaStanica;
        divPS.appendChild(labelaImeStanice);

        let divDPolaska = document.createElement("div");
        divDPolaska.className = "divDPolaska";
        linijaDiv.appendChild(divDPolaska);

        let labelaDPolaska = document.createElement("label");
        labelaDPolaska.className = "labelaDPolaska";
        labelaDPolaska.innerHTML = "Vreme polaska: ";
        divDPolaska.appendChild(labelaDPolaska);

        let labelaDPolaskaVreme = document.createElement("label");
        labelaDPolaskaVreme.innerHTML = this.datumPolaska;
        divDPolaska.appendChild(labelaDPolaskaVreme);

        //---------------------------------------------------------------------- xDDD

        let divDStanica = document.createElement("div");
        divDStanica.className = "divDStanica";
        linijaDiv.appendChild(divDStanica);

        let labelaDStanica = document.createElement("label");
        labelaDStanica.className = "labelaDStanica";
        labelaDStanica.innerHTML = "Dolazna stanica: ";
        divDStanica.appendChild(labelaDStanica);

        let labelaImeStaniceD = document.createElement("label");
        labelaImeStaniceD.innerHTML = this.krajnjaStanica;
        divDStanica.appendChild(labelaImeStaniceD);

        let divDDolaska = document.createElement("div");
        divDDolaska.className = "divDDolaska";
        linijaDiv.appendChild(divDDolaska);

        let labelaDDolaska = document.createElement("label");
        labelaDDolaska.className = "labelaDDolaska";
        labelaDDolaska.innerHTML = "Vreme dolaska: ";
        divDDolaska.appendChild(labelaDDolaska);

        let labelaDDolaskaVreme = document.createElement("label");
        labelaDDolaskaVreme.innerHTML = this.datumDolaska;
        divDDolaska.appendChild(labelaDDolaskaVreme);

        let btnPrijajviSe = document.createElement("button");
        btnPrijajviSe.className = "btnPrijajviSe";
        linijaDiv.appendChild(btnPrijajviSe);
        btnPrijajviSe.onclick = (ev)=>{

        }
    }*/
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
            divPS.classList.add("divPS2");
            linijaDiv.appendChild(divPS);
    
            let labelaPS = document.createElement("label");
            labelaPS.className = "labelaPS";
            labelaPS.innerHTML = Window.lang.text.PolaznaStanica;
            divPS.appendChild(labelaPS);
    
            let labelaImeStanice = document.createElement("label");
            labelaImeStanice.className="labelaDPolaska2";
            labelaImeStanice.innerHTML = this.pocetnaStanica;
            divPS.appendChild(labelaImeStanice);
    
            let divDPolaska = document.createElement("div");
            divDPolaska.classList.add("linDiv");
            divDPolaska.classList.add("divDPolaska");
            linijaDiv.appendChild(divDPolaska);
    
            let labelaDPolaska = document.createElement("label");
            labelaDPolaska.className = "labelaDPolaska2";
            labelaDPolaska.innerHTML =  Window.lang.text.VremePolaska;
            divDPolaska.appendChild(labelaDPolaska);

            let prevoznikDiv = document.createElement("div");
            prevoznikDiv.classList.add("prevDiv2");
            prevoznikDiv.classList.add("linDiv");
            linijaDiv.appendChild(prevoznikDiv);

            let prevoznikLab = document.createElement("label");
            prevoznikLab.innerHTML = Window.lang.text.Prevoznik;
            prevoznikLab.className="prevoznikLab2";
            prevoznikDiv.appendChild(prevoznikLab);

            let prevoznikNam = document.createElement("label");
            prevoznikNam.innerHTML = this.prevoznikUser;
            prevoznikDiv.appendChild(prevoznikNam);
    
            let labelaDPolaskaVreme = document.createElement("label");
            labelaDPolaskaVreme.innerHTML = this.datumPolaska;
            divDPolaska.appendChild(labelaDPolaskaVreme);
    
            //---------------------------------------------------------------------
    
            let divDStanica = document.createElement("div");
            divDStanica.classList.add("linDiv");
            divDStanica.classList.add("divDStanica2");
    
            let labelaDStanica = document.createElement("label");
            labelaDStanica.className = "labelaDStanica2";
            labelaDStanica.innerHTML = Window.lang.text.DolaznaStanica;
            divDStanica.appendChild(labelaDStanica);
    
            let labelaImeStaniceD = document.createElement("label");
            labelaImeStaniceD.innerHTML = this.krajnjaStanica;
            divDStanica.appendChild(labelaImeStaniceD);
    
            let divDDolaska = document.createElement("div");
            divDDolaska.classList.add("linDiv");
            divDDolaska.classList.add("divDDolaska2");
            linijaDiv.appendChild(divDDolaska);
            linijaDiv.appendChild(divDStanica);
    
            let labelaDDolaska = document.createElement("label");
            labelaDDolaska.className = "labelaDDolaska2";
            labelaDDolaska.innerHTML = Window.lang.text.VremeDolaska;
            divDDolaska.appendChild(labelaDDolaska);
    
            let labelaDDolaskaVreme = document.createElement("label");
            labelaDDolaskaVreme.innerHTML = this.datumDolaska;
            divDDolaska.appendChild(labelaDDolaskaVreme);
    
        
    }

}