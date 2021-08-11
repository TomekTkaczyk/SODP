$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    initLicenseModalPlaceHolder('/designers');
});

function EditDesigner(id) {
    $.ajax({
        type: "Get",
        url: `\Designers?handler=EditDesigner&id=${id}`,
        success: function (data) {
            $('#modal-placeholder').html(data);
            $('#editdesigner').modal('show');
        }
    })
}

function EditLicense(designerId, licenseId) {
    $.ajax({
        type: "Get",
        url: `\Designers?handler=EditLicense&designerId=${designerId}&id=${licenseId}`,
        success: function (data) {
            $('#modal-placeholder').html(data);
            $('#editLicense').modal('show');
        }
    })
}

function initLicenseModalPlaceHolder(returnUrl) {

    var placeholderElement = $('#modal-placeholder');

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        console.log("Klik na data-save modal");
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

function initLicenseModalPlaceHolder(returnUrl) {

    var placeholderElement = $('#modal-placeholder');

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        console.log("Klik na data-save modal");
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


