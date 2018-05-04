$(() => {
    MainLayoutEvents.BindEvents();
});

class MainLayoutEvents {
    static BindEvents() {
        MainLayoutEvents.CloseFixedLinkBtnOnClick();
        MainLayoutEvents.AdjustLinkOnReady();
    }

    private static CloseFixedLinkBtnOnClick() {
        $('#fixedCloseBtn').click(function (event) {
            FixedLinkBtn.Init().DoLogic();
        })
    }

    private static AdjustLinkOnReady() {
        AdjustLinkOnReady.Init().DoLogic();
    }
}

class FixedLinkBtn {
    static Init(): FixedLinkBtn {
        return new FixedLinkBtn();
    }

    public DoLogic(): void {
        Helpers.SetVisible('#containerLinks', false);
    }
}

class AdjustLinkOnReady {
    static Init(): AdjustLinkOnReady {
        return new AdjustLinkOnReady();
    }

    public DoLogic(): void {

    }
}