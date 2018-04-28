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
        $("html, body").animate({ scrollTop: $(id).offset().top - 90 }, 1000);
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
    static SetDisable(id, isDisabled) {
        $(id).prop('readonly', isDisabled);
    }
    static SetDataToSessionStorage(key, data) {
        sessionStorage.setItem(key, JSON.stringify(data));
    }
    static GetDataFromSessionStorage(key) {
        return JSON.parse(sessionStorage.getItem(key));
    }
}
//# sourceMappingURL=helpers.js.map