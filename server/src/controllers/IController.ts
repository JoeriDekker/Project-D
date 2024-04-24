import { Express } from 'express'
/**
 * Interface that defines the methods that a controller must implement
 * 
 * @method registerEndpoins - Method that registers the endpoints of the controller
 * @property route - Property that defines the route of the controller
 */
export default interface IController {
    registerEndpoins(app: Express): void
    readonly route: string,
}