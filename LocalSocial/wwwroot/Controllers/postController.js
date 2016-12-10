var PostController = function ($scope,$routeParams, PostService, UserService) {
    $scope.Lat = null;
    $scope.Lng = null;
    $scope.post = {
        userName: "",
        userSurname: "",
        title: '',
        description: '',
        Id: '',
        Comments: [],
    };
    $scope.UserId = "";
    $scope.comment = {
        Content: '',
        PostId: ''
    }
    $scope.userPosts = [];
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
    $scope.GetPosts= function () {
        if (navigator.geolocation) {

            $scope.myLocation = navigator.geolocation.getCurrentPosition(
                function (position) {

                    $scope.Lat = position.coords.latitude;
                    $scope.Lng = position.coords.longitude;
                    $scope.GetPostFromRange();
                    $scope.$digest();
                }
            );
        }
    };
    $scope.GetPostFromRange = function () {
        
        var locationData = {
            Longitude: $scope.Lng,
            Latitude: $scope.Lat
        };

        var promisePosts = PostService.getPostsFromRange(locationData);

        promisePosts.then(function(resp) {
            $scope.userPosts = resp.data;
            },
            function(err) {
                console.log('error w getpostfromrange');
            });
    };
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
                window.location.href = "#/myposts";
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
                $scope.post.Comments = resp.data.Comments;
                $scope.post.userName = resp.data.user.Name;
                $scope.post.userSurname = resp.data.user.Surname;
                console.log(resp);
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
            window.location.href = "#/myposts";
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
            window.location.href = "#/myposts";
        }, function(err) {
            //message z bledem
        });
    };
    $scope.AddComment = function () {
        var commentData = {
            Content: $scope.comment.Content,
            PostId: $scope.comment.PostId,
        }
        var promiseComment = PostService.addComment(commentData)

        promiseComment.then(function (resp) {
            window.location.href = "#/posts/"  + $scope.comment.PostId;
        }, function (err) {

        });
    };
};