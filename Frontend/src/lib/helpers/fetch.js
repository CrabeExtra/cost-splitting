import { browser } from '$app/environment';

let toast;

if (browser) {
  const mod = await import("svelte-sonner");
  toast = mod.toast;
}

import { API_BASE_URL } from '$lib/constants/ServerConstants';


export class ApiClient {

    static async fetchFromBackend(url, options) {
        return await fetch(
            url.startsWith('http') ? url : (`${API_BASE_URL}${url.startsWith('/') ? url : `/${url}`}`),
            options
        );
    } 

    static async get(url) {
        const response = await this.fetchFromBackend(url, { method: 'GET' });

        return await this.handleError(response);
    }

    static async post(url, body) {
        const response = await this.fetchFromBackend(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });

        return await this.handleError(response);
    }

    static async update(url, body, method = 'PATCH') {
        const response = await this.fetchFromBackend(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });

        return await this.handleError(response);
    }

    static async delete(url, id) {
        const response = await this.fetchFromBackend(`${url}/${id}`, {
            method: 'DELETE'
        });

        return await this.handleError(response);
    }

    static async handleError(response) {
        let success = true;
        if(!response.ok) {
            success = false;
            let resJson;

            try {
                resJson = await response.json();
            } catch {}

            if(![500, 404].includes(response.status) && resJson?.message) {
                toast.error(resJson?.message);
            } else {
                toast.error('An unexpected error has occurred.')
            }
            
            return { message: resJson?.message, success }
        }

        return { data: await response?.json(), success };
    }
}