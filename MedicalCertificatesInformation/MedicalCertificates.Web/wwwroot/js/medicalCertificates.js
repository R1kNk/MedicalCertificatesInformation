var mainContainerId = '#main-content';

function sendFormRequest(url, formId, method, replaceIntoId, promiseArray, parameterArray) {
    var data = $(formId).serialize();

    console.log(data);
    $.ajax({
        type: method,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            SetHtml(replaceIntoId, data, promiseArray, parameterArray);
            
        },
        error: function (data) {
            console.log("error") 
               }
    });
}

function SetButtonsAvailability(type ,role){
    switch (type) {
        case 'department':
            if (role === 'admin') {
                EnableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            else {
                DisableCreateBtn();
                DisableEditBtn();
                DisableDeleteBtn();
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
            else {
                DisableCreateBtn();
                DisableEditBtn();
                DisableDeleteBtn();
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
            else {
                EnableCreateBtn();
                DisableEditBtn();
                DisableDeleteBtn();
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
            else {
                EnableCreateBtn();
                EnableEditBtn();
                DisableDeleteBtn();
                DisableMoveBtn();
            }
            break;
        case 'certificate':
            if (role === 'admin') {
                DisableCreateBtn();
                EnableEditBtn();
                EnableDeleteBtn();
                DisableMoveBtn();
            }
            else {
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
        if(promiseArray!=undefined){
            if(parametersArray!=undefined){
                for(var i = 0; i< promiseArray.length; i++){
                    if(parametersArray[i] != undefined)
                    promiseArray[i](parametersArray[i]);
                    else{
                        promiseArray[i]();
                    }
                }
            }
            else{
                for(var i = 0; i< promiseArray.length; i++){
                        promiseArray[i]();
                }
            }
        }
    }
    return 1;
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
    var funcs = new Array();
    funcs.push(toggleFormModal);
   sendRequest('/PhysicalEducation/Create', "GET", "#formModal", funcs);
}

 function SendCreateHospitalRequest() {
     var funcs = new Array();
     funcs.push(GetIndexHospitalRequest);
 sendFormRequest('/Hospital/Create', '#createHospitalForm', 'POST', "#formModal", funcs);

};

function GetEditHospitalRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
     sendIdRequest('/Hospital/Edit', id, "GET", "#formModal", funcs);
}

 function SendEditHospitalRequest() {
    var funcs = new Array();
    funcs.push(GetIndexHospitalRequest);
    sendFormRequest('/Hospital/Edit', '#editHospitalForm', 'POST', "#formModal", funcs);
};

function GetDeleteHospitalRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
   sendIdRequest('/Hospital/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteHospitalRequest() {
    var funcs = new Array();
    funcs.push(GetIndexHospitalRequest);
    sendFormRequest('/Hospital/Delete', '#deleteHospitalForm', 'POST', "#formModal", funcs);
};

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
    sendRequest('/PhysicalEducation/Create', "GET", "#formModal",funcs);
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
    sendIdRequest('/HealthGroup/Delete', id, "GET","#formModal", funcs);
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
    funcs.push(ExecUpdateActionAction);
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
    funcs.push(ExecUpdateActionAction);
  sendFormRequest('/MedicalCertificate/Edit', '#editMedicalCertificateForm', 'POST', "#formModal", funcs);
};

function GetDeleteMedicalCertificateRequest(id) {
  sendIdRequest('/MedicalCertificate/Delete', id, "GET", "#formModal");
}

 function SendDeleteMedicalCertificateRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/MedicalCertificate/Delete', '#deleteMedicalCertificateForm', 'POST', "#formModal", funcs);
};

//Student functions

function GetDetailsStudentRequest(id) {
    sendIdRequest('/Student/Details', id, "GET");
}

function GetCreateStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Create', id, "GET", "#formModal", funcs);
}

 function SendCreateStudentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
  sendFormRequest('/Student/Create', '#createStudentForm', 'POST', "#formModal", funcs);
}

function GetEditStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
   sendIdRequest('/Student/Edit', id, "GET", "#formModal", funcs);
}

function SendEditStudentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
   sendFormRequest('/Student/Edit', '#editStudentForm', 'POST', "#formModal", funcs);
};

function GetDeleteStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteStudentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
   sendFormRequest('/Student/Delete', '#deleteStudentForm', 'POST', "#formModal", funcs);
};

function GetMoveStudentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Student/Move', id, "GET", "#formModal",funcs);
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
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Group/Create', '#createGroupForm', 'POST', "#formModal", funcs);
}

function GetEditGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Group/Edit', id, "GET", "#formModal", funcs);
}
 function SendEditGroupRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Group/Edit', '#editGroupForm', 'POST',"#formModal", funcs);
};

function GetDeleteGroupRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Group/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteGroupRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
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
    funcs.push(ExecUpdateActionAction);
   sendFormRequest('/Course/Create', '#createCourseForm', 'POST', "#formModal", funcs);
}

 function GetEditCourseRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Course/Edit', id, "GET", "#formModal", funcs);
}

function SendEditCourseRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Course/Edit', '#editCourseForm', 'POST', "#formModal", funcs);
};

function GetDeleteCourseRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Course/Delete', id, "GET", "#formModal", funcs);
}

function SendDeleteCourseRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
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
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Department/Create', '#createDepartmentForm', 'POST', "#formModal", funcs);
}

function GetEditDepartmentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Department/Edit', id, "GET", "#formModal", funcs);
}

function SendEditDepartmentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Department/Edit', '#editDepartmentForm', 'POST', "#formModal", funcs);
};

function GetDeleteDepartmentRequest(id) {
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendIdRequest('/Department/Delete', id, "GET", "#formModal");
}

function SendDeleteDepartmentRequest() {
    var funcs = new Array();
    funcs.push(ExecUpdateActionAction);
    sendFormRequest('/Department/Delete', '#deleteDepartmentForm', 'POST', "#formModal", funcs);
};
//Manage

function GetEditPasswordRequest(){
    var funcs = new Array();
    funcs.push(toggleFormModal);
    sendRequest('/Manage/ChangePassword', 'GET', '#formModal', funcs);
}

function SendEditPasswordRequest(){
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

function GetIndexUsersRequest(){
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

    formTree.visit(function(n){
        console.log(n);
        if(n.type=='group'){
            if(n.selected == true){
                result['ActiveGroupsId'].push(n.data.modelId);
            }
            else{
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
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='group'){
            if(n.selected == true){
                dataArray+= '&GroupsId['+counter+']='+n.data.modelId;
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
            if(data.indexOf(substring)!=-1){
                var funcs = [];
                funcs.push(BindFormTreeReportGroups);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal',data, funcs);
            }
            else{
                SetHtml('#formModal',data);
                GetGroupReportRequest(dataArray)
            }
        },
        error: function (data) {
            console.log(data) 
        }
    });
}

function GetGroupReportRequest(data){
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/GroupReport',
        data: data,
        success: function (data) {      
            SetHtml(undefined,data);
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
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='department'){
            if(n.selected == true){
                dataArray+= '&DepartmentsId['+counter+']='+n.data.modelId;
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
            if(data.indexOf(substring)!=-1){
                var funcs = [];
                funcs.push(BindFormTreeReportDepartments);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal',data, funcs);
            }
            else{
                SetHtml('#formModal',data);
                GetDepartmentReportRequest(dataArray);
            }
        },
        error: function (data) {
            console.log(data) 
        }
    });
}

function GetDepartmentReportRequest(data){
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/DepartmentReport',
        data: data,
        success: function (data) {      
            SetHtml(undefined,data);
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
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='course'){
            if(n.selected == true){
                dataArray+= '&CoursesId['+counter+']='+n.data.modelId;
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
            if(data.indexOf(substring)!=-1){
                var funcs = [];
                funcs.push(BindFormTreeReportCourses);
                funcs.push(BindDateTimePickerReportForm);
                SetHtml('#formModal',data, funcs);
            }
            else{
                SetHtml('#formModal',data);
                GetCourseReportRequest(dataArray);
            }
        },
        error: function (data) {
            console.log(data) 
        }
    });
}

function GetCourseReportRequest(data){
    $.ajax({
        type: 'POST',
        cache: false,
        url: '/Report/CourseReport',
        data: data,
        success: function (data) {      
            SetHtml(undefined,data);
        },
        error: function (data) {
            console.log(data); 
        }
    });
}

function ChangeReportType(){
    var value = $('#reportType').val();
    switch(+value){
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

function BindFormTreeEditGroups(id){
    var funcs = [];
    funcs.push(SetCheckboxesOnGroups);
    funcs.push(CheckSelectedGroups);
    funcs.push(ExpandAllNodes);
    var parameters = [];
    parameters[1]= id;
    parameters[2] = '#formTree';
    LoadFormTree(funcs, parameters);    
}

function BindFormTreeReportGroups(){
        var funcs = [];
        funcs.push(SetCheckboxesOnGroups);
        funcs.push(ExpandAllNodes);
        var parameters = [];
        parameters[1] = '#formTree';
        LoadFormTree(funcs, parameters);    
}

function BindFormTreeReportDepartments(){
    var funcs = [];
    funcs.push(SetCheckboxesOnDepartments);
    var parameters = [];
    parameters[1] = '#formTree';
    LoadFormTree(funcs, parameters);    
}

function BindFormTreeReportCourses(){
    var funcs = [];
    funcs.push(SetCheckboxesOnCourses);
    funcs.push(ExpandDepartmentNodes);
    var parameters = [];
    parameters[1] = '#formTree';
    LoadFormTree(funcs, parameters);    
}

 function SetCheckboxesOnGroups(){
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='group'){
        n.checkbox = true;
        }
    });
 }

 function SetCheckboxesOnDepartments(){
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='department'){
        n.checkbox = true;
        }
    });
 }

 function SetCheckboxesOnCourses(){
    var formTree = $('#formTree').fancytree('getTree');
    formTree.visit(function(n){
        console.log(n);
        if(n.type=='course'){
        n.checkbox = true;
        }
    });
 }

 function LoadFormTree(functionsArray, parametersArray){
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
        click: function (event, data) {
            var node = data.node;
            if (node.isActive()) {
                node.setFocus(false);
                node.setActive(false);
                event.preventDefault();
            }

        },
        loadChildren: function(event, data) {
            if(functionsArray!=undefined){
                if(parametersArray!=undefined){
                    for(var i = 0; i< functionsArray.length; i++){
                        if(parametersArray[i] != undefined)
                        functionsArray[i](parametersArray[i]);
                        else{
                            functionsArray[i]();
                        }
                    }
                }
                else{
                    for(var i = 0; i< functionsArray.length; i++){
                        functionsArray[i]();
                    }
                }
            }
            
        }
    });
}

function CheckSelectedGroups(id){
    var formTree = $('#formTree').fancytree('getTree');
    var result = undefined;
    var url =  "/Tree/GetUserGroupsId" + '?id=' + id;
    $.ajax({
        type: "GET",
        cache: false,
        url: url,
        success: function (data) {
        result = data;
        if(result!= undefined){
            formTree.visit(function(n){
                if(n.type=='group'){
                for(var i = 0; i< result.length; i++){
                    if(result[i] == n.data.modelId){
                        n.setSelected(true);
                        break;
                        }
                    }
                    }
                });
            }
        },
            error: function (data) {
                console.log("error")
            }
    }); 
}

function ExpandAllNodes(id){
    var formTree = $(id).fancytree('getTree');
    formTree.visit(function(n){
        if(n.isFolder()){
        n.setExpanded(true);
        }
    });
}

function ExpandDepartmentNodes(id){
    var formTree = $(id).fancytree('getTree');
    formTree.visit(function(n){
        if(n.type=='department'){
        n.setExpanded(true);
        }
    });
}

function toggleFormModal() {
    $('#formModal').modal('toggle');
}

function  isAdmin(){
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

function EnableCreateBtn(){
    $('#createBtn').prop('disabled', false);
}

function DisableCreateBtn(){
    $('#createBtn').prop('disabled', true);
}

function EnableEditBtn(){
    $('#editBtn').prop('disabled', false);
}

function DisableEditBtn(){
    $('#editBtn').prop('disabled', true);
}

function EnableDeleteBtn(){
    $('#deleteBtn').prop('disabled', false);
}

function DisableDeleteBtn(){
    $('#deleteBtn').prop('disabled', true);
}

function EnableMoveBtn(){
    $('#moveBtn').prop('disabled', false);
}

function DisableMoveBtn(){
    $('#moveBtn').prop('disabled', true);
}
// MainButtonFuncs
async function ExecIfAdmin(func, parameter){
    var result = await isAdmin();
    if(result){
       if(func!=undefined){
           if(parameter!=undefined)
           func(parameter)
           else func();
       }
    }
}

function ExecCreateAction(){
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if(!activeNode){
        ExecIfAdmin(GetCreateDepartmentRequest);
    }
    else{
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

function ExecEditAction(){
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if(!activeNode){
    }
    else{
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

function ExecDeleteAction(){
    var tree = $('#tree').fancytree('getTree');
    activeNode = tree.getActiveNode();
    if(!activeNode){
    }
    else{
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

async function ExecUpdateActionAction(){
    var tree = $('#tree').fancytree('getTree');
    var keysArray = {};

    tree.visit(function(n){
        if(n.isExpanded()){
            if(keysArray[n.type] == undefined){
                keysArray[n.type] = new Array();
            }
            keysArray[n.type].push(n.data.modelId);
        }
    });
    
 var res = await tree.reload();
    var tree = $('#tree').fancytree('getTree');
    tree.visit(function(n){
        if(keysArray[n.type]!=undefined){
            var array = keysArray[n.type];
            for(var i =0; i< array.length; i++){
                if(array[i] == n.data.modelId){
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
