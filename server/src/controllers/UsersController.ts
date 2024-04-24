import * as winston from "winston";
import { Express, Request, Response } from "express";
import IController from "./IController";
import { User } from "../modules/user/user.entity";

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

    postUser(req: Request, res: Response) {
        if (req.body.user === undefined) {
            res.status(400).send({ message: "Invalid request" });
            return;
        }
        // Check if user is of type User
        if (!(req.body.user instanceof User)) {
            res.status(400).send({ message: "Invalid request" });
            return;
        }
        // Save the user
        this.usersRepo.save(req.body.user);
    }


}