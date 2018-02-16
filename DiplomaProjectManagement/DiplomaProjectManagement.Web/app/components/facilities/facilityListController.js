(function (app) {
    app.controller('facilityListController', facilityListController);
    facilityListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function facilityListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        //Phân trang
        $scope.facilities = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getFacilities = getFacilities;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteFacility = deleteFacility;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        //Delete all item that you selected
        function deleteMultiple() {

            var listId = [];
            $.each($scope.selected, function (index, item) {
                listId.push(item.ID);
            });

            var config = {
                params: {
                    checkedFacilities: JSON.stringify(listId)
                }
            };
            apiService.del('/api/facilities/deletemulti',
                config,
                (result) => {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' khoa');
                    search();
                },
                (failure) => {
                    notificationService.displayError('Xóa các khoa trên thất bại');
                });
        }

        //Select all item that you would like to delete
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.facilities, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.facilities, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //Button delete multiple only enable when user have selected students
        $scope.$watch("facilities", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteFacility(id) {
            $ngBootbox.confirm('Bạn có thật sự muốn xóa khoa này không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };

                apiService.del('/api/facilities/delete',
                    config,
                    (success) => {
                        notificationService.displaySuccess('Xóa khoa thành công');
                        search();
                    },
                    (failure) => {
                        notificationService.displayError('Xóa khoa thất bại');
                    });
            });
        }

        function search() {
            getFacilities();
        }

        function getFacilities(page) {

            page = page || 0;

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }

            apiService.get('/api/facilities/getall', config, (result) => {

                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không tìm thấy khoa nào trong hệ thống');

                $scope.facilities = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, (failure) => {
                console.log('Load danh sách khoa thất bại');
            });
        }

        $scope.getFacilities();
    }
})(angular.module('application.facilities'));