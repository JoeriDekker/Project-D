import { UUID } from "crypto";
import { User } from "../../modules/user/user.entity";
import uuid from 'uuid';

export interface IUsersRepo {
    save(user: User): void;
    update(user: User): void;
    delete(id: UUID): void;
    getById(id: UUID): User | null;
    getAll(): User[];
}