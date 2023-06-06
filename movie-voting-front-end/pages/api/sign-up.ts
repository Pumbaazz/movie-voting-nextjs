import axios from "axios";
import https from "https";

export default async function signUp() {
    try {
        const agent = new https.Agent({
            rejectUnauthorized: false,
        });

        const response = await axios.patch(
            `${process.env.REACT_APP_BASE_URL}/api/sign-up`,
            {
                httpsAgent: agent,
            }
        );
    } catch (error) {
        console.error(error);
    }
}
