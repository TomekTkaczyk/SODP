document.addEventListener("DOMContentLoaded", function () {
    let toastrMessage = localStorage.getItem("toastrMessage");
    if (toastrMessage) {
        switch (toastrMessage.type) {
            case "error": {
                toastr.error(toastrMessage.message);
                break;
            }
            case "info": {
                toastr.info(toastrMessage.message);
                break;
            }
            case "warning": {
                toastr.warning(toastrMessage.message);
                break;
            }
            default: {
                toastr.success(toastrMessage.message);
                break;
            }
        }
        localStorage.removeItem("toastrMessage");
    }
});

function setMessageAndReload (data) {
    localStorage.setItem("toastrMessage",
        {
            type: data.type,
            message: data.message,
        });
    window.location.reload();
}
