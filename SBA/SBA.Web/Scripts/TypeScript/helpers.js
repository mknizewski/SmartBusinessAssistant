class Helpers {
    static SetAlert(alertClass, alertMessage) {
        let alertDiv = "<div class='" + alertClass + "'>" +
            alertMessage + "</div>";
        $("#tempMessage").append(alertDiv);
    }
    static SetLoadingScreen(enable) {
        if (enable)
            $("#loadingSpinner").show();
        else
            $("#loadingSpinner").hide();
    }
    static ScrollTo(id) {
        $(id).scroll(id);
    }
    static SetVisible(id, isVisible) {
        if (isVisible)
            $(id).show();
        else
            $(id).hide();
    }
    static Clear(id) {
        $(id).empty();
    }
}
//# sourceMappingURL=helpers.js.map