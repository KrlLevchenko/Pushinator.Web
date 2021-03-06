import React, {ChangeEvent, FormEvent, useState} from 'react'
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles'
import {Box, Button, CircularProgress, Container, TextField, Typography} from '@material-ui/core'
import apiClient from '../../ApiClient'
import {toast} from 'react-toastify'
import {useHistory} from 'react-router-dom'
import FacebookIcon from './assets/facebook.png'
import GoogleIcon from './assets/google.jpg'
import MicrosoftIcon from './assets/microsoft.png'

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
        }).catch(() => setIsLoading(false))
    }

    return (
        <Container>
            <Box className={classes.form}>
                <Typography variant="h4" component="h1" gutterBottom>
                    Логин
                </Typography>
                <form onSubmit={onSubmit}>
                    <div className={classes.loginRow}>
                        <TextField label="E-mail" onChange={emailChangeCallback} value={email}/>
                    </div>
                    <div className={classes.loginRow}>
                        <TextField type="password" onChange={passwordChangeCallback} value={password} label="Пароль"/>
                    </div>
                    <Button type="submit" variant="contained" color="primary">
                        Вход
                    </Button>
                </form>
                {isLoading && <CircularProgress size={24} className={classes.progress}/>}

                <div className={classes.oauth2Row}>
                    <a className={classes.oauth2IconWrapper} href="/AuthExternal/Facebook">
                        <img className={classes.oauth2Icon} alt="Facebook login" src={FacebookIcon}/>
                    </a>
                    <a className={classes.oauth2IconWrapper} href="/AuthExternal/Google">
                        <img className={classes.oauth2Icon} alt="Google login" src={GoogleIcon}/>
                    </a>
                    <a className={classes.oauth2IconWrapper} href="/AuthExternal/Microsoft">
                        <img className={classes.oauth2Icon} alt="Microsoft login" src={MicrosoftIcon}/>
                    </a>
                </div>
            </Box>
        </Container>
    )
}

export default Login
