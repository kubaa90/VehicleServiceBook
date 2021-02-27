var currentpath
var fullurl
var subpathCopy

function SubPagePopup(subpath, widthSize) {
    var widthLocal = 1000;
    if (typeof widthSize !== "undefined") {
        widthLocal = widthSize;
    }
    subpathCopy = subpath;
    currentpath = window.location.pathname;
    fullurl = currentpath + subpathCopy;
    console.log('fullurl: ', fullurl, ' widthSize: ', widthSize, ' widthLocal: ', widthLocal);
    //$(document).ready(function () {
    //    $('#warrantyCheckbox').change(function () {
    //        $('#warrantyTerms').fadeToggle();
    //    });
    //});
    $.ajax({
        type: "get",
        url: fullurl,
        cache: false,
        processData: false,
        dataType: "html",
        success: function (result) {
            Swal.fire({
                showClass: {
                    popup: '',
                    icon: ''
                },
                hideClass: {
                    popup: '',
                },
                width: widthLocal,
                html: result,
                showCloseButton: true,
                showConfirmButton: false,
                focusConfirm: false,
            });

            if (!$('#warrantyCheckboxEdit input[type="checkbox"]').prop('checked')) {
                $("#warrantyTermsEdit").fadeToggle(0)
            }
            $('#warrantyCheckboxEdit input[type="checkbox"]').click(function () {
                $('#warrantyTermsEdit').fadeToggle();
            });
            $(".warrantyTermsDetails").hide();
            let checkboxValue = $('#warrantyCheckboxDetails').text().trim();
            if (checkboxValue == 'TAK') {
                $(".warrantyTermsDetails").show();
            }
        },
    });
}