import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { useToastStore } from "../utils";
import { Container } from "@/models/data";

export const useContainerStore = defineStore("data", {
    state: () => ({
        containers: [] as Container[],
    }),
    actions: {
        async getContainers(searchParams: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch('http://localhost:5210/container?' + searchParams, {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    mode: 'cors',
                });

                if (response.status === 401) { throw new Error('Invalid credentials'); }

                if (!response.ok) {
                    const message = 'Generic error';
                    throw new Error(message);
                }

                const okResponse = await response.json() as Container[];
                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: [], error: e.message };
            }
        }
    }
})