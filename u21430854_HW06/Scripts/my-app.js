function CreateProduct() {
    var prod = {
        product_name: $('#createName').val(),
        brand_id: $('#createBrand').val(),
        category_id: $('#createCategory').val(),
        model_year: $('#createYear').val(),
        list_price: $('#createPrice').val()
    };

    $.ajax({
        url: "/Home/CreateProduct",
        data: JSON.stringify(prod),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: (result) => {
            $('#create-product').modal('hide');
        },
        error: (err) => {
            alert(err.responseText);
        }
    });
}