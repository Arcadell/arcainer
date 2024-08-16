export class Me {
    email: string | undefined;
    accessToken: string | undefined;
}

export interface RegisterDto {
    email: string;
    password: string;
}

export interface LoginDto {
    email: string;
    password: string;
    twoFactorCode: string,
    twoFactorRecoveryCode: string
}

export interface AuthErrorResponse {
    errors: {
        [key: string]: string[];
    };
}