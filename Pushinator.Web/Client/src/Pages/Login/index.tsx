import React, { ChangeEvent, FormEvent, useState } from 'react'
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles'
import {Box, Button, CircularProgress, Container, TextField, Typography} from '@material-ui/core'
import apiClient from '../../ApiClient'
import { toast } from 'react-toastify'
import { useHistory } from 'react-router-dom'

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		form: {
			position: 'relative',
		},
		loginRow: {
			margin: '20px 0',
		},
		progress: {
			position: 'absolute',
			top: '50%',
			left: '50%',
			marginTop: -12,
			marginLeft: -12,
		},
	}),
)

const Login: React.FC<any> = () => {
	const classes = useStyles()
	const history = useHistory()
	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')
	const [isLoading, setIsLoading] = useState(false)
	
	const emailChangeCallback = (ev: ChangeEvent<HTMLInputElement>) => setEmail(ev.target.value)
	const passwordChangeCallback = (ev: ChangeEvent<HTMLInputElement>) => setPassword(ev.target.value)

	const onSubmit = (ev: FormEvent) => {
		ev.preventDefault()
		setIsLoading(true)
		apiClient.auth(email, password).then((value) => {
			setEmail('')
			setPassword('')
			setIsLoading(false)

			if (!value.ok) {
				toast('Неверный логин/пароль')
				return
			}

			apiClient.token = value.token
			history.push('/')
		})
	}

	return (
		<Container>
			<Box className={classes.form}>
				<Typography variant="h4" component="h1" gutterBottom>
					Логин
				</Typography>
				<form onSubmit={onSubmit}>
					<div className={classes.loginRow}>
						<TextField label="E-mail" onChange={emailChangeCallback} value={email} />
					</div>
					<div className={classes.loginRow}>
						<TextField type="password" onChange={passwordChangeCallback} value={password} label="Пароль" />
					</div>
					<Button type="submit" variant="contained" color="primary">
						Вход
					</Button>
				</form>
				{isLoading && <CircularProgress size={24} className={classes.progress} />}
			</Box>
		</Container>
	)
}

export default Login
