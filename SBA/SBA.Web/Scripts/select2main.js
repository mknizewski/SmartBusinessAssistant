$(document).ready(function () {
    $('.js-example-basic-single').select2({
        placeholder: "Sortowanie...",
        allowClear: true,
        theme: "classic",
        data: [{ id: 0, text: 'Nazwa [A-Z]' }, { id: 1, text: 'Nazwa [Z-A]' }, { id: 2, text: 'Data [Od najnowszych]' }, { id: 3, text: 'Data [Od najstarszych]' }, { id: 4, text: 'Popularne' }]
    });
});