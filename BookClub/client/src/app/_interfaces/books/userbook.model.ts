import * as internal from "events";

export interface UserBook{
  "userBookId": number,
  "user": number,
  "books": [{
          "bookId": number,
          "category": string,
          "authors": [],
          "price": number,
          "title": string,
          "description": string,
          "publishDate": Date,
          "identifier": string,
          "identifierType": string
  }]
}
