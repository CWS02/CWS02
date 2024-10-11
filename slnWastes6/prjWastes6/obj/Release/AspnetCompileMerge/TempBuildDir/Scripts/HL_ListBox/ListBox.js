function PList_Init() {
    var List1 = document.getElementById("REC_PListBox_1");
    var List2 = document.getElementById("REC_PListBox_2");

    var temp_count1 = List1.options.length;
    var temp_count2 = List2.options.length;
    for (i = 0; i < temp_count1; i++) {
        List1.options.remove(0);
    }
    for (i = 0; i < temp_count2; i++) {
        List2.options.remove(0);
    }
}

//List項目設定
function PListSet(ButtonID) {
    var List1 = document.getElementById("REC_PListBox_1");
    var List2 = document.getElementById("REC_PListBox_2");

    if (ButtonID == 'Add_All') {
        var temp_count = List1.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List1.options[i].text;
            newitem.value = List1.options[i].value;
            List2.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List1.options.remove(0);
        }
    }
    if (ButtonID == 'Remove_All') {
        var temp_count = List2.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List2.options[i].text;
            newitem.value = List2.options[i].value;
            List1.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List2.options.remove(0);
        }
    }
    if (ButtonID == 'Add_Select') {
        var temp_count = List1.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List1.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List1.options[i].text;
                newitem.value = List1.options[i].value;
                List2.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List1.options[i - temp_d].selected) {
                List1.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
    if (ButtonID == 'Remove_Select') {
        var temp_count = List2.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List2.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List2.options[i].text;
                newitem.value = List2.options[i].value;
                List1.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List2.options[i - temp_d].selected) {
                List2.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
}

function getSelPList() {
    var List2 = document.getElementById("REC_PListBox_2");
    var sel_classlist = "";

    var temp_count = List2.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List2.options[i].value;
    }

    return sel_classlist;
}

//修改用
function PList_Init_Modify() {
    var List1 = document.getElementById("REC_PListBox_Modify_1");
    var List2 = document.getElementById("REC_PListBox_Modify_2");

    var temp_count1 = List1.options.length;
    var temp_count2 = List2.options.length;
    for (i = 0; i < temp_count1; i++) {
        List1.options.remove(0);
    }
    for (i = 0; i < temp_count2; i++) {
        List2.options.remove(0);
    }
}

function getSelPList_Modify() {
    var List2 = document.getElementById("REC_PListBox_Modify_2");
    var sel_classlist = "";

    var temp_count = List2.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List2.options[i].value;
    }

    return sel_classlist;
}