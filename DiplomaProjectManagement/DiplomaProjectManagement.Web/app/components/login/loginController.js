(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {

                    if (response != null && response.data.error != undefined) {
                        if (response.data.error == "invalid_grant")
                            notificationService.displayError("Username or password isn't correctly. Please try again.");
                        if (response.data.error == "invalid_role")
                            notificationService.displayError("You haven't got administration permission to login to this page.");
                    }
                    else {
                        notificationService.displayInfo("Login Succeeded. Welcome to administration system.");
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('application'));