/// <reference path="typings/jquery/jquery.d.ts" />
$(function () {
    $.ajaxSetup({ cache: false });
    $(".card").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#modDialog').modal('show');
            $('#dialogContent').html(data);
        });
    });
});
window.onload = function () {
    $(".card-control").attr("disabled", "true");
};
//# sourceMappingURL=Cards.js.map