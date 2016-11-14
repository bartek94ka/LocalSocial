var PostService = function ($http) {

    this.getAll = function () {
        var resp = $http.get('api/posts');
        return resp;
    };

    this.addPost = function (data) {
        console.log(data);
        var resp = $http({
            url: "/api/posts",
            method: "POST",
            data: { Title: data.Title, Description: data.Description, Latitude: data.Latitude, Longitude: data.Longitude },
            headers: { 'Content-Type': 'application/json' }
        });
        return resp;
    };
}