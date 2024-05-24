import React, { useEffect } from "react";
import axios from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { ActionLogboekState } from "./actionlogboek.state";


function ActionLogboek() {
    const currentYear = new Date().getFullYear();

    const ActionLogboekEntry = ({ date, title, details }: ActionLogboekState) => {
        const peilcolor = 'text-green-600';
        const smalldate = date ? date.substring(5, 10) : ''; // Adjusted to get a proper date format

        return (
            <div className="grid grid-cols-10 gap-2 mt-2 shadow-md text-center p-1">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100 col-span-3 ml-4">
                    <span className="font-bold">{smalldate}</span>
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
                console.log("Action log data", res.data);

                // Map the API response to the ActionLogboekState structure
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
                    Actie Logboek 
                    <span className="text-left text-gray-400"> ({actionlog.length} opnamen)</span>
                </div>
                <div className="text-center">
                    <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg">
                        <option selected>Selecteer een maand</option>
                        <option value="01">Januari</option>
                        <option value="02">Februari</option>
                        <option value="03">Maart</option>
                        <option value="04">April</option>
                        <option value="05">Mei</option>
                        <option value="06">Juni</option>
                        <option value="07">Juli</option>
                        <option value="08">Augustus</option>
                        <option value="09">September</option>
                        <option value="10">Oktober</option>
                        <option value="11">November</option>
                        <option value="12">December</option>
                    </select>
                </div>
                <div className="text-center font-bold text-gray-600">
                    {currentYear}
                </div>
            </div>
            <hr className="my-4" />
            <div className="max-h-[300px] overflow-y-auto mt-1 shadow-md">
                {actionlog.map((log, index) => (
                    console.log("Action log", log),
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