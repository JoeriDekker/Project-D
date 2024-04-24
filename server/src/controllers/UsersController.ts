import * as winston from "winston";
import { Express, Request, Response } from "express";
import IController from "./IController";

// SKkkrrrt
export class UsersController implements IController {
    public constructor(
        public readonly route: string,
        private readonly usersRepo: any,
        readonly logger: winston.Logger
    ) { }

    registerEndpoins(app: Express): void {
        // Register the endpoints
        app.get(this.route + "/", this.getUsers);
    }

    getUsers(req: Request, res: Response) {
        res.status(200).send({ message: "lol" });
    }


}