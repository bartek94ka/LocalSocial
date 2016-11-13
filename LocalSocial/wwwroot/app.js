(function($scope) {
    'use strict'
    var app = angular.module('StarterApp', ['ngMaterial', 'ngRoute', 'ngCookies']);

    SessionService.$inject = ['$http', '$cookies'];
    app.service('SessionService', SessionService);

    //SessionController.$inject = ['$scope']

    app.controller('AppCtrl', SideNavController);
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
            .otherwise({
                redirectTo: '/home'
            });
    });
    
})();


