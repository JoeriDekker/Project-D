import React, { useEffect } from "react";
import axios, { AxiosError } from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import { WaterpeilLogboekState, UserResponse } from "./waterpeillogboek.state";
import { t } from "i18next";

function WaterpeilLogboek(){

    const currentYear = new Date().getFullYear();

    const WaterpeilLogboekEntry = ({date, address, level }: WaterpeilLogboekState) => {
        let peilcolor: string = 'text-green-600';
        if (level.toString().includes('-')) {
            peilcolor = 'text-red-600';
        }

        // get only firt 5 characters of date
        let smalldate = date.substring(5, 10);

        // return (
        //     <div className="grid grid-cols-3 gap-2 mt-2 shadow-md text-center">
        //         <div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>{date}</b></div>
        //         <div className="mr-6">Street: <text>{street}</text></div>
        //         <div className="mr-6">Peil: <text className={`${peilcolor}`}>{peil}</text></div>
        //     </div>
        // );

        return (
            <div className="grid grid-cols-10 gap-2 mt-2 shadow-md text-center p-1">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100 col-span-3 ml-4"><text className="font-bold">{smalldate}</text></div>
                <div className="mr-6 col-span-5 text-left"><text className="font-bold text-lg text-gray-400">{address}</text></div>
                <div className="mr-6 col-span-2">Peil: <text className={`${peilcolor}`}>{parseFloat(level).toFixed(2)}</text></div>
            </div>
        );
    };

    const authHeader = useAuthHeader();

    const [waterlogs, setwaterlogs] = React.useState<WaterpeilLogboekState[]>([]);
    const [user, setUser] = React.useState<UserResponse | null>(null);
    const [address, setAddress] = React.useState<string | null>(null);


    useEffect(() => {
        async function fetchWaterlogs() {
            const res = await axios.get(
                process.env.REACT_APP_API_URL + "/api/groundwaterlog",
                {
                    headers: {
                        Authorization: authHeader,
                    },
                }
            );
            setwaterlogs(res.data);
        }
        fetchWaterlogs();

        async function fetchUser() {
            const res = await axios.get(
                process.env.REACT_APP_API_URL + "/api/users",
                {
                    headers: {
                        Authorization: authHeader,
                    },
                }
            );
            setUser(res.data);
            setAddress(res.data.address);
        }
        fetchUser();
    }, [authHeader]);
        

    // const data = [
    //     { date: '08/12', peil: '+0.12m', street: 'Street 1' },
    //     { date: '09/12', peil: '-0.13m', street: 'Street 2' },
    //     { date: '10/12', peil: '+0.13m', street: 'Street 3' },
    //     { date: '11/12', peil: '-0.15m', street: 'Street 4' },
    //     { date: '12/12', peil: '+0.03m', street: 'Street 5' },
    //     { date: '13/12', peil: '-0.13m', street: 'Street 6' },
    // ];

    return (
        <div className=" justify-center">
                            <div className="grid grid-cols-4 ">
                                <div className="col-span-2 font-bold">
                                    {t("logboek.waterleverlogbook")}
                                    <span className="text-left text-gray-400"> ({waterlogs.length} {t("logboek.records")})</span>
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
                                    {/* get current year */}
                                    {currentYear}
                                </div>
                                
                            </div>
                            <hr className="my-4"></hr>
                            <div className="max-h-[300px] overflow-y-auto mt-1 shadow-md">
                                {/* {data.map((entry, index) => <WaterpeilLogboekEntry key={index} {...entry} />)} */}
                                {waterlogs.map((log, index) => (
                                    <WaterpeilLogboekEntry key={index} date={log.date} address={user?.address?.street ?? "Unknown"} level={log.level}  />
                                ))}
                            </div>
                        </div>
    );
}

export default WaterpeilLogboek;