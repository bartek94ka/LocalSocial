var UserService = function($http) {
    this.UpdateData = function(data) {
        var resp = $http({
            url: "/api/users",
            method: "PUT",
            data: { Name: data.Name, Surname: data.Surname, SearchRange: data.SearchRange },
            //data: { Name: data.Name, Surname: data.Surname, OldPassword: data.OldPassword, NewPassword: data.NewPassword },
            headers: { 'Content-Type': 'application/json' }
        });
        return resp;
    }
    this.GetData= function() {
        var resp = $http({
            url: "/api/users",
            method: "GET",
            headers: { 'Content-Type': 'application/json' }
        });
        return resp;
    }
};