import axios from "axios";
import https from "https";
import type { Movie } from "../../../interfaces";
import { useRouter } from "next/router";
import useSwr from "swr";

const fetcher = (url: string) => fetch(url).then((res) => res.json());

export default async function LikeMovie() {
    try {
        const { query } = useRouter();
        const agent = new https.Agent({
            rejectUnauthorized: false,
        });

        const { data, error, isLoading } = useSwr<Movie>(
            query.id
                ? `${process.env.REACT_APP_BASE_URL}/api/like/${query.id}`
                : null,
            {
                // httpsAgent: agent,
                fetcher,
            }
        );
        return <> like api </>;
    } catch (error) {
        console.error(error);
    }
}
