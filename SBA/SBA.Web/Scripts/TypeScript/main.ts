$(() => {
    let scroll = new Scroll();
    let shared = new Shared();
    let article = new Article();

    $(document).ready(function () {
        $("#arrowUp .arrow").click(function () {
            scroll.scrollUp();
        });

        $(document).scroll(function () {
            let y = $(this).scrollTop();
            scroll.arrowAppear(y);
        });

        $(".article-link").each(function () {
            let mainElement = $(this);
            shared.sharedButton(mainElement);
        });

        article.loadArticle();
    });
});

//----------------------------------

class Scroll {
    scrollUp(): void {
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }

    arrowAppear(y): void {
        if (y > 250) {
            $("#arrowUp").show()
        }
        else {
            $("#arrowUp").hide();
        }
    }
}

//--------

class Shared {
    sharedButton(mainElement): void {
        let urlToShare = $(mainElement).parent().children('.article-link').attr('href');

        let sharePathMain_fb = "https://www.facebook.com/sharer/sharer.php?u=";
        let sharePathMain_twitter = "https://twitter.com/home?status=";
        let sharePathMain_google = "https://plus.google.com/share?url=";

        let sharePathFull_fb = sharePathMain_fb + urlToShare;
        let sharePathFull_twitter = sharePathMain_twitter + urlToShare;
        let sharePathFull_google = sharePathMain_google + urlToShare;

        $(mainElement).parent().find('.btnShared-container > .btn-facebook').attr("href", sharePathFull_fb);
        $(mainElement).parent().find('.btnShared-container > .btn-twitter').attr("href", sharePathFull_twitter);
        $(mainElement).parent().find('.btnShared-container > .btn-google').attr("href", sharePathMain_google);
    }
}

//--------

class Article {
    loadArticle(): void {
        $.getJSON("testArticle.json", function (data) {
            $.each(data, function (index, value) {
                $('#title_article').text(value.title);
                $('#date_article').text(value.date);
                $('#describe_article').text(value.describe);
                $('#articleTextContainer_article').html(value.articleText);

                $.get(value.articleTextUrl, function (data) {
                    $('#articleTextContainer_article').html(data);
                }, 'text');
                $('#articleTextOriginalUrl_article').attr("href", value.originalUrl);
            });
        });
    }
}