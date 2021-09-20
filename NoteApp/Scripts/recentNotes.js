$(function () {
    $('.pop').on('click', function () {
        $('.imagepreview').attr('src', $(this).find('img').attr('src'));
        $('#imagemodal').modal('show');
    });
});

$('#readmore').click(function () {
    var text = $(this).prev('#notetext');
    text.toggleClass('summary');
    if (text.hasClass('summary')) {
        $(this).text('more');
    } else {
        $(this).text('less');
    }
});

