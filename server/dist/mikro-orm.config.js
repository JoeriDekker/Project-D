"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const postgresql_1 = require("@mikro-orm/postgresql");
const reflection_1 = require("@mikro-orm/reflection");
const config = {
    // for simplicity, we use the SQLite database, as it's available pretty much everywhere
    driver: postgresql_1.PostgreSqlDriver,
    host: process.env.DB_HOST || "localhost",
    dbName: process.env.DB_NAME || "wam-db",
    user: process.env.DB_USER || "root",
    password: process.env.DB_PASSWORD || "root",
    // folder-based discovery setup, using common filename suffix
    entities: ["dist/**/*.entity.js"],
    entitiesTs: ["src/**/*.entity.ts"],
    // we will use the ts-morph reflection, an alternative to the default reflect-metadata provider
    // check the documentation for their differences: https://mikro-orm.io/docs/metadata-providers
    metadataProvider: reflection_1.TsMorphMetadataProvider,
    // enable debug mode to log SQL queries and discovery information
    debug: true,
};
exports.default = config;
