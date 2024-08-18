export class Me {
    email: string | undefined;
    accessToken: string | undefined;
}

export interface IRegisterDto {
    email: string;
    password: string;
}

export interface ILoginDto {
    email: string;
    password: string;
    twoFactorCode: string,
    twoFactorRecoveryCode: string
}

export interface IAuthErrorResponse {
    errors: {
        [key: string]: string[];
    };
}