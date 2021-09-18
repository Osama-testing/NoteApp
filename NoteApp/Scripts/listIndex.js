$(document).ready(function () {
    $('#myDIV').hide();
    console.log("ready!");
    $("#tagData").select2({
        placeholder: 'Search by Tag...',
        tags: true,
        maximumSelectionLength: 1,
        tokenSeparators: [',', ' ']
    })
});
function txtboxShow() {
    var x = document.getElementById("myDIV");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function ClearSearchBar() {
    console.log("Clear Search bar !")
    $('#tagData').val(null).trigger('change');
}
function SearchByTags() {
    var tagitem = $('#tagData').val();
    if (tagitem == "") {
        alert("Please Insert Tag First !!")
    }
    else {
        console.log(tagitem);
        window.location.href = "/List/SearchByTags?Tag=" + tagitem;
    }

}

setTimeout(function () {
    $('#validation').fadeOut('fast');
}, 3000); // <-- time in milliseconds

