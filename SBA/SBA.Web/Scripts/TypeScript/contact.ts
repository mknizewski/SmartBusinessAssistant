$(() => {
    ContactForm.BindEvents();
})

class ContactForm {
    public static BindEvents() {
        ContactForm.ContactUsBtnOnClick();
    }

    public static SetAnswerLoadingScreen(isEnable) {
        if (isEnable)
            $("#loadingAnswerSpinner").show();
        else
            $("#loadingAnswerSpinner").hide();
    }

    public static SetAnswerSection(isEnable) {
        if (isEnable)
            $("#possibleAnswer").show();
        else
            $("#possibleAnswer").hide();
    }

    private static ContactUsBtnOnClick() {
        $('#btnContactUs').click(function (event) {
            ContactUsBtn.Init().DoLogic(event);
        });
    }
}

class ContactUsBtn {
    public static Init() {
        return new ContactUsBtn();
    }

    public DoLogic(event): void {
        ContactForm.SetAnswerSection(true);
        ContactForm.SetAnswerLoadingScreen(true);
        Helpers.SetVisible("#pleaseWaitInfo", true);
        Helpers.ScrollTo("#possibleAnswer");
        Helpers.Clear("#answer");

        var name = $("#name").val() as string;
        var email = $("#email").val() as string;
        var mobilePhone = "";
        var subject = $("#subject").val() as string;
        var message = $("#message").val() as string;

        var contactModel = new ContactModel(
            name,
            email,
            mobilePhone,
            subject,
            message
        );

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
    public Name: string;
    public Email: string;
    public MobileNumber: string;
    public Subject: string;
    public Message: string;

    constructor(
        name: string,
        email: string,
        mobileNumber: string,
        subject: string,
        message: string) {
        this.Name = name;
        this.Email = email;
        this.MobileNumber = mobileNumber;
        this.Subject = subject;
        this.Message = message;
    }
}