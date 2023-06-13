export type User = {
    id: number;
    Name: string;
    Email: string;
};

export type Movie = {
    movieId: number;
    title: string;
    path: string;
    likes: number;
};

export type MoviesProps = {
    movies: Movie[];
};
