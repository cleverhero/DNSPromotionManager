/// <reference path="typings/jquery/jquery.d.ts" />
function OutBag() {
    return "<span class='glyphicon glyphicon-plus-sign'></span>\n" +
        "Добавить в корзину";
}
function InBag() {
    return "<span class='glyphicon glyphicon-minus-sign'></span>\n" +
        "Убрать из корзины";
}
function AddItem(btn, id) {
    $.ajaxSetup({ cache: false });
    var href = "/Bag/AddItemInBag?ItemId=" + id;
    console.log(href);
    $.get(href, function (data) {
        console.log(data);
        if (data)
            Append(id);
    });
    btn.innerHTML = InBag();
    btn.attributes["class"].textContent = "btn btn-danger btn-sm";
    btn.onclick = function () { return DeleteItem(btn, id); };
}
function apply(cookies) {
    var res = cookies.join(";");
    document.cookie = res;
    console.log(res);
}
function Append(id) {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var parts = cookies[i].split("="), name = parts[0], value = parts[1];
        if (name == "Bag") {
            var ids = value.split(" ");
            if (ids.indexOf(id) != -1)
                return;
            if (value == "")
                value = id;
            else
                value = value += " " + id;
            cookies[i] = name + "=" + value;
            apply(cookies);
            return;
        }
    }
    cookies.push("Bag=" + id);
    apply(cookies);
}
function DeleteItem(btn, id) {
    $.ajaxSetup({ cache: false });
    var href = "/Bag/DeleteItemFromBag?ItemId=" + id;
    console.log(href);
    $.get(href, function (data) {
        console.log(data);
        if (data)
            Remove(id);
    });
    btn.innerHTML = OutBag();
    btn.attributes["class"].textContent = "btn btn-success btn-sm";
    btn.onclick = function () { return AddItem(btn, id); };
    return false;
}
function Remove(id) {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var parts = cookies[i].split("="), name = parts[0], value = parts[1];
        if (name == "Bag") {
            if (value == "")
                return;
            var ids = value.split(" ");
            console.log(ids);
            var IdInd = ids.indexOf(id);
            if (IdInd == -1)
                return;
            value = ids.reduce(function (ans, str, ind, arr) {
                if (ind != IdInd)
                    return ans + " " + str;
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
function FilterController() {
    var filter = {
        Name: $('[name="Filter.Name"]')[0].value,
        Code: $('[name="Filter.Code"]')[0].value,
        MinPrice: $('[name="Filter.MinPrice"]')[0].value,
        MaxPrice: $('[name="Filter.MaxPrice"]')[0].value,
        Characteristics: []
    };
    var charactericCount = $('[name="filterpanel"]')[0]
        .attributes["characteric-count"].value;
    for (var i = 0; i < Number(charactericCount); i++) {
        var variants = [];
        console.log('[name="characteristicpanel' + i.toString() + '"]');
        var variantsCount = $('[name="characteristicpanel'
            + i.toString() + '"]')[0]
            .attributes["variant-count"].value;
        var characteristic = $('[name="characteristicpanel'
            + i.toString() + '"]')[0]
            .attributes["char-id"].value;
        for (var j = 0; j < Number(variantsCount); j++) {
            var name = "Filter.Characteristics[" + i + "].Variants[" + j + "].IsSelected";
            var variant = $('[name="' + name + '"]')[0].value;
            var isSelected = $('[name="' + name + '"]')[0].checked;
            if (isSelected)
                variants.push(variant);
        }
        if (variants.length > 0)
            filter.Characteristics.push({
                Id: characteristic,
                Variants: variants
            });
    }
    $.ajax({
        type: "POST",
        url: "/FatProducts/Products",
        data: 'JSONFilter=' + JSON.stringify(filter),
        success: function (data) { document.body.innerHTML = data; },
        error: function (errmsg) { alert(errmsg); }
    });
}
var href = "/Bag/GetProducts";
console.log(href);
$.get(href, function (ids) {
    for (var _i = 0, ids_1 = ids; _i < ids_1.length; _i++) {
        var id = ids_1[_i];
        var btn = $("#" + id)[0];
        btn.innerHTML = InBag();
        btn.attributes["class"].textContent = "btn btn-danger btn-sm";
        btn.onclick = function () { return DeleteItem(btn, id); };
    }
});
//# sourceMappingURL=FatProducts.js.map