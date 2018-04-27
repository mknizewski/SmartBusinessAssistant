$(() => {
    ContactForm.BindEvents();
});
class ContactForm {
    static BindEvents() {
        ContactForm.ContactUsBtnOnClick();
    }
    static SetAnswerLoadingScreen(isEnable) {
        if (isEnable)
            $("#loadingAnswerSpinner").show();
        else
            $("#loadingAnswerSpinner").hide();
    }
    static SetAnswerSection(isEnable) {
        if (isEnable)
            $("#possibleAnswer").show();
        else
            $("#possibleAnswer").hide();
    }
    static ContactUsBtnOnClick() {
        $('#btnContactUs').click(function (event) {
            ContactUsBtn.Init().DoLogic(event);
        });
    }
}
class ContactUsBtn {
    static Init() {
        return new ContactUsBtn();
    }
    DoLogic(event) {
        ContactForm.SetAnswerSection(true);
        ContactForm.SetAnswerLoadingScreen(true);
        Helpers.SetVisible("#pleaseWaitInfo", true);
        Helpers.ScrollTo("#possibleAnswer");
        Helpers.Clear("#answer");
        var name = $("#name").val();
        var email = $("#email").val();
        var mobilePhone = "";
        var subject = $("#subject").val();
        var message = $("#message").val();
        var contactModel = new ContactModel(name, email, mobilePhone, subject, message);
        $.ajax({
            url: "Contact/Send",
            method: "POST",
            data: contactModel,
            success: function (data) {
                ContactForm.SetAnswerLoadingScreen(false);
                Helpers.SetVisible("#pleaseWaitInfo", false);
                $("#answer").html(data);
            },
            error: function (data) {
                ContactForm.SetAnswerLoadingScreen(false);
                Helpers.SetVisible("#pleaseWaitInfo", false);
            }
        });
    }
}
class ContactModel {
    constructor(name, email, mobileNumber, subject, message) {
        this.Name = name;
        this.Email = email;
        this.MobileNumber = mobileNumber;
        this.Subject = subject;
        this.Message = message;
    }
}
//# sourceMappingURL=contact.js.map