/// <reference path="typings/jquery/jquery.d.ts" />
function OutBag(): string {
    return "<span class='glyphicon glyphicon-plus-sign'></span>\n" +
           "Добавить в корзину";
}

function InBag(): string {
    return "<span class='glyphicon glyphicon-minus-sign'></span>\n" +
           "Убрать из корзины";
}

function AddItem(btn, id: string) {
    $.ajaxSetup({ cache: false });

    var href = "/Bag/AddItemInBag?ItemId=" + id;
    console.log(href);

    $.get(href, function (data) {
        console.log(data as boolean);
        if (data as boolean) Append(id);
    });

    btn.innerHTML = InBag();
    btn.attributes["class"].textContent = "btn btn-danger btn-sm";
    btn.onclick = () => DeleteItem(btn, id);

}

function apply(cookies: string[]) {
    var res = cookies.join(";");
    document.cookie = res;
    console.log(res)
}

function Append(id: string) {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {

        var parts = cookies[i].split("="),
            name = parts[0],
            value = parts[1];
        if (name == "Bag") {
            var ids = value.split(" ");
            if (ids.indexOf(id) != -1) return;

            if (value == "") value = id;
            else value = value += " " + id;

            cookies[i] = name + "=" + value;
            apply(cookies);
            return;
        }
    }
    cookies.push("Bag=" + id);
    apply(cookies);
}

function DeleteItem(btn, id: string) {
    $.ajaxSetup({ cache: false });

    var href = "/Bag/DeleteItemFromBag?ItemId=" + id;
    console.log(href);

    $.get(href, function (data) {
        console.log(data as boolean);
        if (data as boolean) Remove(id);
    });

    btn.innerHTML = OutBag();
    btn.attributes["class"].textContent = "btn btn-success btn-sm";
    btn.onclick = () => AddItem(btn, id);

    return false;
}

function Remove(id: string) {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {

        var parts = cookies[i].split("="),
            name = parts[0],
            value = parts[1];
        if (name == "Bag") {
            if (value == "") return;

            var ids: string[] = value.split(" ");
            console.log(ids);
            var IdInd = ids.indexOf(id);
            if (IdInd == -1) return;

            value = ids.reduce((ans, str, ind, arr): string => {
                if (ind != IdInd) return ans + " " + str;
                return ans;
            }, "");

            console.log(value);

            cookies[i] = name + "=" + value;
            apply(cookies);
            return;
        }
    }
    return;
}

var href = "/Bag/GetProducts";
console.log(href);


$.get(href, function (ids) {
    for (var id of ids) {
        var btn = $("#" + id)[0];

        btn.innerHTML = InBag();
        btn.attributes["class"].textContent = "btn btn-danger btn-sm";
        btn.onclick = () => DeleteItem(btn, id);
    }
});