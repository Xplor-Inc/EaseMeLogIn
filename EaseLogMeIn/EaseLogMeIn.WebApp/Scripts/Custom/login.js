
$(document).ready(function () {
    $("#UserId").focus();
    $("#submit").on("click", function () {
        var isValidForm = true;
        $("input[type='text'], input[type='password']").each(function () {
            if (isValidForm) { isValidForm = IsTextEmpty($(this));  IsTextEmpty($(this)); }
            else { IsTextEmpty($(this)); }
        })
        if (isValidForm) {
            $("#submit").attr("value", "Please wait");
        }
        return isValidForm;
    })
})
