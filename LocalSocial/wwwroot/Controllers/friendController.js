var FriendController = function ($scope, FriendService) {

    $scope.users = [];

    $scope.userPosts = [];

    $scope.user = {
        Name: '',
        Surname: '',
        Email: '',
    };
    $scope.findFriend = function() {
        var promise = FriendService.FindFriend($scope.user);
        promise.then(function(resp) {
                $scope.users = resp.data;
            },
            function(err) {
                console.log("blad w findFriend");
            });
    };

    $scope.addFriend = function (userItem) {
        var promise = FriendService.AddFriend(userItem);

        promise.then(function (succes) {
                $scope.users.splice($scope.users.findIndex(x => x.Email === userItem.Email), 1);
            },
            function(err) {

            });
    };
    $scope.removeFriend = function(userItem) {
        var promise = FriendService.RemoveFriend(userItem);

        promise.then(function (succes) {
            $scope.users.splice($scope.users.findIndex(x => x.Email === userItem.Email), 1);
        },
            function (err) {

            });
    };
    $scope.getMyFriends = function() {
        var promiseFriends = FriendService.GetMyFriends();

        promiseFriends.then(function(resp) {
                $scope.users = resp.data;
            },
            function(err) {
                console.log('error');
            });
    };
    $scope.getMyFriendsPosts = function() {
        var promisePosts = FriendService.GetMyFriendsPosts();

        promisePosts.then(function(resp) {
            $scope.userPosts = resp.data;
            console.log(resp.data);
        },function(err) {
            console.log('blad w getmyfriendsposts');
        });
    };
};