function breedSearch() {
    var breed = $('#breedSearch').val().toUpperCase();

    var cards = document.getElementById("cards");
    var cardsList = cards.getElementsByClassName("my-card");

    for (var i = 0; i < cardsList.length; i++) {
        cardsList[i].style.display = "none";
        if (cardsList[i].className.toUpperCase().indexOf(breed) > -1) {
            cardsList[i].style.display = "";
        }
    }
}


function cardPuppySearch() {
    var breed = $('#breedSelect').val();
    var sex = $('#sexSelect').val();

    var cards = document.getElementById("cards");
    var cardsList = cards.getElementsByClassName("my-card");

    for (var i = 0; i < cardsList.length; i++) {
        cardsList[i].style.display = "none";
        if ((cardsList[i].className.indexOf(breed) > -1) && (cardsList[i].className.indexOf(sex) > -1)) {
            cardsList[i].style.display = "";
        }
    }
}

