import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { useToastStore } from "../utils";
import type { Image } from "@/models/data";

export const useImageStore = defineStore("imageData", {
    state: () => ({}),
    actions: {
        async getImages(searchParams: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/image?' + searchParams, {
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

                const okResponse = await response.json() as Image[];

                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: [], error: e.message };
            }
        }
    }
})