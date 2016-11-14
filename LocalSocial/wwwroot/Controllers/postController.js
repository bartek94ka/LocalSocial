var PostController = function ($scope, $window) {
    $scope.Lat = null;
    $scope.Lng = null;
    if (navigator.geolocation) {
        
        $scope.myLocation = navigator.geolocation.getCurrentPosition(
            function (position) {
                console.log(position);
                $scope.Lat = position.coords.latitude;
                $scope.Lng = position.coords.longitude;
            }
        );
    }
    $scope.post = {
        title: '',
        descryption: '',
    };
    $scope.addPost = function() {

    };
};
