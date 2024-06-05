import React, { useEffect } from "react";
import axios from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import { t } from "i18next";
import { WaterStorageState, UserResponse } from "./waterstorage.state";


function WaterStorage() {

    const WaterStorageEntry = ({ typeStorage, waterStored, state}: WaterStorageState) => {
        let color = "";

        if ( state == "1") {
            color = "text-green-500";
            state = t("waterstorage.active");
        }
        else if ( state == "2"){
            color = "text-yellow-500";
            state = t("waterstorage.inactive");
        }
            
        else{
            color = "text-red-500";
            state = t("waterstorage.error");
        }


        return (
            <div className="grid grid-cols-10 gap-2 mt-2 shadow-md text-center p-1">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100 col-span-4 ml-4">
                    <span className="font-bold">{typeStorage}</span>
                </div>
                <div className="mr-6 col-span-4 text-center">
                    <span className="font-bold text-lg text-gray-400">{waterStored} {t("waterstorage.liters")}</span>
                </div>
                <div className="mr-6 col-span-2 text-left">
                    <span className={`${color} font-bold text-lg`}>{state}</span>
                </div>
            </div>
        );
    };

    const authHeader = useAuthHeader();

    const [waterstorage, setWaterStorage] = React.useState<WaterStorageState[]>([]);
    const [users, setUser] = React.useState<UserResponse | null>(null);

    useEffect(() => {
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
        }
    
        fetchUser();
    }, []);

    useEffect(() => {
        async function fetchActionLogs() {
            if (!users) {
                // users is not defined yet, skip this run
                return;
            }
    
            try {
                const res = await axios.get(
                    process.env.REACT_APP_API_URL + "/api/waterstorage/" + users.id,
                    {
                        headers: {
                            Authorization: authHeader,
                        },
                    }
                );
    
                if (Array.isArray(res.data)) {
                    setWaterStorage(res.data);
                } else {
                    console.error("Error: expected an array but received", res.data);
                }
            } catch (error) {
                console.error("Error fetching water storage", error);
            }
        }
    
        fetchActionLogs();
    }, [authHeader, users]);

    return (
        <div className="justify-center">
            <div className="grid grid-cols-4">
                <div className="col-span-2 font-bold">
                    {t("waterstorage.source")}
                </div>
            </div>
            <hr className="my-4" />
            <div className="max-h-[300px] overflow-y-auto mt-1 shadow-md">
            {waterstorage.map((log, index) => (
                <WaterStorageEntry 
                    key={index} 
                    typeStorage={log.typeStorage}
                    waterStored={log.waterStored}
                    state={log.state}
                />
            ))}
            </div>
        </div>
    );
}

export default WaterStorage;