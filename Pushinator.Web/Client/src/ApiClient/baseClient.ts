import { toast } from 'react-toastify'
import { TokenStorage } from './tokenStorage'

export abstract class BaseClient {
	
	private humanTexts: any = {
		'user_already_exist': 'Пользователь с таким e-mail уже существует'
	}
	
	protected async execute<T>(method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, body: any = null): Promise<T> {
		const response = await this.getResponse(url, method, body);
		return await response.json()
	}

	protected async executeEmptyResponse(method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, body: any = null): Promise<void> {
		await this.getResponse(url, method, body);
	}

	private async getResponse(url: string, method: "GET" | "POST" | "PUT" | "DELETE", body: any) {
		const token = TokenStorage.getToken()

		const response = await fetch(url, {
			method: method,
			headers: token
				? {
					'Content-Type': 'application/json',
					Authorization: `Bearer ${token}`,
				}
				: {'Content-Type': 'application/json'},
			body: body ? JSON.stringify(body) : undefined,
		})
		if (response.status === 401) {
			document.location.href = '/'
			throw new Error('Unauthorized')
		}
		if (response.status >= 500 || response.status === 404) {
			toast.error('Ошибка на сервере!')
			throw new Error('Internal Server error')
		}
		if (response.status === 400) {
			const responseText = await response.text()
			const text = this.humanTexts[responseText]
			toast.error(text || responseText)
			throw new Error('Bad request')
		}
		return response;
	}
}
