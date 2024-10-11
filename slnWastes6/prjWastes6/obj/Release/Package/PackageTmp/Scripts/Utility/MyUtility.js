//==========================檢查數字（不可等於0）===============================
function checkNum(sValue) {
    if (((!(sValue > 0)) || (sValue == "")))							//不是數字
    {
        return false;
    }
    else														//是數字
    {
        return true;
    }
}

//==========================檢查數字（可等於0）===============================
function checkZeroNum(sValue) {
    if (((!(sValue >= 0)) || (sValue == "")))							//不是數字
    {
        return false;
    }
    else														//是數字
    {
        return true;
    }
}

//==============去空白處理=======================================================
//Trim() , Ltrim() , RTrim()
String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); } //去除頭尾空白
String.prototype.lTrim = function () { return this.replace(/(^\s*)/g, ""); }       //去除左側（頭）空白
String.prototype.rTrim = function () { return this.replace(/(\s*$)/g, ""); }       //去除右側（尾）空白
String.prototype.Trim = function () { return this.lTrim().rTrim(); }                //利用LTrim、RTrim來實做的trim
//================================================================================

/**
 * 檢查Eamil是否正確
 */
function chkEmail(s_email) {
    var check = /.+@.+\..+/;
    if (!(s_email.match(check))) {
        return false;
    }
    else {
        return true;
    }
}

//檢查是否為數字，且可限制小數點
function checkNum1(objField, strFieldName, intLength) {
    if (objField.value != "") {
        if ((isNaN(objField.value.charAt(0))) && (objField.value.charAt(0) != "-")) {
            alert(strFieldName + "請輸入數值！");
            objField.focus();
            return true;
        }


        if (!(objField.value.substring(1, objField.value.length) >= 0)) {
            alert(strFieldName + "請輸入數值！");
            objField.focus();
            return true;
        }

        if ((objField.value.length - (intLength + 1)) > 0) {
            if ((objField.value.indexOf(".") < (objField.value.length - (intLength + 1))) && (objField.value.indexOf(".") != -1)) {
                alert(strFieldName + "只能輸入到小數點以下" + intLength + "位！");
                objField.focus();
                return true;
            }
        }
    }
}

//======== 以下區塊為設計共用下拉選單物件用 =======================================================

//初始化學制下拉選單
//vDType：資料權限類型
//vSubDType：資料權限子類型
//ID_Edu02：學制下拉選單ID
//ID_Department：學部下拉選單ID
//DefaultValue：預設值
function Initialize_DropDowList_Edu02(vDType, vSubDType, vID_Edu02, vID_Department, DefaultValue) {
    ChangeEduDepartment(vDType, vSubDType, vID_Department, vID_Edu02, "", "", "")
    if (DefaultValue.length > 0) { $("#" + vID_Edu02).val(DefaultValue.trim()); }
}


//初始化科系下拉選單
//vDType：資料權限類型
//vSubDType：資料權限子類型
//vID_Edu04：科系下拉選單ID
//vID_Edu02：學制下拉選單ID
//vID_Edu03：學院下拉選單ID
//DefaultValue：預設值
function Initialize_DropDowList_Edu04(vDType, vSubDType, vID_Edu04, vID_Edu02, vID_Edu03, DefaultValue) {
    var selectedValue_Edu02;
    var selectedValue_Edu03;

    if (vID_Edu02.length == 0) {
        selectedValue_Edu02 = "";
    }
    else {
        selectedValue_Edu02 = $('#' + vID_Edu02 + ' option:selected').val();
    }

    if (vID_Edu03.length == 0) {
        selectedValue_Edu03 = "";
    }
    else {
        selectedValue_Edu03 = $('#' + vID_Edu03 + ' option:selected').val();
    }

    GetEdu04(vDType, vSubDType, selectedValue_Edu02, selectedValue_Edu03, vID_Edu04);

    if (DefaultValue.length > 0) { $("#" + vID_Edu04).val(DefaultValue.trim()); }
}


//初始化年級下拉選單
//vDType：資料權限類型
//vSubDType：資料權限子類型
//ID_Grade：年級下拉選單ID
//vID_Edu02：學制下拉選單ID
//DefaultValue：預設值
function Initialize_DropDowList_Grade(vDType, vSubDType, vID_Grade, vID_Edu02, DefaultValue) {
    var selectedValue_Edu02;

    if (vID_Edu02.length == 0) {
        selectedValue_Edu02 = "";
    }
    else {
        selectedValue_Edu02 = $('#' + vID_Edu02 + ' option:selected').val();
    }

    GetGrade(vDType, vSubDType, selectedValue_Edu02, vID_Grade)

    if (DefaultValue.length > 0) { $("#" + vID_Grade).val(DefaultValue.trim()); }
}


//初始化班級下拉選單
//vDType：資料權限類型
//vSubDType：資料權限子類型
//vID_Edu06：班級下拉選單ID
//vID_Edu04：科系下拉選單ID
//vID_Grade：年級下拉選單ID
//DefaultValue：預設值
function Initialize_DropDowList_Edu06(vDType, vSubDType, vID_Edu06, vID_Edu04, vID_Grade, DefaultValue) {
    var selectedValue_Edu04;
    var selectedValue_Grade

    if (vID_Edu04.length == 0) {
        selectedValue_Edu04 = "";
    }
    else {
        selectedValue_Edu04 = $('#' + vID_Edu04 + ' option:selected').val();
    }

    if (vID_Grade.length == 0) {
        selectedValue_Grade = "";
    }
    else {
        selectedValue_Grade = $('#' + vID_Grade + ' option:selected').val();
    }

    GetEdu06(vDType, vSubDType, selectedValue_Edu04, selectedValue_Grade, vID_Edu06)

    if (DefaultValue.length > 0) { $("#" + vID_Edu06).val(DefaultValue.trim()); }
}


//初始化學生下拉選單(取消不用)
//vDType：資料權限類型
//vSubDType：資料權限子類型
//ID_Student：學生下拉選單ID
//vID_Edu06：班級下拉選單ID
//DefaultValue：預設值
//function Initialize_DropDowList_Student(vDType, vSubDType, vID_Student, vID_Edu06, DefaultValue) {
//    var selectedValue_Edu02;

//    if (vID_Edu06.length == 0) {
//        selectedValue_Edu02 = "";
//    }
//    else {
//        selectedValue_Edu06 = $('#' + vID_Edu06 + ' option:selected').val();
//    }

//    GetStudent(vDType, vSubDType, selectedValue_Edu06, vID_Student)

//    if (DefaultValue.length > 0) { $("#" + vID_Student).val(DefaultValue.trim()); }
//}

//初始化人事下拉選單
//vDType：資料權限類型
//vSubDType：資料權限子類型
//ID_EmpMain：人事下拉選單ID
//ID_Unit：單位下拉選單ID
//DefaultValue：預設值
function Initialize_DropDowList_EmpMain(vDType, vSubDType, vID_EmpMain, vID_Unit, DefaultValue) {
    ChangeUnit(vDType, vSubDType, vID_Unit, vID_EmpMain, "", "", "")
    if (DefaultValue.length > 0) { $("#" + vID_EmpMain).val(DefaultValue.trim()); }
}



//異動學院下拉選單事件
function ChangeEdu03(vDType, vSubDType, vID_Self, vID_Edu02, vID_Edu04, vID_Edu06) {
    if (vID_Edu04.length > 0) {
        var selectedValue_Edu02 = $('#' + vID_Edu02 + ' option:selected').val();
        var selectedValue_Edu03 = $('#' + vID_Self + ' option:selected').val();
        if ($.trim(selectedValue_Edu02).length > 0 || $.trim(selectedValue_Edu03).length > 0) {
            GetEdu04(vDType, vSubDType, selectedValue_Edu02, selectedValue_Edu03, vID_Edu04);
            if (vID_Edu06.length > 0) {
                Initialize_DropDowList_Edu06(vDType, vSubDType, vID_Edu06, vID_Edu04, "", "")
            }
        }
    }
}

//異動學部下拉選單事件
function ChangeEduDepartment(vDType, vSubDType, vID_Self, vID_Edu02, vID_Edu04, vID_Grade, vID_Edu06) {
    if (vID_Edu02.length > 0) {
        var selectedValue = $('#' + vID_Self + ' option:selected').val();
        if ($.trim(selectedValue).length > 0) {
            GetEdu02(vDType, vSubDType, selectedValue, vID_Edu02);
            if (vID_Edu04.length > 0) {
                Initialize_DropDowList_Edu04(vDType, vSubDType, vID_Edu04, vID_Edu02, "");
            }
            if (vID_Grade.length > 0) {
                Initialize_DropDowList_Grade(vDType, vSubDType, vID_Grade, vID_Edu02, "")
            }
            if (vID_Edu06.length > 0) {
                Initialize_DropDowList_Edu06(vDType, vSubDType, vID_Edu06, vID_Edu04, "", "")
            }
        }
    }
}

//異動學制下拉選單事件
function ChangeEdu02(vDType, vSubDType, vID_Self, vID_Edu03, vID_Edu04, vID_Grade, vID_Edu06) {
    var selectedValue_Edu02 = $('#' + vID_Self + ' option:selected').val();
    var selectedValue_Edu03 = $('#' + vID_Edu03 + ' option:selected').val();
    if ($.trim(selectedValue_Edu02).length > 0 || $.trim(selectedValue_Edu03).length > 0) {
        if (vID_Grade.length > 0) {
            GetGrade(vDType, vSubDType, selectedValue_Edu02, vID_Grade);
        }
        if (vID_Edu04.length > 0) {
            GetEdu04(vDType, vSubDType, selectedValue_Edu02, selectedValue_Edu03, vID_Edu04);
        }

        if (vID_Edu06.length > 0) {
            Initialize_DropDowList_Edu06(vDType, vSubDType, vID_Edu06, vID_Edu04, "", "")
        }
    }
}

//異動科系下拉選單事件
function ChangeEdu04(vDType, vSubDType, vID_Self, vID_Grade, vID_Edu06) {
    if (vID_Edu06.length > 0) {
        var selectedValue_Edu04 = $('#' + vID_Self + ' option:selected').val();
        var selectedValue_Grade = $('#' + vID_Grade + ' option:selected').val();
        if ($.trim(selectedValue_Edu04).length > 0 || $.trim(selectedValue_Grade).length > 0) {
            if (vID_Edu06.length > 0) {
                GetEdu06(vDType, vSubDType, selectedValue_Edu04, selectedValue_Grade, vID_Edu06)
            }
        }
    }
}

//異動年級下拉選單事件
function ChangeGrade(vDType, vSubDType, vID_Self, vID_Edu04, vID_Edu06) {
    if (vID_Edu06.length > 0) {
        var selectedValue_Edu04 = $('#' + vID_Edu04 + ' option:selected').val();
        var selectedValue_Grade = $('#' + vID_Self + ' option:selected').val();
        if ($.trim(selectedValue_Edu04).length > 0 || $.trim(selectedValue_Grade).length > 0) {
            GetEdu06(vDType, vSubDType, selectedValue_Edu04, selectedValue_Grade, vID_Edu06)
        }
    }
}

//異動班級下拉選單事件(取消不用)
//function ChangeEdu06(vDType, vSubDType, vID_Self, vID_Student) {
//    if (vID_Student.length > 0) {
//        var selectedValue_Edu06 = $('#' + vID_Self + ' option:selected').val();
//        if ($.trim(selectedValue_Edu06).length > 0) {
//            GetStudent(vDType, vSubDType, selectedValue_Edu06, vID_Student)
//        }
//    }
//}

//異動單位下拉選單事件
function ChangeUnit(vDType, vSubDType, vID_Self, vID_EmpMain) {
    if (vID_EmpMain.length > 0) {
        var selectedValue = $('#' + vID_Self + ' option:selected').val();
        if ($.trim(selectedValue).length > 0) {
            GetEmpMain(vDType, vSubDType, selectedValue, vID_EmpMain);
        }
    }
}

//取得學制下拉選單資料
function GetEdu02(vDType, vSubDType, vDepartment, vID_Edu02) {
    $.ajax({
        url: ROOT + 'ajaxService/Edu02',
        data: {
            Department: vDepartment,
            DType: vDType,
            SubDType: vSubDType
        },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#' + vID_Edu02).removeAttr("disabled")
                $('#' + vID_Edu02).empty();
                $.each(data, function (i, item) {
                    $('#' + vID_Edu02).append($('<option></option>').val(item.Key).text(item.Value));
                });
            }
            else {
                $('#' + vID_Edu02).prop("disabled", true);
                $('#' + vID_Edu02).empty();
                $('#' + vID_Edu02).append($('<option></option>').val("-1").text("-----"));
            }
        }
    });
}

//取得科系下拉選單資料
function GetEdu04(vDType, vSubDType, vEdu02, vEdu03, vID_Edu04) {
    $.ajax({
        url: ROOT + 'ajaxService/Edu04',
        data: {
            Edu02: vEdu02,
            Edu03: vEdu03,
            DType: vDType,
            SubDType: vSubDType
        },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#' + vID_Edu04).removeAttr("disabled")
                $('#' + vID_Edu04).empty();
                $.each(data, function (i, item) {
                    $('#' + vID_Edu04).append($('<option></option>').val(item.Key).text(item.Value));
                });
            }
            else {
                $('#' + vID_Edu04).prop("disabled", true);
                $('#' + vID_Edu04).empty();
                $('#' + vID_Edu04).append($('<option></option>').val("-1").text("-----"));
            }
        }
    });
}

//取得年級下拉選單資料
function GetGrade(vDType, vSubDType, vEdu02, vID_Grade) {
    $.ajax({
        url: ROOT + 'ajaxService/Grade',
        data: {
            Edu02: vEdu02,
            DType: vDType,
            SubDType: vSubDType
        },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#' + vID_Grade).removeAttr("disabled")
                $('#' + vID_Grade).empty();
                $.each(data, function (i, item) {
                    $('#' + vID_Grade).append($('<option></option>').val(item.Key).text(item.Value));
                });
            }
            else {
                $('#' + vID_Grade).prop("disabled", true);
                $('#' + vID_Grade).empty();
                $('#' + vID_Grade).append($('<option></option>').val("-1").text("-----"));
            }
        }
    });
}

//取得班級下拉選單資料
function GetEdu06(vDType, vSubDType, vEdu04, vGrade, vID_Edu06) {
    $.ajax({
        url: ROOT + 'ajaxService/Edu06',
        data: {
            Edu04: vEdu04,
            Grade: vGrade,
            DType: vDType,
            SubDType: vSubDType
        },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#' + vID_Edu06).removeAttr("disabled")
                $('#' + vID_Edu06).empty();
                $.each(data, function (i, item) {
                    $('#' + vID_Edu06).append($('<option></option>').val(item.Key).text(item.Value));
                });
            }
            else {
                $('#' + vID_Edu06).prop("disabled", true);
                $('#' + vID_Edu06).empty();
                $('#' + vID_Edu06).append($('<option></option>').val("-1").text("-----"));
            }
        }
    });
}

//取得學生下拉選單資料(取消不用)
//function GetStudent(vDType, vSubDType, vEdu06, vID_Student) {
//    $.ajax({
//        url: ROOT + 'ajaxService/Student',
//        data: {
//            Edu06: vEdu06,
//            DType: vDType,
//            SubDType: vSubDType
//        },
//        type: 'post',
//        cache: false,
//        async: false,
//        dataType: 'json',
//        success: function (data) {
//            if (data.length > 0) {
//                $('#' + vID_Student).removeAttr("disabled")
//                $('#' + vID_Student).empty();
//                $.each(data, function (i, item) {
//                    $('#' + vID_Student).append($('<option></option>').val(item.Key).text(item.Value));
//                });
//            }
//            else {
//                $('#' + vID_Student).prop("disabled", true);
//                $('#' + vID_Student).empty();
//                $('#' + vID_Student).append($('<option></option>').val("-1").text("-----"));
//            }
//        }
//    });
//}

//取得人事下拉選單資料
function GetEmpMain(vDType, vSubDType, vUnit, vID_EmpMain) {
    $.ajax({
        url: ROOT + 'ajaxService/EmpMain',
        data: {
            Unit: vUnit,
            DType: vDType,
            SubDType: vSubDType
        },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#' + vID_EmpMain).removeAttr("disabled")
                $('#' + vID_EmpMain).empty();
                $.each(data, function (i, item) {
                    $('#' + vID_EmpMain).append($('<option></option>').val(item.Key).text(item.Value));
                });
            }
            else {
                $('#' + vID_EmpMain).prop("disabled", true);
                $('#' + vID_EmpMain).empty();
                $('#' + vID_EmpMain).append($('<option></option>').val("-1").text("-----"));
            }
        }
    });
}