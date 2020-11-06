var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    let dataRole = '';
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Account/List",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "userName", "width": "12%" },
            { "data": "name", "width": "12%" },
            { "data": "surname", "width": "12%" },
            {
                "data": "role",
                "render": function (data) {
                    dataRole = data;
                    return data;
                },
                "width": "12%"
            },
            { "data": "email", "width": "12%" },
            { "data": "phoneNumber", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    var unBlockButton = `<a onclick=UnBlock("/Account/UnBlockUser/${data}")
                    class="btn btn-warning text-white" style="cursor:pointer">
                    <i class="fas fa-unlock"></i>
                    </a>`;

                    var blockButton = `<a onclick=Block("/Account/BlockUser/${data}") 
                    class="btn btn-warning text-white" style="cursor:pointer">
                    <i class="fas fa-lock"></i> 
                    </a>`;

                    return `
                            <div class="text-center">
                                <a href="/Account/Edit/${data
                        }" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                        ${dataRole === 'Zablokowany' ? unBlockButton : blockButton}
                                <a onclick=Delete("/Account/Delete/${data
                        }") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                                
                            </div>
                           `;
                    
                },
                "width": "28%"
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
function Block(url) {
    swal({
        title: "Czy na pewno chcesz zablokować?",
        text: "To spowoduje zablokowanie użytkownika aż do odblokowania",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willBlock) => {
        if (willBlock) {
            $.ajax({
                type: "BLOCK",
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
function UnBlock(url) {
    console.log(null)
    swal({
        title: "Czy na pewno chcesz odblokować?",
        text: "To spowoduje odblokowanie użytkownika",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willUnBlock) => {
        if (willUnBlock) {
            $.ajax({
                type: "UNBLOCK",
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