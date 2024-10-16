export interface ApiResponse<T> {
    result: T[];
    errors: string[];
    timeGenerated: string;
}