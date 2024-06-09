import React, { useEffect } from "react";
import axios, { AxiosError } from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import { WaterpeilLogboekState} from "./waterpeillogboek.state";
import { UserResponse } from "../../pages/LoginScreen/LoginScreen.state";
import { t } from "i18next";

function WaterpeilLogboek(){

    const currentYear = new Date().getFullYear();

    const WaterpeilLogboekEntry = ({date, address, level }: WaterpeilLogboekState) => {
        let peilcolor: string = 'text-green-600';
        let idealHeight = user?.waterLevelSettings.idealHeight ?? 0;
        if (parseFloat(level) < idealHeight) {
            peilcolor = 'text-red-600';
        }

        // get only firt 5 characters of date
        let smalldate = date.substring(5, 10);

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
                            <div className="max-h-[335px] overflow-y-auto mt-1 shadow-md">
                                {/* {data.map((entry, index) => <WaterpeilLogboekEntry key={index} {...entry} />)} */}
                                {waterlogs.map((log, index) => (
                                    <WaterpeilLogboekEntry key={index} date={log.date} address={user?.address?.street ?? "Unknown"} level={log.level}  />
                                ))}
                            </div>
                        </div>
    );
}

export default WaterpeilLogboek;