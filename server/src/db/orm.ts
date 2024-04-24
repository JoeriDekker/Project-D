import { MikroORM } from "@mikro-orm/postgresql";
import config from "../mikro-orm.config";

(async () => { const orm = await MikroORM.init(config); export default orm; })();
