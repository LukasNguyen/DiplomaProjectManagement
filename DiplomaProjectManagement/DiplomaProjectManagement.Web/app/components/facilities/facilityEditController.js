(function (app) {
    app.controller('facilityEditController', facilityEditController);
    facilityEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function facilityEditController($scope, apiService, notificationService, $state, $stateParams) {

        $scope.facility = {};
        $scope.updateFacility = updateFacility;

        function updateFacility() {
            apiService.put('/api/facilities/update', $scope.facility, (result) => {
                notificationService.displaySuccess('Khoa: ' + result.data.Name + ' được cập nhật thông tin.');
                $state.go('facilities');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function loadFacilityDetail() {
            apiService.get('/api/facilities/getbyid/' + $stateParams.id, null, (result) => {
                $scope.facility = result.data;
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }

        loadFacilityDetail();
    }
})(angular.module('application.facilities'));