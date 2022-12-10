function Delete(url, msg) {
    console.log(msg);
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
                        toastr.success(data.message);
                        window.location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}