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
}
//# sourceMappingURL=helpers.js.map