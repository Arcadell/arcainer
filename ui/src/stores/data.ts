import { defineStore } from "pinia";
import { useAuthStore } from "./auth";

export const useDataStore = defineStore("data", {
    state: () => ({
        containers: [],
    }),
    actions: {
        async getEntities(route: string, searchParams: string) {
            try {
                const auth = useAuthStore();
                const response = await fetch('http://localhost:5210/' + route + '?' + searchParams, {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + auth.getAccessToken(),
                    }),
                    mode: 'cors',
                });

                if (response.status === 401) { throw new Error('Invalid credentials'); }

                if (!response.ok) {
                    const message = 'Generic error';
                    throw new Error(message);
                }

                const okResponse = await response.json();
                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                return { ok: false, error: e.message };
            }
        },
        getContainers() {
            this.getEntities('container', '');
            return this.containers;
        }
    }
})