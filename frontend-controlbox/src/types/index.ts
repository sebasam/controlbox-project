export interface User {
  id: string;
  email: string;
  userName: string;
  token: string;
}

export interface Category {
  id: string;
  name: string;
}

export interface Book {
  id: string;
  title: string;
  author: string;
  categoryName: string;
  summary: string;
}

export interface Review {
  id: string;
  rating: number;
  comment: string;
  userName: string;
  userId: string;
  createdAt: string;
}

export interface BookDetail extends Book {
  reviews: Review[];
}