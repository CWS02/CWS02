function PList_Init_1() {
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
function PListSet1(ButtonID) {
    var List1 = document.getElementById("REC_PListBox_1");
    var List2 = document.getElementById("REC_PListBox_2");

    if (ButtonID == 'Add_All_1') {
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
    if (ButtonID == 'Remove_All_1') {
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
    if (ButtonID == 'Add_Select_1') {
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
    if (ButtonID == 'Remove_Select_1') {
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


function getSelPList_1() {
    var List2 = document.getElementById("REC_PListBox_2");
    var sel_classlist = "";

    var temp_count = List2.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List2.options[i].value;
    }

    return sel_classlist;
}


//第二組
function PList_Init_2() {
    var List3 = document.getElementById("REC_PListBox_3");
    var List4 = document.getElementById("REC_PListBox_4");

    var temp_count1 = List3.options.length;
    var temp_count2 = List4.options.length;
    for (i = 0; i < temp_count1; i++) {
        List3.options.remove(0);
    }
    for (i = 0; i < temp_count2; i++) {
        List4.options.remove(0);
    }
}

//List項目設定
function PListSet2(ButtonID) {
    var List3 = document.getElementById("REC_PListBox_3");
    var List4 = document.getElementById("REC_PListBox_4");

    if (ButtonID == 'Add_All_2') {
        var temp_count = List3.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List3.options[i].text;
            newitem.value = List3.options[i].value;
            List4.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List3.options.remove(0);
        }
    }
    if (ButtonID == 'Remove_All_2') {
        var temp_count = List4.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List4.options[i].text;
            newitem.value = List4.options[i].value;
            List3.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List4.options.remove(0);
        }
    }
    if (ButtonID == 'Add_Select_2') {
        var temp_count = List3.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List3.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List3.options[i].text;
                newitem.value = List3.options[i].value;
                List4.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List3.options[i - temp_d].selected) {
                List3.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
    if (ButtonID == 'Remove_Select_2') {
        var temp_count = List4.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List4.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List4.options[i].text;
                newitem.value = List4.options[i].value;
                List3.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List4.options[i - temp_d].selected) {
                List4.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
}


function getSelPList_2() {
    var List4 = document.getElementById("REC_PListBox_4");
    var sel_classlist = "";

    var temp_count = List4.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List4.options[i].value;
    }

    return sel_classlist;
}

//第三組
function PList_Init_3() {
    var List5 = document.getElementById("REC_PListBox_5");
    var List6 = document.getElementById("REC_PListBox_6");

    var temp_count1 = List5.options.length;
    var temp_count2 = List6.options.length;
    for (i = 0; i < temp_count1; i++) {
        List5.options.remove(0);
    }
    for (i = 0; i < temp_count2; i++) {
        List6.options.remove(0);
    }
}

//List項目設定
function PListSet3(ButtonID) {
    var List5 = document.getElementById("REC_PListBox_5");
    var List6 = document.getElementById("REC_PListBox_6");

    if (ButtonID == 'Add_All_3') {
        var temp_count = List5.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List5.options[i].text;
            newitem.value = List5.options[i].value;
            List6.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List5.options.remove(0);
        }
    }
    if (ButtonID == 'Remove_All_3') {
        var temp_count = List6.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List6.options[i].text;
            newitem.value = List6.options[i].value;
            List5.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List6.options.remove(0);
        }
    }
    if (ButtonID == 'Add_Select_3') {
        var temp_count = List5.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List5.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List5.options[i].text;
                newitem.value = List5.options[i].value;
                List6.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List5.options[i - temp_d].selected) {
                List5.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
    if (ButtonID == 'Remove_Select_3') {
        var temp_count = List6.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List6.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List6.options[i].text;
                newitem.value = List6.options[i].value;
                List5.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List6.options[i - temp_d].selected) {
                List6.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
}


function getSelPList_3() {
    var List6 = document.getElementById("REC_PListBox_6");
    var sel_classlist = "";

    var temp_count = List6.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List6.options[i].value;
    }

    return sel_classlist;
}

//第四組
function PList_Init_4() {
    var List7 = document.getElementById("REC_PListBox_7");
    var List8 = document.getElementById("REC_PListBox_8");

    var temp_count1 = List7.options.length;
    var temp_count2 = List8.options.length;
    for (i = 0; i < temp_count1; i++) {
        List7.options.remove(0);
    }
    for (i = 0; i < temp_count2; i++) {
        List8.options.remove(0);
    }
}

//List項目設定
function PListSet4(ButtonID) {
    var List7 = document.getElementById("REC_PListBox_7");
    var List8 = document.getElementById("REC_PListBox_8");

    if (ButtonID == 'Add_All_4') {
        var temp_count = List7.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List7.options[i].text;
            newitem.value = List7.options[i].value;
            List8.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List7.options.remove(0);
        }
    }
    if (ButtonID == 'Remove_All_4') {
        var temp_count = List8.options.length;
        for (i = 0; i < temp_count; i++) {
            var newitem = document.createElement("option");
            newitem.text = List8.options[i].text;
            newitem.value = List8.options[i].value;
            List7.add(newitem);
        }
        for (i = 0; i < temp_count; i++) {
            List8.options.remove(0);
        }
    }
    if (ButtonID == 'Add_Select_4') {
        var temp_count = List7.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List7.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List7.options[i].text;
                newitem.value = List7.options[i].value;
                List8.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List7.options[i - temp_d].selected) {
                List7.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
    if (ButtonID == 'Remove_Select_4') {
        var temp_count = List8.options.length;
        var temp_d = 0;
        for (i = 0; i < temp_count; i++) {
            if (List8.options[i].selected) {
                var newitem = document.createElement("option");
                newitem.text = List8.options[i].text;
                newitem.value = List8.options[i].value;
                List7.add(newitem);
            }
        }
        for (i = 0; i < temp_count; i++) {
            if (List8.options[i - temp_d].selected) {
                List8.options.remove(i - temp_d);
                temp_d = temp_d + 1;
            }
        }
    }
}


function getSelPList_4() {
    var List8 = document.getElementById("REC_PListBox_8");
    var sel_classlist = "";

    var temp_count = List8.options.length;
    for (i = 0; i < temp_count; i++) {
        if (sel_classlist.length > 0) sel_classlist += ",";
        sel_classlist += List8.options[i].value;
    }

    return sel_classlist;
}
