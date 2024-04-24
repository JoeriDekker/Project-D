"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UsersController = void 0;
// SKkkrrrt
class UsersController {
    constructor(route, usersRepo, logger) {
        this.route = route;
        this.usersRepo = usersRepo;
        this.logger = logger;
    }
    registerEndpoins(app) {
        // Register the endpoints
        app.get(this.route + "/", this.getUsers);
    }
    getUsers(req, res) {
        res.status(200).send({ message: "lol" });
    }
}
exports.UsersController = UsersController;
