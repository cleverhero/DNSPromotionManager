/// <reference path="typings/jquery/jquery.d.ts" />
function DeleteFromBag(btn, id: string) {
    $.ajaxSetup({ cache: false });

    var href = "/Bag/DeleteItemFromBag?ItemId=" + id;
    console.log(href);

    $.get(href, function (data) {
        console.log(data);
    });

    $("tr#" + id).remove();
    return false;
}