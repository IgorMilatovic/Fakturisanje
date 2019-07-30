
//Ispis cene na izbor stavke iz dropdown-a
function cenaStavkeZaIspis(stavka) {

    let id = stavka;
    let cena;
    let xhr;

    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    if (xhr == null) {
        console.log("Your browser does not support XMLHTTP!");
    }

    xhr.open("GET", `/FakturaViewModels/CenaStavke/${id}`);
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
    cenaStavkeZaIspis(stavka)
})