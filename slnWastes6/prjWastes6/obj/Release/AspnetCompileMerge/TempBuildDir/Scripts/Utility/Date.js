//將日期時間格式轉換成單純日期格式
function GetDateValue(strDate) {
    if (strDate != "") {
        var indx = strDate.indexOf(" ");
        if (indx != -1) {
            strDate = strDate.substr(0, indx);
        }
    }

    return strDate;
}

/**
 * 取得萬年曆
 * @return
 */
function getDate(thisElement) {
    calendar(thisElement, 'yyyy/mm/dd', '/eHR');
}

//檢查輸入的日期格式是否正確(西元)
function IsDate_1(dstText) {
    data = dstText.match(/(\d{2})\/(\d{2})\/(\d{2})/);
    if (!data || !dstText) return false; else return true;
}

//檢查輸入的日期格式是否正確(西元)
function IsDate(str) {
    var re = new RegExp("^([0-9]{4})[./]{1}([0-9]{1,2})[./]{1}([0-9]{1,2})$");
    var strDataValue;
    var infoValidation = true;

    if ((strDataValue = re.exec(str)) != null) {
        var i;
        i = parseFloat(strDataValue[1]);
        if (i <= 0 || i > 9999) { // 年
            infoValidation = false;
        }

        i = parseFloat(strDataValue[2]);
        if (i <= 0 || i > 12) { // 月
            infoValidation = false;
        }
        i = parseFloat(strDataValue[3]);
        if (i <= 0 || i > 31) { // 日
            infoValidation = false;
        }
    } else {
        infoValidation = false;
    }

    return infoValidation;
}