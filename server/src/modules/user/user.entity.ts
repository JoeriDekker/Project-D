import { Entity, PrimaryKey, Property, UuidType } from "@mikro-orm/core";
import uuid from 'uuid'
@Entity()
export class User {
    @PrimaryKey({ type: 'uuid'})
    id = uuid.v4();

    @Property()
    fullName!: string;

    @Property()
    email!: string;

    @Property()
    password!: string;
}