﻿var DiplomaProjectController = function () {
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

        $('#btnDiplomaProjectSearch').off('click').on('click',
            function () {
                loadData();
            });


        $('body').on('click', '#btnRegisterDiplomaProject', function (e) {
            e.preventDefault();

            var diplomaProjectId = $(this).data("id");
            var diplomaProjectName = $(this).data("name");
            var lecturerName = $(this).data("lecturer");

            $('#diplomaProjectModalId').html(diplomaProjectId);
            $('#diplomaProjectModalName').html(diplomaProjectName);
            $('#diplomaProjectModalLecturerName').html(lecturerName);
            $('#registerDiplomaProjectModal').modal('show');
        });

        $('body').on('click', '#btnAcceptRegisterDiplomaProject', function (e) {
            e.preventDefault();
            $('#registerDiplomaProjectModal').modal('hide');
            $.ajax({
                type: 'POST',
                url: '/DiplomaProject/Register',
                data: {
                    id: $('#diplomaProjectModalId').text()
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status === 0) {
                        toastr.success('Đăng ký đề tài thành công');
                        loadData();
                    } else if (response.status === 1) {
                        toastr.error('Đăng ký đề tài thất bại');
                        loadData();
                    } else {
                        toastr.error('Đăng ký đề tài thất bại do đề tài đã đủ số người đăng ký');
                        loadData();
                    }
                },
                error: function (status) {
                    toastr.error('Đăng ký đề tài thất bại');
                    loadData();
                }
            });
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/DiplomaProject/GetDiplomaProjectToRegisterPagination',
            data: {
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize,
                keyword: $('#txtKeyword').val()
            },
            dataType: 'json',
            success: function (response) {
                common.startLoading();

                if (!response.status) {
                    $('#tbl-content').html('<td colspan="5" style="height:30px; text-align:center"><strong>Bạn đã đạt đồ án, không thể đăng ký đề tài được nữa.</strong></td>');
                    $('#lblTotalRecords').text(0);
                    common.stopLoading();
                    return;
                }

                if (response.data.Items.length === 0) {
                    $('#tbl-content').html('<td colspan="5" style="height:30px; text-align:center"><strong>Không có dữ liệu.</strong></td>');
                    $('#lblTotalRecords').text(response.data.TotalCount);
                    common.stopLoading();
                    return;
                }

                $.each(response.data.Items, function (i, item) {
                    render += Mustache.render(template,
                        {
                            ID: item.ID,
                            Name: item.Name,
                            LecturerName: item.LecturerName,
                            Description: item.Description
                        });

                    $('#lblTotalRecords').text(response.data.TotalCount);

                    if (render !== '') {
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
                toastr.error('Không có dữ liệu.');
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