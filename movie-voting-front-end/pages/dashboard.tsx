import React from "react";
import "./_app";
import { Movie, MoviesProps } from "@/interfaces";
import Navbar from "./components/navbar";

export default function Dashboard({ movies }: MoviesProps) {
    /**
     * Handles liking a movie by sending a PATCH request to the /api/like endpoint
     * and refreshing the page to reflect the updated likes count.
     *
     * @param {Movie} movie - the movie to be liked
     */
    const handleLikeMovie = async (movie: Movie) => {
        try {
            const response = await fetch(`/api/like/${movie.movieId}`, {
                method: "PATCH",
            });
            if (response.ok) {
                // Reload the page to reflect the updated likes count
                window.location.reload();
            } else {
                throw new Error("Failed to update likes");
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
                        key={movie.movieId}
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

                            <div className="pt-2">
                                <button
                                    className="btn bg-indigo-500 rounded-md px-2 py-2 text-sm font-medium text-white hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 drop-shadow-lg"
                                    onClick={() => {
                                        handleLikeMovie(movie);
                                    }}
                                >
                                    Like
                                </button>
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
export async function getServerSideProps() {
    const endpointUrl = `${process.env.REACT_APP_BASE_CLIENT}/api/get-movies`;
    const res = await fetch(endpointUrl);
    const movies = await res.json();
    return { props: { movies } };
}
