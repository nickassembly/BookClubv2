var BookUser = /** @class */ (function () {
    function BookUser(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    BookUser.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
    };
    return BookUser;
}());
//# sourceMappingURL=bookuser.js.map