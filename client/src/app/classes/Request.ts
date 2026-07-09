import { RequestStatus } from "./RequestStatus";
import { Category } from "./Category";

export interface Request {
       id?: string;
    userId?: string;
    userName?: string;
    title: string;
    content: string;
    lat?: number | null;
    lng?: number | null;
    status: RequestStatus;
    categoryId: string;
    category?: Category;
}