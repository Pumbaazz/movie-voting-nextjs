export type User = {
    id: string;
    name: string;
    email: string;
};

export type Movie = {
    id: string;
    title: string;
    path: string;
    likes: number;
};

export type MoviesProps = {
    movies: Movie[];
};
