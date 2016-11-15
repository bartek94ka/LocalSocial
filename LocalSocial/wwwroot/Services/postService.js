var PostService = function ($http) {

    this.getAll = function () {
        var resp = $http.get('api/posts/all');
        return resp;
    };

    this.addPost = function (data) {
        console.log(data);
        var resp = $http({
            url: "/api/posts/add",
            method: "POST",
            data: { Title: data.Title, Description: data.Description, Latitude: data.Latitude, Longitude: data.Longitude },
            headers: { 'Content-Type': 'application/json' }
        });
        return resp;
    };
    this.getMyPost = function() {
        var resp = $http({
            url: "/api/posts/myposts",
            method: "GET",
            headers: { 'Content-Type': 'application/json' }
        });
        return resp;
    };
}