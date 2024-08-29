export class Me {
    email: string | undefined;
    loginResponse!: ILoginResponse;
    loginTimeSave!: number;
}

export interface IRegisterDto {
    email: string;
    password: string;
}

export interface ILoginDto {
    email: string;
    password: string;
    twoFactorCode?: string,
    twoFactorRecoveryCode?: string
}

export interface ILoginResponse {
    tokenType: string
    accessToken: string
    expiresIn: number
    refreshToken: string
}

export interface IRefeshDto {
    refreshToken: string;
}

export interface IAuth {
    email: string,
    loginResponse: ILoginResponse | undefined
    loginTimeSave: number | undefined;
}

export interface IAuthErrorResponse {
    errors: {
        [key: string]: string[];
    };
}