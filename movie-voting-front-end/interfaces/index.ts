export type User = {
    id: number;
    name: string;
    email: string;
    password: string;
};

export type MoviesProps = {
    movies: Movie[];
};

export type Movie = {
    movieId: number;
    title: string;
    path: string;
    likes: number;
};
