$(function () {

    $(".MultiTypes").sortable({
        update: function (event, ui) {
            var properties = $(this).sortable("toArray");
            for (var x = 0; x < properties.length; x++) {
                $("#" + properties[x]).find(".sortId input[type=hidden]").val(x + 1);
            }
        }
    });

});