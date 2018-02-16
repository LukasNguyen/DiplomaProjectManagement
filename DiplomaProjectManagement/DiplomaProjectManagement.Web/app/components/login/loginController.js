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
                            notificationService.displayError("Tài khoản hoặc mật khẩu không chính xác.");
                        if (response.data.error == "invalid_role")
                            notificationService.displayError("Tài khoản không có quyền admin để truy cập hệ thống này.");
                    }
                    else {
                        notificationService.displayInfo("Đăng nhập thành công. Chào mừng đến hệ thống của Admin.");
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('application'));