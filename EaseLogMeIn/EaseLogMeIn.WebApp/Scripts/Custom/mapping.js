function checkAll(ele) {
	var id = $(ele).attr("data-id");
	var ischecked = $(ele).is(":checked");
	$("#usrSlctError").html('');
	if (ischecked) {
		$("#" + id + " input[type='checkbox']").prop("checked", true);
	}
	else {
		if (id === "Webs87t") {
			$("#usrSlctError").html('Select atleast one Website');
		}
		else {
			$("#usrSlctError").html('Select atleast one User');
		}
		$("#" + id + " input[type='checkbox']").prop("checked", false);
	}
}
function checkSingle(ele) {
	var id = $(ele).parent().parent().parent().parent().attr("id");
	ckh(id);
}
function ckh(id) {
	try {
		var totalCheckboxes = $("#" + id + " .chkSingle").length;
		var checkedCheckboxes = $("#" + id + " .chkSingle:checked").length;
		$("#usrSlctError").html('');
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
			if (id === "Webs87t") {
				$("#usrSlctError").html('Select atleast one Website');
			}
			else {
				$("#usrSlctError").html('Select atleast one User');
			}
		}
	} catch (e) {
		console.log(e);
	}
}
function FillUsers(id) {
	$.ajax({
		url: "/Mapping/GetByUser/" + id,
		type: "GET",
		contenType: 'application/json; charset=utf-8',
		success: function (data) {
			if (data) {
				console.log(data);
				console.log(id);
				$("#Webs87t input[type='checkbox']").each(function () {
					var wid = $(this).attr("dataid");
					for (var i = 0; i < data.length; i++) {
						if (wid === data[i].WebsiteId) {
							$(this).prop("checked", true);
							break;
						}
					}
				});
				ckh("Webs87t");
			}
		},
		error: function (responce) {
			console.log(responce);
		}
	});
}
function FillWebsites(id) {
	$.ajax({
		url: "/Mapping/GetByWebsite/" + id,
		type: "GET",
		contenType: 'application/json; charset=utf-8',
		success: function (data) {
			if (data) {
				$("#Use5rr input[type='checkbox']").each(function () {
					var wid = $(this).attr("dataid");
					for (var i = 0; i < data.length; i++) {
						if (wid === data[i].UserId) {
							$(this).prop("checked", true);
							break;
						}
					}
				});
				ckh("Use5rr");
			}
		},
		error: function (responce) {
			console.log(responce);
		}
	});
}
$(document).ready(function () {
	$("#submit").on("click", function () {
		var isValidForm = IsTextEmpty($("#MappingType"));
		var typ = $("#MappingType").val();
		if (typ === "1") {
			isValidForm = IsTextEmpty($("#UserId"));
			var wbchk = $("#Webs87t .chkSingle:checked").length;
			if (wbchk === 0) {
				isValidForm = false;
				$("#usrSlctError").html('Select atleast one Website');
			}
		}
		if (typ === "2") {
			isValidForm = IsTextEmpty($("#WebsiteId"));
			var wbchk = $("#Use5rr .chkSingle:checked").length;
			if (wbchk === 0) {
				isValidForm = false;
				$("#usrSlctError").html('Select atleast one User');
			}
		}
		return isValidForm;
	});

	$("select").on("change", function () {
		IsTextEmpty($(this));
	})

	$("#UserId").on("change", function () {
		var uid = $(this).val();
		$("#WckeckAll")[0].indeterminate = false;
		$("#Webs87t input[type='checkbox']").prop("checked", false);
		if (uid.length > 0) {
			FillUsers(uid);
		}
	})
	$("#WebsiteId").on("change", function () {
		var uid = $(this).val();
		$("#Use5rr input[type='checkbox']").prop("checked", false);
		$("#UckeckAll")[0].indeterminate = false;

		if (uid.length > 0) {
			FillWebsites(uid);
		}
	})


	$("#MappingType").on("change", function () {
		$("#UserId").val('');
		$("#WebsiteId").val('');
		$("#UserId").removeClass('is-invalid').removeClass('is-valid');
		$("#WebsiteId").removeClass('is-invalid').removeClass('is-valid');
		$("#UckeckAll")[0].indeterminate = false;
		$("#WckeckAll")[0].indeterminate = false;

		$("input[type='checkbox']").prop("checked", false);
		$("#usrSlctError").html('');
		var val = $(this).val();
		if (val.length === 0) {
			$(".User54").hide();
			$(".Website34").hide();
		}
		if (val === "1") {
			$(".User54").show();
			$(".Website34").hide();
			$("#submit").attr("value", "Map User");
		} if (val === "2") {
			$(".User54").hide();
			$(".Website34").show();
			$("#submit").attr("value", "Map Website");
		}
	})
})
