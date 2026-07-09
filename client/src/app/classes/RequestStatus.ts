export enum RequestStatus {
    Open = 0,      // בקשה חדשה שפתוחה לכולם
    InProgress = 1, // מישהו התחיל לעזור
    Closed = 2,    // הבקשה הסתיימה בהצלחה
    Canceled = 3   // המשתמש ביטל את הבקשה
}