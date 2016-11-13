//2.
var SessionController = function ($scope, $cookieStore, $rootScope, SessionService) {
    
    //my declarations
    $scope.response = "";
    $scope.Email = "";
    $scope.Password = "";
    $scope.ConfirmPassword = "";

    $scope.ai_user = "";
    $scope.UPLzPa_G_hg = "";
    $scope.ai_session = "";

    //Function to logout user
    $scope.logoff = function () {
        var promiselogoff = SessionService.logoff();
        $rootScope.IsUserLogged = false;
    };

    //Function to Login. This will generate Token 
    $scope.login2 = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            Email: $scope.Email,
            Password: $scope.Password
        };

        var promiselogin = SessionService.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.Email = resp.data.Email;
            console.log($rootScope.IsUserLogged);
            $rootScope.IsUserLogged = true;
            console.log($rootScope.IsUserLogged);
        }, function (err) {

            $scope.response = "Error " + err.status;
            $rootScope.IsUserLogged = false;
        });
        //window.location.href = "https://www.google.pl/";
    };

    //Function to register user
    $scope.registerUser2 = function () {

        $scope.response = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.Email,
            Password: $scope.Password,
            ConfirmPassword: $scope.ConfirmPassword
        };

        var promiseregister = SessionService.register(userRegistrationInfo);

        promiseregister.then(function (resp) {

            $scope.Email = resp.data.Email;
            $rootScope.IsUserLogged = true;
        }, function (err) {

            $scope.response = "Error " + err.status;
            $rootScope.IsUserLogged = false;
        });
    };

};
