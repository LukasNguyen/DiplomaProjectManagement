(function (app) {
    app.filter('registrationTimeStatusFilter', function () {
        return function (input) {
            if (input == 0) {
                return "Opening";
            } else if (input == 1) {
                return "Teacher assigning grades";
            } else {
                return "Closed";
            }
        }
    });
})(angular.module('application.common'));