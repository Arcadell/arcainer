export type TToastStatus = 'success' | 'error' | 'warning' | 'info';

export interface IToast {
    id?: number;
    message: string;
    status: TToastStatus;
    timeout?: number;
}

export interface TToastPayload {
    message: string;
    timeout?: number;
    infinite_timeout?: boolean;
}