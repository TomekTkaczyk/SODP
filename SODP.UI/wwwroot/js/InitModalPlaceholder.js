function initModalPlaceHolder(returnUrl) {
    var placeholderElement = $('#modal-placeholder');

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        console.log('Klik na [data-save="modal"]');
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            var isValid = newBody.find('[name="IsValidate"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                window.location = returnUrl;
            }
        });
    });
}
