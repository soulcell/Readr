import BookModel from "./book";
import User from "./user";

type MutualLike = {
    user: User,
    hisBook: BookModel,
    myBook: BookModel,
};

export default MutualLike;