function Delete(url, msg) {
    if (msg == null) {
        msg = "Usunięcie rekordu. Operacja nie może być cofnięta.";
    }
    swal({            
        title: "Czy jesteś pewien?",
        text: msg,
        icon: "error",
        buttons: ["Anuluj","Tak"],
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function () {
                    toastr.options.onHidden = function () {
                        window.location.reload();
                    };
                    toastr.success("Operacja usuniecia powiodła się.");
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    toastr.error(jqXHR.responseJSON.message);
                }
            })
        }
    })
}