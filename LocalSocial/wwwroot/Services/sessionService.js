//1.
var SessionService = function ($http) {

    this.register = function (userInfo) {
        var resp = $http({
            url: "/api/test/register",
            method: "POST",
            data: { Email: userInfo.Email, Password: userInfo.Password, ConfirmPassword: userInfo.ConfirmPassword },
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

    this.logoff = function ($cookieStore) {
        var resp = $http({
            url: "/api/test/logoff",
            method: "POST",
        });
    }
};