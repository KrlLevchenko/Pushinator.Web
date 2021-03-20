import React, { useCallback } from 'react'
import { AppBar, Button, createStyles, Link, Toolbar as MaterialUIToolbar} from '@material-ui/core'
import { TokenStorage } from '../../ApiClient/tokenStorage'
import { useHistory } from 'react-router-dom'
import { makeStyles, Theme } from '@material-ui/core/styles'

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		spacer: {
			flexGrow: 1,
		},
		toolbarLink: {
			color: '#fff',
		},
	}),
)

export const Toolbar: React.FC<any> = () => {
	const isLogged = !!TokenStorage.getToken()

	const history = useHistory()
	const classes = useStyles()

	const logout = useCallback(() => {
		TokenStorage.clearToken()
		history.push('/')
	}, [history])

	return (
		<AppBar position="static">
			<MaterialUIToolbar>
				{!isLogged && (
					<>
						<Link className={classes.toolbarLink} component={Button} color="inherit" href="/login">
							Войти
						</Link>
						<Link className={classes.toolbarLink} component={Button} color="inherit" href="/register">
							Зарегистрироваться
						</Link>
					</>
				)}
				{isLogged && (
					<>
						<div className={classes.spacer} />
						<Link className={classes.toolbarLink} component={Button} onClick={logout} color="primary">
							Выйти
						</Link>
					</>
				)}
			</MaterialUIToolbar>
		</AppBar>
	)
}
