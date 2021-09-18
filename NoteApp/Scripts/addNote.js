$(document).ready(function () {
    console.log("ready!");
    $("img").hide();
    $("iframe").hide();
    $("audio").hide();

});
function ShowTextbox() {
    $("#text").show();
    $("#file").hide();
}
function ShowFile() {
    $("#file").show();
    $("#text").hide();

}
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var fileTypes = ['jpg', 'jpeg', 'png'];  //acceptable file types
        var filevedio = ['mp4', 'ever', 'you', 'want'];  //acceptable file types
        var fileaudio = ['mp3', 'ogg'];  //acceptable file types
        var extension = input.files[0].name.split('.').pop().toLowerCase(),  //file extension from input file
            isImages = fileTypes.indexOf(extension) > -1;  //is extension in acceptable types
        isVideos = filevedio.indexOf(extension) > -1;  //is extension in acceptable types
        isAudio = fileaudio.indexOf(extension) > -1;  //is extension in acceptable types
        console.log(extension);
        console.log(isImages);
        console.log(isVideos);
        var reader = new FileReader();
        reader.onload = function (e) {
            if (isImages) {
                $("img").show();
                $("iframe").hide();
                $('#img')
                    .attr('src', e.target.result)
                    .width(395)
                    .height(200);
                console.log("img uplaoaded");
            }
            else if (isVideos) {
                $("img").hide();
                $("iframe").show();
                $('#vedio')
                    .attr('src', e.target.result)
                    .width(395)
                    .height(200);
                console.log("vedio uplaoaded");
            }
            else if (isAudio) {
                $("img").hide();
                $("iframe").hide();
                $("audio").show();
                $('#audio')
                    .attr('src', e.target.result)
                    .width(395)
                    .height(60);
                console.log("audio uplaoaded");

            }

        };
        reader.readAsDataURL(input.files[0]);
    }
}


function test() {
    var a = $("#tagData").val();
    console.log(a);

}
$("#tagData").select2({
    placeholder: 'Select Tag',
    tags: true,
    tokenSeparators: [',', ' '],

})
function validation() {
    var a = $("#noteDesciption").val();
    var b = $("#noteFile").val();
    if (a == "" && b == "") {
        alert("Please Enter Note !!");
    }
    else {
        $("#btnSubmit").attr('type', 'submit');
    }


}