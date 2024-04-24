"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.appInstance = void 0;
const express_1 = __importDefault(require("express"));
const dotenv_1 = __importDefault(require("dotenv"));
const cors_1 = __importDefault(require("cors"));
const logger_1 = __importDefault(require("./logger"));
const UsersController_1 = require("./controllers/UsersController");
// Loads the dotenv variables
dotenv_1.default.config();
const app = (0, express_1.default)();
const port = process.env.PORT || 5000;
// Allowed origins
const allowedOrigins = ['http://localhost:3000'];
/* Middlewares */
app.use((0, cors_1.default)({
    origin: allowedOrigins
}));
app.use(express_1.default.json());
/* Endpoint controllers */
const controllers = [
    new UsersController_1.UsersController("/api/users", null, logger_1.default)
];
for (const controller of controllers) {
    controller.registerEndpoins(app);
}
app.listen(port, () => {
    logger_1.default.log('info', `Server successfully started at port ${port}`);
});
// The app instance is exported to be used in the tests
exports.appInstance = app;
