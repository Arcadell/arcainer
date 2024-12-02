
import type { IToast, TToastPayload, TToastStatus } from "@/models/utils";
import { defineStore } from "pinia";

export const useToastStore = defineStore("toast", {
    state: (): { toasts: IToast[] } => ({
        toasts: [],
    }),
    actions: {
        addToast(payload: TToastPayload, status: TToastStatus) {
            const toast: IToast = {
                id: Date.now(),
                message: payload.message,
                status: status,
                timeout: payload.timeout ?? 3000,
            };

            this.toasts.push(toast);

            if (!payload.infinite_timeout) {
                setTimeout(() => {
                    this.toasts = this.toasts.filter(t => t.id !== toast.id);
                }, toast.timeout);
            }

            return toast;
        },
        removeToast(toast: IToast) {
            this.toasts = this.toasts.filter(t => t.id !== toast.id);
        },
        success(paylaod: TToastPayload) {
            this.addToast(paylaod, "success");
        },
        warning(paylaod: TToastPayload) {
            this.addToast(paylaod, "warning");
        },
        error(paylaod: TToastPayload) {
            this.addToast(paylaod, "error");
        },
    }
})