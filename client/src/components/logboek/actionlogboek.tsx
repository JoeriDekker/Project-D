import React, { useEffect } from "react";
import axios from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { ActionLogboekState } from "./actionlogboek.state";

import { t } from "i18next";


function ActionLogboek() {
    const currentYear = new Date().getFullYear();

    const ActionLogboekEntry = ({ date, title, details }: ActionLogboekState) => {

        const dateObj = new Date(date);
        const day = dateObj.getDate(); // get the day as a number (1-31)
        const month = dateObj.getMonth() + 1; // get the month as a number (0-11) and add 1 to make it human-readable (1-12)

        return (
            <div className="grid grid-cols-10 gap-2 mt-2 shadow-md text-center p-1">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100 col-span-3 ml-4">
                    <span className="font-bold">{day}/{month}</span>
                </div>
                <div className="mr-6 col-span-2 text-left">
                    <span className="font-bold text-lg text-gray-400">{title}</span>
                </div>
                <div className="mr-6 col-span-5">
                    <span>{details}</span>
                </div>
            </div>
        );
    };

    const authHeader = useAuthHeader();

    const [actionlog, setActionlog] = React.useState<ActionLogboekState[]>([]);

    useEffect(() => {
        async function fetchActionLogs() {
            try {
                const res = await axios.get(
                    process.env.REACT_APP_API_URL + "/api/actionlog",
                    {
                        headers: {
                            Authorization: authHeader,
                        },
                    }
                );

                const mappedLogs = res.data.map((log: any) => ({
                    date: log.dateTimeStamp,
                    title: log.actionType.title,
                    details: log.actionType.details,
                }));
                
                setActionlog(mappedLogs);
            } catch (error) {
                console.error("Error fetching action logs", error);
            }
        }
        fetchActionLogs();
    }, [authHeader]);

    return (
        <div className="justify-center">
            <div className="grid grid-cols-4">
                <div className="col-span-2 font-bold">
                    {t("logboek.actionlogbook")}
                    <span className="text-left text-gray-400"> ({actionlog.length} {t("logboek.records")})</span>
                </div>
                <div className="text-center">
                    <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg">
                        <option selected>{t("logboek.selectmonth")}  </option>
                        <option value="01">{t("logboek.january")} </option>
                        <option value="02">{t("logboek.february")}</option>
                        <option value="03">{t("logboek.march")}</option>
                        <option value="04">{t("logboek.april")}</option>
                        <option value="05">{t("logboek.may")}</option>
                        <option value="06">{t("logboek.june")}</option>
                        <option value="07">{t("logboek.july")}</option>
                        <option value="08">{t("logboek.august")}</option>
                        <option value="09">{t("logboek.september")}</option>
                        <option value="10">{t("logboek.october")}</option>
                        <option value="11">{t("logboek.november")}</option>
                        <option value="12">{t("logboek.december")}</option>
                    </select>
                </div>
                <div className="text-center font-bold text-gray-600">
                    {currentYear}
                </div>
            </div>
            <hr className="my-4" />
            <div className="max-h-[300px] overflow-y-auto mt-1 shadow-md">
                {actionlog.map((log, index) => (
                    <ActionLogboekEntry 
                        key={index} 
                        date={log.date} 
                        title={log.title} 
                        details={log.details} 
                    />
                ))}
            </div>
        </div>
    );
}

export default ActionLogboek;