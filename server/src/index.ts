import express, { Express, Request, Response } from "express";
import dotenv from "dotenv";
import cors from "cors";
import logger from "./logger";
import UsersController from "./controllers/UsersController";
import IController from "./controllers/IController";

// Loads the dotenv variables
dotenv.config();

const app: Express = express();
const port = process.env.PORT || 5000;

// Allowed origins
const allowedOrigins = ['http://localhost:3000'];

/* Middlewares */
app.use(cors({
    origin: allowedOrigins
}));

app.use(express.json());

/* Endpoint controllers */
const controllers: IController[] = [
    new UsersController("/api/users", null, logger)
];

for (const controller of controllers) {
    controller.registerEndpoins(app);
}

app.listen(port, () => {
    logger.log('info', `Server successfully started at port ${port}`)
});