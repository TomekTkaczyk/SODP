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

    initModalPlaceHolder('/designers');
});






//function NewDesigner(id) {
//    $.ajax({
//        type: "Get",
//        url: `\Designers?handler=NewDesigner&id=${id}`,
//        success: function (data) {
//            $('#modal-placeholder').html(data);
//            $('#editdesigner').modal('show');
//        }
//    })
//}

//function NewLicense(designerId, licenseId) {
//    $.ajax({
//        type: "Get",
//        url: `\Designers?handler=NewLicense&designerId=${designerId}&id=${licenseId}`,
//        success: function (data) {
//            $('#modal-placeholder').html(data);
//            $('#newLicense').modal('show');
//        }
//    })
//}

//function initLicenseModalPlaceHolder(returnUrl) {
//    var placeholderElement = $('#modal-placeholder');

//    placeholderElement.on('click', '[data-save="modal"]', function (event) {
//        console.log("Klik na data-save modal");
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
//                window.location = `/Licenses/Edit?Id=${document.getElementById('id').value}`;
//            }
//        });
//    });
//}

//function initLicenseModalPlaceHolder(returnUrl) {

//    var placeholderElement = $('#modal-placeholder');

//    placeholderElement.on('click', '[data-save="modal"]', function (event) {
//        console.log("Klik na data-save modal");
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
//                window.location = returnUrl;
//            }
//        });
//    });
//}


