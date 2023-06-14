export class LinijaIzmena{
    
    constructor(id,polaznaStanica,krajnjaStanica,prevoznik,vremePolaska,vremeDolaska){
        this.id = id;
        this.polaznaStanica = polaznaStanica;
        this.krajnjaStanica = krajnjaStanica;
        this.prevoznik = prevoznik;

        this.datumPolaska = vremePolaska.split("T")[0];
        this.vremePolaska = vremePolaska.split("T")[1];
        this.datumDolaska = vremeDolaska.split("T")[0];
        this.vremeDolaska = vremeDolaska.split("T")[1];
        this.kontejner = null;
    }

    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");
        
        while(host.firstChild != null)
            host.removeChild(host.firstChild);
        
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("column");
        host.appendChild(this.kontejner);

        let initialDiv = document.createElement("div");
        initialDiv.classList.add("korisnik");
        this.kontejner.appendChild(initialDiv);
        //------------
        let polazakDiv = document.createElement("div");
        polazakDiv.classList.add("column");
        initialDiv.appendChild(polazakDiv);

        let ps = document.createElement("label");
        ps.innerHTML = Window.lang.text.departure_station+": " + this.polaznaStanica;
        let pv = document.createElement("label");
        pv.innerHTML = Window.lang.text.departure_time + ": " + this.vremePolaska;
        polazakDiv.appendChild(ps);
        polazakDiv.appendChild(pv);
        //-------------
        let prev = document.createElement("label");
        prev.innerHTML = Window.lang.text.transporter+": " + this.prevoznik;
        initialDiv.appendChild(prev);
        //-------------
        let dolazakDiv = document.createElement("div");
        dolazakDiv.classList.add("column");
        initialDiv.appendChild(dolazakDiv);

        let ds = document.createElement("label");
        ds.innerHTML = Window.lang.text.arrival_station+": " + this.krajnjaStanica;
        let dv = document.createElement("label");
        dv.innerHTML = Window.lang.text.arrival_time+": " + this.vremeDolaska;
        dolazakDiv.appendChild(ds);
        dolazakDiv.appendChild(dv);

        //-------------

        let ukloniBtn = document.createElement("button");
        ukloniBtn.classList.add("dugme");
        ukloniBtn.innerHTML = Window.lang.text.delete;
        initialDiv.appendChild(ukloniBtn);

        ukloniBtn.onclick = (ev) =>{
            fetch("http://localhost:5001/Voznja/ObrisiVoznju/"+this.id,{method: "DELETE"});
        }
        //----------------
        let hoverDiv = document.createElement("div");
        this.kontejner.appendChild(hoverDiv);

        this.kontejner.onmouseenter = (ev) =>{
            while(hoverDiv.firstChild != null)
                hoverDiv.removeChild(hoverDiv.firstChild);

            initialDiv.classList.add("initialDiv");
            
            let izmenaDiv = document.createElement("div");
            izmenaDiv.classList.add("izmenaDiv");
            izmenaDiv.classList.add("column");
            izmenaDiv.classList.add("center");
            hoverDiv.appendChild(izmenaDiv);

            let naslov = document.createElement("h3");
            naslov.innerHTML = Window.lang.text.change_line;
            izmenaDiv.appendChild(naslov);

            let staniceDiv = document.createElement("div");
            izmenaDiv.appendChild(staniceDiv);

            let polazakIn = document.createElement("input");
            polazakIn.classList.add("inp");
            polazakIn.placeholder = Window.lang.text.departure_station;
            polazakIn.value = this.polaznaStanica;
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
            dolazakIn.placeholder = Window.lang.text.arrival_station;
            dolazakIn.value = this.krajnjaStanica;
            staniceDiv.appendChild(dolazakIn);

            //-------
            let datumDiv = document.createElement("div");
            izmenaDiv.appendChild(datumDiv);

            let dp = document.createElement("input");
            dp.classList.add("inp");
            dp.type = "date";
            dp.value = this.datumPolaska;
            datumDiv.appendChild(dp);
            
            let dd = document.createElement("input");
            dd.classList.add("inp");
            dd.type = "date";
            dd.value = this.datumDolaska;
            datumDiv.appendChild(dd);

            //--

            let vremeDiv = document.createElement("div");
            izmenaDiv.appendChild(vremeDiv);

            let vp = document.createElement("input");
            vp.classList.add("inp");
            vp.type = "time";
            vp.placeholder = Window.lang.text.departure_time;
            vp.value = this.vremePolaska;
            vremeDiv.appendChild(vp);
            
            let vd = document.createElement("input");
            vd.classList.add("inp");
            vd.type = "time";
            vd.placeholder = Window.lang.text.arrival_time;
            vd.value = this.vremeDolaska;
            vremeDiv.appendChild(vd);

            //----------
            /*
            let prevoznikIn = document.createElement("input");
            prevoznikIn.classList.add("inp");
            prevoznikIn.placeholder = "Prevoznik";
            prevoznikIn.value = this.prevoznik;
            */
            //---------

            let izmeniBtn = document.createElement("button");
            izmeniBtn.classList.add("dugme");
            izmeniBtn.innerHTML = Window.lang.text.change;
            izmenaDiv.appendChild(izmeniBtn);

            izmeniBtn.onclick = async (ev) => {
                //fetch
                if(polazakIn.value != "" && dolazakIn.value != "" && dp.value != "" && dd.value != "" && vp.value != "" && vd.value != ""){
                await fetch(`http://localhost:5001/Voznja/IzmeniVoznju/${this.id}/${polazakIn.value}/${dolazakIn.value}/${dp.value+" "+vp.value}/${dd.value +" "+ vd.value}`,{method: "PUT"})
                .then(p=>{
                    if(p.status == 200){
                        this.polaznaStanica = polazakIn.value;
                        this.krajnjaStanica = dolazakIn.value;
                        this.datumPolaska = dp.value;
                        this.vremePolaska = vp.value;
                        this.datumDolaska = dd.value;
                        this.vremeDolaska = vd.value;
                    }else{
                        alert(Window.lang.text.PodaciNisuValidni);
                    }
                });
                this.Crtaj(host);
            
            }else{
                alert(Window.lang.text.NisuUnetiPodaci);
            }
            
        }
        this.kontejner.onmouseleave = (ev) =>{
            while(hoverDiv.firstChild != null)
                hoverDiv.removeChild(hoverDiv.firstChild);
            initialDiv.classList.remove("initialDiv");
        }

    }
}

}