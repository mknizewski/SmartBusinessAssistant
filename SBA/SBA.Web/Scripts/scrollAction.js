$(document).ready(function () {
    //arrrow
    $("#arrowUp .arrow").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });

    $(document).scroll(function () {
        var y = $(this).scrollTop();
        if (y > 250) {
            $("#arrowUp").show()
        }
        else {
            $("#arrowUp").hide();
        }
    });
});
