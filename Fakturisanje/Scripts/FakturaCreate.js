
let izabranaStavka = document.querySelector("#Stavke");
let cenaStavkeIspis = document.querySelector(".cenaStavke");
let kolicina = document.querySelector("#Unosi_Kolicina");
let ukupno = document.querySelector("#Faktura_Ukupno");
let dodajStavku = document.querySelector(".dodajStavkuButton");
let stavkaValidMess = document.querySelector(".stavkaValidMess");
let vremeValidMess = document.querySelector(".vremeValidMess");
let izabraneStavkeLista = document.querySelector(".izabraneStavkeLista");
let skupStavkizaUnos = document.querySelector("#skupStavkizaUnos");
let skupKolicinazaUnos = document.querySelector("#skupKolicinazaUnos");
let submitButton = document.querySelector("#submitButton");

//postavljanje polja na inicijalne vrednosti
kolicina.value = 1;
cenaStavkeIspis.value = 0;
ukupno.value = 0;
    
let skupStavki = []; 
let skupKolicina = [];
let skupCena = [];

//Inicijalna vrednost - datum polje
let danasnjiDatum = document.querySelector(".myDate");
let danas = new Date();
danasnjiDatum.value = danas.toISOString().substr(0, 10);

//dodavanje i uklanjanje stavke u i iz liste, takodje odgovarajuceg elementa iz nizova stavki i kolicina
function dodajStavkuUlistu(redniBr, stavka, cena, kolicina) {
    let stavkaDiv = document.createElement("div");
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
        $(this).css({
            'cursor': 'pointer',
            'color': 'darkorange'
        });
    });
    $(ukloni).mouseout(function () {
        $(this).css('color', 'orange');
    });
    
    stavkaP.innerHTML = redniBr + ". " + stavka + "\&nbsp; |\&nbsp; cena: " + cena + "\&nbsp; |\&nbsp; kol: " + kolicina + "\&nbsp; - \&nbsp;";
    
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
    })
}

//Dodavanje stavki
function textIzabraneStavke(sel) {
    return sel.options[sel.selectedIndex].text;
}

dodajStavku.addEventListener("click", function () {
    vremeValidMess.innerHTML = "";
    if (izabranaStavka.value != "" && kolicina.value != "" && kolicina.value != 0) {
        if (!skupStavki.includes(izabranaStavka.value)) {

            //dodavanje izabranih stavki i kolicina u skupove
            skupStavki.push(izabranaStavka.value);
            skupKolicina.push(kolicina.value);
            stavkaValidMess.innerHTML = "";

            //dodavanje stavke u listu na frontu
            dodajStavkuUlistu(skupStavki.length, textIzabraneStavke(izabranaStavka), cenaStavkeIspis.value, kolicina.value)

            //ukupno ispis na frontu
            function RoundNum(num, length) {
                var number = Math.round(num * Math.pow(10, length)) / Math.pow(10, length);
                return number;
            }
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

submitButton.addEventListener("click", function (e) {
    //e.preventDefault();
    vremeValidMess.innerHTML = "";
    let datum = document.querySelector("#Faktura_Datum").value;
    let izabraniDatum = new Date(datum);
    let danas = new Date();
    if (skupStavki == 0) {
        e.preventDefault();
        stavkaValidMess.innerHTML = "Bar jedna stavka mora biti dodana";
    }
    else if (izabraniDatum > danas) {
        e.preventDefault();
        vremeValidMess.innerHTML = "Vreme ne sme biti u budućnosti";
    }
    skupStavkizaUnos.value = skupStavki;
    skupKolicinazaUnos.value = skupKolicina;
})




