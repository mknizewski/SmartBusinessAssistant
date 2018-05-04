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
        $("html, body").animate({ scrollTop: $(id).offset().top - 90 }, 1000);
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

    static SetDisable(id, isDisabled): void {
        $(id).prop('readonly', isDisabled);
    }

    static SetDataToSessionStorage(key, data): void {
        sessionStorage.setItem(key, JSON.stringify(data));
    }

    static GetDataFromSessionStorage(key): object {
        return JSON.parse(sessionStorage.getItem(key));
    }
}