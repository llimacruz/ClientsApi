var urlApi = "http://localhost:50083/api/clients";

function toggleListEdit() {
    if ($("#divEdit").css('display') == 'none')
    {
        $("#divEdit").show();
        $("#clientsList").hide();
        $("#boxList").hide();
    }
    else
    {
        $("#divEdit").hide();
        $("#clientsList").show();
        $("#boxList").show();
    }
}

function cancel() {
    toggleListEdit();
}

function getClients() {
    $.ajax({
        type: "GET",
        url: urlApi
    }).success(function (data) {
        $("#clientsList").empty();
        $.each(data, function (a, b) {
            var editLink = "<a href='#' onclick='edit(" + b.CPF + ")'>Edit</a>";
            var deleteLink = "<a href='#' onclick='deleteClient(" + b.CPF + ")'>Delete</a>";
            $("#clientsList").append("<div>" + editLink + " | " +  deleteLink + " | " + b.CPF + " - " + b.Name + "</div>");
        });
    });
}

function edit(id) {
    toggleListEdit();

    $.ajax({
        type: "GET",
        url: urlApi + "/" + id
    }).success(function (data) {
        $("#txtCPF").val(id);
        $("#txtCPF").prop('disabled', true);
        $("#txtName").val(data.Name);
        $("#txtEmail").val(data.Email);
        $("#txtMaritalStatus").val(data.MaritalStatus);
        $("#txtPhoneNumbers").val(data.PhoneNumbersList);
        $("#txtStreet").val(data.Street);
        $("#txtCity").val(data.City);
        $("#txtState").val(data.State);
        $("#txtCountry").val(data.Country);
        $("#txtZip").val(data.Zip);
    });
}

function newClient() {
    toggleListEdit();

    $("#txtCPF").val('');
    $("#txtCPF").prop('disabled', false);
    $("#txtName").val('');
    $("#txtEmail").val('');
    $("#txtMaritalStatus").val('');
    $("#txtPhoneNumbers").val('');
    $("#txtStreet").val('');
    $("#txtCity").val('');
    $("#txtState").val('');
    $("#txtCountry").val('');
    $("#txtZip").val('');
}

function save() {
    var cpf = $("#txtCPF").val();
    var name = $("#txtName").val();
    var email = $("#txtEmail").val();
    var maritalStatus = $("#txtMaritalStatus").val();
    var phoneNumbers = $("#txtPhoneNumbers").val();
    var street = $("#txtStreet").val();
    var city = $("#txtCity").val();
    var state = $("#txtState").val();
    var country = $("#txtCountry").val();
    var zip = $("#txtZip").val();

    var numbers = phoneNumbers.split(',');

    var client = { 
        CPF: cpf, 
        Name: name, 
        Email: email, 
        MaritalStatus: maritalStatus,
        PhoneNumbersList: numbers,
        Street: street, 
        City: city,
        State: state, 
        Country: country, 
        Zip: zip 
    };

    var type = ($("#txtCPF").prop("disabled")) ? "PUT" : "POST";
    $.ajax({
        type: type,
        data: JSON.stringify(client),
        url: urlApi,
        contentType: "application/json",
        dataType: "json"
    }).success(function (res) {
        alert("saved!");
        getClients();
        toggleListEdit();
    }).error(function (err){
        alert("not saved :(");
    });
}

function deleteClient(cpf) {
    $.ajax({
        type: "DELETE",
        url: urlApi + "/" + cpf
    }).success(function (res) {
        alert("deleted!");
        getClients();
    });
}
