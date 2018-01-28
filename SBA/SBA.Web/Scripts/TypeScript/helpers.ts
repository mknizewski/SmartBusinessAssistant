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
}