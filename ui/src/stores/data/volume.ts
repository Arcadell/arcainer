import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { useToastStore } from "../utils";
import type { Volume } from "@/models/data";

export const useVolumeStore = defineStore("volumeData", {
    state: () => ({}),
    actions: {
        async getVolumes(searchParams: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/volume?' + searchParams, {
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

                const okResponse = await response.json() as Volume[];

                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: [], error: e.message };
            }
        },
        async deleteVolumes(volumes: Volume[]) {
            try {
                const ids = volumes.map(c => c.name);

                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/volume/delete', {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    body: JSON.stringify(ids),
                    mode: 'cors',
                });

                if (response.status === 401) { throw new Error('Invalid credentials'); }

                if (!response.ok) {
                    const message = 'Generic error';
                    throw new Error(message);
                }

                return { data: null, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: null, error: e.message };
            }
        }
    }
})