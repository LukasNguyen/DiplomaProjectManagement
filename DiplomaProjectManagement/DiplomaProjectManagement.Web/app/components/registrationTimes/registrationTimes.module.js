(function () {
    angular.module('application.registrationTimes', ['application.common']).config(config);

    config.$inject = ['$stateProvider'];

    function config($stateProvider) {
        $stateProvider.state('registrationTimes',
            {
                url: '/registrationTimes',
                templateUrl: '/app/components/registrationTimes/registrationTimeListView.html',
                parent: 'base',
                controller: 'registrationTimeListController'
            }).state('registrationTime_add',
            {
                url: '/registrationTime_add',
                templateUrl: '/app/components/registrationTimes/registrationTimeAddView.html',
                parent: 'base',
                controller: 'registrationTimeAddController'
            }).state('registrationTime_edit',
            {
                url: '/registrationTime_edit/:id',
                templateUrl: '/app/components/registrationTimes/registrationTimeEditView.html',
                parent: 'base',
                controller: 'registrationTimeEditController'
            });
    }
})();