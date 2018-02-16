(function (app) {
    app.controller('registrationTimeListController', registrationTimeListController);
    registrationTimeListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function registrationTimeListController($scope, apiService, notificationService) {

        //Phân trang
        $scope.registrationTimes = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getRegistrationTimes = getRegistrationTimes;
        $scope.search = search;
        $scope.keyword = '';

        function search() {
            getRegistrationTimes();
        }

        function getRegistrationTimes(page) {

            page = page || 0;

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }

            apiService.get('/api/registrationTimes/getall', config, (result) => {

                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không tìm thấy đợt đăng ký nào trong hệ thống');

                $scope.registrationTimes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, (failure) => {
                console.log('Load danh sách đợt đăng ký thất bại');
            });
        }

        $scope.getRegistrationTimes();
    }
})(angular.module('application.registrationTimes'));