$(document).ready(function () {
	$("[data-toggle=tooltip]").tooltip();
    $("#submit").on("click", function () {
        var isValidForm = true;
        $("#ty645r5 input[type='text']").each(function () {
            var a = $(this).attr("data-req");
            if (a !== undefined) {
                if (isValidForm) { isValidForm = IsTextEmpty($(this)); }
                else { IsTextEmpty($(this)); }                
            }
        });
        if (isValidForm) {
			isValidForm = IsTextEmpty($("#ConfigurationType"));
        }
        else {
			IsTextEmpty($("#ConfigurationType"));
        }
        if (isValidForm) {
            $("#submit").attr("value", "Please wait...");
        }
        return isValidForm;
    });

    $("#Search").on("click", function () {
        var isValidForm = IsTextEmpty($("#WebsiteName"));
        
        if (isValidForm) {
            $("#Search").attr("value", "Please wait... Searching...");
            $("#Paging_PageIndex").val('0');
        }
        return isValidForm;
    });

    $(".fa-eye").on("click", function () {
        $(this).siblings("span").html($(this).attr("data-pass"));
    });

    var serch = $("#WebsiteName").val().length;
    if (serch > 0) {
        $("#ty645r5").removeClass('show');
        $("#gdvgav54").addClass('collapsed');
        $("#terf45r5").addClass('show');
        $("#btnS65f").removeClass('collapsed');
    }
});

var rvt = $("#webForm input[name='__RequestVerificationToken']").val();
function ResetForm() {
    $("input[type='text']").val("");
    $("input[type='text']").removeClass("is-invalid");
    $("input[type='text']").removeClass("is-valid");
    $("#reset").hide();
    $("#submit").attr("value", "Add Website");
    $("#submit").attr("formaction", "/website");
}
function Edit(ele) {
    var columns = $(ele).parent().parent().find("td");
    $("#Name").val(columns[1].innerText);
    $("#URL").val(columns[2].innerText);
    $("#UserId").val(columns[3].innerText);
	$("#UserIdTextId").val(columns[6].innerText);
	$("#PasswordTextId").val(columns[7].innerText);
	if (columns[5].innerText === "ById") { $("#ConfigurationType").val('1'); }
	else if (columns[5].innerText === "ByName") { $("#ConfigurationType").val('2'); }
	$("#ButtonId").val(columns[8].innerText);
	$("#Script").val(columns[9].innerText);

    $("#UniqueId").val($(ele).parent().attr("data-id"));
    $("#Password").val($(columns[4]).children().attr("data-pass"));
	$("#ty645r5 input[type='text']").switchClass("is-invalid", "is-valid");
	$("#ConfigurationType").switchClass("is-invalid", "is-valid");
    $("#submit").attr("value", "Update");
    $("#submit").attr("formaction", "/website/update");
    $("#terf45r5").removeClass('show');
    $("#btnS65f").addClass('collapsed');
	$("#ty645r5").addClass('show');
	$("#Use5rr").hide();
	$("#reset").show();
    $("#gdvgav54").removeClass('collapsed');
    SmoothScroll();
}
function Remove(ele) {
    var user = $(ele).parent().attr("data-user");
    if (confirm('Are you sure to Delete website with User = ' + user + '?')) {
        var id = $(ele).parent().attr("data-id");
        $.ajax({
            url: "/Website/Delete",
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
                    DispalyPop(true, 'Website Deleted Successfully');
                }
                else {
                    DispalyPop(false, 'Unable to delete Website!!');
                }
            },
            error: function (responce) {
                DispalyPop(false, 'Unable to delete Website!!');
            }
        });
    }
    ResetForm();
}
function Lock(ele) {
    var user = $(ele).parent().attr("data-user");
    if (confirm('Are you sure to Block website with User = ' + user + '?')) {
        var id = $(ele).parent().attr("data-id");
        $.ajax({
            url: "/Website/Block",
            type: "POST",
            data: { UniqueId: id, __RequestVerificationToken: rvt },
            contenType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data > 0) {
                    $(ele).parent().html('<span class="fa fa-unlock" title="UnBlock Website" onclick="UnLock(this)"></span>');
                    DispalyPop(true, 'Website Blocked Successfully');
                }
                else {
                    DispalyPop(false, 'Unable to Block Website!!');
                }
            },
            error: function (responce) {
                DispalyPop(false, 'Unable to Block Website!!');
            }
        });
    }
    ResetForm();
}
function UnLock(ele) {
    var user = $(ele).parent().attr("data-user");
    if (confirm('Are you sure to UnBlock website with User = ' + user + '?')) {
        var id = $(ele).parent().attr("data-id");
        $.ajax({
            url: "/Website/UnBlock",
            type: "POST",
            data: { UniqueId: id, __RequestVerificationToken: rvt },
            contenType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data > 0) {
                    $(ele).parent().html('<span class="fa fa-remove" title="Delete Website" onclick="Remove(this)"></span> &nbsp;<span class="fa fa-edit" title="Update website" onclick="Edit(this)"></span> &nbsp;<span class="fa fa-lock" title="Block Website" onclick="Lock(this)"></span>');
                    DispalyPop(true, 'Website UnBlocked Successfully');
                }
                else {
                    DispalyPop(false, 'Unable to UnBlock Website!!');
                }
            },
            error: function (responce) {
                DispalyPop(false, 'Unable to UnBlock Website!!');
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

