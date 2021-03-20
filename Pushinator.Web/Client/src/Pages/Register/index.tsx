import React, { ChangeEvent, FormEvent, useState } from 'react'
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles'
import { Box, Button, CircularProgress, Container, TextField, Typography } from '@material-ui/core'
import apiClient, { RegisterRequest } from '../../ApiClient/authClient'
import { toast } from 'react-toastify'
import { useHistory } from 'react-router-dom'
import { Toolbar } from '../../Components/Toolbar'
import { TokenStorage } from '../../ApiClient/tokenStorage'

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		form: {
			position: 'relative',
			margin: '10px 0',
		},
		registerRow: {
			margin: '20px 0',
		},
		progress: {
			position: 'absolute',
			top: '50%',
			left: '50%',
			marginTop: -12,
			marginLeft: -12,
		},
		oauth2Row: {
			display: 'flex',
		},
		oauth2IconWrapper: {
			display: 'block',
			width: '48px',
			height: '48px',
			marginTop: '15px',
			marginRight: '10px',
			overflow: 'hidden',
		},
		oauth2Icon: {
			width: '100%',
			height: '100%',
		},
	}),
)

const Login: React.FC<any> = () => {
	const classes = useStyles()
	const history = useHistory()
	const [isLoading, setIsLoading] = useState(false)

	const [formData, setFormData] = useState<RegisterRequest>({
		email: '',
		name: '',
		password: '',
	})

	const createUpdater = (prop: keyof RegisterRequest) => (ev: ChangeEvent<HTMLInputElement>) =>
		setFormData({ ...formData, [prop]: ev.target.value })

	const onSubmit = (ev: FormEvent) => {
		ev.preventDefault()
		setIsLoading(true)
		apiClient
			.register(formData)
			.then((data) => {
				if (data.userExist) {
					toast.error('Пользователь уже существует')
					return
				}
				toast.info('Пользователь зарегистрирован')
				TokenStorage.setToken(data.token)
				history.push('/')
			})
			.finally(() => setIsLoading(false))
	}

	return (
		<>
			<Toolbar />

			<Container>
				<Box className={classes.form}>
					<Typography variant="h4" component="h1" gutterBottom>
						Регистрация
					</Typography>
					<form onSubmit={onSubmit}>
						<div className={classes.registerRow}>
							<TextField label="E-mail" onChange={createUpdater('email')} value={formData.email} />
						</div>
						<div className={classes.registerRow}>
							<TextField
								type="password"
								onChange={createUpdater('password')}
								value={formData.password}
								label="Пароль"
							/>
						</div>
						<div className={classes.registerRow}>
							<TextField onChange={createUpdater('name')} value={formData.name} label="Имя" />
						</div>
						<Button type="submit" variant="contained" color="primary">
							Вход
						</Button>
					</form>
					{isLoading && <CircularProgress size={24} className={classes.progress} />}
				</Box>
			</Container>
		</>
	)
}

export default Login
