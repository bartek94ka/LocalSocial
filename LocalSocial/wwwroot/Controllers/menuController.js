var MenuController = function () {
    this.items = [];
    this.items.push(MenuItem("Strona główna", "/"));
    this.items.push(MenuItem("Znajomi", "/friends"));
    this.items.push(MenuItem("Moje posty", "/myposts"));
    this.items.push(MenuItem("Zmiana zasięgu", "/range"));
    this.items.push(MenuItem("Ustawienia konta", "/settings"));
};
MenuItem = function (text, link) {
    return {
        LinkText: text,
        LinkReference: link,
    };
}