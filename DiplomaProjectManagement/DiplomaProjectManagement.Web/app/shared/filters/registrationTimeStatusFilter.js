(function (app) {
    app.filter('registrationTimeStatusFilter', function () {
        return function (input) {
            if (input == 0) {
                return "Mở cho sinh viên đăng ký";
            } else if (input == 1) {
                return "Giáo viên chấm điểm";
            } else {
                return "Đóng";
            }
        }
    });
})(angular.module('application.common'));