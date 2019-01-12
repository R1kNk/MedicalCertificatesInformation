﻿var mainContainerId = '#main-content';

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

function sendRequest(url, method, replaceIntoId) {
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
//

function GetIndexHospitalRequest() {
    sendRequest('/Hospital/Index', "GET");
}

function GetDetailsHospitalRequest(id) {
    sendIdRequest('/Hospital/Details', id, "GET");
}

function GetCreateHospitalRequest() {
    sendRequest('/PhysicalEducation/Create', "GET");
}

function SendCreateHospitalRequest() {
    sendFormRequest('/Hospital/Create', '#createHospitalForm', 'POST');
};

function GetEditHospitalRequest(id) {
    sendIdRequest('/Hospital/Edit', id, "GET");
}

function SendEditHospitalRequest() {
    sendFormRequest('/Hospital/Edit', '#editHospitalForm', 'POST');
};

function GetDeleteHospitalRequest(id) {
    sendIdRequest('/Hospital/Delete', id, "GET");
}

function SendDeleteHospitalRequest() {
    sendFormRequest('/Hospital/Delete', '#deleteHospitalForm', 'POST');
};

//Physical education functions

function GetIndexPhysicalEducationRequest() {
    sendRequest('/PhysicalEducation/Index', "GET");
}

function GetDetailsPhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Details', id, "GET");
}

function GetCreatePhysicalEducationRequest() {
    sendRequest('/PhysicalEducation/Create', "GET");
}

function SendCreatePhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Create', '#createPhysicalEducationForm', 'POST');
}

function GetEditPhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Edit', id, "GET");
}

function SendEditPhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Edit', '#editPhysicalEducationForm', 'POST');
};

function GetDeletePhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Delete', id, "GET");
}

function SendDeletePhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Delete', '#deletePhysicalEducationForm', 'POST');
};

//Health group functions

function GetIndexHealthGroupRequest() {
    sendRequest('/HealthGroup/Index', "GET");
}

function GetDetailsHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Details', id, "GET");
}

function GetCreateHealthGroupRequest() {
    sendRequest('/HealthGroup/Create', "GET");
}

function SendCreateHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Create', '#createHealthGroupForm', 'POST');
}

function GetEditHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Edit', id, "GET");
}

function SendEditHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Edit', '#editHealthGroupForm', 'POST');
};

function GetDeleteHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Delete', id, "GET");
}

function SendDeleteHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Delete', '#deleteHealthGroupForm', 'POST');
};

//Medical certificates functions

function GetDetailsMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Details', id, "GET");
}

function GetCreateMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Create', id, "GET");
}

function SendCreateMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Create', '#createMedicalCertificateForm', 'POST');
}

function GetEditMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Edit', id, "GET");
}

function SendEditMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Edit', '#editMedicalCertificateForm', 'POST');
};

function GetDeleteMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Delete', id, "GET");
}

function SendDeleteMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Delete', '#deleteMedicalCertificateForm', 'POST');
};

//Student functions

function GetDetailsStudentRequest(id) {
    sendIdRequest('/Student/Details', id, "GET");
}

function GetCreateStudentRequest(id) {
    sendIdRequest('/Student/Create', id, "GET");
}

function SendCreateStudentRequest() {
    sendFormRequest('/Student/Create', '#createStudentForm', 'POST');
}

function GetEditStudentRequest(id) {
    sendIdRequest('/Student/Edit', id, "GET");
}

function SendEditStudentRequest() {
    sendFormRequest('/Student/Edit', '#editStudentForm', 'POST');
};

function GetDeleteStudentRequest(id) {
    sendIdRequest('/Student/Delete', id, "GET");
}

function SendDeleteStudentRequest() {
    sendFormRequest('/Student/Delete', '#deleteStudentForm', 'POST');
};

function GetMoveStudentRequest(id) {
    sendIdRequest('/Student/Move', id, "GET");
}

function SendMoveStudentRequest() {
    sendFormRequest('/Student/Delete', '#moveStudentForm', 'POST');
};