function selectView(view) {
    $('.display').not('#' + view + "Display").hide();

    $("#editCompanyTypeId option:first").attr('selected', 'selected');
    $('#editCompanyName').val('');
    $("#addCompanyTypeId option:first").attr('selected', 'selected');
    $('#addCompanyName').val('');

    $("*[id*='-ajax-link-target']")
            .each(function () {
                $(this).empty();
            });

    $('#' + view + "Display").show();
}


$(document).ready(function () {

    getAllCompanies();
    $('.back-to-companies').click(function() {
        getAllCompanies();
    });

    $('#add').click(function() {
        selectView("add");
    });

    $('#submitAdd').click(addCompanyOnSaveClick);

    $('#submitEdit').click(editCompanyOnSaveClick);
});
// gets all companies using ajax request
function getAllCompanies() {
    $.ajax({
        url: '/api/company',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            displayData(data);
            selectView("summary");
        }
    });
}

// display companies data
function displayData(companies) {
    $('#tableBody').empty();
    $.each(companies,
        function(index, company) {
            $('#tableBody').append("<tr><td>" +
                company.CompanyId +
                "</td><td> " +
                company.CompanyTypeName +
                "</td><td>" +
                company.CompanyName +
                "</td><td>" +
                company.NumberOfStuff +
                "</td><td><a href='javascript:void(0);' class='edit-item' onclick='editCompany(this);'>Редактировать</a></td>" +
                "<td><a href='javascript:void(0);' class='delete-item' onclick='deleteCompany(this);'>Удалить</a></td></tr>");
        });
}

function editCompany(el) {
    var id = $(el).closest('tr').children().first().text();
    getCompany(id);
    selectView("edit");
}

function deleteCompany(el) {
    var id = $(el).closest('tr').children().first().text();
    $.ajax({
        url: '/api/company/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result)
                getAllCompanies();
            else
                alert("Could not delete");
        }
    });
}

function editCompanyOnSaveClick(e) {
    e.preventDefault();
    // get actual company information
    var company = {
        CompanyId: $('#editCompanyId').val(),
        CompanyName: $('#editCompanyName').val(),
        CompanyTypeId: $('#editCompanyTypeId').val()
    };

    $.ajax({
        url: '/api/company/company',
        type: 'PUT',
        data: JSON.stringify(company),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result) {
                getAllCompanies();
            } else
                alert("Could not update company with id=" + company.CompanyId);
        }
    });
}

function addCompanyOnSaveClick(e) {
    e.preventDefault();
    // get actual company information
    var company = {
        CompanyName: $('#addCompanyName').val(),
        CompanyTypeId: $('#addCompanyTypeId').val()
    };
    $.ajax({
        url: '/api/company/',
        type: 'POST',
        data: JSON.stringify(company),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result)
                getAllCompanies();
            else
                alert("Could not add company '" + company.CompanyName + "'");
        }
    });
}

// get company data by specified company id and insert it into edit block
function getCompany(id) {

    $.ajax({
        url: '/api/company/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (company) {
            if (company != null) {
                $("#editCompanyId").val(company.CompanyId);
                $("#editCompanyName").val(company.CompanyName);
                $("#editCompanyTypeId").val(company.CompanyTypeId);
            }
            else
                alert("Could not get company information by id=" + id);
        }
    });
}



