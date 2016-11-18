//2.
var SessionController = function ($scope, $cookieStore, $rootScope, $localStorage, SessionService) {
    
    //my declarations
    $scope.response = "";
    $scope.Email = "";
    $scope.Password = "";
    $scope.ConfirmPassword = "";

    //Function to logout user
    $scope.logout = function () {
        var promiselogoff = SessionService.logout();
        $localStorage.$reset();
        window.location.href = "#/";
        window.location.reload();
    };
    $scope.login = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            Email: $scope.Email,
            Password: $scope.Password
        };

        var promiselogin = SessionService.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.Email = resp.data.Email;
            $localStorage.IsLogged = true;
            window.location.href = "#/myposts";
            //window.location.reload();
        }, function (err) {
            $scope.response = "Error " + err.status;
            console.log('error');
            console.log(promiselogin);
        });
        //window.location.href = "https://www.google.pl/";
    };
    //Function to register user
    $scope.registerUser = function () {

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
            $localStorage.IsLogged = true;
            window.location.href = "#/myposts";
        }, function (err) {

            $scope.response = "Error " + err.status;
            console.log('error');
            console.log(promiseregister.login);
        });
    };
};
