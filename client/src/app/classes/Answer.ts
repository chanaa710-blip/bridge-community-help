export interface Answer {
    id?: string,
    requestId: string,
    userId: string,
    userName?: string,
    content: string,
    createdAt?: Date
}