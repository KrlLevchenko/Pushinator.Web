import { BaseClient } from './baseClient'

class AuthClient extends BaseClient {
	auth(request: AuthRequest): Promise<AuthResponse> {
		return this.execute<AuthResponse>('POST', '/api/auth/login', {
			email: request.email,
			password: request.password,
		})
	}

	register(request: RegisterRequest): Promise<RegisterResponse> {
		return this.execute<RegisterResponse>('POST', '/api/auth/register', request)
	}
}
const instance = new AuthClient()
export default instance

export interface AuthResponse {
	ok: boolean
	token: string
}

export interface RegisterResponse {
	userExist: boolean
	token: string
}

export interface AuthRequest {
	email: string
	password: string
}

export interface RegisterRequest {
	email: string
	password: string
	name: string
}
