let izabranaStavka = document.querySelector("#Stavke");
let cenaStavkeIspis = document.querySelector(".cenaStavke");
let kolicina = document.querySelector("#Unosi_Kolicina");
let ukupno = document.querySelector("#Faktura_Ukupno");
let dodajStavku = document.querySelector(".dodajStavkuButton");
let stavkaValidMess = document.querySelector(".stavkaValidMess");
let izabraneStavkeLista = document.querySelector(".izabraneStavkeLista");
let skupStavkizaUnos = document.querySelector("#skupStavkizaUnos");
let submitButton = document.querySelector("#submitButton");
    
let skupStavki = []; 
let skupKolicina = [];

//Inicajalna vrednost - datum polje
let danasnjiDatum = document.querySelector(".myDate");
let danas = new Date();
danasnjiDatum.value = danas.toISOString().substr(0, 10);

//Ispis cene na izbor stavke iz dropdown-a
function cenaStavke(stavka) {

    let id = stavka;
    let cena;
    let xhr;

    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    if (xhr == null) {
        console.log("Your browser does not support XMLHTTP!");
    }

    xhr.open("GET", `../FakturaViewModels/CenaStavke/${id}`);
    xhr.send(null);

    (xhr.onreadystatechange = function () {
        if (xhr.status === 200 && xhr.readyState === 4) {
            let xhrt = xhr.responseText;
            cena = JSON.parse(xhrt);

            cenaStavkeIspis.value = cena;
        }
    })();
}

izabranaStavka.addEventListener("change", function () {
    let stavka = izabranaStavka.value;
    cenaStavke(stavka)
})

function dodajStavkuUlistu(redniBr, stavka, cena, kolicina) {
    let stavkaP = document.createElement("p");
    stavkaP.innerHTML = redniBr + ". " + stavka + "\&nbsp; |\&nbsp; cena: " + cena + "\&nbsp; |\&nbsp; kol: " + kolicina;
    izabraneStavkeLista.appendChild(stavkaP);
}

//Dodavanje stavki
function textIzabraneStavke(sel) {
    return sel.options[sel.selectedIndex].text;
}

dodajStavku.addEventListener("click", function () {

    if (izabranaStavka.value != "" && kolicina.value != "") {
        if (!skupStavki.includes(izabranaStavka.value)) {

            //dodavanje izabranih stavki i kolicina u skupove
            skupStavki.push(izabranaStavka.value);
            skupKolicina.push(kolicina.value);
            stavkaValidMess.innerHTML = "";

            //dodavanje stavke u listu na frontu
            dodajStavkuUlistu(skupStavki.length, textIzabraneStavke(izabranaStavka), cenaStavkeIspis.value, kolicina.value)

            //ukupno ispis na frontu
            ukupno.value = Number(ukupno.value) + Number(cenaStavkeIspis.value) * kolicina.value;
        }
        else {
            stavkaValidMess.innerHTML = "Ova stavka je vec izabrana";
        }    
    }
    else {
        stavkaValidMess.innerHTML = "Izaberite stavku i unesite kolicinu";
    }
})

submitButton.addEventListener("click", function () {
    skupStavkizaUnos.value = skupStavki;
    skupKolicinazaUnos.value = skupKolicina;
})




