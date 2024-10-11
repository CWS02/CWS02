/**
 * 跟 LayoutHtml.cs 有關係的物件應用
 */
var LayoutHtml = {};

//==關於 Collapse 的一些處理=================================================================
/**
 * 打開Collapse
 * @param collapseId 物件Id
 * @returns
 */
LayoutHtml.CollapseOpen = function (collapseId) {
    var outsidePanel = document.getElementById("panel-" + collapseId);
    if (!outsidePanel) return;

    var innerPanel = outsidePanel.children[0];
    var currentPanel = innerPanel.children[1];
    var currentId = currentPanel.id;
    var panel = $("#" + currentId);
    panel.attr("class", "panel-collapse collapse in");
}

/**
 * 收起Collapse
 * @param collapseId 物件Id
 * @returns 
 */
LayoutHtml.CollapseClose = function (collapseId) {
    var outsidePanel = document.getElementById("panel-" + collapseId);
    if (!outsidePanel) return;

    var innerPanel = outsidePanel.children[0];
    var currentPanel = innerPanel.children[1];
    var currentId = currentPanel.id;
    var panel = $("#" + currentId);
    panel.attr("class", "panel-collapse collapse");
}

//===========================================================================

//==關於 ModalDialog 的一些處理=================================================================

//防跳窗內有含篩選功能的下拉無法正常執行 開始=====
//http://www.telerik.com/forums/dropdownlist-with-server-filtering-on-bootstrap-modal
function focusinModalInitial() {
    $('.modal').on('shown.bs.modal',
        function () {
            $(document).off('focusin.modal');
        }
    );
}
$(document).ready(function () {
    focusinModalInitial();
});
//防跳窗內有含篩選功能的下拉無法正常執行 結束=====

/**
 * 開啟Modal Dialog視窗
 * @param dialogId 物件Id
 * @returns 
 */
LayoutHtml.ModalDialogOpen = function (dialogId, dialogTitle) {
    //自訂標題
    if (dialogTitle != undefined) {
        $("#" + dialogId).find(".modal-title").html(dialogTitle);
    }

    $("#" + dialogId).modal({ show: true, backdrop: 'static' });//點任何地方也不會把視窗關掉
    //$("#" + dialogId).modal({ show: true });//點任何地方就會把視窗關掉
}

LayoutHtml.ModalDialogOpenUrl = function (dialogId, url, dialogTitle) {
    //加載中
    //=====================
    $("#" + dialogId).load(url, null, function (response, status, xhr) {
        if (status === "success") {
            //自訂標題
            if (dialogTitle != undefined) {
                $("#" + dialogId).find(".modal-title").html(dialogTitle);
            }

            $("#" + dialogId).find(".modal").modal({ show: true, backdrop: 'static' });//點任何地方也不會把視窗關掉    

            //==重新註冊日期元件的事件========================
            datetimepickerInitial();
        }
        else if (status === "error") {
            //發生例外錯誤
            var errors = GetAjaxResponseText(xhr.responseText);
            kendo.alert("訊息", errors, 'M', '確定');
        } else {
            alert(status);
        }
    });
}

/**
 * 關閉Modal Dialog視窗
 * @param dialogId 物件Id
 * @returns 
 */
LayoutHtml.ModalDialogClose = function (dialogId) {
    $("#" + dialogId).modal('hide');
    $("#" + dialogId).find(".modal").modal('hide');
}
//===========================================================================