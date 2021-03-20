export class TokenStorage {
	private static readonly TokenKey = 'Token'

	public static getToken(): string | null {
		return window.localStorage[TokenStorage.TokenKey]
	}

	public static setToken(token: string) {
		window.localStorage[TokenStorage.TokenKey] = token
	}

	public static clearToken() {
		window.localStorage.removeItem(TokenStorage.TokenKey)
	}
}
