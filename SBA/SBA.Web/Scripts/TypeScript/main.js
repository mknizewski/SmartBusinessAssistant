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
    }
}
//# sourceMappingURL=main.js.map