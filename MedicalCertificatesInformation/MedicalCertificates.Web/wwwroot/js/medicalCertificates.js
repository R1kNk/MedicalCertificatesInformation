var mainContainerId = '#main-content';

function sendFormRequest(url, formId, method, replaceIntoId, promise, parameter) {
    var data = $(formId).serialize();

    console.log(data);
    $.ajax({
        type: method,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            SetHtml(replaceIntoId, data);
            if(promise!=undefined){
                if(parameter!=undefined)
                promise(parameter);
                else
                promise();
            }
        },
        error: function (data) {
            console.log("error") 
               }
    });
}

function sendIdRequest(url, id, method, replaceIntoId, promise, parameter) {
    url = url + '?id=' + id;
        $.ajax({
            type: method,
            cache: false,
            url: url,
            success: function (data) {
                SetHtml(replaceIntoId, data);
                if(promise!=undefined){
                    if(parameter!=undefined)
                    promise(parameter);
                    else
                    promise();
                }
            },
            error: function (data) {
                console.log("error")
            }
        });
}

function sendRequest(url, method, replaceIntoId, promise, parameter) {
    $.ajax({
        type: method,
        cache: false,
        url: url,
        success: function (data) {
            SetHtml(replaceIntoId, data);
            if(promise!=undefined){
                if(parameter!=undefined)
                promise(parameter);
                else
                promise();
            }
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
    sendRequest('/PhysicalEducation/Create', "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateHospitalRequest() {
    sendFormRequest('/Hospital/Create', '#createHospitalForm', 'POST', "#formModal", GetIndexHospitalRequest);
};

function GetEditHospitalRequest(id) {
    sendIdRequest('/Hospital/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditHospitalRequest() {
    sendFormRequest('/Hospital/Edit', '#editHospitalForm', 'POST', "#formModal", GetIndexHospitalRequest);
};

function GetDeleteHospitalRequest(id) {
    sendIdRequest('/Hospital/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteHospitalRequest() {
    sendFormRequest('/Hospital/Delete', '#deleteHospitalForm', 'POST', "#formModal", GetIndexHospitalRequest);
};

//Physical education functions

function GetIndexPhysicalEducationRequest() {
    sendRequest('/PhysicalEducation/Index', "GET");
}

function GetDetailsPhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Details', id, "GET");
}

function GetCreatePhysicalEducationRequest() {
    sendRequest('/PhysicalEducation/Create', "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreatePhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Create', '#createPhysicalEducationForm', 'POST', "#formModal", GetIndexPhysicalEducationRequest);
}

function GetEditPhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditPhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Edit', '#editPhysicalEducationForm', 'POST', "#formModal", GetIndexPhysicalEducationRequest);
};

function GetDeletePhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeletePhysicalEducationRequest() {
    sendFormRequest('/PhysicalEducation/Delete', '#deletePhysicalEducationForm', 'POST', "#formModal", GetIndexPhysicalEducationRequest);
};

//Health group functions

function GetIndexHealthGroupRequest() {
    sendRequest('/HealthGroup/Index', "GET");
}

function GetDetailsHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Details', id, "GET");
}

function GetCreateHealthGroupRequest() {
    sendRequest('/HealthGroup/Create', "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Create', '#createHealthGroupForm', 'POST', "#formModal", GetIndexHealthGroupRequest);
}

function GetEditHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Edit', '#editHealthGroupForm', 'POST', "#formModal", GetIndexHealthGroupRequest);
};

function GetDeleteHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Delete', id, "GET","#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteHealthGroupRequest() {
    sendFormRequest('/HealthGroup/Delete', '#deleteHealthGroupForm', 'POST', "#formModal", GetIndexHealthGroupRequest);
};

//Medical certificates functions

function GetDetailsMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Details', id, "GET");
}

function GetCreateMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Create', id, "GET", "#formModal", BindDateTimePickers);
    $('#formModal').modal('toggle');
}

function SendCreateMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Create', '#createMedicalCertificateForm', 'POST', "#formModal", BindDateTimePickers);
}

function GetEditMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Edit', id, "GET", "#formModal", BindDateTimePickers);
    $('#formModal').modal('toggle');
}

function SendEditMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Edit', '#editMedicalCertificateForm', 'POST', "#formModal", BindDateTimePickers);
};

function GetDeleteMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteMedicalCertificateRequest() {
    sendFormRequest('/MedicalCertificate/Delete', '#deleteMedicalCertificateForm', 'POST', "#formModal");
};

//Student functions

function GetDetailsStudentRequest(id) {
    sendIdRequest('/Student/Details', id, "GET");
}

function GetCreateStudentRequest(id) {
    sendIdRequest('/Student/Create', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateStudentRequest() {
    sendFormRequest('/Student/Create', '#createStudentForm', 'POST', "#formModal");
}

function GetEditStudentRequest(id) {
    sendIdRequest('/Student/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditStudentRequest() {
    sendFormRequest('/Student/Edit', '#editStudentForm', 'POST', "#formModal");
};

function GetDeleteStudentRequest(id) {
    sendIdRequest('/Student/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteStudentRequest() {
    sendFormRequest('/Student/Delete', '#deleteStudentForm', 'POST', "#formModal");
};

function GetMoveStudentRequest(id) {
    sendIdRequest('/Student/Move', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendMoveStudentRequest() {
    sendFormRequest('/Student/Move', '#moveStudentForm', 'POST', "#formModal");
};

//Group functions

function GetDetailsGroupRequest(id) {
    sendIdRequest('/Group/Details', id, "GET");
}

function GetCreateGroupRequest(id) {
    sendIdRequest('/Group/Create', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateGroupRequest() {
    sendFormRequest('/Group/Create', '#createGroupForm', 'POST', "#formModal");
}

function GetEditGroupRequest(id) {
    sendIdRequest('/Group/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditGroupRequest() {
    sendFormRequest('/Group/Edit', '#editGroupForm', 'POST',"#formModal");
};

function GetDeleteGroupRequest(id) {
    sendIdRequest('/Group/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteGroupRequest() {
    sendFormRequest('/Group/Delete', '#deleteGroupForm', 'POST', "#formModal");
};

//Course functions

function GetDetailsCourseRequest(id) {
    sendIdRequest('/Course/Details', id, "GET");
}

function GetCreateCourseRequest(id) {
    sendIdRequest('/Course/Create', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateCourseRequest() {
    sendFormRequest('/Course/Create', '#createCourseForm', 'POST', "#formModal");
}

function GetEditCourseRequest(id) {
    sendIdRequest('/Course/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditCourseRequest() {
    sendFormRequest('/Course/Edit', '#editCourseForm', 'POST', "#formModal");
};

function GetDeleteCourseRequest(id) {
    sendIdRequest('/Course/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteCourseRequest() {
    sendFormRequest('/Course/Delete', '#deleteCourseForm', 'POST', "#formModal");
};

//Department functions

function GetDetailsDepartmentRequest(id) {
    sendIdRequest('/Department/Details', id, "GET");
}

function GetCreateDepartmentRequest(id) {
    sendIdRequest('/Department/Create', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateDepartmentRequest() {
    sendFormRequest('/Department/Create', '#createDepartmentForm', 'POST', "#formModal");
}

function GetEditDepartmentRequest(id) {
    sendIdRequest('/Department/Edit', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendEditDepartmentRequest() {
    sendFormRequest('/Department/Edit', '#editDepartmentForm', 'POST', "#formModal");
};

function GetDeleteDepartmentRequest(id) {
    sendIdRequest('/Department/Delete', id, "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendDeleteDepartmentequest() {
    sendFormRequest('/Department/Delete', '#deleteDepartmentForm', 'POST', "#formModal");
};

//Admin funcs

function GetCreateAccountRequest() {
    sendRequest('/Admin/Register', "GET", "#formModal");
    $('#formModal').modal('toggle');
}

function SendCreateAccountRequest() {
    sendFormRequest('/Admin/Register', '#registerAccountForm', 'POST', "#formModal");
}

//Other

function LoadTree() {
    
}

function BindDateTimePickers() {
    var bindDatePicker = function () {
        $(".date").datetimepicker({
            format: 'YYYY.MM.DD',
            locale: 'ru',
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        }).find('input:first').on("blur", function () {
            // check if the date is correct. We can accept dd-mm-yyyy and yyyy-mm-dd.
            // update the format if it's yyyy-mm-dd
            var date = parseDate($(this).val());

            if (!isValidDate(date)) {
                //create date based on momentjs (we have that)
                date = moment().format('YYYY.MM.DD');
            }

            $(this).val(date);
        });
    }

    var bindradioButtons = function() {
        $("#IsUsingTermNo").prop("checked", true);
        $("#IsUsingTermYes").prop("checked", false);

        $('input[type=radio][name^=IsUsingTerm]').change(function() {
            if (this.value === 'true'){
                $('#finishDateBlock').hide();
                $('#certificateTermBlock').show();
            } 
            else{
                $('#certificateTermBlock').hide();
                $('#finishDateBlock').show();
            }
        });
    }

    var isValidDate = function (value, format) {
        format = format || false;
        // lets parse the date to the best of our knowledge
        if (format) {
            value = parseDate(value);
        }

        var timestamp = Date.parse(value);

        return isNaN(timestamp) == false;
    }

    var parseDate = function (value) {
        var m = value.match(/^(\d{1,2})(\/|-)?(\d{1,2})(\/|-)?(\d{4})$/);
        if (m)
            value = m[5] + '-' + ("00" + m[3]).slice(-2) + '-' + ("00" + m[1]).slice(-2);

        return value;
    }

    bindDatePicker();
    bindradioButtons();
}