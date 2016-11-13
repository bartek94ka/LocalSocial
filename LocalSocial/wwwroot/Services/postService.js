var PostService = function ($scope, $http) {

    this.getAll = function () {
        var resp = $http.get('api/post');
        return resp;
    };

    this.addPost = function(data) {
        var resp = $http({
            url: "/api/post",
            method: "POST",
            data: { Title: data.Title, Description: data.Description },
            headers: { 'Content-Type': 'application/json' },
        });
        return resp;
    };
}