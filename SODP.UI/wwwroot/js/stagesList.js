$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            var placeholderElement = $('#modal-placeholder');
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    InitModalPlaceHolder('/stages');
});
