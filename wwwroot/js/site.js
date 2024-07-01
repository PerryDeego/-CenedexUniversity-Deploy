// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/******Created by D.Perry | dd.perry @hotmail.com******/

//JQuery dynamic table search.
$(document).ready(function () {
    $("#search").on("keyup", function () {
        var input, filter, table, tr, td, i;
        input = $("#search");
        filter = input.val().toUpperCase();
        table = $("#tbl-list");
        tr = table.find("tr");

        tr.each(function () {
            var data = $(this);
            $(this).find('td').each(function () {
                td = $(this);
                if (td.html().toUpperCase().indexOf(filter) > -1) {
                    data.show();
                    return false;
                } else {
                    data.hide();
                }
            })
        })
    })
});


/* --- Delete confirmation Sweetalert notifcation message. --- */
$(document).ready(function () {
    $("#btnDelete").click(function (e) { // amend this
        e.preventDefault(); // add this
        var form = this;
        Swal.fire({
            title: 'Are you sure?',
            text: "It will permanently deleted !",
            icon: 'warning',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $("#formData").submit(); // Submit the form
                Swal.fire('Deleted!', '', 'success')
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    });
});

// -- Ajax Form Add CreateReport -- //
/*  When user click add CreateReport button, modal show create-modal form event occurs. -- */
$(document).on('click', '.create-modal', function () {
    $('#modalForm').modal("show");
    $('#modal-form')[0].reset();
    $('.error').html(''); // Reset error messahges when form close.
    $('.modal-title').addClass('fa fa-file-text').text('Student Course Report');
});

/* --- Validate user input for CreateRoport form. --- */
$(document).ready(function ($) {
    $("#modal-form").validate({
        rules: {
            FirstName: "required",
            LastName: "required",
            CousreName: "required",
            FromDate: "required",
            ToDate: "required"
        },
        messages: {
            FirstName: "Please enter student First Name",
            LastName: "Please enter student Last Name",
            CousreName: "The Course Name is required",
            FromDate: "From Date is required",
            ToDate: "To Date is required",
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});


