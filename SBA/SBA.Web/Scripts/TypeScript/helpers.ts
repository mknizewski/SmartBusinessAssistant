class Helpers {
    static SetAlert(alertClass, alertMessage): void {
        let alertDiv = "<div class='" + alertClass + "'>" +
                        alertMessage + "</div>";

        $("#tempMessage").append(alertDiv);
    }

    static SetLoadingScreen(enable): void {
        if (enable)
            $("#loadingSpinner").show();
        else
            $("#loadingSpinner").hide();
    }

    static ScrollTo(id): void {
        $(id).scroll(id);
    }

    static SetVisible(id, isVisible): void {
        if (isVisible)
            $(id).show();
        else
            $(id).hide();
    }

    static Clear(id): void {
        $(id).empty();
    }
}