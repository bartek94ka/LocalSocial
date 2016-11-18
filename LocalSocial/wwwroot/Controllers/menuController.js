var MenuController = function ($rootScope, $scope, $mdSidenav, $localStorage) {
    this.items = [];
    console.log($localStorage.IsLogged);
    if ($localStorage.IsLogged === true) {
        this.items.push(MenuItem("Strona główna", "#/", "home"));
        this.items.push(MenuItem("Znajdź znajomych", "#/findfriends", "group"));
        this.items.push(MenuItem("Znajomi", "#/friends", "group"));
        this.items.push(MenuItem("Posty znajomych", "#/friendsposts", "note"));
        this.items.push(MenuItem("Moje posty", "#/myposts", "note"));
        this.items.push(MenuItem("Dodaj post", "#/addpost", "note_add"));
        this.items.push(MenuItem("Ustawienia konta", "#/settings", "settings"));
        this.items.push(MenuItem("Wyloguj się", "#/logout", "power_settings_new"));
    } else {
        this.items.push(MenuItem("Strona główna", "#/", "home"));
        this.items.push(MenuItem("Logowanie", "#/login", "login"));
        this.items.push(MenuItem("Rejestracja", "#/register", "home"));
    }
    $scope.toggleSidenav = function (menuId) {
        $mdSidenav(menuId).toggle();
    };
};
MenuItem = function (text, link, icon) {
    return {
        LinkText: text,
        LinkReference: link,
        LinkIcon: icon,
    };
}