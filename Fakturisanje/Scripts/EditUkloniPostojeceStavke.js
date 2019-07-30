
let postojeceStavkeDiv = document.querySelectorAll(".divPostojeceStavke");

let nizStavkiZaBrisanje = [];

for (var i = 0; i < postojeceStavkeDiv.length; i++) {

    let removeDiv = document.querySelector(".divPostojeceStavke_" + (i + 1));
    let hiddenId = document.querySelector(".idPostojeceStavke_" + (i + 1));
    let cena = document.querySelector(".cenaPostojeceStavke_" + (i + 1)); 
    let kolicina = document.querySelector(".kolicinaPostojeceStavke_" + (i + 1));
    
    document.querySelector(".ukloniPostojeceStavke_" + (i + 1)).addEventListener("click", function () {
        nizStavkiZaBrisanje.push(hiddenId.value);
        removeDiv.remove();

        let ukupnoRazlika = Number(ukupno.value) - cena.value * kolicina.value;
        ukupno.value = RoundNum(ukupnoRazlika, 2);
        prebrojStavkeIsetujUkupno();
    })
}






























