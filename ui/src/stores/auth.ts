import type { AuthErrorResponse, RegisterDto } from "@/models/auth";
import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        loggedIn: false,
    }),
    actions: {
        login() {
            this.loggedIn = true;
        },
        logout() {
            this.loggedIn = false;
        },
        async register(registerDto: RegisterDto) {
            try {
                const response = await fetch('http://localhost:5210/identity/register', {
                    method: 'POST',
                    headers: new Headers({ 'Content-Type': 'application/json' }),
                    mode: 'cors',
                    body: JSON.stringify(registerDto),
                });

                if (!response.ok) {
                    const trueResponse = await response.json() as AuthErrorResponse;
                    let message = '';
                    for (const [key, value] of Object.entries(trueResponse.errors)) {
                        console.error(key, value);

                        value.forEach((v: string) => {
                            message += `${v}\n`;
                        });
                    }

                    console.log(message);
                    throw new Error(message);
                }
            } catch (e: Error | any) {
                alert(e.message);
                return false;
            }
        }
    },
});