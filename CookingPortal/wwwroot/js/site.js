// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function jsAddLike(id) {
   
    $.ajax({
        url: '/Home/jsAddLike', 
        data: { id: id },
        type: "POST",
        success: function (data) {
           // alert('Added');
        }
    });
}

function sortChange() {
    let sorting = document.querySelector(".sortSelect").value;
    location.href = `/Home/Index?idSorting=${sorting}`;

   
    //$.ajax({
    //    type: "GET",
    //    url: '/Home/Index',
    //    data: { idSorting: sorting },
    //    success: function (data) {
    //        console.log(data);
    //    }
    //});
}

