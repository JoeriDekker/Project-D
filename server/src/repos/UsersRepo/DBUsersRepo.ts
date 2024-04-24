import { UUID } from "crypto";
import { User } from "../../modules/user/user.entity";
import { IUsersRepo } from "./IUsersRepo";
import orm from "../../db/orm";
export class DBUsersRepo implements IUsersRepo {
    
    save(user: User) {
        orm.em.persist(user);
    }
    update(user: User) {
        throw new Error("Method not implemented.");
    }
    delete(id: UUID) {
        throw new Error("Method not implemented.");
    }
    getById(id: UUID) : User | null {
        throw new Error("Method not implemented.");
    }
    getAll() : User[] {
        throw new Error("Method not implemented.");
    }
}