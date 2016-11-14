var UserController = function($scope, UserService) {
    $scope.Name = '';
    $scope.Surname = '';
    $scope.Email = '';
    $scope.OldPassword = '';
    $scope.NewPassword = '';
    $scope.ConfirmPassword = '';
    $scope.Message = '';

    $scope.SaveChanges = function ()
    {
        var UserData = {
            Name: $scope.Name,
            Surname: $scope.Surname
        };

        var promiseSave = UserService.UpdateData(UserData);

        promiseSave.then(function(resp) {
            $scope.Message = "Zapisano";
                console.log(resp);
            },
            function(err) {

            });
    };
    $scope.LoadData = function() {
        var promiseGet = UserService.GetData();

        promiseGet.then(function(resp) {
            console.log(resp);
                $scope.Name = resp.data.Name;
                $scope.Surname = resp.data.Surname;
            },
        function(err) {
            console.log('Blad w loaddata');
        });
    };
};