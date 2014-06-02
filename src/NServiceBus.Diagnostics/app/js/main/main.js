'use strict';

angular.module('main', [])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/', { templateUrl: '/js/main/main.tpl.html', controller: 'MainCtrl' });
    }])
    .controller('MainCtrl', function ($scope, $http) {

        $scope.model = [];
        
        $http.get('/api').then(function (response) {
            $scope.model = [{ title: "Incoming", steps: response.data.incoming }, { title: "Outgoing", steps: response.data.outgoing }];
        });
    });