var DiplomaProjectRegistrationController = function () {
    this.initialize = function () {
        loadData();
        assignGrades();
    }

    function assignGrades() {

        $('#btnAssignGrades').off('click').on('click', function () {

            var listStudentToAssignGrades = [];

            $.each($('.introduced-grades-input'), function(i, item) {
                    listStudentToAssignGrades.push({
                        DiplomaProjectId: $(item).data('dpid'),
                        StudentId: $(item).data('sid'),
                        RegistrationTimeId: $(item).data('rid'),
                        IntroducedGrades: $(item).val()
                    });
            });

            $.each($('.reviewed-grades-input'), function (i, item) {
                listStudentToAssignGrades[i].ReviewedGrades = $(item).val();
            });

            console.log(listStudentToAssignGrades);
            $.ajax({
                type: 'POST',
                url: '/DiplomaProjectRegistration/AssignGrades',
                data: {
                    viewModel: JSON.stringify(listStudentToAssignGrades)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status === 0) {
                        toastr.success('Chấm điểm sinh viên thành công');
                        loadData();
                    } else if (response.status === 1) {
                        toastr.error('Điểm nhập phải trong khoảng từ 0 đến 10');
                    }else if (response.status === 2){
                        toastr.error('Điểm nhập không đúng định dạng số');
                    }else {
                        toastr.error('Chấm điểm sinh viên thất bại');
                    }
                },
                error: function (status) {
                    toastr.error('Chấm điểm sinh viên thất bại');
                }
            });
        });
    }

    function loadData() {

        $.ajax({
            type: 'GET',
            url: '/DiplomaProjectRegistration/GetRegistrationTimes',
            dataType: 'json',
            success: function (response) {

                if (response.length === 0) {
                    toastr.error('Không có dữ liệu.');
                    return;
                }

                $.each(response.data, function (i, item) {
                    $('#selectorRegistrationTime')
                        .append($("<option></option>")
                            .attr("value", item.ID)
                            .text(item.Name));
                });

                var selectedValue = $("#selectorRegistrationTime option:selected").val();
                getStudentToAssignGrades(selectedValue);

                $('#selectorRegistrationTime').on('change', function () {
                    getStudentToAssignGrades(this.value);
                })

            },
            error: function (status) {
                console.log(status);
                toastr.error('Không có dữ liệu.');
            }
        });
    }

    function getStudentToAssignGrades(selectedValue) {

        var template = $('#table-template').html();
        var render = "";

        $.ajax({
            type: 'GET',
            url: '/DiplomaProjectRegistration/GetStudentToAssignGrades',
            data: {
                registrationTimeId: selectedValue
            },
            dataType: 'json',
            success: function (response) {
                common.startLoading();

                if (response.data.length === 0) {
                    $('#tbl-content').html(
                        '<td colspan="5" style="height:30px; text-align:center"><strong>Không có dữ liệu.</strong></td>');
                    $('#btnAssignGrades').hide();
                    common.stopLoading();
                    return;
                }

                $.each(response.data, function (i, item) {
                    render += Mustache.render(template,
                        {
                            StudentId: item.StudentId,
                            StudentName: item.StudentName,
                            IntroducedGrades: item.IntroducedGrades,
                            ReviewedGrades: item.ReviewedGrades,
                            DiplomaProjectName: item.DiplomaProjectName,
                            RegistrationTimeId: item.RegistrationTimeId,
                            DiplomaProjectId: item.DiplomaProjectId
                        });

                    if (render != '') {
                        $('#tbl-content').html(render);

                        if (item.RegistrationStatus === 1) {
                            $('#btnAssignGrades').show();
                        } else {

                            $('#btnAssignGrades').hide();

                            if (item.RegistrationStatus === 2) {
                                $('.introduced-grades').html(item.IntroducedGrades);
                                $('.reviewed-grades').html(item.ReviewedGrades);
                            } else {
                                $('.introduced-grades').html('Chưa thể cập nhật');
                                $('.reviewed-grades').html('Chưa thể cập nhật');
                            }
                        }

                        common.stopLoading();
                    }
                });
            },
            error: function (status) {
                console.log(status);
                toastr.error('Không có dữ liệu.');
            }
        });
    }
}