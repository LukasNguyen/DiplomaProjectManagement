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
                        '<td colspan="6" style="height:30px; text-align:center"><strong>Không có dữ liệu.</strong></td>');
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
                            FinalGrades: (item.IntroducedGrades != null && item.ReviewedGrades != null) ? (parseFloat(item.IntroducedGrades) * 0.6 + parseFloat(item.ReviewedGrades) * 0.4).toFixed(1) : null,
                            DiplomaProjectName: item.DiplomaProjectName,
                            RegistrationTimeId: item.RegistrationTimeId,
                            DiplomaProjectId: item.DiplomaProjectId,
                            TeamName: (item.TeamName != null) ? item.TeamName : "Không có"
                        });

                    if (render != '') {
                        $('#tbl-content').html(render);
                        common.stopLoading();
                    }
                });

                var registrationTimeStatus = response.data[0].RegistrationStatus;
                displayGradesByRegistrationTimeStatus(registrationTimeStatus);
            },
            error: function (status) {
                console.log(status);
                toastr.error('Không có dữ liệu.');
            }
        });
    }

    function displayGradesByRegistrationTimeStatus(registrationTimeStatus) {
        switch (registrationTimeStatus) {
            case 0:
                $('#btnAssignGrades').hide();
                $('.introduced-grades').html('Chưa thể cập nhật');
                $('.reviewed-grades').html('Chưa thể cập nhật');
                $('.final-grades').html('Chưa thể cập nhật');
                break;
            case 1:
                $('#btnAssignGrades').show();
                break;
            case 2:
                $('#btnAssignGrades').hide();
                $.each($('.introduced-grades'), function (i, item) {
                    var introducedGrades = $(this).find(':input')[0].value;
                    if (introducedGrades === "") {
                        $(this).html(0);
                    } else {
                        $(this).html(introducedGrades);
                    }
                });
                $.each($('.reviewed-grades'), function () {
                    var reviewedGrades = $(this).find(':input')[0].value;
                    if (reviewedGrades === "") {
                        $(this).html(0);
                    } else {
                        $(this).html(reviewedGrades);
                    }
                });
                $.each($('.final-grades'), function (i, item) {
                    if (item.innerHTML === "") {
                        item.innerHTML = 0;
                    }
                });
                break;
        }
    }
}