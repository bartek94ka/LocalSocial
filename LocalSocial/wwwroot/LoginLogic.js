//1.
app.service('loginservice', function ($http) {

    this.register = function (userInfo) {
        var resp = $http({
            url: "/api/test/register",
            method: "POST",
            data: { Email: userInfo.Email, Password: userInfo.Password, ConfirmPassword: userInfo.ConfirmPassword},
            headers: { 'Content-Type': 'application/json' },
        });
        return resp;
    };

    this.login = function (userlogin) {

        var resp = $http({
            url: "/api/test/login",
            method: "POST",
            data: { Email: userlogin.Email, Password: userlogin.Password },
            headers: { 'Content-Type': 'application/json' },
        });
        return resp;
    };

    this.logoff = function() {
        var resp = $http({
            url: "/api/test/logoff",
            method: "POST",
        });
    }
});
//2.
app.controller('logincontroller', function ($scope, loginservice) {

    //my declarations
    $scope.responseData2 = "";
    $scope.Email = "";
    $scope.Password = "";
    $scope.ConfirmPassword = "";

    $scope.ai_user = "";
    $scope.UPLzPa_G_hg = "";
    $scope.ai_session = "";

    //Function to logout user
    $scope.logoff = function() {
        var promiselogoff = loginservice.logoff();
        
    };

    //Function to Login. This will generate Token 
    $scope.login2 = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            Email: $scope.Email,
            Password: $scope.Password
        };

        var promiselogin = loginservice.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.Email = resp.data.Email;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            
            sessionStorage.setItem('ai_user', resp.data.ai_user);
            sessionStorage.setItem('UPLzPa_G_hg', resp.data.UPLzPa_G_hg);
            sessionStorage.setItem('ai_session', resp.data.ai_session);
        }, function (err) {

            $scope.responseData2 = "Error " + err.status;
        });
        //window.location.href = "https://www.google.pl/";
    };

    //Function to register user
    $scope.registerUser2 = function () {

        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.Email,
            Password: $scope.Password,
            ConfirmPassword: $scope.ConfirmPassword
        };

        var promiseregister = loginservice.register(userRegistrationInfo);

        promiseregister.then(function (resp) {

            $scope.Email = resp.data.Email;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('ai_user', resp.data.ai_user);
            sessionStorage.setItem('UPLzPa_G_hg', resp.data.UPLzPa_G_hg);
            sessionStorage.setItem('ai_session', resp.data.ai_session);
        }, function (err) {

            $scope.responseData2 = "Error " + err.status;
        });
    };

    

    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "";
    $scope.userRegistrationPassword = "";
    $scope.userRegistrationConfirmPassword = "";

    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here

    //Function to register user
    $scope.registerUser = function () {

        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.userRegistrationEmail,
            Password: $scope.userRegistrationPassword,
            ConfirmPassword: $scope.userRegistrationConfirmPassword
        };

        var promiseregister = loginservice.register(userRegistrationInfo);

        promiseregister.then(function (resp) {
            $scope.responseData = "User is Successfully";
            $scope.userRegistrationEmail = "";
            $scope.userRegistrationPassword = "";
            $scope.userRegistrationConfirmPassword = "";
        }, function (err) {
            $scope.responseData = "Error " + err.status;
        });
    };


    $scope.redirect = function () {
        window.location.href = '/Employee/Index';
    };

    //Function to Login. This will generate Token 
    $scope.login = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = loginservice.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.userName = resp.data.userName;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/Employee/Index';
        }, function (err) {

            $scope.responseData = "Error " + err.status;
        });

    };
});