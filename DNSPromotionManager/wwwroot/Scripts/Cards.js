/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="./Loader.ts" />
$(function () {
    $.ajaxSetup({ cache: false });
    $(".card").click(function (e) {
        var _this = this;
        e.preventDefault();
        switch (this.attributes["event"].nodeValue) {
            case "del": {
                document.getElementById("CardTitle").textContent = "Удалить";
                break;
            }
            case "create": {
                document.getElementById("CardTitle").textContent = "Создать";
                break;
            }
            case "edit": {
                document.getElementById("CardTitle").textContent = "Редактировать";
                break;
            }
        }
        $('#modDialog').modal('show');
        $('#dialogContent').html(LoaderHtml());
        console.log(this.href);
        $.get(this.href, function (data) {
            console.log(data);
            $('#dialogContent').html(data);
        }).then(function () {
            if (_this.attributes["event"].nodeValue == "del")
                $(".card-control").attr("disabled", "true");
        });
    });
});
//# sourceMappingURL=Cards.js.map