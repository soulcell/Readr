import Genre from "./genre";

type BookTitle = {
    id: number,
    title: string,
    author: string,
    genre?: Genre,
    coverUrl?: string,
};

export default BookTitle;