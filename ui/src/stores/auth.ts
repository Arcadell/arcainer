import type { IAuthErrorResponse, ILoginDto, ILoginResponse, IRegisterDto } from "@/models/auth";
import { defineStore } from "pinia";

import { useToastStore } from "./utils";

const toastStore = useToastStore();

export const useAuthStore = defineStore("auth", {
    state: () => ({
        loggedIn: false,
    }),
    actions: {
        async login(loginDto: ILoginDto) {
            try {
                const response = await fetch('http://localhost:5210/identity/login', {
                    method: 'POST',
                    headers: new Headers({ 'Content-Type': 'application/json' }),
                    mode: 'cors',
                    body: JSON.stringify(loginDto),
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

                const trueResponse = await response.json() as ILoginResponse;
                localStorage.setItem('auth', JSON.stringify(trueResponse));

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