function puppySearch() {
    var breed = $('#breedSelect').val();
    var sex = $('#sexSelect').val();

    var table = document.getElementById("puppyTable");
    var tr = table.getElementsByTagName("tr");

    for (var i = 0; i < tr.length; i++) {
        tr[i].style.display = "none";
        if ((tr[i].className.indexOf(breed) > -1) && (tr[i].className.indexOf(sex) > -1))
        {
            tr[i].style.display = "";
        }
    }
}