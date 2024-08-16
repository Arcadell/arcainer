import { defineStore } from "pinia";

export const useCounterStore = defineStore("auth", {
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
        register() {

        }
    },
});