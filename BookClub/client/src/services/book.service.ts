import axios from 'axios';
import { Credentials } from '@/models/credentials.interface';
import { BaseService } from './base.service';
import { Observable } from 'rxjs/Rx';

class BookService extends BaseService {

    private static instance: BookService;
    private googleAPIKey = "AIzaSyCjqD7OtvMLj-JMh3erdPRh_qWyRJvnvxw" as string;

    private constructor() {
        super();
    }

    public static get Instance() {
       // Do you need arguments? Make it a regular method instead.
       return this.instance || (this.instance = new this());
    }

    public getBooks(): Observable<any> {
        return Observable.fromPromise(axios.get(`${this.api}/book`))
        .map((res: any) => res.data)
        .catch((error: any) => this.handleError(error.response));
    }

    public getUserBooks(): Observable<any> {
        return Observable.fromPromise(axios.post(`${this.api}/book/user`))
            .map((res: any) => res.data)
            .catch((error: any) => this.handleError(error.response));
        
    }
    
    public getBookDetails(bookTitle: string): Promise<any> {   
        var parsedTitle = encodeURI(bookTitle);
        var instance = axios.create();
        delete instance.defaults.headers.common['Authorization'];
        return instance.get(`https://www.googleapis.com/books/v1/volumes?key=${this.googleAPIKey}&q=intitle:${parsedTitle}`);
    }
}

// export a singleton instance in the global namespace
export const bookService = BookService.Instance;
