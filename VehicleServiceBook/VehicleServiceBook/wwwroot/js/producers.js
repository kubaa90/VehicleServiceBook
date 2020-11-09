﻿var dataTable;

$(document).ready(function () {
    /*$.ajax("/Admin/Producer/GetAll")
        .done(function (result) {
            console.log(result.data);
        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            alert("complete");
        });*/
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Producer/List",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "address", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Producer/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Producer/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}
function Delete(url) {
    swal({
        title: "Czy na pewno chcesz usunąć?",
        text: "Nie będzie możliwości przywrócenia danych!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}