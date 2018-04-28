$(() => {
    ContactForm.BindEvents();
})

class ContactForm {
    public static BindEvents() {
        ContactForm.ContactUsBtnOnClick();
        ContactForm.HandUpBtnOnClick();
        ContactForm.HandDownBtnOnClick();
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

    private static HandUpBtnOnClick() {
        $('#handUpBtn').click(function (event) {
            HandUpBtn.Init().DoLogic();
        });
    }

    private static HandDownBtnOnClick() {
        $("#handDownBtn").click(function (event) {
            HandDownBtn.Init().DoLogic();
        })
    }
}

class ContactUsBtn {
    public static Init() {
        return new ContactUsBtn();
    }

    public SetLoadingSectionVisible(): void {
        ContactForm.SetAnswerSection(true);
        ContactForm.SetAnswerLoadingScreen(true);
        Helpers.SetVisible("#pleaseWaitInfo", true);
        Helpers.ScrollTo("#possibleAnswer");
        Helpers.Clear("#answer");
        Helpers.SetDisable("#btnContactUs", true);
        Helpers.SetVisible("#userFeedback", false);
        Helpers.Clear("#serverFeedback");
    }

    public DoLogic(event): void {
        this.SetLoadingSectionVisible();

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
                Helpers.SetDisable("#btnContactUs", false);

                if (data.HaveAnswer) {
                    Helpers.SetVisible("#userFeedback", true);
                    Helpers.SetDataToSessionStorage("answerData", data);

                    $("#answerPropability").text("Dokładność odpowiedzi: " + data.Propability + "%");
                    $("#answer").html(data.Answer);
                }
                else {
                    $("#answer").html(data.ErrorMessage);
                }
            },
            error: function (data) {
                ContactForm.SetAnswerLoadingScreen(false);
                Helpers.SetVisible("#pleaseWaitInfo", false);
                Helpers.SetDisable("#btnContactUs", false);
            }
        });
    }
}

class HandUpBtn {
    public static Init() {
        return new HandUpBtn();
    }

    public DoLogic(): void {
        var dictionary = Helpers.GetDataFromSessionStorage("answerData");
        var questionModel = new QuestionModel(
            parseInt(dictionary["AnswerId"]),
            dictionary["Question"]
        );

        $.ajax({
            url: "Contact/HandUp",
            method: "POST",
            data: questionModel,
            success: function (data) {
                $("#serverFeedback").text(data);
                Helpers.SetVisible("#userFeedback", false);
            }
        });
    }
}

class HandDownBtn {
    public static Init() {
        return new HandDownBtn();
    }

    public DoLogic(): void {

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

class QuestionModel {
    public AnswerId: number;
    public Question: string;

    constructor(
        answerId: number,
        question: string) {
        this.AnswerId = answerId;
        this.Question = question;
    }
}