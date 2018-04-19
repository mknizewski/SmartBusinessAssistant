$(document).ready(function () {
    //json
    $.getJSON("file2.json", function (data) {
        $.each(data, function (index, value) {
            $('#title_article').text(value.title);
            $('#date_article').text(value.date);
            $('#describe_article').text(value.describe);
            $('#articleTextContainer_article').html(value.articleText);
            //from file
            $.get(value.articleTextUrl, function (data) {
                $('#articleTextContainer_article').html(data);
            }, 'text');
            $('#articleTextOriginalUrl_article').attr("href", value.originalUrl);
        });
    });
});