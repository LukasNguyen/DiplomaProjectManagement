(function (app) {
    app.controller('registrationTimeAddController', registrationTimeAddController);
    registrationTimeAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$filter'];

    function registrationTimeAddController($scope, apiService, notificationService, $state, $filter) {

        $scope.registrationTime = {};
        $scope.RegisteredDate = undefined;
        $scope.TeacherAssignGradesDate = undefined;
        $scope.ClosedDate = undefined;

    $scope.addRegistrationTime = addRegistrationTime;

        function addRegistrationTime() {
            assignDataToModel();
            apiService.post('/api/registrationTimes/create', $scope.registrationTime, (result) => {
                notificationService.displaySuccess('Đợt đăng ký đồ án: ' + result.data.Name + ' được thêm vào hệ thống.');
                $state.go('registrationTimes');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function assignDataToModel() {
            $scope.registrationTime.RegisteredDate = $filter('date')($scope.RegisteredDate, 'MM/dd/yyyy');
            $scope.registrationTime.TeacherAssignGradesDate = $filter('date')($scope.TeacherAssignGradesDate, 'MM/dd/yyyy');
            $scope.registrationTime.ClosedDate = $filter('date')($scope.ClosedDate, 'MM/dd/yyyy');
        }
    }
})(angular.module('application.registrationTimes'));