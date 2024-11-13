import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { Network } from "@/models/data";
import { useToastStore } from "../utils";

export const useNetworkStore = defineStore("networkData", {
    state: () => ({}),
    actions: {
        async getNetworks(searchParams: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch('http://localhost:5210/network?' + searchParams, {
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

                const okResponse = await response.json() as Network[];

                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: [], error: e.message };
            }
        },
        async deleteNetworks(networks: Network[]) {
            try {
                const ids = networks.map(c => c.id);

                const auth = useAuthStore();
                const response = await fetch('http://localhost:5210/network/delete', {
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