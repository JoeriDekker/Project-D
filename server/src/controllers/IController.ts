import { Express } from 'express'
export default interface IController {
    registerEndpoins(app: Express): void
    readonly route: string,
}