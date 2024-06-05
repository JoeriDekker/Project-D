import React, { useState } from "react";
import { t } from "i18next";
import Input from "../Input/Input";

function Automatic() {
    const [retypeword, setRetypeword] = useState("jahoor");

    function retypewordFunction() {

        var word = " ";
        word += `${retypeword} `;
        return word.toUpperCase();
    }


    return (
        <form className="h-full">
            <h1 className="text-xl font-medium">{t("Water.automation")}</h1>
            <div className="space-y-5 px-5 pt-5">
                <Input
                    label={t("Water.retypetext1") + retypewordFunction() + t("Water.retypetext2")}
                    needed
                    neededText={t("Water.retypewhy")}
                    placeholder={""}
                    width="auto max-w-xs"
                    onChange={() => { }}
                    name="validation"
                />
            </div>
        </form>
    )
}


export default Automatic;
