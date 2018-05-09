/// <reference path="typings/jquery/jquery.d.ts" />

function LoaderHtml(): string {
    return '<div class="wrapper">' +
                '<div class="circle circle-1 text-center"></div>' +
                '<div class="circle circle-1a"></div>' +
                '<div class="circle circle-2"> </div>' +
                '<div class="circle circle-3"> </div>' +
           '</div>';
}

$(function () {
    $.ajaxSetup({ cache: false });

    $(".card").click(function (e) {
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
        }).then(() => {
            if (this.attributes["event"].nodeValue == "del")
                $(".card-control").attr("disabled", "true");
        });

    });
})