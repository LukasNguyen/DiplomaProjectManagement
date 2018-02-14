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
                notificationService.displaySuccess('Facility name: ' + result.data.Name + ' are added to system.');
                $state.go('facilities');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }
    }
})(angular.module('application.facilities'));