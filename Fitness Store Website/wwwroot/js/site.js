// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    var filterForm = document.querySelector('aside form');
    if (!filterForm) return;

    filterForm.addEventListener('submit', function (e) {
        e.preventDefault();
        var url = filterForm.action || window.location.href;
        var formData = new FormData(filterForm);

        fetch(url, {
            method: 'GET',
            headers: { 'X-Requested-With': 'XMLHttpRequest' },
            body: null
        }).then(function (res) {
            return res.text();
        }).then(function (html) {
            var grid = document.getElementById('product-grid');
            if (grid) grid.innerHTML = html;
        }).catch(function (err) { console.error(err); });
    });
});
