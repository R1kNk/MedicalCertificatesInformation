﻿var mainContainerId = '#main-content';

function sendFormRequest(url, formId, method, replaceIntoId, promiseArray, parameterArray) {
    var data = $(formId).serialize();

    console.log(data);
    $.ajax({
        type: method,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            SetHtml(replaceIntoId, data);
            if(promiseArray!=undefined){
                if(parameterArray!=undefined){
                    for(var i = 0; i< promiseArray.length; i++){
                        promiseArray[i](parameterArray[i]);
                    }
                }
                else{
                    for(var i = 0; i< promiseArray.length; i++){
                        promiseArray[i]();
                    }
                }
            }
        },
        error: function (data) {
            console.log("error") 
               }
    });
}

function sendIdRequest(url, id, method, replaceIntoId, promiseArray, parameterArray) {
    url = url + '?id=' + id;
        $.ajax({
            type: method,
            cache: false,
            url: url,
            success: function (data) {
                SetHtml(replaceIntoId, data);
                if(promiseArray!=undefined){
                    if(parameterArray!=undefined){
                        for(var i = 0; i< promiseArray.length; i++){
                            promiseArray[i](parameterArray[i]);
                        }
                    }
                    else{
                        for(var i = 0; i< promiseArray.length; i++){
                            promiseArray[i]();
                        }
                    }
                }
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
            SetHtml(replaceIntoId, data);
            if(promiseArray!=undefined){
                if(parameterArray!=undefined){
                    for(var i = 0; i< promiseArray.length; i++){
                        promiseArray[i](parameterArray[i]);
                    }
                }
                else{
                    for(var i = 0; i< promiseArray.length; i++){
                        promiseArray[i]();
                    }
                }
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
    funcs.push(BindDateTimePickers);
    funcs.push(toggleFormModal);
    sendIdRequest('/MedicalCertificate/Create', id, "GET", "#formModal", funcs);
}

 function SendCreateMedicalCertificateRequest() {
    var funcs = [];
    funcs.push(BindDateTimePickers)
    funcs.push(ExecUpdateActionAction);
   sendFormRequest('/MedicalCertificate/Create', '#createMedicalCertificateForm', 'POST', "#formModal", funcs);
}

function GetEditMedicalCertificateRequest(id) {
    var funcs = new Array();
    funcs.push(BindDateTimePickers);
    funcs.push(toggleFormModal)
    sendIdRequest('/MedicalCertificate/Edit', id, "GET", "#formModal", funcs);
}

 function SendEditMedicalCertificateRequest() {
    var funcs = new Array();
    funcs.push(BindDateTimePickers);
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

//Admin funcs

function GetCreateAccountRequest() {
    var funcs = new Array();
    funcs.push(toggleFormModal);
   sendRequest('/Admin/Register', "GET", "#formModal", funcs);
}

function SendCreateAccountRequest() {
    sendFormRequest('/Admin/Register', '#registerAccountForm', 'POST', "#formModal");
}

//Other
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


//

function BindDateTimePickers() {
    var bindDatePicker = function () {
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
            // check if the date is correct. We can accept dd-mm-yyyy and yyyy-mm-dd.
            // update the format if it's yyyy-mm-dd
            var date = parseDate($(this).val());

            //if (!isValidDate(date)) {
            //    //create date based on momentjs (we have that)
            //    date = moment().format('DD.MM.YYYY');
            //}

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