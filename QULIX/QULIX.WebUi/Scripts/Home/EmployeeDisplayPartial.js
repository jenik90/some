$(function () {

    var deleteBtn;

    $('#dialog').dialog({
        buttons: [{
            text: "OK", click: function () {
                $(this).dialog("close");
                deleteBtn.closest('form').submit();
                deleteBtn = null;
                //window.location.href = $('#delete-btn').attr("href");
            }
        },
        {
            text: "Отмена", click: function () {
                $(this).dialog("close");
            }
        }]
        , modal: true, autoOpen: false, resizable: false
    });

    $('#delete-btn').click(function (e) {
        e.preventDefault();
        $('#dialog').dialog("open");
        deleteBtn = $(this);
    });

})