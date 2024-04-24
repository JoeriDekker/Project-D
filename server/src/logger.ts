import * as winston from "winston";
const logger = winston.createLogger({
    transports: [
        new winston.transports.Console({format: winston.format.simple()}),
        new winston.transports.File({ filename: 'syslog.log', level: 'info'}),
        new winston.transports.File({ filename: 'errorlog.log', level: 'error'})
    ],
});

export default logger;