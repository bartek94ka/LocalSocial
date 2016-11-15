var PostController = function ($scope, PostService) {
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
        description: ''
    };
    $scope.userPosts = [];
    $scope.GetMyPosts = function () {
        var promiseMyPost = PostService.getMyPost();

        promiseMyPost.then(function (resp) {
            console.log(resp.data);
            $scope.userPosts = resp.data;
            console.log($scope.userPosts);
        },function(err) {
                console.log('blad w getmyposts');
            }
        );

    };
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

//if (navigator.geolocation) {

//    $scope.myLocation = navigator.geolocation.getCurrentPosition(
//        function (position) {
//            console.log(position);
//            $scope.Lat = position.coords.latitude;
//            $scope.Lng = position.coords.longitude;
//        }
//    );
//}