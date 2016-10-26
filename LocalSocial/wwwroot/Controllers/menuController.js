var MenuController = function () {
    this.items = [];
    this.items.push(MenuItem("Strona główna", "/", "home"));
    this.items.push(MenuItem("Znajomi", "/friends", "group"));
    this.items.push(MenuItem("Moje posty", "/myposts", "note"));
    this.items.push(MenuItem("Dodaj post", "/addpost", "note_add"));
    this.items.push(MenuItem("Zmiana zasięgu", "/range", "gps_fixed"));
    this.items.push(MenuItem("Ustawienia konta", "/settings", "settings"));
    this.items.push(MenuItem("Wyloguj się", "/", "power_settings_new"));
};
MenuItem = function (text, link, icon) {
    return {
        LinkText: text,
        LinkReference: link,
        LinkIcon: icon,
    };
}