function CheckBox(url, checked) {
    let value = checked ? 1 : 0;
    $.ajax({
        type: "PUT",
        url: `${url}${value}`,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(value),
        success: function (response)
        {
            console.log(url);
            console.log(response);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            console.log(exception);
        }
    });
}
