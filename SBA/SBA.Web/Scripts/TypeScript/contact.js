$(() => {
    ContactForm.BindEvents();
});
class ContactForm {
    static BindEvents() {
        ContactForm.ContactUsBtnOnClick();
        ContactForm.HandUpBtnOnClick();
        ContactForm.HandDownBtnOnClick();
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
    static HandUpBtnOnClick() {
        $('#handUpBtn').click(function (event) {
            HandUpBtn.Init().DoLogic();
        });
    }
    static HandDownBtnOnClick() {
        $("#handDownBtn").click(function (event) {
            HandDownBtn.Init().DoLogic();
        });
    }
}
class ContactUsBtn {
    static Init() {
        return new ContactUsBtn();
    }
    SetLoadingSectionVisible() {
        ContactForm.SetAnswerSection(true);
        ContactForm.SetAnswerLoadingScreen(true);
        Helpers.SetVisible("#pleaseWaitInfo", true);
        Helpers.ScrollTo("#possibleAnswer");
        Helpers.Clear("#answer");
        Helpers.SetDisable("#btnContactUs", true);
        Helpers.SetVisible("#userFeedback", false);
        Helpers.Clear("#serverFeedback");
    }
    DoLogic(event) {
        event.preventDefault();
        if (!$('#contactForm').valid())
            return;
        this.SetLoadingSectionVisible();
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
                $("#answer").text("W danej chwili funkcja szybkiej odpowiedzi jest niedostępna. Przepraszamy.");
            }
        });
    }
}
class HandUpBtn {
    static Init() {
        return new HandUpBtn();
    }
    DoLogic() {
        var dictionary = Helpers.GetDataFromSessionStorage("answerData");
        var questionModel = new QuestionModel(parseInt(dictionary["AnswerId"]), dictionary["Question"]);
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
    static Init() {
        return new HandDownBtn();
    }
    DoLogic() {
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
class QuestionModel {
    constructor(answerId, question) {
        this.AnswerId = answerId;
        this.Question = question;
    }
}
//# sourceMappingURL=contact.js.map