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
                success: function (data) {
                    if (data.success) {
                        window.location.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}