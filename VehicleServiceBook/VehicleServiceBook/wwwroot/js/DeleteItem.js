var currentpath;
var fullurl;
var subpathCopy;

function Delete(subpath) {
    subpathCopy = subpath
    currentpath = window.location.pathname
    fullurl = currentpath + subpathCopy
    Swal.fire({
        title: 'Czy jesteś pewien?',
        text: "Nie będzie możliwości przywrócenia danych!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Tak, usuń',
        cancelButtonText: 'Nie, wróć',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: fullurl,
            });
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            Swal.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            )
        }
    })
}