var MenuController = function ($rootScope, $scope, $mdSidenav) {
    this.items = [];
    if ($rootScope.IsUserLogged === false) {
        this.items.push(MenuItem("Strona główna", "#/", "home"));
        this.items.push(MenuItem("Logowanie", "/", "home"));
        this.items.push(MenuItem("Rejestracja", "/", "home"));
    } else{
        this.items.push(MenuItem("Strona główna", "#/", "home"));
        this.items.push(MenuItem("Znajomi", "/friends", "group"));
        this.items.push(MenuItem("Moje posty", "/myposts", "note"));
        this.items.push(MenuItem("Dodaj post", "#/addpost", "note_add"));
        this.items.push(MenuItem("Zmiana zasięgu", "#/range", "gps_fixed"));
        this.items.push(MenuItem("Ustawienia konta", "#/settings", "settings"));
        this.items.push(MenuItem("Wyloguj się", "/", "power_settings_new"));
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