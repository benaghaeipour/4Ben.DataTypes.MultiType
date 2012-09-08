//$(function () {

//    var mTypeNew = $(".New");
//    var mTypeNewFields = mTypeNew.find(".newFields");
//    var mTypeNewFieldsHeight = mTypeNewFields.height();
//    var hidden = true;

//    mTypeNewFields.hide();
//    mTypeNew.find("h3").click(function () {
//        if (hidden) {
//            mTypeNew.find(".header").height(mTypeNewFieldsHeight + 33);
//            hidden = false;
//        } else {
//            mTypeNew.find(".header").height(22);
//            hidden = true;
//        }
//        mTypeNewFields.toggle();
//    });

//    $(".Edit .property").each(function () {
//        var edit = $(this);
//        var editFields = edit.find(".editFields");
//        var editFieldsHeight = editFields.height();
//        var hidden = true;

//        editFields.hide();
//        edit.find("h3").click(function () {
//            if (hidden) {
//                edit.find(".header").height(editFieldsHeight + 33);
//                hidden = false;
//            } else {
//                edit.find(".header").height(22);
//                hidden = true;
//            }
//            editFields.toggle();
//        });
//    });

//    $(".Edit").sortable({
//        update: function (event, ui) {
//            var properties = $(this).sortable("toArray");
//            for (var x = 0; x < properties.length; x++) {
//                $("#" + properties[x]).find(".sortId input[type=hidden]").val(x + 1);
//            }
//        }
//    });
//});