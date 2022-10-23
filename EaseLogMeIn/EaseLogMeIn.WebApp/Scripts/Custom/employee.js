$(document).ready(function () {
	$("#submit").on("click", function () {
		var isValidForm = true;
		$(".ttx56r4 input[type='text']").each(function () {
			var a = $(this).attr("data-req");
			if (a !== undefined) {
				if (isValidForm) { isValidForm = IsTextEmpty($(this)); }
				else { IsTextEmpty($(this)); }
			}
		});
		var checked = $("#IsNonADUser").is(":checked");
		if (checked) {
			$(".dvadUser input[type='text']").each(function () {
				if (isValidForm) { isValidForm = IsTextEmpty($(this)); }
				else { IsTextEmpty($(this)); }
			});
		}
		if (isValidForm) {
			$("#submit").attr("value", "Please wait...");
		}
		return isValidForm;
	});

	$("#Search").on("click", function () {
		var isValidForm = IsTextEmpty($("#EmployeeName"));

		if (isValidForm) {
			$("#Search").attr("value", "Please wait... Searching...");
			$("#Paging_PageIndex").val('0');
		}
		return isValidForm;
	});

	var serch = $("#EmployeeName").val().length;
	if (serch > 0) {
		$("#ty645r5").removeClass('show');
		$("#gdvgav54").addClass('collapsed');
		$("#terf45r5").addClass('show');
		$("#btnS65f").removeClass('collapsed');
	}
});

function NonADUser() {
	var checked = $("#IsNonADUser").is(":checked");
	if (checked) {
		$(".dvadUser").show();
		$("#IsNonADUser").siblings('span').html('Yes');
	}
	else {
		$(".dvadUser").hide();
		$("#IsNonADUser").siblings('span').html('No');
	}
	var txt = $(".dvadUser input[type='text']");
	txt.val('');
	txt.removeClass('is-valid');
	txt.removeClass('is-invalid');
}

var rvt = $("#webForm input[name='__RequestVerificationToken']").val();
function ResetForm() {
	$("input[type='text']").val("");
	$("input[type='text']").removeClass("is-invalid");
	$("input[type='text']").removeClass("is-valid");
	$("#reset").hide();
	$("#submit").attr("value", "Add Employee");
	$("#submit").attr("formaction", "/Employee");
}
function Edit(ele) {
	var columns = $(ele).parent().parent().find("td");
	$("#Name").val(columns[1].innerText);
	$("#UserId").val(columns[2].innerText);
	$("#MACId").val(columns[4].innerText);
	$("#UniqueId").val($(ele).parent().attr("data-id"));
	$("#Webs87t").hide(); $("#reset").show();
	var IsNonADUser = columns[3].innerText.trim();
	if (IsNonADUser === "Yes") {
		$("#dvMACId").show();
		$("#IsNonADUser").siblings('span').html('Yes');
		$("#IsNonADUser").prop("checked", true);
	}
	else {
		$("#dvMACId").hide();
		$("#IsNonADUser").siblings('span').html('No');
		$("#IsNonADUser").prop("checked", false);
	}
	$("#Password").val($(columns[4]).children().attr("data-pass"));
	$("#ty645r5 input[type='text']").switchClass("is-invalid", "is-valid");
	$("#submit").attr("value", "Update");
	$("#submit").attr("formaction", "/Employee/update");
	$("#terf45r5").removeClass('show');
	$("#btnS65f").addClass('collapsed');
	$("#ty645r5").addClass('show');
	$("#gdvgav54").removeClass('collapsed');
	SmoothScroll();
}
function Remove(ele) {
	var user = $(ele).parent().attr("data-user");
	if (confirm('Are you sure to Delete Employee with User = ' + user + '?')) {
		var id = $(ele).parent().attr("data-id");
		$.ajax({
			url: "/Employee/Delete",
			type: "POST",
			data: { UniqueId: id, __RequestVerificationToken: rvt },
			contenType: 'application/json; charset=utf-8',
			success: function (data) {
				if (data > 0) {
					$(ele).parent().parent().remove();
					var rows = $("#wblist").find("tr");
					if (rows.length == 1) {
						$("#wblist").remove();
					}
					else {
						for (var i = 1; i < rows.length; i++) {
							var tc = $(rows[i]).find("td");
							tc[0].innerText = i;
						}
					}
					ValidatePaging(10);
					DispalyPop(true, 'Employee Deleted Successfully');
				}
				else {
					DispalyPop(false, 'Unable to delete Employee!!');
				}
			},
			error: function (responce) {
				DispalyPop(false, 'Unable to delete Employee!!');
			}
		});
	}
	ResetForm();
}
function Lock(ele) {
	var user = $(ele).parent().attr("data-user");
	if (confirm('Are you sure to Block Employee with User = ' + user + '?')) {
		var id = $(ele).parent().attr("data-id");
		$.ajax({
			url: "/Employee/Block",
			type: "POST",
			data: { UniqueId: id, __RequestVerificationToken: rvt },
			contenType: 'application/json; charset=utf-8',
			success: function (data) {
				if (data > 0) {
					$(ele).parent().html('<span class="fa fa-unlock" title="UnBlock Employee" onclick="UnLock(this)"></span>');
					DispalyPop(true, 'Employee Blocked Successfully');
				}
				else {
					DispalyPop(false, 'Unable to Block Employee!!');
				}
			},
			error: function (responce) {
				DispalyPop(false, 'Unable to Block Employee!!');
			}
		});
	}
	ResetForm();
}
function UnLock(ele) {
	var user = $(ele).parent().attr("data-user");
	if (confirm('Are you sure to UnBlock Employee with User = ' + user + '?')) {
		var id = $(ele).parent().attr("data-id");
		$.ajax({
			url: "/Employee/UnBlock",
			type: "POST",
			data: { UniqueId: id, __RequestVerificationToken: rvt },
			contenType: 'application/json; charset=utf-8',
			success: function (data) {
				if (data > 0) {
					$(ele).parent().html('<span class="fa fa-remove" title="Delete Employee" onclick="Remove(this)"></span> &nbsp;<span class="fa fa-edit" title="Update Employee" onclick="Edit(this)"></span> &nbsp;<span class="fa fa-lock" title="Block Employee" onclick="Lock(this)"></span>');
					DispalyPop(true, 'Employee UnBlocked Successfully');
				}
				else {
					DispalyPop(false, 'Unable to UnBlock Employee!!');
				}
			},
			error: function (responce) {
				DispalyPop(false, 'Unable to UnBlock Employee!!');
			}
		});
	}
	ResetForm();
}
function checkAll(ele) {
	var id = $(ele).attr("data-id");
	var ischecked = $(ele).is(":checked");
	if (ischecked) {
		$("#" + id + " input[type='checkbox']").prop("checked", true);
	}
	else {
		$("#" + id + " input[type='checkbox']").prop("checked", false);
	}
}
function checkSingle(ele) {
	try {
		var id = $(ele).parent().parent().parent().parent().attr("id");
		var totalCheckboxes = $("#" + id + " .chkSingle").length;
		var checkedCheckboxes = $("#" + id + " .chkSingle:checked").length;
		if (totalCheckboxes === checkedCheckboxes) {
			$("#" + id + " .all").prop('checked', true);
			$("#" + id + " .all")[0].indeterminate = false;
		}
		else if (checkedCheckboxes > 0) {
			$("#" + id + " .all").prop('checked', false);
			$("#" + id + " .all")[0].indeterminate = true;
		}
		else if (checkedCheckboxes === 0) {
			$("#" + id + " .all").prop('checked', false);
			$("#" + id + " .all")[0].indeterminate = false;
		}
	} catch (e) {
		console.log(e);
	}
}
