'use strict';

angular.module('diag', [
    'ngRoute',
    'ngAnimate',
    'main']);

angular.module('diag')
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.otherwise({ redirectTo: '/' });
}]);

angular.module('diag')
    .run(['$rootScope', '$log', function ($rootScope, $log) {
        $rootScope.$log = $log;
    }]);

angular.module('diag')
    .controller('AppCtrl', function () {

        
    }
);