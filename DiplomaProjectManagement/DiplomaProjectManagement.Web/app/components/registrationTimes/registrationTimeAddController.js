(function (app) {
    app.controller('registrationTimeAddController', registrationTimeAddController);
    registrationTimeAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function registrationTimeAddController($scope, apiService, notificationService, $state) {

        $scope.registrationTime = {
        };
        $scope.addRegistrationTime = addRegistrationTime;

        function addRegistrationTime() {

            apiService.post('/api/registrationTimes/create', $scope.registrationTime, (result) => {
                notificationService.displaySuccess('Đợt đăng ký đồ án: ' + result.data.Name + ' được thêm vào hệ thống.');
                $state.go('registrationTimes');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }
    }
})(angular.module('application.registrationTimes'));