﻿var mainContainerId = '#main-content';

function sendFormRequest(url, formId, method, replaceIntoId, promiseArray, parameterArray) {
    var data = $(formId).serialize();
    $.ajax({
        type: method,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            SetHtml(replaceIntoId, data, promiseArray, parameterArray);

        },
        error: function (data) {
            console.log(data)
        }
    });
}

function SetButtonsAvailability(type, role) {

    if (role !== 'admin') {
        DisableCreateBtn();
        DisableEditBtn();
        DisableDeleteBtn();
        DisableMoveBtn();
    }

    switch (type) {
        case 'department':
            if (role === 'admin') {
                EnableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            break;
        case 'course':
            if (role === 'admin') {
                EnableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            break;
        case 'group':
            if (role === 'admin') {
                EnableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            break;
        case 'student':
            if (role === 'admin') {
                EnableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                EnableMoveBtn();
            }
            break;
        case 'certificate':
            if (role === 'admin') {
                DisableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            break;
    }
}

function sendIdRequest(url, id, method, replaceIntoId, promiseArray, parameterArray) {
    url = url + '?id=' + id;
    $.ajax({
        type: method,
        cache: false,
        url: url,
        success: function (data) {
            SetHtml(replaceIntoId, data, promiseArray, parameterArray);
        },
        error: function (data) {
            console.log("error")
        }
    });
}

function sendRequest(url, method, replaceIntoId, promiseArray, parameterArray) {
    $.ajax({
        type: method,
        cache: false,
        url: url,
        success: function (data) {
            SetHtml(replaceIntoId, data, promiseArray, parameterArray);

        },
        error: function (data) {
            console.log("error")
        }
    });
}

async function SetHtml(containerId, data, promiseArray, parametersArray) {
    if (containerId == undefined)
        containerId = mainContainerId;
    if (data != undefined) {
        $(containerId).html(data);
        if (promiseArray != undefined) {
            if (parametersArray != undefined) {
                for (var i = 0; i < promiseArray.length; i++) {
                    if (parametersArray[i] != undefined)
                        promiseArray[i](parametersArray[i]);
                    else {
                        promiseArray[i]();
                    }
                }
            }
            else {
                for (var i = 0; i < promiseArray.length; i++) {
                    promiseArray[i]();
                }
            }
        }
    }
    return 1;
}

//Physical education functions

function GetIndexPhysicalEducationRequest() {
    sendRequest('/PhysicalEducation/Index', "GET");
}

function GetDetailsPhysicalEducationRequest(id) {
    sendIdRequest('/PhysicalEducation/Details', id, "GET");
}

function GetCreatePhysicalEducationRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/PhysicalEducation/Create', "GET", "#formModal", funcs);
}

function SendCreatePhysicalEducationRequest() {
    var funcs = new Array();
    funcs.push(GetIndexPhysicalEducationRequest);
    sendFormRequest('/PhysicalEducation/Create', '#createPhysicalEducationForm', 'POST', "#formModal", funcs);
}

function GetEditPhysicalEducationRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/PhysicalEducation/Edit', id, "GET", "#formModal", funcs);
}

function SendEditPhysicalEducationRequest() {
    var funcs = new Array();
    funcs.push(GetIndexPhysicalEducationRequest);
    sendFormRequest('/PhysicalEducation/Edit', '#editPhysicalEducationForm', 'POST', "#formModal", funcs);
};

function GetDeletePhysicalEducationRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/PhysicalEducation/Delete', id, "GET", "#formModal", funcs);
}

function SendDeletePhysicalEducationRequest() {
    var funcs = new Array();
    funcs.push(GetIndexPhysicalEducationRequest);
    sendFormRequest('/PhysicalEducation/Delete', '#deletePhysicalEducationForm', 'POST', "#formModal", funcs);
};

//Health group functions

function GetIndexHealthGroupRequest() {
    sendRequest('/HealthGroup/Index', "GET");
}

function GetDetailsHealthGroupRequest(id) {
    sendIdRequest('/HealthGroup/Details', id, "GET");
}

function GetCreateHealthGroupRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/HealthGroup/Create', "GET", "#formModal", funcs);
}

function SendCreateHealthGroupRequest() {
    var funcs = new Array();
    funcs.push(GetIndexHealthGroupRequest);
    sendFormRequest('/HealthGroup/Create', '#createHealthGroupForm', 'POST', "#formModal", funcs);
}

function GetEditHealthGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/HealthGroup/Edit', id, "GET", "#formModal", funcs);
}

function SendEditHealthGroupRequest() {
    var funcs = new Array();
    funcs.push(GetIndexHealthGroupRequest);
    sendFormRequest('/HealthGroup/Edit', '#editHealthGroupForm', 'POST', "#formModal", funcs);
};

function GetDeleteHealthGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/HealthGroup/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteHealthGroupRequest() {
    var funcs = new Array();
    funcs.push(GetIndexHealthGroupRequest);
    sendFormRequest('/HealthGroup/Delete', '#deleteHealthGroupForm', 'POST', "#formModal", funcs);
};

//Medical certificates functions

function GetDetailsMedicalCertificateRequest(id) {
    sendIdRequest('/MedicalCertificate/Details', id, "GET");
}

function GetCreateMedicalCertificateRequest(id) {
    var funcs = new Array();
    funcs.push(BindDateTimePickerMedicalCertificatesForm);
    funcs.push(toggleFormModal);
    sendIdRequest('/MedicalCertificate/Create', id, "GET", "#formModal", funcs);
}

function SendCreateMedicalCertificateRequest() {
    var funcs = [];
    funcs.push(BindDateTimePickerMedicalCertificatesForm)
    funcs.push(ExecCreateCertificateAction);
    sendFormRequest('/MedicalCertificate/Create', '#createMedicalCertificateForm', 'POST', "#formModal", funcs);
}

function GetEditMedicalCertificateRequest(id) {
    var funcs = new Array();
    funcs.push(BindDateTimePickerMedicalCertificatesForm);
    funcs.push(toggleFormModal)
    sendIdRequest('/MedicalCertificate/Edit', id, "GET", "#formModal", funcs);
}

function SendEditMedicalCertificateRequest() {
    var funcs = new Array();
    funcs.push(BindDateTimePickerMedicalCertificatesForm);
    sendFormRequest('/MedicalCertificate/Edit', '#editMedicalCertificateForm', 'POST', "#formModal", funcs);
};

function GetDeleteMedicalCertificateRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal)
    sendIdRequest('/MedicalCertificate/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteMedicalCertificateRequest() {
    var funcs = new Array();
    funcs.push(ExecDeleteCertificateAction);
    sendFormRequest('/MedicalCertificate/Delete', '#deleteMedicalCertificateForm', 'POST', "#formModal", funcs);
};

//Student functions

function GetDetailsStudentRequest(id) {
    sendIdRequest('/Student/Details', id, "GET");
}

function GetCreateStudentRequest(id) {
    var funcs = new Array();
    funcs.push(BindDateTimePickerStudentForm);
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Create', id, "GET", "#formModal", funcs);
}

function SendCreateStudentRequest() {
    var funcs = new Array();
    funcs.push(BindDateTimePickerStudentForm);
    funcs.push(ExecCreateNodeAction);
    sendFormRequest('/Student/Create', '#createStudentForm', 'POST', "#formModal", funcs);
}

function GetEditStudentRequest(id) {
    var funcs = new Array();
    funcs.push(BindDateTimePickerStudentForm);
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Edit', id, "GET", "#formModal", funcs);
}

function SendEditStudentRequest() {
    var funcs = new Array();
    funcs.push(BindDateTimePickerStudentForm);
    funcs.push(ExecEditNodeAction);
    sendFormRequest('/Student/Edit', '#editStudentForm', 'POST', "#formModal", funcs);
};

function GetDeleteStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteStudentRequest() {
    var funcs = new Array();
    funcs.push(ExecDeleteNodeAction);
    sendFormRequest('/Student/Delete', '#deleteStudentForm', 'POST', "#formModal", funcs);
};

function GetMoveStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Move', id, "GET", "#formModal", funcs);
}

function SendMoveStudentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Student/Move', '#moveStudentForm', 'POST', "#formModal", funcs);
};

//Group functions

function GetDetailsGroupRequest(id) {
    sendIdRequest('/Group/Details', id, "GET");
}

function GetCreateGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Group/Create', id, "GET", "#formModal", funcs);
}

function SendCreateGroupRequest() {
    var funcs = new Array();
    funcs.push(ExecCreateNodeAction);
    sendFormRequest('/Group/Create', '#createGroupForm', 'POST', "#formModal", funcs);
}

function GetEditGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Group/Edit', id, "GET", "#formModal", funcs);
}
function SendEditGroupRequest() {
    var funcs = new Array();
    funcs.push(ExecEditNodeAction);
    sendFormRequest('/Group/Edit', '#editGroupForm', 'POST', "#formModal", funcs);
};

function GetDeleteGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Group/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteGroupRequest() {
    var funcs = new Array();
    funcs.push(ExecDeleteNodeAction);
    sendFormRequest('/Group/Delete', '#deleteGroupForm', 'POST', "#formModal", funcs);
};

//Course functions

function GetDetailsCourseRequest(id) {
    sendIdRequest('/Course/Details', id, "GET");
}

function GetCreateCourseRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Course/Create', id, "GET", "#formModal", funcs);
}

function SendCreateCourseRequest() {
    var funcs = new Array();
    funcs.push(ExecCreateNodeAction);
    sendFormRequest('/Course/Create', '#createCourseForm', 'POST', "#formModal", funcs);
}

function GetEditCourseRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Course/Edit', id, "GET", "#formModal", funcs);
}

function SendEditCourseRequest() {
    var funcs = new Array();
    funcs.push(ExecEditNodeAction);
    sendFormRequest('/Course/Edit', '#editCourseForm', 'POST', "#formModal", funcs);
};

function GetDeleteCourseRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Course/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteCourseRequest() {
    var funcs = new Array();
    funcs.push(ExecDeleteNodeAction);
    sendFormRequest('/Course/Delete', '#deleteCourseForm', 'POST', "#formModal", funcs);
};

//Department functions

function GetDetailsDepartmentRequest(id) {
    sendIdRequest('/Department/Details', id, "GET");
}

function GetCreateDepartmentRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/Department/Create', "GET", "#formModal", funcs);
}

function SendCreateDepartmentRequest() {
    var funcs = new Array();
    funcs.push(ExecCreateDepartmentAction);
    sendFormRequest('/Department/Create', '#createDepartmentForm', 'POST', "#formModal", funcs);
}

function GetEditDepartmentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Department/Edit', id, "GET", "#formModal", funcs);
}

function SendEditDepartmentRequest() {
    var funcs = new Array();
    funcs.push(ExecEditDepartmentAction);
    sendFormRequest('/Department/Edit', '#editDepartmentForm', 'POST', "#formModal", funcs);
};

function GetDeleteDepartmentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Department/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteDepartmentRequest() {
    var funcs = new Array();
    funcs.push(ExecDeleteNodeAction);
    sendFormRequest('/Department/Delete', '#deleteDepartmentForm', 'POST', "#formModal", funcs);
};

//Manage

function GetEditPasswordRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/Manage/ChangePassword', 'GET', '#formModal', funcs);
}

function SendEditPasswordRequest() {
    sendFormRequest('/Manage/ChangePassword', '#changePasswordForm', 'POST', '#formModal');
}

//Admin funcs

function GetCreateAccountRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/Admin/Register', "GET", "#formModal", funcs);
}

function SendCreateAccountRequest() {
    var funcs = new Array();
    funcs.push(GetIndexUsersRequest);
    sendFormRequest('/Admin/Register', '#registerAccountForm', 'POST', "#formModal", funcs);
}

function GetIndexUsersRequest() {
    sendRequest('/Admin/Users', "GET");
}

function GetEditUserNameRequest(id) {

    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Admin/EditUserName', id, "GET", "#formModal", funcs);
}

function SendEditUserNameRequest() {
    var funcs = new Array();
    funcs.push(GetIndexUsersRequest)
    sendFormRequest('/Admin/EditUserName', '#editUserNameForm', 'POST', "#formModal", funcs);
};

function GetDeleteUserRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Admin/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteUserRequest() {
    var funcs = new Array();
    funcs.push(GetIndexUsersRequest)
    sendFormRequest('/Admin/Delete', '#deleteUserForm', 'POST', "#formModal", funcs);
};

function GetEditUserGroupsRequest(id) {
    var funcs = new Array();
    funcs.push(BindFormTreeEditGroups);
    funcs.push(toggleFormModal);
    var parameters = new Array();
    parameters.push(id);
    sendIdRequest('/Admin/EditUserGroups', id, "GET", "#formModal", funcs, parameters);
}

function SendEditUserGroupsRequest() {
    //var funcs = new Array();
    //funcs.push(GetIndexUsersRequest)
    //sendFormRequest('/Admin/EditUserGroups', '#editUserGroupsForm', 'POST', "#formModal", funcs);

    var result = {};
    var value = $('#userIdEditGroups').val();
    result['UserId'] = value;
    result['ActiveGroupsId'] = [];
    result['InactiveGroupId'] = [];

    var formTree = $('#formTree').fancytree('getTree');

    formTree.visit(function (n) {
        if (n.type == 'group') {
            if (n.selected == true) {
                result['ActiveGroupsId'].push(n.data.modelId);
            }
            else {
                result['InactiveGroupId'].push(n.data.modelId);
            }
        }
    });
    var jsonResult = JSON.stringify(result);
    $.ajax({
        type: "POST",
        cache: false,
        url: '/Admin/EditUserGroups',
        contentType: "application/json; charset=utf-8",
        data: jsonResult,
        success: function (data) {
            SetHtml("#formModal", data);
            GetIndexUsersRequest();
        },
        error: function (data) {
            console.log("error")
        }
    });

};


function GetEditManagerDepartmentRequest(id, selectedDepartmentId) {
    var funcs = new Array();
    funcs.push(BindFormTreeEditManagerDepartment);
    funcs.push(toggleFormModal);
    var parameters = [];
    if (selectedDepartmentId) {
        parameters.push(selectedDepartmentId);
    }
    sendIdRequest('/Admin/EditManagerUserDepartment', id, "GET", "#formModal", funcs, parameters);
}

function SendEditManagerUserDepartmentRequest() {
    //var funcs = new Array();
    //funcs.push(GetIndexUsersRequest)
    //sendFormRequest('/Admin/EditUserGroups', '#editUserGroupsForm', 'POST', "#formModal", funcs);

    var result = {};
    var value = $('#userIdEditDepartment').val();
    result['UserId'] = value;
    result['DepartmentsId'] = [];

    var formTree = $('#formTree').fancytree('getTree');

    formTree.visit(function (n) {
        if (n.type == 'department') {
            if (n.selected == true) {
                result['DepartmentsId'].push(n.data.modelId);
            }
        }
    });
    var jsonResult = JSON.stringify(result);
    $.ajax({
        type: "POST",
        cache: false,
        url: '/Admin/EditManagerUserDepartment',
        contentType: "application/json; charset=utf-8",
        data: jsonResult,
        success: function (data) {
            SetHtml("#formModal", data);
            GetIndexUsersRequest();
        },
        error: function (data) {
            console.log("error")
        }
    });

};


//Reports
function GetConfigureGroupReportRequest() {
    var funcs = new Array();
    funcs.push(BindFormTreeReportGroups)
    funcs.push(BindDateTimePickerReportForm);
    funcs.push(toggleFormModal);
    sendRequest('/Report/ConfigureGroupReport', "GET", "#formModal", funcs);
}

async function SendConfigureGroupReportRequest() {
    var dataArray = $('#configureGroupReport').serialize();
    var formTree = $('#formTree').fancytree('getTree');

    var counter = 0;
    formTree.visit(function (n) {

        if (n.type == 'group') {
            if (n.selected == true) {
                dataArray += '&GroupsId[' + counter + ']=' + n.data.modelId;
                counter++;
            }
        }
    });
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/ConfigureGroupReport',
        data: dataArray,
        success: function (data) {
            var substring = '&#x41E;&#x448;&#x438;&#x431;&#x43A;&#x430';
            if (data.indexOf(substring) != -1) {
                var funcs = [];
                funcs.push(BindFormTreeReportGroups);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal', data, funcs);
            }
            else {
                SetHtml('#formModal', data);
                GetGroupReportRequest(dataArray)
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function GetGroupReportRequest(data) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/GroupReport',
        data: data,
        success: function (data) {
            SetHtml(undefined, data);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function GetConfigureDepartmentReportRequest() {
    var funcs = new Array();
    funcs.push(BindFormTreeReportDepartments)
    funcs.push(BindDateTimePickerReportForm);
    funcs.push(toggleFormModal);
    sendRequest('/Report/ConfigureDepartmentReport', "GET", "#formModal", funcs);
}

async function SendConfigureDepartmentReportRequest() {
    var dataArray = $('#configureDepartmentReport').serialize();
    var formTree = $('#formTree').fancytree('getTree');

    var counter = 0;
    formTree.visit(function (n) {

        if (n.type == 'department') {
            if (n.selected == true) {
                dataArray += '&DepartmentsId[' + counter + ']=' + n.data.modelId;
                counter++;
            }
        }
    });
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/ConfigureDepartmentReport',
        data: dataArray,
        success: function (data) {
            var substring = '&#x41E;&#x448;&#x438;&#x431;&#x43A;&#x430';
            if (data.indexOf(substring) != -1) {
                var funcs = [];
                funcs.push(BindFormTreeReportDepartments);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal', data, funcs);
            }
            else {
                SetHtml('#formModal', data);
                GetDepartmentReportRequest(dataArray);
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function GetDepartmentReportRequest(data) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/DepartmentReport',
        data: data,
        success: function (data) {
            SetHtml(undefined, data);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

//
function GetConfigureCourseReportRequest() {
    var funcs = new Array();
    funcs.push(BindFormTreeReportCourses)
    funcs.push(BindDateTimePickerReportForm);
    funcs.push(toggleFormModal);
    sendRequest('/Report/ConfigureCourseReport', "GET", "#formModal", funcs);
}

async function SendConfigureCourseReportRequest() {
    var dataArray = $('#configureCourseReport').serialize();
    var formTree = $('#formTree').fancytree('getTree');

    var counter = 0;
    formTree.visit(function (n) {

        if (n.type == 'course') {
            if (n.selected == true) {
                dataArray += '&CoursesId[' + counter + ']=' + n.data.modelId;
                counter++;
            }
        }
    });
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/ConfigureCourseReport',
        data: dataArray,
        success: function (data) {
            var substring = '&#x41E;&#x448;&#x438;&#x431;&#x43A;&#x430';
            if (data.indexOf(substring) != -1) {
                var funcs = [];
                funcs.push(BindFormTreeReportCourses);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal', data, funcs);
            }
            else {
                SetHtml('#formModal', data);
                GetCourseReportRequest(dataArray);
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function GetCourseReportRequest(data) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/CourseReport',
        data: data,
        success: function (data) {
            SetHtml(undefined, data);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

//HealthSheetReport

function GetConfigureHealthSheetReportRequest() {
    var funcs = new Array();
    funcs.push(BindFormTreeReportHealthSheet)
    funcs.push(toggleFormModal);
    sendRequest('/Report/ConfigureHealthSheetReport', "GET", "#formModal", funcs);
}

async function SendConfigureHealthSheetReportRequest() {
    var dataArray = $('#configureHealthSheetReport').serialize();
    var formTree = $('#formTree').fancytree('getTree');

    var counter = 0;
    formTree.visit(function (n) {

        if (n.type == 'group') {
            if (n.selected == true) {
                dataArray += '&GroupsId[' + counter + ']=' + n.data.modelId;
                counter++;
            }
        }
    });
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/ConfigureHealthSheetReport',
        data: dataArray,
        success: function (data) {
            var substring = '&#x41E;&#x448;&#x438;&#x431;&#x43A;&#x430';
            if (data.indexOf(substring) != -1) {
                var funcs = [];
                funcs.push(BindFormTreeReportHealthSheet);
                SetHtml('#formModal', data, funcs);
            }
            else {
                SetHtml('#formModal', data);
                GetHealthSheetReportRequest(dataArray)
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function GetHealthSheetReportRequest(data) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/HealthSheetReport',
        data: data,
        success: function (data) {
            SetHtml(undefined, data);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

//PEDepartmentReport 

function GetConfigurePEDepartmentReportRequest() {
    var funcs = new Array();
    funcs.push(BindFormTreeReportPEDepartment)
    funcs.push(toggleFormModal);
    sendRequest('/Report/ConfigurePEDepartmentReport', "GET", "#formModal", funcs);
}

async function SendConfigurePEDepartmentReportRequest() {
    var dataArray = $('#configurePEDepartmentReport').serialize();
    var formTree = $('#formTree').fancytree('getTree');

    var counter = 0;
    formTree.visit(function (n) {

        if (n.type == 'department') {
            if (n.selected == true) {
                dataArray += '&DepartmentsId[' + counter + ']=' + n.data.modelId;
                counter++;
            }
        }
    });
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/ConfigurePEDepartmentReport',
        data: dataArray,
        success: function (data) {
            var substring = '&#x41E;&#x448;&#x438;&#x431;&#x43A;&#x430';
            if (data.indexOf(substring) != -1) {
                var funcs = [];
                funcs.push(BindFormTreeReportPEDepartment);
                SetHtml('#formModal', data, funcs);
            }
            else {
                SetHtml('#formModal', data);
                GetPEDepartmentReportRequest(dataArray)
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function GetPEDepartmentReportRequest(data) {
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/PEDepartmentReport',
        data: data,
        success: function (data) {
            SetHtml(undefined, data);
        },
        error: function (data) {
            console.log(data);
        }
    });
}


function ChangeReportType() {
    var value = $('#reportType').val();
    switch (+value) {
        case 2:
            $('#reportOnDate').show();
            $('#reportMonthsCount').hide();
            break;
        case 3:
            $('#reportOnDate').hide();
            $('#reportMonthsCount').show();
            break;
        default:
            $('#reportOnDate').hide();
            $('#reportMonthsCount').hide();
            break;
    }
}

//Other

function BindFormTreeEditGroups(id) {
    var funcs = [];
    funcs.push(SetCheckboxesOnGroups);
    funcs.push(CheckSelectedGroups);
    funcs.push(ExpandAllNodes);
    var parameters = [];
    parameters[1] = id;
    parameters[2] = '#formTree';
    LoadFormTree(funcs, parameters);
}

function BindFormTreeEditManagerDepartment(selectedDepartmentId) {
    var funcs = [];
    funcs.push(SetCheckboxesOnDepartments);
    funcs.push(CheckSelectedDepartment);

    var parameters = [];
    parameters[1] = selectedDepartmentId;
    LoadFormTree(funcs, parameters, clickSelectableOnlyOne('#formTree', 'department'));
}

function BindFormTreeReportGroups() {
    var funcs = [];
    funcs.push(SetCheckboxesOnGroups);
    funcs.push(ExpandAllNodes);
    var parameters = [];
    parameters[1] = '#formTree';
    LoadFormTree(funcs, parameters);
}

function BindFormTreeReportHealthSheet() {
    var funcs = [];
    funcs.push(SetCheckboxesOnGroups);
    funcs.push(SetOnlyOneClickable);
    var parameters = [];
    LoadFormTree(funcs, parameters, clickSelectableOnlyOne('#formTree', 'group' ));
}

function BindFormTreeReportPEDepartment() {
    var funcs = [];
    funcs.push(SetCheckboxesOnDepartments);
    funcs.push(SetOnlyOneClickable);
    var parameters = [];
    LoadFormTree(funcs, parameters, clickSelectableOnlyOne('#formTree', 'department'));
}

function clickSelectableOnlyOne(treeId, type) {
    return (event, data) => {
        var node = data.node;
        

        if (node.type === type) {
            var formTree = $(treeId).fancytree('getTree');

            formTree.visit(function (n) {
                if (n.type == type && n !== node) {
                    n.setActive(false);
                    n.setSelected(false);
                }
            });
        }

    }
}

function BindFormTreeReportDepartments() {
    var funcs = [];
    funcs.push(SetCheckboxesOnDepartments);
    var parameters = [];
    parameters[1] = '#formTree';
    LoadFormTree(funcs, parameters);
}

function BindFormTreeReportCourses() {
    var funcs = [];
    funcs.push(SetCheckboxesOnCourses);
    funcs.push(ExpandDepartmentNodes);
    var parameters = [];
    parameters[1] = '#formTree';
    LoadFormTree(funcs, parameters);
}

function SetCheckboxesOnGroups() {
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function (n) {

        if (n.type == 'group') {
            n.checkbox = true;
        }
    });
}

function SetOnlyOneClickable() {
    var formTree = $('#formTree').fancytree('getTree');

}



function SetCheckboxesOnDepartments() {
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function (n) {

        if (n.type == 'department') {
            n.checkbox = true;
        }
    });
}

function SetCheckboxesOnCourses() {
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function (n) {

        if (n.type == 'course') {
            n.checkbox = true;
        }
    });
}

function LoadFormTree(functionsArray, parametersArray, clickEvent) {
    $("#formTree").fancytree({
        source: $.ajax({
            url: '/Tree/GetManagementFormHierarchy',
            method: 'GET',
            dataType: "json",
            success: function (data) {
            }
        }),
        cache: false,
        clickFolderMode: 1,
        click: clickEvent || function (event, data) {
            var node = data.node;
            if (node.isActive()) {
                node.setFocus(false);
                node.setActive(false);
                event.preventDefault();
            }

        },
        loadChildren: function (event, data) {
            if (functionsArray != undefined) {
                if (parametersArray != undefined) {
                    for (var i = 0; i < functionsArray.length; i++) {
                        if (parametersArray[i] != undefined)
                            functionsArray[i](parametersArray[i]);
                        else {
                            functionsArray[i]();
                        }
                    }
                }
                else {
                    for (var i = 0; i < functionsArray.length; i++) {
                        functionsArray[i]();
                    }
                }
            }

        }
    });
    $("#formTree").addClass('non-selectable');
}

function CheckSelectedGroups(id) {
    var formTree = $('#formTree').fancytree('getTree');
    var result = undefined;
    var url = "/Tree/GetUserGroupsId" + '?id=' + id;
    $.ajax({
        type: "GET",
        cache: false,
        url: url,
        success: function (data) {
            result = data;
            if (result != undefined) {
                formTree.visit(function (n) {
                    if (n.type == 'group') {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i] == n.data.modelId) {
                                n.setSelected(true);
                                break;
                            }
                        }
                    }
                });
            }
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function CheckSelectedDepartment(departmentId) {
    var formTree = $('#formTree').fancytree('getTree');
    if (departmentId !== undefined) {
        formTree.visit(function (n) {
            if (n.type == 'department') {
                if (departmentId == n.data.modelId) {
                    n.setSelected(true);
                }
            }
        });
    }   
    
}

function ExpandAllNodes(id) {
    var formTree = $(id).fancytree('getTree');
    formTree.visit(function (n) {
        if (n.isFolder()) {
            n.setExpanded(true);
        }
    });
}

function ExpandDepartmentNodes(id) {
    var formTree = $(id).fancytree('getTree');
    formTree.visit(function (n) {
        if (n.type == 'department') {
            n.setExpanded(true);
        }
    });
}

function toggleFormModal() {
    $('#formModal').modal('toggle');
}

function isAdmin() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Tree/GetRole",
            success: function (data) {
                if (data == "admin") {
                    resolve(true);
                } else {
                    resolve(false);
                }
            }
        });
    });
}

function EnableCreateBtn() {
    $('#createBtn').prop('disabled', false);
}

function DisableCreateBtn() {
    $('#createBtn').prop('disabled', true);
}

function EnableEditBtn() {
    $('#editBtn').prop('disabled', false);
}

function DisableEditBtn() {
    $('#editBtn').prop('disabled', true);
}

function EnableDeleteBtn() {
    $('#deleteBtn').prop('disabled', false);
}

function DisableDeleteBtn() {
    $('#deleteBtn').prop('disabled', true);
}

function EnableMoveBtn() {
    $('#moveBtn').prop('disabled', false);
}

function DisableMoveBtn() {
    $('#moveBtn').prop('disabled', true);
}
// MainButtonFuncs
async function ExecIfAdmin(func, parameter) {
    var result = await isAdmin();
    if (result) {
        if (func != undefined) {
            if (parameter != undefined)
                func(parameter)
            else func();
        }
    }
}

function ExecCreateAction() {
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if (!activeNode) {
        ExecIfAdmin(GetCreateDepartmentRequest);
    }
    else {
        var type = activeNode.type;
        var role = activeNode.data.userRole;
        var modelId = activeNode.data.modelId;
        switch (type) {
            case 'department':
                if (role === 'admin') {
                    GetCreateCourseRequest(modelId);
                }
                break;
            case 'course':
                if (role === 'admin') {
                    GetCreateGroupRequest(modelId);
                }
                break;
            case 'group':
                GetCreateStudentRequest(modelId);
                break;
            case 'student':
                GetCreateMedicalCertificateRequest(modelId);
                break;
        }
    }

}

function ExecEditAction() {
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if (!activeNode) {
    }
    else {
        var type = activeNode.type;
        var role = activeNode.data.userRole;
        var modelId = activeNode.data.modelId;
        switch (type) {
            case 'department':
                if (role === 'admin') {
                    GetEditDepartmentRequest(modelId);
                }
                break;
            case 'course':
                if (role === 'admin') {
                    GetEditCourseRequest(modelId);
                }
                break;
            case 'group':
                if (role === 'admin') {
                    GetEditGroupRequest(modelId);
                }
                break;
            case 'student':
                GetEditStudentRequest(modelId);
                break;
            case 'certificate':
                GetEditMedicalCertificateRequest(modelId);
                break;
        }
    }
}

function ExecDeleteAction() {
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if (!activeNode) {
    }
    else {
        var type = activeNode.type;
        var role = activeNode.data.userRole;
        var modelId = activeNode.data.modelId;
        activeNode.expand = true;
        switch (type) {
            case 'department':
                if (role === 'admin') {
                    GetDeleteDepartmentRequest(modelId);
                }
                break;
            case 'course':
                if (role === 'admin') {
                    GetDeleteCourseRequest(modelId);
                }
                break;
            case 'group':
                if (role === 'admin') {
                    GetDeleteGroupRequest(modelId);
                }
                break;
            case 'student':
                if (role === 'admin') {
                    GetDeleteStudentRequest(modelId);
                }
                break;
            case 'certificate':
                GetDeleteMedicalCertificateRequest(modelId);
                break;
        }
    }
}
function getNodeInfo() {
    return {
        title: $('#nodeTitle').text(),
        modelId: +$('#nodeModelId').text(),
        parentId: $('#nodeParentId').text(),
        userRole: $('#nodeUserRole').text(),
        type: $('#nodeType').text(),
        folder: ($('#nodeFolder').text() === 'True' ? true : false),
    }
}

function ExecCreateDepartmentAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        var newNode = getNodeInfo();
        $('#tree').fancytree('getRootNode').addChildren(newNode);
        $('#tree').fancytree('getRootNode').sortChildren();
    }
}

function ExecEditDepartmentAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        $('#tree').fancytree("getActiveNode").setTitle($('#nodeTitle').text());
        $('#tree').fancytree('getRootNode').sortChildren();
    }
}

function ExecCreateCertificateAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        var newNode = getNodeInfo();
        $('#tree').fancytree('getActiveNode').addChildren(newNode);
        $('#tree').fancytree('getActiveNode').sortChildren((a, b) => {
            var x = a.data.modelId;
            var y = b.data.modelId;
            return x === y ? 0 : x > y ? 1 : -1;
        });
        var counter = 1;
        $('#tree').fancytree('getActiveNode').visit(function (n) {
            n.setTitle('Медицинская справка №' + counter);
            counter++;
        });
    }
}

function ExecDeleteCertificateAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        var parentNode = $('#tree').fancytree('getActiveNode').parent;
        $('#tree').fancytree('getActiveNode').remove();
        parentNode.sortChildren((a, b) => {
            var x = a.data.modelId;
            var y = b.data.modelId;
            return x === y ? 0 : x > y ? 1 : -1;
        });
        var counter = 1;
        parentNode.visit(function (n) {
            n.title = 'Медицинская справка №' + counter;
        });
    }
}

function ExecCreateNodeAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        var newNode = getNodeInfo();
        var activeNode = $('#tree').fancytree('getActiveNode');
        activeNode.addChildren(newNode);
        activeNode.setExpanded();
        activeNode.sortChildren();
    }
}

function ExecEditNodeAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        var activeNode = $('#tree').fancytree('getActiveNode');
        activeNode.setTitle($('#nodeTitle').text());
        activeNode.parent.sortChildren();
    }
}

function ExecDeleteNodeAction() {
    var isSuccessful = $('#isSuccessful').text();
    if (isSuccessful === 'True') {
        $('#tree').fancytree("getActiveNode").remove();
    }
}

async function ExecUpdateActionAction() {
    var tree = $('#tree').fancytree('getTree');
    var keysArray = {};

    tree.visit(function (n) {
        if (n.isExpanded()) {
            if (keysArray[n.type] == undefined) {
                keysArray[n.type] = new Array();
            }
            keysArray[n.type].push(n.data.modelId);
        }
    });

    var res = await tree.reload();
    var tree = $('#tree').fancytree('getTree');
    tree.visit(function (n) {
        if (keysArray[n.type] != undefined) {
            var array = keysArray[n.type];
            for (var i = 0; i < array.length; i++) {
                if (array[i] == n.data.modelId) {
                    n.setExpanded(true);
                    break;
                }
            }
        }
    });

}


function bindDatePicker() {
    $(".date").datetimepicker({
        format: 'DD.MM.YYYY',
        locale: 'ru',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    }).find('input:first').on("blur", function () {
        var date = parseDate($(this).val());
        $(this).val(date);
    });
}

function bindradioButtons() {
    $("#IsUsingTermNo").prop("checked", true);
    $("#IsUsingTermYes").prop("checked", false);

    $('input[type=radio][name^=IsUsingTerm]').change(function () {
        if (this.value === 'true') {
            $('#finishDateBlock').hide();
            $('#certificateTermBlock').show();
        }
        else {
            $('#certificateTermBlock').hide();
            $('#finishDateBlock').show();
        }
    });
}

function parseDate(value) {
    var m = value.match(/^(\d{1,2})(\/|-)?(\d{1,2})(\/|-)?(\d{4})$/);
    if (m)
        value = m[5] + '-' + ("00" + m[3]).slice(-2) + '-' + ("00" + m[1]).slice(-2);

    return value;
}

function BindDateTimePickerReportForm() {
    bindDatePicker();
}

function BindDateTimePickerMedicalCertificatesForm() {
    var isValidDate = function (value, format) {
        format = format || false;
        // lets parse the date to the best of our knowledge
        if (format) {
            value = parseDate(value);
        }

        var timestamp = Date.parse(value);

        return isNaN(timestamp) == false;
    }

    bindDatePicker();
    bindradioButtons();
}

function BindDateTimePickerStudentForm() {
    var isValidDate = function (value, format) {
        format = format || false;
        // lets parse the date to the best of our knowledge
        if (format) {
            value = parseDate(value);
        }

        var timestamp = Date.parse(value);

        return isNaN(timestamp) == false;
    }

    bindDatePicker();
}


function print() {
    var element = document.getElementById('main-content').firstElementChild.innerHTML;
    var title = '';
    if (element != undefined) {
        title = element;
    }
    title = transliterate(title);
    var size = Math.floor(($('#main-content').width() * 0.26458333333719));
    if (+size < 210) {
        size = '210mm';
    } else {
        size = size + 'mm';
    }
    return xepOnline.Formatter.Format('main-content',
        { pageWidth: '210mm', pageHeight: '297mm', render: 'download', pageMargin: '0.2in', filename: title });
 }
      

function transliterate(word) {
    var answer = ""
        , a = {};

    a["Ё"] = "YO"; a["Й"] = "I"; a["Ц"] = "TS"; a["У"] = "U"; a["К"] = "K"; a["Е"] = "E"; a["Н"] = "N"; a["Г"] = "G"; a["Ш"] = "SH"; a["Щ"] = "SCH"; a["З"] = "Z"; a["Х"] = "H"; a["Ъ"] = "\'";
    a["ё"] = "yo"; a["й"] = "i"; a["ц"] = "ts"; a["у"] = "u"; a["к"] = "k"; a["е"] = "e"; a["н"] = "n"; a["г"] = "g"; a["ш"] = "sh"; a["щ"] = "sch"; a["з"] = "z"; a["х"] = "h"; a["ъ"] = "\'";
    a["Ф"] = "F"; a["Ы"] = "I"; a["В"] = "V"; a["А"] = "a"; a["П"] = "P"; a["Р"] = "R"; a["О"] = "O"; a["Л"] = "L"; a["Д"] = "D"; a["Ж"] = "ZH"; a["Э"] = "E";
    a["ф"] = "f"; a["ы"] = "i"; a["в"] = "v"; a["а"] = "a"; a["п"] = "p"; a["р"] = "r"; a["о"] = "o"; a["л"] = "l"; a["д"] = "d"; a["ж"] = "zh"; a["э"] = "e";
    a["Я"] = "Ya"; a["Ч"] = "CH"; a["С"] = "S"; a["М"] = "M"; a["И"] = "I"; a["Т"] = "T"; a["Ь"] = "i"; a["Б"] = "B"; a["Ю"] = "YU";
    a["я"] = "ya"; a["ч"] = "ch"; a["с"] = "s"; a["м"] = "m"; a["и"] = "i"; a["т"] = "t"; a["ь"] = "i"; a["б"] = "b"; a["ю"] = "yu";

    for (i in word) {
        if (word.hasOwnProperty(i)) {
            if (a[word[i]] === undefined) {
                answer += word[i];
            } else {
                answer += a[word[i]];
            }            
        }
    }
    return answer;
}