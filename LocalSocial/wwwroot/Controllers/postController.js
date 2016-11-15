var PostController = function ($scope,$routeParams, PostService) {
    $scope.Lat = null;
    $scope.Lng = null;
    $scope.GetLocation = function() {
        if (navigator.geolocation) {

            $scope.myLocation = navigator.geolocation.getCurrentPosition(
                function (position) {
                    
                    $scope.Lat = position.coords.latitude;
                    $scope.Lng = position.coords.longitude;
                    $scope.$digest();
                }
            );
        }
    };

    $scope.post = {
        title: '',
        description: '',
        Id: '',
};
    $scope.userPosts = [];
    $scope.GetMyPosts = function () {

        var promiseMyPost = PostService.getMyPost();

        promiseMyPost.then(function (resp) {
            $scope.userPosts = resp.data;
        },function(err) {
                console.log('blad w getmyposts');
            }
        );

    };
    $scope.DeleteMyPost = function(Post) {
        var promiseDelete = PostService.deletePost(Post.Id);

        promiseDelete.then(function(resp) {

            },
            function(err) {

            });
    };
    $scope.GetPost = function () {
        var promisePost = PostService.getPost($routeParams.postId);

        promisePost.then(function(resp) {
                $scope.post.title = resp.data.Title;
                $scope.post.description = resp.data.Description;
                $scope.post.Id = resp.data.Id;
            },
            function(err) {

            });
    };
    $scope.SavePost = function () {
        var postData = {
            Title: $scope.post.title,
            Description: $scope.post.description,
            Latitude: 1.3,
            Longitude: 1.3,
        };
        var promisePost = PostService.editPost(postData, $scope.post.Id);
        promisePost.then(function (resp) {
            //zmiana okna na moje ogloszenia
        }, function (err) {
            //message z bledem
        });
    }
    $scope.AddPost = function() {
        var postData = {
            Title: $scope.post.title,
            Description: $scope.post.description,
            Latitude: $scope.Lat,
            Longitude: $scope.Lng,
        };

        var promisePost = PostService.addPost(postData);

        promisePost.then(function(resp) {
            //zmiana okna na moje ogloszenia
        }, function(err) {
            //message z bledem
        });
    };
};