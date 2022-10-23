
$(document).ready(function () {
	$("#OldPassword").focus();
	$("#Submit").on("click", function () {
		var isValidForm = true;
		$("input[type='text'], input[type='password']").each(function () {
			if (isValidForm) { isValidForm = IsTextEmpty($(this));  IsTextEmpty($(this)); }
			else { IsTextEmpty($(this)); }
		})
		if (isValidForm) {
			$("#Submit").attr("value", "Please wait");
		}
		return isValidForm;
	})
})
