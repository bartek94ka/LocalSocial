(function($scope) {
    'use strict'
    var app = angular.module('StarterApp', ['ngMaterial', 'ngRoute', 'ngCookies']);

    SessionService.$inject = ['$http', '$cookies'];
    app.service('SessionService', SessionService);

    //SessionController.$inject = ['$scope']

    app.controller('AppCtrl',
    [
        '$scope', '$mdSidenav', function($scope, $mdSidenav) {
            $scope.toggleSidenav = function(menuId) {
                $mdSidenav(menuId).toggle();
            };

            var list = [];
            for (var i = 0; i < 100; i++) {
                list.push({
                    name: 'List Item ' + i,
                    idx: i
                });
            }
            $scope.list = list;
            $scope.imagePath = 'img/small.png';
        }
    ]);
    app.controller('MenuController', MenuController);
    app.controller('ContentController', ContentController);
    app.controller('SessionController', SessionController);
    app.run(function ($rootScope) {
        $rootScope.IsUserLogged = false;
    });
    
    app.config(function ($routeProvider) {
        
        $routeProvider
            .when('/posts',
            {
                templateUrl: 'Views/Posts/index.html',
                controller: 'PostsController',
                controllerAs: 'postCtrl'
            })
            .when('/login',
            {
                templateUrl: 'Views/Login/index.html'
            })
            .when('/',
            {
                templateUrl: 'Views/Home/index.html'
            })
            .when('/home',
            {
                templateUrl: 'Views/Home/index.html'
            })
            //.otherwise({
            //    redirectTo: '/home'
            //});
    });
    
})();


