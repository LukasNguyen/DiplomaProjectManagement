(function (app) {
    app.controller('facilityAddController', facilityAddController);
    facilityAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function facilityAddController($scope, apiService, notificationService, $state) {

        $scope.facility = {
            Status: true
        };
        $scope.addFacility = addFacility;

        function addFacility() {

            apiService.post('/api/facilities/create', $scope.facility, (result) => {
                notificationService.displaySuccess('Khoa: ' + result.data.Name + ' được thêm vào hệ thống.');
                $state.go('facilities');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }
    }
})(angular.module('application.facilities'));