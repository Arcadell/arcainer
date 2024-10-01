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
                const response = await fetch('http://localhost:5210/volume?' + searchParams, {
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
        }
    }
})