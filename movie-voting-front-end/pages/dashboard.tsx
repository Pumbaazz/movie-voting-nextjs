import React from "react";
import "./_app";
import { Movie, MoviesProps } from "@/interfaces";
import Navbar from "./components/navbar";
import jwtDecode from "jwt-decode";
import Swal from "sweetalert2";
import { GetServerSideProps } from "next";

export default function Dashboard({ movies }: MoviesProps) {
    /**
     * Handles liking a movie by sending a PATCH request to the /api/like endpoint
     * and refreshing the page to reflect the updated likes count.
     *
     * @param {Movie} movie - the movie to be liked
     */
    const handleDislikeMovie = async (movie: Movie) => {
        try {
            const token = localStorage.getItem("jwtToken");
            if (!token) return;
            const user = jwtDecode(token) as {
                id: string;
                name: string;
                email: string;
            };

            const movieId = movie.id;
            const userId = user.id;

            const requestOptions: RequestInit = {
                method: "PATCH",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ movieId, userId }),
            };

            const response = await fetch(`/api/dislike`, requestOptions);
            if (response.ok) {
                window.location.reload();
            } else {
                let statusText = "";
                switch (response.status) {
                    case 409:
                        statusText = "Already disliked!";
                        break;
                    default:
                        statusText = "An error occurred. Please try again.";
                        break;
                }
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: `${statusText}`,
                });
            }
        } catch (error) {
            console.error(error);
        }
    };
    const handleLikeMovie = async (movie: Movie) => {
        try {
            const token = localStorage.getItem("jwtToken");
            if (!token) return;
            const user = jwtDecode(token) as {
                id: string;
                name: string;
                email: string;
            };

            const movieId = movie.id;
            const userId = user.id;

            const requestOptions: RequestInit = {
                method: "PATCH",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ movieId, userId }),
            };

            const response = await fetch(`/api/like`, requestOptions);
            if (response.ok) {
                window.location.reload();
            } else {
                let statusText = "";
                switch (response.status) {
                    case 409:
                        statusText = "Already liked!";
                        break;
                    default:
                        statusText = "An error occurred. Please try again.";
                        break;
                }
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: `${statusText}`,
                });
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <>
            <Navbar />
            <div className="grid">
                {movies.map((movie: Movie) => (
                    <div
                        key={movie.id}
                        className="grid grid-cols-2 m-4 border border-solid border-slate-200"
                    >
                        <div className="col">
                            <img
                                src={movie.path}
                                width={"50%"}
                                alt="banner image"
                            />
                        </div>

                        <div className="col">
                            <h1 className="font-medium text-slate-900 text-3xl">
                                {movie.title}
                            </h1>
                            <div>
                                <span className="text-slate-500 text-xl">
                                    Likes: {movie.likes}
                                </span>
                            </div>

                            <div className="flex">
                                <div className="pt-2 px-2">
                                    <button
                                        className="btn bg-indigo-500 rounded-md px-2 py-2 text-sm font-medium text-white hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 drop-shadow-lg"
                                        onClick={() => {
                                            handleLikeMovie(movie);
                                        }}
                                    >
                                        Like
                                    </button>
                                </div>
                                <div className="pt-2">
                                    <button
                                        className="btn bg-red-500 rounded-md px-2 py-2 text-sm font-medium text-white hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 drop-shadow-lg"
                                        onClick={() => {
                                            handleDislikeMovie(movie);
                                        }}
                                    >
                                        Dislike
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </>
    );
}

/**
 * Retrieves a list of movies from the server-side API endpoint and returns them as props.
 *
 * @return {Object} An object with a `props` key containing the `movies` list.
 */
export const getServerSideProps: GetServerSideProps<{ movies: any }> = async ({
    res,
}) => {
    res.setHeader(
        "Cache-Control",
        "public, s-maxage=31536000,stale-while-revalidate=59"
    );

    const endpointUrl = `${process.env.REACT_APP_BASE_CLIENT}/api/get-movies`;
    const response = await fetch(endpointUrl);
    const movies = await response.json();
    return { props: { movies } };
};
