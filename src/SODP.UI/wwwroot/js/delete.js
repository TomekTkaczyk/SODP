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
                    window.location.reload();
                },
                done: function (data, textStatus, jqXHR) {
                    console.log(data);
                    console.log(textStatus);
                    console.log(jqXHR);
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            })
        }
    })
}