import { Me, type IAuthErrorResponse, type IAuth, type ILoginDto, type ILoginResponse, type IRegisterDto } from "@/models/auth";
import { defineStore } from "pinia";
import { useToastStore } from "./utils";

const toastStore = useToastStore();

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
            this.me.accessToken = authObj.loginResponse.accessToken;
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

                if (!response.ok) {
                    const errorResponse = await response.json() as IAuthErrorResponse;
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
                    loginResponse: okResponse
                }

                this.me.email = login.email;
                this.me.accessToken = login.loginResponse.accessToken;

                localStorage.setItem('auth', JSON.stringify(login));
                return true;
            } catch (e: Error | any) {
                toastStore.error({ message: e.message });
                return false;
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

                return true;
            } catch (e: Error | any) {
                toastStore.error({ message: e.message });
                return false;
            }
        }
    },
});