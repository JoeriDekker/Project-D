import { AnyButton2 } from "../Button/AnyButton";
import Input from "../Input/Input";
import { t } from "i18next"

function LoginAdjust() {

    return (
        <>
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
            <Input label={t("Login.password")} placeholder="***************" width="1/2 px-5 pt-5" />
            <a href="/wwforgor" className="px-5 underline text-secondaryCol">{t("Login.adjustpass")}</a>

            <div className="">
                <AnyButton2 link="/account" text={t('Login.adjustbutton')} />
            </div>
        </>
    );
}

export default LoginAdjust;