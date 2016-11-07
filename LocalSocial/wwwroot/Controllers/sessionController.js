//2.
var SessionController = function ($scope, SessionService) {

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
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            console.print(resp);
            //sessionStorage.setItem('ai_user', resp.data.ai_user);
            //sessionStorage.setItem('UPLzPa_G_hg', resp.data.UPLzPa_G_hg);
            //sessionStorage.setItem('ai_session', resp.data.ai_session);
        }, function (err) {

            $scope.response = "Error " + err.status;
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
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('ai_user', resp.data.ai_user);
            sessionStorage.setItem('UPLzPa_G_hg', resp.data.UPLzPa_G_hg);
            sessionStorage.setItem('ai_session', resp.data.ai_session);
        }, function (err) {

            $scope.response = "Error " + err.status;
        });
    };

};
