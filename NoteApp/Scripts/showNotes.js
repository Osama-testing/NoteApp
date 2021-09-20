$('#readmore').click(function () {
    var text = $(this).prev('#notetext');
    text.toggleClass('summary');
    if (text.hasClass('summary')) {
        $(this).text('more');
    } else {
        $(this).text('less');
    }
});

