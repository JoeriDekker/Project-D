import React from "react";
import Navbar from "../../components/navbar/navbar";
import Input from "../../components/Input/Input";
import { t } from "i18next";
import { AnyButton2 } from "../../components/Button/AnyButton";

function AnyPage() {
    const connected = false;

    return (
        <div className="bg-backgroundCol w-screen h-screen py-5 flex dir-row">
            <Navbar />
            <div className="space-y-10 bg-white w-full h-full rounded-xl mr-5 p-5">

                {/* Account setting section  */}
                <div>
                    <h1 className="text-xl font-medium">{t("Login.adjust")}</h1>
                    <h2 className="text-l font-medium opacity-40">{t("Login.current")}</h2>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5" >
                        <Input label={t("Login.street")} placeholder="Gortaanse straat" />

                        <Input label={t("Login.housenum")} placeholder="8" width="1/2" />
                    </div>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5" >
                        <Input label={t("Login.zipcode")} placeholder="3857 KG" width="1/2" />
                        <Input label={t("Login.place")} placeholder="Gouda" />
                    </div>
                    {/* TODO: Link this do an actual forget page */}
                    <Input label={t("Login.password")} placeholder="***************" width="1/2 px-5 pt-5" />
                    <a href="/wwforgor" className="px-5 underline text-secondaryCol">{t("Login.adjustpass")}</a>

                    <div className="mx-5">
                        <AnyButton2 link="/account" text={t('Login.adjustbutton')} />
                    </div>

                </div>

                {/* Pc connection section */}
                <div>
                    <h1 className="text-xl font-medium">{t("PC.connection")}</h1>
                    <h2 className="text-l font-medium opacity-40">{t("PC.foundin")}</h2>
                    <div className="space-y-5 px-5 pt-5">
                        <div>
                            <h3 className="text-m font-medium">{t("PC.uuid")}</h3>
                            <p className="opacity-40">{t("PC.uuidexplain")}</p>
                            <Input label="UUID" needed neededText={t("PC.foundon")} placeholder="b678ef40-524f-4890-a40b-efae6e85113e" width="auto max-w-xs" />
                        </div>
                        <div>
                            <h3 className="text-m font-medium">{t("PC.security")}</h3>
                            <p className="opacity-40">{t("PC.securityexplain")}</p>
                            <div className="">
                                <Input label="Key" needed neededText={t("PC.keyon")} placeholder="**********" width="auto max-w-xs" />

                            </div>
                        </div>
                        <div>
                            <div className="flex dir-row gap-2 items-center">
                                <h3 className="text-m font-medium">{t("PC.status")}</h3>
                                {connected ?
                                    <div className="bg-green-500 w-3 h-3 rounded-xl"></div>
                                    :
                                    <div className="bg-red-500 w-3 h-3 rounded-xl"></div>
                                }
                            </div>
                            <p className="opacity-40">{t("PC.statusexplain")}</p>
                        </div>
                        <AnyButton2 link="/account" text={t('PC.reconnect')} />
                    </div>

                </div>
            </div>
        </div>
    );
}

export default AnyPage;