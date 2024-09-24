import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { useToastStore } from "../utils";
import { Container, ContainerCommands } from "@/models/data";

export const useContainerStore = defineStore("data", {
    state: () => ({}),
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
        },
        async handleContainers(containers: Container[], command: ContainerCommands) {
            try {
                let url = null;

                switch (command) {
                    case ContainerCommands.Start:
                        url = 'http://localhost:5210/container/start';
                        break;
                    case ContainerCommands.Stop:
                        url = 'http://localhost:5210/container/stop';
                        break;
                    case ContainerCommands.Delete:
                        url = 'http://localhost:5210/container/delete';
                        break;
                    default:
                        throw new Error('Invalid command');
                }

                const ids = containers.map(c => c.id);

                const auth = useAuthStore();
                const response = await fetch(url, {
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