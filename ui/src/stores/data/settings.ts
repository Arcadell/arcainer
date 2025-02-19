import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { Settings } from "@/models/data";
import { useToastStore } from "../utils";
import router from "@/router";

export const useSettingStore = defineStore("settingData", {
    state: () => ({}),
    actions: {
        async getSetting() {
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/setting', {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    mode: 'cors',
                });

                if (response.status === 401) {
                    router.push({ name: 'login' });
                    throw new Error('Invalid credentials');
                }

                if (!response.ok) {
                    const message = 'Generic error';
                    throw new Error(message);
                }

                const okResponse = await response.json() as Settings;

                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: null, error: e.message };
            }
        },
        async updateSetting(updateSetting: Settings) {
            const toastStore = useToastStore();
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/setting/update', {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    body: JSON.stringify(updateSetting),
                    mode: 'cors',
                });

                if (response.status === 401) {
                    router.push({ name: 'login' });
                    throw new Error('Invalid credentials');
                }

                if (!response.ok) {
                    const errorResponse = await response.json();
                    const message = JSON.stringify(errorResponse);
                    throw new Error(message);
                }
                toastStore.success({ message: "Settings updated successfully" });

                return { data: null, error: null };
            } catch (e: Error | any) {
                toastStore.error({ message: e.message, timeout: 20000 });

                return { data: null, error: e.message };
            }
        },
    }
})