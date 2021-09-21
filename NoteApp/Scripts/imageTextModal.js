function noteDetail(id) {
    console.log(id)
    $.ajax({
        type: 'GET',
        url: "/Note/NoteDetail/" + id,
        dataType: 'json',
        success: function (result) {
            console.log(result)
            $('#noteText').empty();
            $('#noteText').append(result);

        }
    });
}


$(function () {
    $('.pop').on('click', function () {
        $('.imagepreview').attr('src', $(this).find('img').attr('src'));
        $('#imagemodal').modal('show');
    });
});