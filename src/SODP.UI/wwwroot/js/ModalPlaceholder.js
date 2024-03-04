

//function modalPlaceHolder(returnUrl, onSucessCallBack) {
//    if (onSucessCallBack == null) {
//        onSucessCallBack = window.location.reload();
//    }
//    $('[data-toggle="tooltip"]').tooltip();
//    $('button[data-toggle="ajax-modal"]').click(function (event) {
//        event.preventDefault();
//        var url = $(this).data('url');
//        $.get(url).done(function (data) {
//            var placeholderElement = $('#modal-placeholder');
//            placeholderElement.html(data);
//            placeholderElement.find('.modal').modal('show');
//        });
//    });
//    initModalPlaceHolder(returnUrl, onSucessCallBack)
//}

//function initModalPlaceHolder(returnUrl, onSucessCallBack) {
//    var placeholderElement = $('#modal-placeholder');
//    placeholderElement.on('click', '[data-save="modal"]', function (event) {
//        event.preventDefault();
//        var form = $(this).parents('.modal').find('form');
//        var actionUrl = form.attr('action');
//        var dataToSend = form.serialize();
//        $.post(actionUrl, dataToSend).done(function (data) {
//            var newBody = $('.modal-body', data);
//            placeholderElement.find('.modal-body').replaceWith(newBody);
//            var isValid = newBody.find('[name="IsValidate"]').val() == 'True';
//            if (isValid) {
//                placeholderElement.find('.modal').modal('hide');
//                onSucessCallBack();
//            }
//        });
//    });
//}

