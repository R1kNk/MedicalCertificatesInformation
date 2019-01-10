var mainContainerId = '#main-content';

function sendFormRequest(url, formId, method, replaceIntoId) {
    var data = $(formId).serialize();

    console.log(data);
    $.ajax({
        type: method,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            SetHtml(replaceIntoId, data);
        },
        error: function (data) {
            $('#main-content').replaceWith(data);
        }
    });
}

function sendIdRequest(url, id, method, replaceIntoId) {
    url = url + '?id=' + id;
        $.ajax({
            type: method,
            cache: false,
            url: url,
            success: function (data) {
                SetHtml(replaceIntoId, data);
            },
            error: function (data) {
                console.log("error")
            }
        });
}

function SetHtml(containerId, data) {
    if (containerId == undefined)
        containerId = mainContainerId;
    if (data != undefined) {
        $(containerId).html(data);
    }
}

//Hospital functions

function SendCreateHospitalRequest() {
    sendFormRequest('/Hospital/Create', '#createHospitalForm', 'POST');
};

function SendEditHospitalRequest() {
    sendFormRequest('/Hospital/Edit', '#editHospitalForm', 'POST');
};

function SendDeleteHospitalRequest(id) {
    sendIdRequest('/Hospital/Delete', id, 'POST');
};

//Physical education functions