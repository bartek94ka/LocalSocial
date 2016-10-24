var PostsController = function ($scope, $http) {
    $http.get('api/post')
        .success(function (data, status, headers, config) {
            $scope.postsList = data;
        })
        .error(function (data, status, headers, config) {
            //log error
        });

}
