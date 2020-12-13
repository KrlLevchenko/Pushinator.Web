import { AuthResponse } from './Contracts/AuthResponse'

class ApiClient {
	token: string | undefined

	auth(email: string, password: string): Promise<AuthResponse> {
		return this.execute<AuthResponse>('POST', '/api/auth', {
			email: email,
			password: password,
		})
	}

	private async execute<T>(method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, body: any = null): Promise<T> {
		const response = await fetch(url, {
			method: method,
			headers: this.token
				? {
						'Content-Type': 'application/json',
						Authorization: `Bearer ${this.token}`,
				  }
				: { 'Content-Type': 'application/json' },
			body: body ? JSON.stringify(body) : undefined,
		})
		if (response.status === 401) {
			document.location.href = '/'
			throw new Error('Unauthorized')
		}

		return await response.json()
	}
}

const instance = new ApiClient()
export default instance
