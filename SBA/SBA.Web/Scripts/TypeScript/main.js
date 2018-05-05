$(() => {
    MainLayoutEvents.BindEvents();
});
class MainLayoutEvents {
    static BindEvents() {
        MainLayoutEvents.CloseFixedLinkBtnOnClick();
        MainLayoutEvents.AdjustLinkOnReady();
    }
    static CloseFixedLinkBtnOnClick() {
        $('#fixedCloseBtn').click(function (event) {
            FixedLinkBtn.Init().DoLogic();
        });
    }
    static AdjustLinkOnReady() {
        AdjustLinkOnReady.Init().DoLogic();
    }
}
class FixedLinkBtn {
    static Init() {
        return new FixedLinkBtn();
    }
    DoLogic() {
        Helpers.SetVisible('#containerLinks', false);
    }
}
class AdjustLinkOnReady {
    static Init() {
        return new AdjustLinkOnReady();
    }
    DoLogic() {
        $.ajax({
            url: "/Home/GetFastLinks",
            method: "GET",
            success: function (data) {
                Helpers.SetVisible("#linksSpinner", false);
                $("#links").html(data);
            },
            error: function (data) {
                Helpers.SetVisible("#linksSpinner", false);
                $("#links").text("W danej chwili funkcja szybkich linków jest niedostępna. Przepraszamy.");
            }
        });
    }
}
//# sourceMappingURL=main.js.map