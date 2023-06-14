export class Recenzija{

    constructor(id,stanicaPrevoznik,sadrzaj,korisnik,pos){
        this.id = id;
        this.stanicaPrevoznik = stanicaPrevoznik;
        this.sadrzaj = sadrzaj;
        this.korisnik = korisnik;
        this.pos = pos;
        this.kontejner = null;
    }

    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");
        
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("korisnik");
        host.appendChild(this.kontejner);

        let korLab = document.createElement("label");
        korLab.innerHTML = "Autor: " + this.korisnik;
        this.kontejner.appendChild(korLab);

        let sadLab = document.createElement("label");
        sadLab.innerHTML = this.sadrzaj;
        this.kontejner.appendChild(sadLab);

        let spLab = document.createElement("label");
        spLab.innerHTML = this.stanicaPrevoznik;
        this.kontejner.appendChild(spLab);

        let btn = document.createElement("button");
        btn.classList.add("dugme");
        btn.innerHTML = Window.lang.text.remove;
        this.kontejner.appendChild(btn);

        btn.onclick = (ev) =>{
            //fetch
            if(this.pos == 0){
            fetch("http://localhost:5001/Recenzije/ObrisiRecenzijuStanice/"+this.id,{method: "DELETE"}).then(p=>{
                if(p.status == 200)
                    host.removeChild(this.kontejner);
            });
        }else{
            fetch("http://localhost:5001/Recenzije/ObrisiRecenzijuPrevoznika/"+this.id,{method: "DELETE"}).then(p=>{
                if(p.status == 200)
                    host.removeChild(this.kontejner);
            });
        }
        }
    }
}