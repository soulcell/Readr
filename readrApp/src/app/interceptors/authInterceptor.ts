import { HttpInterceptorFn } from "@angular/common/http";
import { AUTH_TOKEN } from "src/consts/local-storage";

export const authInterceptor: HttpInterceptorFn = (req, next) => {

    const token = localStorage.getItem(AUTH_TOKEN);

    if (token) {
        const authenticatedReq = req.clone({
            headers: req.headers.set("Authorization", "Bearer " + token)
        });
        return next(authenticatedReq);
    }
    else {
        return next(req);
    }
}