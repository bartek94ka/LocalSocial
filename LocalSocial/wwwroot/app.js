﻿(function($scope) {
    'use strict'
    var app = angular.module('StarterApp', ['ngMaterial', 'ngRoute', 'ngCookies', 'ngMessages', 'ngStorage']);


    SessionService.$inject = ['$http', '$cookies'];
    app.service('SessionService', SessionService);
    PostService.$inject = ['$http'];
    app.service('PostService', PostService);
    app.service('UserService', UserService);
    UserService.$inject = ['$http'];
    FriendService.$inject = ['$http'];
    app.service('FriendService', FriendService);
    

    app.controller('AppCtrl', SideNavController);
    app.controller('MenuController', MenuController);
    app.controller('SessionController', SessionController);
    app.controller('PostController', PostController);
    app.controller('UserController', UserController);
    app.controller('FriendController', FriendController);

    app.config(function ($routeProvider) {
        $routeProvider
            .when('/addcomment',
            {
                templateUrl: 'Views/AddComment/index.html'
            })
            .when('/addpost',
            {
                templateUrl: 'Views/AddPost/index.html'
                //controller: 'postController',
                //controllerAs: 'postCtrl'
            })
            .when('/editpost/:postId',
            {
                templateUrl: 'Views/EditPost/index.html'
            })
            .when('/myposts',
            {
                templateUrl: 'Views/MyPosts/index.html',
                controller: 'PostController',
                resolve: PostController.GetMyPosts
            })
            .when('/findfriend',
            {
                templateUrl: 'Views/FindFriend/index.html'
            })
            .when('/friends',
            {
                templateUrl: 'Views/Friends/index.html'
            })
            .when('/friendsposts',
            {
                templateUrl: 'Views/FriendsPosts/index.html'
            })
            .when('/range',
            {
                templateUrl: 'Views/UserLocation/index.html'
            })
            .when('/login',
            {
                templateUrl: 'Views/Login/index.html'
            })
            .when('/register',
            {
                templateUrl: 'Views/Register/index.html'
            })
            .when('/logout',
            {
                templateUrl: 'Views/Logout/index.html'
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


