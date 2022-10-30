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

function ShowProduct(prodId) {
    $.ajax({
        url: 'Home/GetProduct/' + prodId,
        type: 'GET',
        data: {
            prodId: prodId
        },
        contentType: 'application/json;charset=UTF-8',
        dataType: 'json',
        success: (result) => {
            $('#readTitle').html(result.product_name);
            $('#readName').html(result.prodName);
            $('#readYear').html(result.prodYear);
            $('#readPrice').html(result.prodPrice);
            $('#readBrand').html(result.brandName);
            $('#readCategory').html(result.catName);
            $('#1-quantity').html(result.prodStock[0].quantity);
            $('#2-quantity').html(result.prodStock[1].quantity);
            $('#3-quantity').html(result.prodStock[2].quantity);
            $('#read-product').modal('show');
        },
        error: (err) => {
            alert(err.responseText);
        }
    });

    return false;
}