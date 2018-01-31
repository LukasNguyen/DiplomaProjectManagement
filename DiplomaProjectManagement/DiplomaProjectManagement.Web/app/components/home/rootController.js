(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        console.log(authenticationService.getTokenInfo());
        console.log(authData);
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;
        $scope.sideBar = "/app/shared/views/sideBar.html";


        //authenticationService.validateRequest();
    }
})(angular.module('application'));