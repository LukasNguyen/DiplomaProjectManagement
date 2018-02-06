(function () {
    angular.module('application.facilities', ['application.common']).config(config);

    config.$inject = ['$stateProvider'];

    function config($stateProvider) {
        $stateProvider.state('facilities',
            {
                url: '/facilities',
                templateUrl: '/app/components/facilities/facilityListView.html',
                parent: 'base',
                controller: 'facilityListController'
            }).state('facility_add',
            {
                url: '/facility_add',
                templateUrl: '/app/components/facilities/facilityAddView.html',
                parent: 'base',
                controller: 'facilityAddController'
            }).state('facility_edit',
            {
                url: '/facility_edit/:id',
                templateUrl: '/app/components/facilities/facilityEditView.html',
                parent: 'base',
                controller: 'facilityEditController'
            });
    }
})();