import { Me, type IAuthErrorResponse, type IAuth, type ILoginDto, type ILoginResponse, type IRegisterDto, type IRefeshDto } from "@/models/auth";
import router from "@/router";
import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        me: new Me(),
    }),
    actions: {
        checkLogin() {
            const auth = localStorage.getItem('auth');
            if (!auth) { return false; }
            const authObj = JSON.parse(auth) as IAuth;

            this.me.email = authObj.email;
            this.me.loginResponse = authObj.loginResponse;
            this.me.loginTimeSave = authObj.loginTimeSave;

            return true;
        },
        async login(loginDto: ILoginDto) {
            try {
                const response = await fetch('http://localhost:5210/identity/login', {
                    method: 'POST',
                    headers: new Headers({ 'Content-Type': 'application/json' }),
                    mode: 'cors',
                    body: JSON.stringify(loginDto),
                });

                if (response.status === 401) { throw new Error('Invalid credentials'); }

                if (!response.ok) {
                    const errorResponse = await response.json() as IAuthErrorResponse;
                    console.error(JSON.stringify(errorResponse));
                    let message = '';
                    for (const [key, value] of Object.entries(errorResponse.errors)) {
                        console.error(key, value);

                        value.forEach((v: string) => {
                            message += `${v}\n`;
                        });
                    }

                    throw new Error(message);
                }

                const okResponse = await response.json() as ILoginResponse;
                const login: IAuth = {
                    email: loginDto.email,
                    loginResponse: okResponse,
                    loginTimeSave: Math.floor(Date.now() / 1000),
                }

                this.me.email = login.email;
                this.me.loginResponse = login.loginResponse;
                this.me.loginTimeSave = login.loginTimeSave;

                localStorage.setItem('auth', JSON.stringify(login));
                return { ok: true, error: null };
            } catch (e: Error | any) {
                return { ok: false, error: e.message };
            }
        },
        logout() {
            localStorage.removeItem('auth');
        },
        async register(registerDto: IRegisterDto) {
            try {
                const response = await fetch('http://localhost:5210/identity/register', {
                    method: 'POST',
                    headers: new Headers({ 'Content-Type': 'application/json' }),
                    mode: 'cors',
                    body: JSON.stringify(registerDto),
                });

                if (!response.ok) {
                    const trueResponse = await response.json() as IAuthErrorResponse;
                    let message = '';
                    for (const [key, value] of Object.entries(trueResponse.errors)) {
                        console.error(key, value);

                        value.forEach((v: string) => {
                            message += `${v}\n`;
                        });
                    }

                    throw new Error(message);
                }

                return { ok: true, error: null };
            } catch (e: Error | any) {
                return { ok: false, error: e.message };
            }
        },
        async refresh(refreshDto: IRefeshDto) {
            try {
                const response = await fetch('http://localhost:5210/identity/refresh', {
                    method: 'POST',
                    headers: new Headers({ 'Content-Type': 'application/json' }),
                    mode: 'cors',
                    body: JSON.stringify(refreshDto),
                });

                if (!response.ok) {
                    const errorResponse = await response.json() as IAuthErrorResponse;
                    console.error(JSON.stringify(errorResponse));
                    let message = '';
                    for (const [key, value] of Object.entries(errorResponse.errors)) {
                        console.error(key, value);

                        value.forEach((v: string) => {
                            message += `${v}\n`;
                        });
                    }

                    throw new Error(message);
                }

                const okResponse = await response.json() as ILoginResponse;
                const login: IAuth = {
                    email: this.me.email ?? '',
                    loginResponse: okResponse,
                    loginTimeSave: Math.floor(Date.now() / 1000),
                }

                this.me.email = login.email;
                this.me.loginResponse = login.loginResponse;

                localStorage.setItem('auth', JSON.stringify(login));
                return { ok: true, error: null };
            } catch (e: Error | any) {
                return { ok: false, error: e.message };
            }
        },
        async getAccessToken(): Promise<string> {
            if (!this.me.loginResponse) {
                router.push({ name: 'login' });
                return '';
            }

            if (this.isTokenExpired()) {
                await this.refresh({ refreshToken: this.me.loginResponse.refreshToken });
            }

            return this.me.loginResponse.accessToken;
        },
        isTokenExpired(): boolean {
            if (!this.me.loginResponse || !this.me.loginTimeSave) { return true; }

            const now = Math.floor(Date.now() / 1000);
            const exp = this.me.loginTimeSave + this.me.loginResponse.expiresIn;

            return now >= exp;
        }
    },
});