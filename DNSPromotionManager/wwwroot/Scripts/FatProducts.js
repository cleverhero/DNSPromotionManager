/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="./Loader.ts" />
var Characteristic = /** @class */ (function () {
    function Characteristic() {
    }
    return Characteristic;
}());
var Filter = /** @class */ (function () {
    function Filter() {
    }
    return Filter;
}());
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
    if (document.cookie == "") {
        var cookies = ["Bag=" + id];
        apply(cookies);
        return;
    }
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var parts = cookies[i].split("="), name = parts[0], value = parts[1];
        if (name == "Bag") {
            var ids = value.split("#");
            if (ids.indexOf(id) != -1)
                return;
            if (value == "")
                value = id;
            else
                value += "#" + id;
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
            var ids = value.split("#");
            console.log(ids);
            var IdInd = ids.indexOf(id);
            if (IdInd == -1)
                return;
            value = ids.reduce(function (ans, str, ind, arr) {
                if (ind != IdInd)
                    if (ans == "")
                        return str;
                    else
                        ans + "#" + str;
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
        Name: $('[name="Name"]')[0].value,
        Code: $('[name="Code"]')[0].value,
        MinPrice: $('[name="MinPrice"]')[0].value,
        MaxPrice: $('[name="MaxPrice"]')[0].value,
        Kinds: [],
        Characteristics: []
    };
    var kindCount = $('[name="kindpanel"]')[0]
        .attributes["kind-count"].value;
    for (var i = 0; i < Number(kindCount); i++) {
        var name = "Kinds[" + i + "]";
        var variant = $('[name="' + name + '"]')[0].value;
        var isSelected = $('[name="' + name + '"]')[0].checked;
        if (isSelected)
            filter.Kinds.push(variant);
    }
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
            var name = "Characteristics[" + i + "].Variants[" + j + "]";
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
    $("#table").html(LoaderHtml());
    $.ajax({
        type: "POST",
        url: "/FatProducts/Products",
        data: 'JSONFilter=' + JSON.stringify(filter)
    }).fail(function (errmsg) {
        alert(errmsg.statusText);
    }).done(function (data) {
        $("#table").html(data);
    });
}
//# sourceMappingURL=FatProducts.js.map