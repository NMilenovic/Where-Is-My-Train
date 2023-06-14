export class Korisnik{

    constructor(id,username,banned,time,par){
        this.id = id;
        this.username = username;
        this.banned = banned;
        this.time = time;
        this.kontejner = null;
        this.par = par;
    }

    Crtaj(host){
        if(!host)
            throw new Error("nije unet host");

        while(host.firstChild != null)
            host.removeChild(host.firstChild);
        
        

        let korLab = document.createElement("label");
        korLab.innerHTML = Window.lang.text.username + ": " + this.username;
        host.appendChild(korLab);

        let statusLab = document.createElement("label");
        this.banned? statusLab.innerHTML = "Status: "+Window.lang.text.banned:statusLab.innerHTML = "Status: Ok"; 
        host.appendChild(statusLab);

        let btnBan;
        
        if(this.banned){
            host.className = "ban";
            btnBan = document.createElement("button");
            btnBan.classList.add("dugme");
            btnBan.innerHTML = Window.lang.text.unban;
            host.appendChild(btnBan);
            btnBan.onclick = async (ev) =>{
                await fetch("http://localhost:5001/Korisnik/UnbanKorisnika/"+this.username,{method: "PUT"});
                this.banned = false;
                this.Crtaj(host);
            }
        }else{
            host.className = "noBan";
            btnBan = document.createElement("button");
            btnBan.classList.add("dugme");
            btnBan.innerHTML = Window.lang.text.ban;
            host.appendChild(btnBan);
            btnBan.onclick = async (ev) =>{
                await fetch("http://localhost:5001/Korisnik/BanujKorinsika/"+this.username,{method: "PUT"});
                this.banned = true;
                this.Crtaj(host);
            }
        }

        
    }
}