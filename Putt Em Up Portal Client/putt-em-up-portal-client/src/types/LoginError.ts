export interface LoginError{

    type: string;
    title: string;
    status: number;
    errors: { [key: string]: string[] };
    traceId: string;
}