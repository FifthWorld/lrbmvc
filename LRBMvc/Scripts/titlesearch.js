function SearchByOrganisation() {
    window.location = "/earchive/titlesearch/?industry=true&name="
        + $('#org')[0].value;
}

function SearchByName() {
    window.location = "/earchive/titlesearch/?owner=true&surname="
    + $('#sName')[0].value
    + "&firstname=" + $('#fName')[0].value
    + "&middlename=" + $('#mName')[0].value;

}

function SearchByLocation() {
    window.location = "/earchive/titlesearch/?location=true"
        + "&town=" + $('#town')[0].value
        + "&street=" + $('#street')[0].value
        + "&lga=" + $('#lga')[0].value;
}

function SearchByPRKNumber() {
    if ($('prkNo').text.length > 0) {
        window.location = "/earchive/titlesearch/?prkNo=" + $('#prkNo')[0].value;
    }
}