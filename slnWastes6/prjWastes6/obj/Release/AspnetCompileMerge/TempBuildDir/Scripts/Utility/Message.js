message = {};
//==整合bootstrapQ=====================================================
//模擬開啟javascript的alert視窗
message.alert = function (Msg, func) {
    var options = {
        url: "",
        fade: "fade",
        close: true,
        title: "<span style='font-size: 18px; font-family: 微軟正黑體, Verdana, Helvetica, Sans-Serif;'><i class=\"fa fa-warning\"></i> 網頁訊息</span>",
        btn: false,
        okbtn: "<i class=\"fa fa-check\"></i> 確　定",
        qubtn: "",
        msg: "<span style='font-size: 16px; font-family: 微軟正黑體, Verdana, Helvetica, Sans-Serif;'>" + Msg + "</span>",
        big: false,
        show: false,
        remote: false,
        backdrop: "static",
        keyboard: true,
        style: ""
    };

    if (func == "") {
        bootstrapQ.alert(options);
    }
    else {
        bootstrapQ.alert(options, func);
    }
}

//模擬開啟javascript的confirm視窗
message.confirm = function (Msg, ok, cancel) {
    var options = {
        url: "",
        fade: "fade",
        close: true,
        title: "<span style='font-size: 18px; font-family: 微軟正黑體, Verdana, Helvetica, Sans-Serif;'><i class=\"fa fa-warning\"></i> 網頁訊息</span>",
        btn: false,
        okbtn: "<i class=\"fa fa-check\"></i> 確　定",
        qubtn: "<i class=\"fa fa-times\"></i> 取　消",
        msg: "<span style='font-size: 16px; font-family: 微軟正黑體, Verdana, Helvetica, Sans-Serif;'>" + Msg + "</span>",
        big: false,
        show: false,
        remote: false,
        backdrop: "static",
        keyboard: true,
        style: ""
    };
    bootstrapQ.confirm(options, ok, cancel);
}

//開啟等待訊息(BlackMask=true用黑遮罩；其他狀況為透明遮罩)
message.openWaitTip = function (BlackMask) {
    document.getElementById('TipBox').style.display = "block";
    $("#TipShadow").show();
    $("body").scrollTop = "0px";

    var Covering = "WhiteCovering";
    if (BlackMask == true) Covering = "BlackCovering";
    $("#" + Covering).css({ "display": "block", "height": document.body.scrollHeight + "px" });
}

//關閉等待訊息
message.closeWaitTip = function () {
    $("#TipBox").hide();
    $("#TipShadow").hide();
    $("#WhiteCovering").hide();
    $("#BlackCovering").hide();
}

//開啟iframe視窗
message.openIframe = function (title, url, height) {
    //參考資料 http://www.bkjia.com/webzh/883250.html

    var xxxxx_NoPermissionModal = document.getElementById("xxxxx_NoPermissionModal");
    var xxxxx_NoPermissioniframe = document.getElementById("xxxxx_NoPermissioniframe");
    var xxxxx_NoPermissionModalTitle = document.getElementById("xxxxx_NoPermissionModalTitle");
    if (!xxxxx_NoPermissionModal || !xxxxx_NoPermissioniframe || !xxxxx_NoPermissionModalTitle) return;

    if (title == "") title = "&nbsp;";
    xxxxx_NoPermissionModalTitle.innerHTML = title;
    xxxxx_NoPermissioniframe.style.height = String(height) + "px";

    var frameSrc = url;
    $("#" + xxxxx_NoPermissioniframe.id).attr("src", "");
    $("#" + xxxxx_NoPermissioniframe.id).attr("src", frameSrc);
    $('#' + xxxxx_NoPermissionModal.id).modal({ show: true, backdrop: 'static' });
}

//關閉iframe小視窗
message.closeIframe = function () {
    var xxxxx_NoPermissionModalClose = document.getElementById("xxxxx_NoPermissionModalClose");
    if (xxxxx_NoPermissionModalClose) xxxxx_NoPermissionModalClose.click();
}

//開啟查詢視窗
message.openQuery = function (cId, title) {
    var objDiv = document.getElementById(cId);
    if (!objDiv) return;

    var QueryXXX_NoPermissionModal = objDiv.children[0];
    if (!QueryXXX_NoPermissionModal) return;    
    if (QueryXXX_NoPermissionModal.className != "modal fade") return;
    
    //塞入title，使用者有傳入的話
    if (title != undefined) {
        if (title != "") {
            var modal_dialog = QueryXXX_NoPermissionModal.children[0];
            if (!modal_dialog) return;

            var modal_content = modal_dialog.children[0];
            if (!modal_content) return;

            var modal_header = modal_content.children[0];
            if (!modal_header) return;

            modal_header.children[1].innerHTML = title;
        }
    }

    //隨機取Id
    QueryXXX_NoPermissionModal.id = "QueryXXX_NoPermissionModal" + String(Math.random()).replace(".", "");
    $('#' + QueryXXX_NoPermissionModal.id).modal({ show: true, backdrop: 'static' });
}