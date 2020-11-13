var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Fault/List",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "description", "width": "30%" },
            { "data": "vehicle.number", "width": "15%" },
            { "data": "addDateTimeString", "width": "15%" },
            { "data": "identityUser.userName", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Fault/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-info"></i> 
                                </a>
                                <a href="/Fault/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Fault/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "25%"
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