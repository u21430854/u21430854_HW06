//---------------------------------CREATE PRODUCT---------------------------------------
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
            $('#createName').val("");
            $('#createYear').val(0);
            $('#createPrice').val(0);
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

//---------------------------------READ PRODUCT---------------------------------------
//show product details when reading product
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
            $('#readTitle').html(result.prodName);
            $('#readName').html(result.prodName);
            $('#readYear').html(result.prodYear);
            $('#readPrice').html(result.prodPrice);
            $('#readBrand').html(result.brandName);
            $('#readCategory').html(result.catName);

            for (let i = 0; i < 3; i++) {
                let j = i + 1;
                //reset font colour back to black
                $('#' + j + '-quantity').css('color', 'black');

                if (result.prodStock[i].quantity == 0) {
                    $('#' + j + '-quantity').html('Out of stock');
                    $('#' + j + '-quantity').css('color', 'red');
                }
                else {
                    $('#' + j + '-quantity').html(result.prodStock[i].quantity);
                }
            }

            //show modal, table and correct button
            $('#read-stores').css('display', 'block');
            $('#confirm-delete').css('display', 'none');
            $('#delete-btn').css('display', 'none');
            $('#read-product').modal('show');
        },
        failure: (err) => {
            alert(err.responseText);
        },
        error: (err) => {
            alert(err.responseText);
        }
    });

    return false;
}

//---------------------------------UPDATE PRODUCT---------------------------------------
//get and show product deets for updating product
function GetProductToUpdate(prodId) {
    $.ajax({
        url: 'Home/GetProductToUpdate/' + prodId,
        type: 'GET',
        data: {
            prodId: prodId
        },
        contentType: 'application/json;charset=UTF-8',
        dataType: 'json',
        success: (result) => {
            $('#createName').val(result.product_name);
            $('#createYear').val(result.model_year);
            $('#createPrice').val(result.list_price);
            $('#createBrand').val(result.brand_id);
            $('#createCategory').val(result.category_id);
            $('#updateId').val(result.product_id);

            //show modal and correct button
            $('#add-btn').css('display', 'none');
            $('#update-btn').css('display', 'inline');
            $('#create-product').modal('show');
        },
        failure: (err) => {
            alert(err.responseText);
        },
        error: (err) => {
            alert(err.responseText);
        }
    });
}

function UpdateProduct() {
    var prod = {
        product_id: $('#updateId').val(),
        product_name: $('#createName').val(),
        brand_id: $('#createBrand').val(),
        category_id: $('#createCategory').val(),
        model_year: $('#createYear').val(),
        list_price: $('#createPrice').val()
    };

    $.ajax({
        url: "/Home/UpdateProduct",
        data: JSON.stringify(prod),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: (result) => {
            //clear form
            $('#createName').val("");
            $('#createYear').val(0);
            $('#createPrice').val(0);

            //hide modal and revert to show add button
            $('#add-btn').css('display', 'inline');
            $('#update-btn').css('display', 'none');
            $('#create-product').modal('hide');

            //notify user of success
            alert('Poduct has been updated!')
            //refresh page so updated data is displayed
            location.reload(true);
        },
        error: (err) => {
            alert(err.responseText);
        }
    });
}

//---------------------------------DELETE PRODUCT---------------------------------------
//get and show product deets for updating product
function GetProductToDelete(prodId) {
    $.ajax({
        url: 'Home/GetProductToDelete/' + prodId,
        type: 'GET',
        data: {
            prodId: prodId
        },
        contentType: 'application/json;charset=UTF-8',
        dataType: 'json',
        success: (result) => {
            $('#readTitle').html(result.prodName);
            $('#deleteId').val(result.prodId);
            $('#readName').html(result.prodName);
            $('#readYear').html(result.prodYear);
            $('#readPrice').html(result.prodPrice);
            $('#readBrand').html(result.brandName);
            $('#readCategory').html(result.catName);

            //show modal, confirm message and correct button
            $('#read-stores').css('display', 'none');
            $('#confirm-delete').css('display', 'block');
            $('#delete-btn').css('display', 'inline');
            $('#read-product').modal('show');
        },
        failure: (err) => {
            alert(err.responseText);
        },
        error: (err) => {
            alert(err.responseText);
        }
    });
}

//send product ID for deletion
function DeleteProduct() {
    //get product id
    let prodId = $('#deleteId').val();

    $.ajax({
        url: "/Home/DeleteProduct/" + prodId,
        type: "POST",
        data: JSON.stringify({ "prodId": prodId }),
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: (result) => {
            //notify user of success
            alert('Poduct has been deleted!');

            //revert modal to read-product mode (because same modal is used for deleting and reading), but don't show it
            $('#read-stores').css('display', 'block');
            $('#confirm-delete').css('display', 'none');
            $('#delete-btn').css('display', 'none');

            //refresh page so changes are displayed
            location.reload(true);
        },
        error: (err) => {
            alert(err.responseText);
        },
        failure: (err) => {
            alert(err.responseText);
        }
    });
}