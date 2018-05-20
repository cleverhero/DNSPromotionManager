/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="./Loader.ts" />

class Characteristic {
    Id: String;
    Variants: Array<String>;
}

class Filter {
    Name: String;
    Code: String;
    MinPrice: String;
    MaxPrice: String;
    Kinds: Array<String>;
    Characteristics: Array<Characteristic>;
}

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
    if (document.cookie == "") {
        var cookies = ["Bag=" + id];
        apply(cookies);
        return;
    }

    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {

        var parts = cookies[i].split("="),
            name = parts[0],
            value = parts[1];
        if (name == "Bag") {
            var ids = value.split("#");
            if (ids.indexOf(id) != -1) return;

            if (value == "") value = id;
            else value += "#" + id;

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

            var ids: string[] = value.split("#");
            console.log(ids);
            var IdInd = ids.indexOf(id);
            if (IdInd == -1) return;

            value = ids.reduce((ans, str, ind, arr): string => {
                if (ind != IdInd)
                    if (ans == "") return str;
                    else ans + "#" + str;
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
    var filter: Filter = {
        Name: ($('[name="Name"]')[0] as HTMLInputElement).value,
        Code: ($('[name="Code"]')[0] as HTMLInputElement).value,
        MinPrice: ($('[name="MinPrice"]')[0] as HTMLInputElement).value,
        MaxPrice: ($('[name="MaxPrice"]')[0] as HTMLInputElement).value,
        Kinds: [],
        Characteristics: []
    };


    var kindCount = $('[name="kindpanel"]')[0]
        .attributes["kind-count"].value;

    for (var i = 0; i < Number(kindCount); i++) {
        var name = "Kinds[" + i + "]";
        var variant = ($('[name="' + name + '"]')[0] as HTMLInputElement).value;
        var isSelected = ($('[name="' + name + '"]')[0] as HTMLInputElement).checked;

        if (isSelected) filter.Kinds.push(variant);
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
            var variant = ($('[name="' + name + '"]')[0] as HTMLInputElement).value;
            var isSelected = ($('[name="' + name + '"]')[0] as HTMLInputElement).checked;

            if (isSelected) variants.push(variant);
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
