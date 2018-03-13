var DiplomaProjectRegistrationController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {

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

                $.each(response.data, function(i, item) {
                    $('#selectorRegistrationTime')
                        .append($("<option></option>")
                            .attr("value", item.ID)
                            .text(item.Name));
                });

                var selectedValue = $("#selectorRegistrationTime option:selected").val();
                alert(selectedValue);

            },
            error: function (status) {
                console.log(status);
                toastr.error('Không có dữ liệu.');
            }
        });
    }
}