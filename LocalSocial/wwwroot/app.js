(function($scope) {
    'use strict'
    var app = angular.module('StarterApp', ['ngMaterial', 'ngRoute', 'ngCookies', 'ngMessages']);

    //app.constructor(Auth, $state)
    //{
    //    this.Auth = Auth;
    //    this.$state = $state;
    //};

    SessionService.$inject = ['$http', '$cookies'];
    app.service('SessionService', SessionService);
    PostService.$inject = ['$http'];
    app.service('PostService', PostService);
    app.service('UserService', UserService);
    //LocationService.$inject = ['$window'];
    //app.service('LocationService', LocationService);
    UserService.$inject = ['$http'];
    

    app.controller('AppCtrl', SideNavController);
    app.controller('MenuController', MenuController);
    app.controller('ContentController', ContentController);
    app.controller('SessionController', SessionController);
    app.controller('PostController', PostController);
    app.controller('UserController', UserController);
    //app.controller('LocationController', LocationController);
    
    app.config(function ($routeProvider) {
        
        $routeProvider
            .when('/addpost',
            {
                templateUrl: 'Views/AddPost/index.html'
                //controller: 'postController',
                //controllerAs: 'postCtrl'
            })
            .when('/myposts',
            {
                templateUrl: 'Views/MyPosts/index.html',
                controller: 'PostController',
                resolve: PostController.GetMyPosts
            })
            .when('/range',
            {
                templateUrl: 'Views/UserLocation/index.html'
            })
            .when('/login',
            {
                templateUrl: 'Views/Login/index.html'
            })
            .when('/settings',
            {
                templateUrl: 'Views/Settings/index.html'
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


