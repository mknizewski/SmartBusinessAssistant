$(document).ready(function () {
    $(".article-link").each(function () {
        let urlToShare = $(this).parent().children('.article-link').attr('href');

        let sharePathMain_fb = "https://www.facebook.com/sharer/sharer.php?u=";
        let sharePathMain_twitter = "https://twitter.com/home?status=";
        let sharePathMain_google = "https://plus.google.com/share?url=";

        let sharePathFull_fb = sharePathMain_fb + urlToShare;
        let sharePathFull_twitter = sharePathMain_twitter + urlToShare;
        let sharePathFull_google = sharePathMain_google + urlToShare;

        // alert($(this).parent().children('ul').children('li').last().attr('class'));
        //alert($(this).parent().find('.btnShared-container > .btn-facebook').attr('class'));
        $(this).parent().find('.btnShared-container > .btn-facebook').attr("href", sharePathFull_fb);
        $(this).parent().find('.btnShared-container > .btn-twitter').attr("href", sharePathFull_twitter);
        $(this).parent().find('.btnShared-container > .btn-google').attr("href", 'https://plus.google.com/share?url=wp.pl');
    });


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

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/pl_PL/sdk.js#xfbml=1&version=v2.12&appId=1489889927800681&autoLogAppEvents=1';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));