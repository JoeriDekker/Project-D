import React, { useEffect } from "react";
import axios from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import { t } from "i18next";
import { WaterStorageState, User } from "./waterstorage.state";


function WaterStorage() {

    const ActionLogboekEntry = ({ title, stored}: any) => {

        

        return (
            <div className="grid grid-cols-10 gap-2 mt-2 shadow-md text-center p-1">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100 col-span-3 ml-4">
                    <span className="font-bold">{title}</span>
                </div>
                <div className="mr-6 col-span-2 text-left">
                    <span className="font-bold text-lg text-gray-400">{stored}</span>
                </div>
               
            </div>
        );
    };

    const authHeader = useAuthHeader();

    const [waterstorage, setWaterStorage] = React.useState<WaterStorageState[]>([]);
    const [user, setUser] = React.useState<User | null>(null);


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

        console.log("CONTROL PC ID: " + user?.miniPCs.uuid);

        async function fetchActionLogs() {
            try {
                const res = await axios.get(
                    process.env.REACT_APP_API_URL + "/api/waterstorage/" + user?.miniPCs.uuid,
                    {
                        headers: {
                            Authorization: authHeader,
                        },
                    }
                );
                const mappedLogs = res.data.map((log: WaterStorageState) => ({
                    typeStorage: log.typeStorage,
                    waterStored:  + log.waterStored + t("waterstorage.liters"),
                }));
                
                
                setWaterStorage(mappedLogs);
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
                    {t("waterstorage.source")}
                </div>
                
               
            </div>
            <hr className="my-4" />
            <div className="max-h-[300px] overflow-y-auto mt-1 shadow-md">
                {waterstorage.map((log, index) => (
                    <ActionLogboekEntry 
                        key={index} 
                        typeStorage={log.typeStorage}
                        waterStored={log.waterStored}
                    />
                ))}
            </div>
        </div>
    );
}

export default WaterStorage;