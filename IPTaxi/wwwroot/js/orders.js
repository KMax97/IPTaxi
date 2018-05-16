function LoadOrders() {
    var req = new XMLHttpRequest();
    req.open("GET", "/api/orders1");
    req.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
            for (order of JSON.parse(this.responseText)) {
                AddOrderLocally(order);
            }
        }
    };
    req.send();
}

function EndOrderAdding() {
    AddOrder(document.getElementById("orderAddedTelephone").value
        , document.getElementById("orderAddedStartStreet").value
        , document.getElementById("orderAddedStartHouse").value
        , document.getElementById("orderAddedEndStreet").value
        , document.getElementById("orderAddedEndHouse").value);

    document.getElementById("orderAddedTelephone").value = "";
    document.getElementById("orderAddedStartStreet").value = "";
    document.getElementById("orderAddedStartHouse").value = "";
    document.getElementById("orderAddedEndStreet").value = "";
    document.getElementById("orderAddedEndHouse").value = "";
}

function AddOrder(telephone, startStreet, startHouse, endStreet, endHouse) {
    var req = new XMLHttpRequest();
    req.open("POST", "/api/orders1");
    req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    req.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 201) {
            AddOrderLocally(JSON.parse(this.responseText));
        }
    };
    req.send(JSON.stringify({telephone, startStreet, startHouse, endStreet, endHouse}));
}

function AddOrderLocally(order) {
    document.getElementById("orderListBody").innerHTML += "<tr id='order" + order.numberOfOrder + "'>" + "<td>"
        + escapeHtml(order.numberOfTelephone) + "</td>" + "<td class='orderStartAddress'>"
        + escapeHtml(order.startStreet) + ", " + order.numberOfStartHouse + "</td>" + "<td class='orderEndAddress'>"
        + escapeHtml(order.finalStreet) + ", " + order.numberOfFinalHouse + "</td>" + "<td>"
        + "<button class='btn btn-primary' onclick='StartOrderEditing(JSON.parse(this.parentNode.parentNode.getElementsByClassName(\"orderHiddenJson\")[0].innerText));'>Редактировать</button>"
        + "</td>" + "<td>"
        + "<button class='btn btn-danger' onclick='RemoveOrder(JSON.parse(this.parentNode.parentNode.getElementsByClassName(\"orderHiddenJson\")[0].innerText));'>Удалить</button>"
        + "</td>" + "<td style='display: none' class='orderHiddenJson'>" + escapeHtml(JSON.stringify(order))
        + "</td></tr>";
}

function StartOrderEditing(order) {
    document.getElementById("orderEditedTelephone").value = order.numberOfTelephone;
    document.getElementById("orderEditedStartStreet").value = order.startStreet;
    document.getElementById("orderEditedStartHouse").value = order.numberOfStartHouse;
    document.getElementById("orderEditedEndStreet").value = order.finalStreet;
    document.getElementById("orderEditedEndHouse").value = order.numberOfFinalHouse;
    document.getElementById("orderHiddenEditedJson").value = JSON.stringify(order);
}

function EndOrderEditing() {
    var order = JSON.parse(document.getElementById("orderHiddenEditedJson").value);
    order.startStreet = document.getElementById("orderEditedStartStreet").value;
    order.numberOfStartHouse = document.getElementById("orderEditedStartHouse").value;
    order.finalStreet = document.getElementById("orderEditedEndStreet").value;
    order.numberOfFinalHouse = document.getElementById("orderEditedEndHouse").value;

    document.getElementById("orderEditedTelephone").value = "";
    document.getElementById("orderEditedStartStreet").value = "";
    document.getElementById("orderEditedStartHouse").value = "";
    document.getElementById("orderEditedEndStreet").value = "";
    document.getElementById("orderEditedEndHouse").value = "";
    document.getElementById("orderHiddenEditedJson").value = "";

    UpdateOrder(order);
}

function UpdateOrder(order) {
    var req = new XMLHttpRequest();
    req.open("PUT", "/api/orders1/" + order.numberOfOrder);
    req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    req.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 204) {
            UpdateOrderLocally(order);
        }
    };
    req.send(JSON.stringify(order));
}

function UpdateOrderLocally(order) {
    var row = document.getElementById("order" + order.numberOfOrder);
    row.getElementsByClassName("orderHiddenJson")[0].innerText = JSON.stringify(order);
    row.getElementsByClassName("orderStartAddress")[0].innerText = order.startStreet + ", " + order.numberOfStartHouse;
    row.getElementsByClassName("orderEndAddress")[0].innerText = order.finalStreet + ", " + order.numberOfFinalHouse;
}

function RemoveOrder(order) {
    var req = new XMLHttpRequest();
    req.open("DELETE", "/api/orders1/" + order.numberOfOrder);
    req.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
            RemoveOrderLocally(order);
        }
    };
    req.send();
}

function RemoveOrderLocally(order) {
    var elem = document.getElementById("order" + order.numberOfOrder);
    elem.parentNode.removeChild(elem);
}

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}