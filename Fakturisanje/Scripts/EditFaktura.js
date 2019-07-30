
let izabranaStavka = document.querySelector(".Stavke");
let cenaStavkeIspis = document.querySelector(".cenaStavkeEdit");
let kolicina = document.querySelector("#editUnosPom_Kolicina");
let ukupno = document.querySelector(".Faktura_Ukupno");
let dodajStavku = document.querySelector(".dodajStavkuButtonEdit");
let stavkaValidMess = document.querySelector(".stavkaValidMessEdit");
let vremeValidMess = document.querySelector(".vremeValidMessEdit");
let izabraneStavkeLista = document.querySelector(".izabraneStavkeListaEdit");
let skupStavkizaUnos = document.querySelector(".skupStavkizaUnos");
let skupKolicinazaUnos = document.querySelector(".skupKolicinazaUnos");
let stavkeZaBrisanje = document.querySelector(".stavkeZaBrisanje");
let submitButton = document.querySelector("#submitButtonEdit");

//format datuma za ispis
let editDatum = document.querySelector(".myDateEdit");
let datum = new Date(editDatum.value);
datum = ((datum.getMonth() + 1) + '/' + (datum.getDate()) + '/' + (datum.getFullYear()));
editDatum.value = datum;
//

function RoundNum(num, length) {
    let number = Math.round(num * Math.pow(10, length)) / Math.pow(10, length);
    return number;
}

//Vraca trenutni niz starih stavki 
function postojeceStavke() {
    let postStavke = document.querySelectorAll(".idPostojeceStavke");
    let idPostStavki = [];

    postStavke.forEach(function (it) {
        idPostStavki.push(it.value);
    })

    return idPostStavki;
};
//

function prebrojStavkeIsetujUkupno() {

    let stavke = document.querySelectorAll(".redniBroj");

    for (var i = 0; i < stavke.length; i++) {
        stavke[i].innerHTML = i + 1 + ". ";
    }

    if (stavke.length == 0) {
        ukupno.value = 0.00;
    }
};

//postavljanje polja na inicijalne vrednosti
kolicina.value = 1;
cenaStavkeIspis.value = 0;

let skupStavki = [];
let skupKolicina = [];
let skupCena = [];
//

//dodavanje i uklanjanje stavke u i iz liste, takodje odgovarajuceg elementa iz nizova stavki i kolicina
function dodajStavkuUlistuEdit(redniBr, stavka, cena, kolicina) {
    let stavkaDiv = document.createElement("div");
    stavkaDiv.setAttribute("class", "divPostojeceStavke");
    let redniBrojSpan = document.createElement("span");
    redniBrojSpan.setAttribute("class", "redniBroj");
    let stavkaP = document.createElement("span");
    let ukloni = document.createElement("span");
    let hiddenIndex = document.createElement("input");

    //hidden koji cuva index poslednjeg elementa niza stavki, za pomoc pri uklanjanju elemenata iz
    //niza pri uklanjaju dodate stavke
    hiddenIndex.setAttribute("type", "hidden");
    hiddenIndex.value = skupStavki[skupStavki.length - 1];

    ukloni.innerHTML = "ukloni";
    ukloni.style.color = "orange";
    $(ukloni).mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    $(ukloni).mouseout(function () {
        $(this).css('color', 'orange');
    });

    stavkaP.innerHTML = stavka + " \&nbsp; |\&nbsp; cena: " + cena + " \&nbsp; |\&nbsp; kol: " + kolicina + " \&nbsp; - \&nbsp;";
    
    stavkaDiv.appendChild(redniBrojSpan);
    stavkaDiv.appendChild(stavkaP);
    stavkaDiv.appendChild(ukloni);
    stavkaDiv.appendChild(hiddenIndex);
    izabraneStavkeLista.appendChild(stavkaDiv);

    //ukloni stavku i odgovarajuce elemente skupova
    ukloni.addEventListener("click", function (e) {

        var removeId = skupStavki.indexOf(hiddenIndex.value);
        skupStavki.splice(removeId, 1);
        skupKolicina.splice(removeId, 1);

        ukupno.value = Number(ukupno.value) - skupCena[removeId];
        skupCena.splice(removeId, 1);
        stavkaDiv.remove();

        //REDNI BROJ
        let redniBroj = document.querySelectorAll(".redniBroj");
        prebrojStavkeIsetujUkupno();
    })
}
//

//Dodavanje stavki funkcija
function textIzabraneStavkeEdit(sel) {
    return sel.options[sel.selectedIndex].text;
}
//

//Dodavanje stavki
dodajStavku.addEventListener("click", function () {
    vremeValidMess.innerHTML = "";

    if (izabranaStavka.value != "" && kolicina.value != "" && kolicina.value != 0) {
        if (!skupStavki.includes(izabranaStavka.value) && !postojeceStavke().includes(izabranaStavka.value)) {

            //dodavanje izabranih stavki i kolicina u skupove
            skupStavki.push(izabranaStavka.value);
            skupKolicina.push(kolicina.value);
            stavkaValidMess.innerHTML = "";

            //dodavanje stavke u listu na frontu
            dodajStavkuUlistuEdit(skupStavki.length, textIzabraneStavkeEdit(izabranaStavka), cenaStavkeIspis.value, kolicina.value)
            let redniBroj = document.querySelectorAll(".redniBroj");
            prebrojStavkeIsetujUkupno()

            //ukupno ispis na frontu
            var ukupnoRezultat = Number(ukupno.value) + Number(cenaStavkeIspis.value) * kolicina.value;
            ukupno.value = RoundNum(ukupnoRezultat, 2);
            skupCena.push(Number(cenaStavkeIspis.value) * kolicina.value);

            //refresh polja na dodavanje stavke
            kolicina.value = 1;
            izabranaStavka.value = "";
            izabranaStavka.options[izabranaStavka.selectedIndex].text = "Izaberi stavku";
            cenaStavkeIspis.value = 0.00;
        }
        else {
            stavkaValidMess.innerHTML = "Ova stavka je vec izabrana";
        }
    }
    else {
        stavkaValidMess.innerHTML = "Izaberite stavku i unesite kolicinu, bar jedna stavka mora biti dodana";
    }
})
//

//Submit sa validacijama
submitButton.addEventListener("click", function (e) {
    //e.preventDefault();

    vremeValidMess.innerHTML = "";
    let datum = document.querySelector(".Faktura_Datum").value;
    let izabraniDatum = new Date(datum);
    let danas = new Date();
    let divPostojeceStavke = document.querySelectorAll(".divPostojeceStavke");

    //provera vremena
    if (izabraniDatum > danas) {
        e.preventDefault();
        vremeValidMess.innerHTML = "Vreme ne sme biti u budućnosti";
    }
    //provera da li postoje stavke 
    else if (divPostojeceStavke.length == 0) {
        e.preventDefault();
        stavkaValidMess.innerHTML = "Bar jedna stavka mora biti dodana";
    }
    
    stavkeZaBrisanje.value = nizStavkiZaBrisanje;
    skupStavkizaUnos.value = skupStavki;
    skupKolicinazaUnos.value = skupKolicina;
})
//



