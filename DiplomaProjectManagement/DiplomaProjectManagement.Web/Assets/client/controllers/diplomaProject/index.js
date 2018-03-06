var DiplomaProjectController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            loadData(true);
        });
        $('#btnDiplomaProjectSearch').off('click').on('click', function () {
            loadData();
        });

        $('body').on('click', '#btnDeleteDiplomaProject', function (e) {
            e.preventDefault();

            var diplomaProjectId = $(this).data("id");
            var diplomaProjectName = $(this).data("name");
            $('#diplomaProjectModalId').html(diplomaProjectId);
            $('#diplomaProjectModalName').html(diplomaProjectName);
            $('#deleteDiplomaProjectModal').modal('show');
        });
        $('body').on('click', '#btnAcceptDeleteDiplomaProject', function (e) {
            e.preventDefault();
            $('#deleteDiplomaProjectModal').modal('hide');
            $.ajax({
                type: 'POST',
                url: '/DiplomaProject/Delete',
                data: {
                    id: $('#diplomaProjectModalId').text()
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        common.notify('Xóa đồ án thành công', 'success');
                        loadData();
                    } else {
                        common.notify('Xóa đồ án thất bại', 'error');
                    }
                },
                error: function (status) {
                    common.notify('Xóa đồ án thất bại', 'error');
                }
            });
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/DiplomaProject/GetDiplomaProjectPagination',
            data: {
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize,
                keyword: $('#txtKeyword').val()
            },
            dataType: 'json',
            success: function (response) {
                common.startLoading();

                if (response.data.Items.length == 0) {
                    $('#tbl-content').html('<td colspan="4" style="height:30px; text-align:center"><strong>Không có dữ liệu.</strong></td>');
                    $('#lblTotalRecords').text(response.data.TotalCount);
                    common.stopLoading();
                    return;
                }

                $.each(response.data.Items, function (i, item) {
                    render += Mustache.render(template,
                        {
                            ID: item.ID,
                            Name: item.Name,
                            Description: item.Description,
                            NumberOfStudentsRegistered: item.NumberOfStudentsRegistered
                        });

                    $('#lblTotalRecords').text(response.data.TotalCount);

                    if (render != '') {
                        $('#tbl-content').html(render);
                        common.stopLoading();
                    }
                    wrapPaging(response.data.TotalPages, function () {
                        loadData();
                    }, isPageChanged);
                });
            },
            error: function (status) {
                console.log(status);
                common.notify('Không có dữ liệu.', 'error');
            }
        });
    }
    function wrapPaging(totalPages, callBack, changePageSize) {
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalPages,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                common.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}