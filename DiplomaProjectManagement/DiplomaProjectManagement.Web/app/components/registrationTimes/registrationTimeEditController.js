(function (app) {
    app.controller('registrationTimeEditController', registrationTimeEditController);
    registrationTimeEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function registrationTimeEditController($scope, apiService, notificationService, $state, $stateParams) {

        $scope.registrationTime = {};
        $scope.updateRegistrationTime = updateRegistrationTime;

        function updateRegistrationTime() {
            apiService.put('/api/registrationTimes/update', $scope.registrationTime, (result) => {
                notificationService.displaySuccess('Đợt đăng ký đồ án : ' + result.data.Name + ' cập nhật thành công.');
                $state.go('registrationTimes');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function loadRegistrationTimeDetail() {
            apiService.get('/api/registrationTimes/getbyid/' + $stateParams.id, null, (result) => {
                $scope.registrationTime = result.data;
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }

        loadRegistrationTimeDetail();
    }
})(angular.module('application.registrationTimes'));