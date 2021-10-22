$(function () {
    $(document).ready(function () {

        /****
         * Action Event Methonds
         ****/

        $("#itemCategory").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/webservice_data.asmx/GetCategoriesByName",
                    data: "{'categoryName':'" + document.getElementById('itemCategory').value + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {                        
                        response(data);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hf..Id]").val(i.item.val);
            },
            minLength: 1
        });


        /// Save Category
        $('#btnAddCategory').click(function () {
            var category_name = $.trim($('#category').val());
            var message = '';
            if (category_name == '') {
                message = 'Category cannot be blank.'
            }
            if (message.length == 0) {

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "/webservice_data.asmx/SaveCategory",
                    data: "{'categoryName':'" + category_name + "'}",
                    success: function (Record) {
                        /*$('#category').val();*/

                        if (Record.d == true) {

                            $('#Result').text("Your record has been created successfully!");
                        }
                        else {
                            $('#Result').text("Your record is not inserted.");
                        }
                        $('#category').val("");
                    },
                    Error: function (textMsg) {
                        console.log(JSON.stringify(error));
                        $('#Result').text("Error: " + Error);
                    }
                });
            }
            else {
                $('#Result').html('');
                $('#Result').html(message);
            }
            $('#Result').fadeIn();
        });

        /// Save Item
        $('#btnAddItem').click(function () {
            var category_name = $.trim($('#itemCategory').val());
            var item_name = $.trim($('#item').val());
            $('#Result').text('');
            var message = '';
            if (category_name == '') {
                message = 'Category cannot be blank.'
            }
            if (message.length == 0) {

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "/webservice_data.asmx/SaveItems",
                    data: "{'categoryName':'" + category_name + "','itemName':'" + item_name +"'}",
                    success: function (Record) {
                        $('#category').val(); console.log(Record.d);
                        if (Record.d == true) {
                            $('#Result').text(Record.d);
                            $('#Result').css('color', 'red');
                        }
                        else {
                            $('#Result').text("Your record has been created!");
                            $('#Result').css('color', 'green');
                        }
                        $('#itemCategory').val('');
                        $('#item').val('');
                    },
                    Error: function (textMsg) {
                        console.log(JSON.stringify(error));
                        $('#Result').text("Error: " + Error);
                    }
                });
            }
            else {
                $('#Result').html('');
                $('#Result').html(message);
            }
            $('#Result').fadeIn();
        });

    });



    /***
     * Pulling data from data file
     ***/

    $.ajax({
        method: 'GET',
        url: '/webservice_data.asmx/GetToDoItems',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            console.log(typeof (data));
            data = Object.entries(data);

            let html = '';
            for (var i = 0; i < data.length; i++) {
                html += `
                            <li class="list-group-item">
                                <span class="name">${data[i].ItemName}</span>
                                <span class="name">${data[i].CategoryName}</span>
                                <button class="btn btn-default btn-xs pull-right remove-edit">
                                    <span class="glyphicon glyphicon-edit"></span>
                                </button>
                                <button class="btn btn-default btn-xs pull-right remove-item">
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </li>
                  `;
            }

            /*html = `<div class="col-md-6  col-sm-12">${html}</div>`*/

            $('#item-list').html(html);
        },
        error: function () {

        }
    })

    /****
         * Tried to read data
     ****/
    $.getJSON("https://localhost:44352/Data/Category.json", function (data) {
        let html = '';
        for (var i = 0; i < data.length; i++) {
            html += `
                         <ul class="list-group cat-group">
                               <li class="list-group-item">
                                <span class="name">${data[i].category_name}</span>
                                <button type="button" class="btn btn-default btn-xs pull-right remove-item" onclick="viewCategory(this)" value=${data[i].category_name}>
                                    <span class="glyphicon glyphicon-th-list"></span>
                                </button>
                                <button type="button" class="btn btn-default btn-xs pull-right edit-edit" onclick="editCategory(this)" value=${data[i].category_name}>
                                    <span class="glyphicon glyphicon-edit"></span>
                                </button>
                                <button type="button" class="btn btn-default btn-xs pull-right view-item" onclick="delCategory(this)" value=${data[i].category_name}>
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </li>
                        </ul>
                  `;
        }
        $('#category_list').html(html);
    });

    
    var jqxhr = $.getJSON("https://localhost:44352/Data/Items.json", function (data) {
        console.log("success");
        let html = '';
        for (var i = 0; i < data.length; i++) {
            html += `
                            <li class="list-group-item" value=${data[i].CategoryName}>
                                <span class="name">${data[i].ItemName}</span>
                                <span class="name item-cat" style="display:none">${data[i].CategoryName}</span>
                                <button type="button" class="btn btn-default btn-xs pull-right remove-edit" onclick="editItem(this)" value=${data[i].ItemName}>
                                    <span class="glyphicon glyphicon-edit"></span>
                                </button>
                                <button type="button" class="btn btn-default btn-xs pull-right remove-item" onclick="delItem(this)" value=${data[i].ItemName}>
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </li>
                  `;
        }
        $('#item-list').html(html);
    })
        .done(function () {
            console.log("second success");
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });


    // Set another completion function for the request above
    jqxhr.always(function () {
        console.log("second complete");
    });
});



/**
* Action Methods, VIEW,EDIT,DELETE
* @param {any} obj
*/
function delCategory(obj) {
    $.ajax({
        url: "/webservice_data.asmx/DeleteCategory",
        data: "{'categoryName':'" + obj.value + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            response(data);
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}


function editCategory(obj) {
    $.ajax({
        url: "/webservice_data.asmx/GetCategoryByName",
        data: "{'categoryName':'" + obj.value + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#category').val(data.d);
            response(data);
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function viewCategory(obj) {
    $(obj.siblings).css({ "background-color": "" });
    $(obj.parentNode).css('background-color', '#a5c573');
    //$.ajax({
    //    url: "/webservice_data.asmx/GetItemsByCategoryName",
    //    data: "{'categoryName':'" + obj.value + "'}",
    //    dataType: "json",
    //    type: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        response(data);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    },
    //    failure: function (response) {
    //        alert(response.responseText);
    //    }
    //});
}



function delItem(obj) {
    $.ajax({
        url: "/webservice_data.asmx/DeleteItem",
        data: "{'itemName':'" + obj.value + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            response(data);
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function editItem(obj) {
    var cat = obj.parent().attr('value');
    $('#itemCategory').val(cat);
    $('#item').val(obj.value);
    //$.ajax({
    //    url: "/webservice_data.asmx/GetCategoryByName",
    //    data: "{'categoryName':'" + obj.value + "'}",
    //    dataType: "json",
    //    type: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        $('#itemCategory').val(data[0].CategoryName);
    //        $('#item').val(data[0].ItemName);
    //        response(data);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    },
    //    failure: function (response) {
    //        alert(response.responseText);
    //    }
    //});
}