$(function () {

    $("#tree").fancytree({
        extensions: ["edit", "filter"],
        source: {
            url: "/Tree/GetManagementHierarchy",
            cache: false
        },
        expanded: true,
        clickFolderMode: 1,
        click: function (event, data) {
            var node = data.node;
            if (node.isActive()) {
                node.setFocus(false);
                node.setActive(false);
                event.preventDefault();
                var role = node.data.userRole;
                DisableDeleteBtn();
                DisableEditBtn();
                DisableMoveBtn();
                DisableCreateBtn();
                if (role == 'admin') {
                    EnableCreateBtn();
                }

            }

        },
        activate: function (event, data) {
            var tree = $('#tree').fancytree('getTree');
            activeNode = tree.getActiveNode();
            var type = activeNode.type;
            var role = activeNode.data.userRole;
            var modelId = activeNode.data.modelId;

            SetButtonsAvailability(type, role);

        },
        dblclick: function (event, data) {
            // A node was activated: display its title:
            var node = data.node;
            var type = node.type;
            var role = node.data.userRole;
            var modelId = node.data.modelId;
            console.log(type);
            console.log(modelId);

            switch (type) {
                case 'department':
                    if (role === 'admin') {
                        GetDetailsDepartmentRequest(modelId);
                    }
                    break;
                case 'course':
                    if (role === 'admin') {
                        GetDetailsCourseRequest(modelId);
                    }
                    break;
                case 'group':
                    GetDetailsGroupRequest(modelId);
                    break;
                case 'student':
                    GetDetailsStudentRequest(modelId);
                    break;
                case 'certificate':
                    GetDetailsMedicalCertificateRequest(modelId);
                    break;
            }

        }
    });