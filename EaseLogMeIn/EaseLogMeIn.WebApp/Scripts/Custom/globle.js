
/// Return True for non empty text box, Dropdown else return false
function IsTextEmpty(ele) {
    var val = $(ele).val();
    if (val === undefined) { return false; }
    if (val.length === 0) {
        $(ele).switchClass("is-valid", "is-invalid");
        return false;
    }
    else {
        $(ele).switchClass("is-invalid", "is-valid");
        return true;
    }
}

function SmoothScroll() {
    window.scroll(0, 0);
}

function DispalyPop(IsSuccess, text) {
    if (IsSuccess) {
        $("#dataTxt").removeClass("dngr")
        $("#dataTxt").addClass("text-success")
    }
    else {
        $("#dataTxt").removeClass("text-success")
        $("#dataTxt").addClass("dngr")
    }
    $("#overlayPop").fadeIn(100);
    $("#alertWin").fadeIn(100);
    $("#dataTxt").html(text);

    setTimeout(function () {
        $("#overlayPop").fadeOut(200);
        $("#alertWin").fadeOut(200);
    }, 1000 * 1);
}

function PreventMultipleClick() {
    $(".btn").attr("disabled", "disabled");
    $(".btn").css("cursor", "not-allowed");

}

window.onbeforeunload = PreventMultipleClick();

$(document).ready(function () {
	$("input[type='text'], Input[type='number'], input[type='password']").attr("autocomplete", "off");

    $("input[type='text'], input[type='password'], select").on("change", function () {
        var a = $(this).attr("data-req");
        if (a !== undefined) { IsTextEmpty($(this)); }
    })
    var pageUrl = window.location.pathname;
    pageUrl = decodeURI(pageUrl);
    if (pageUrl.length > 1) {
        $("#topmenu a").each(function () {
            var path = $(this).attr("href");
            if (path === pageUrl) {
                $(this).addClass("current");
            }
            if (path.length > 1 && pageUrl.indexOf(path) > -1) {
                $(this).addClass("current");
            }
        })
    }
    else {
        $("#topmenu a").first().addClass("current");
    }
})
function IsValidUrl(ele) {
    var val = $(ele).val();
    if (val === undefined) { return false; }
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + //port
        '(\\?[;&amp;a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i');
    if (val.length > 10) {
        var regex = new RegExp(pattern);
        if (val.match(regex)) {
            $(ele).switchClass("is-invalid", "is-valid");
            return true;
        }
    }
    $(ele).switchClass("is-valid", "is-invalid");
    return false;
}

function ValidatePaging(pageSize) {
    var pct = parseInt($(".pct").html());
    var cnt = parseInt($(".cnt").html());
    $(".pct").html(pct - 1);
    $(".cnt").html(cnt - 1);
    if (cnt - 1 == pageSize) {
        $("#next").addClass("disabled");
    }
}
function nextPage() {
    var id = parseInt(($("#Paging_PageIndex").val())) + 1;
    $("#Paging_PageIndex").val(id);
}
function prevPage() {
    var id = parseInt(($("#Paging_PageIndex").val())) - 1;
    $("#Paging_PageIndex").val(id);
}