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
            //clear form
            $('#createName').val();
            $('#createBrand').val();
            $('#createCategory').val();
            //hide modal
            $('#create-product').modal('hide');
            //notify user of success
            alert('New product created!')
            //refresh page so new data is displayed
            location.reload(true);
        },
        error: (err) => {
            alert(err.responseText);
        }
    });
}