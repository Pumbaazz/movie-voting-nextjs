import axios from "axios";
import https from 'https';

export default async function getMovies(req, res) {
    try {
        const agent = new https.Agent({
            rejectUnauthorized: false,
        });
        const response = await axios.get(`${process.env.REACT_APP_BASE_URL}/api/get-movies`, {
            httpsAgent: agent,
        });

        // Temp code for testing no ssl cert
        // const response = await axios.get(`${process.env.REACT_APP_BASE_URL}/api/get-movies`);

        res.status(200).json(response.data);
    } catch (error) {
        console.error(error);
    }

}