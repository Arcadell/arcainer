import { defineStore } from "pinia";
import { useAuthStore } from "../auth";
import { useToastStore } from "../utils";
import { Container, ContainerCommands, CreateContainerCommand, Stack } from "@/models/data";
import router from '@/router';

export const useContainerStore = defineStore("containerData", {
    state: () => ({}),
    actions: {
        async getContainers(searchParams: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/container?' + searchParams, {
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

                const okResponse = await response.json() as Container[];

                return { data: okResponse, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: [], error: e.message };
            }
        },
        async getStacks(stackName: string = '') {
            try {
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/container/stack/' + stackName, {
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

                const okResponse = await response.json() as Stack[];

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
                        url = '/api/container/start';
                        break;
                    case ContainerCommands.Stop:
                        url = '/api/container/stop';
                        break;
                    case ContainerCommands.Delete:
                        url = '/api/container/delete';
                        break;
                    default:
                        throw new Error('Invalid command');
                }

                const ids = containers.map(c => c.id);

                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + url, {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    body: JSON.stringify(ids),
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

                return { data: null, error: null };
            } catch (e: Error | any) {
                const toastStore = useToastStore();
                toastStore.error({ message: e.message });

                return { data: null, error: e.message };
            }
        },
        async createContainer(createContainerCommand: CreateContainerCommand) {
            const toastStore = useToastStore();
            try {
                const toast = toastStore.addToast({ message: `[${createContainerCommand.name}] Pulling image and creating/updating the container please wait...`, infinite_timeout: true }, 'warning')
                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/container/create', {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    body: JSON.stringify(createContainerCommand),
                    mode: 'cors',
                });
                toastStore.removeToast(toast);

                if (response.status === 401) {
                    router.push({ name: 'login' });
                    throw new Error('Invalid credentials');
                }

                if (!response.ok) {
                    const message = 'Generic error';
                    throw new Error(message);
                }
                toastStore.success({ message: "Compose created/updated successfully" });

                return { data: null, error: null };
            } catch (e: Error | any) {
                toastStore.error({ message: e.message });

                return { data: null, error: e.message };
            }
        },
        async deleteStacks(stacks: Stack[]) {
            const toastStore = useToastStore();
            try {
                const names = stacks.map(c => c.name);

                const auth = useAuthStore();
                const response = await fetch((import.meta.env.DEV ? import.meta.env.VITE_API_URL : '') + '/api/container/stack/delete', {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + await auth.getAccessToken(),
                    }),
                    body: JSON.stringify(names),
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

                return { data: null, error: null };
            } catch (e: Error | any) {
                toastStore.error({ message: e.message });

                return { data: null, error: e.message };
            }
        }
    }
})