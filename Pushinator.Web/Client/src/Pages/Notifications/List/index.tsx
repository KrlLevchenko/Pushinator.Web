import React from 'react'
import { Toolbar } from '../../../Components/Toolbar'
import { Button, Container, createStyles, Theme } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		clientPushButton: {
			margin: theme.spacing(1),
		},
	}),
)

const NotificationList: React.FC<any> = () => {
	const classes = useStyles()

	return (
		<>
			<Toolbar />
			<Container className={classes.clientPushButton}>
				<Button>Click me</Button>
			</Container>
		</>
	)

	// const [isPushButtonVisible, setIsPushButtonVisible] = useState(false)
	//
	// useEffect(() => {
	// 	navigator.serviceWorker.register('/serviceWorker.js').then((x) => {
	// 		setIsPushButtonVisible(true);
	// 	})
	// 	Notification.requestPermission().then((s) => console.log('notification status', s))
	// }, [])
	//
	// const showNotification = () => {
	// 	navigator.serviceWorker.getRegistration().then((r) => r?.showNotification('Test local notification'))
	// }
	//
	// const longCalculation = () => {
	// 	navigator.serviceWorker.getRegistration().then((r) => {
	// 		r?.active?.postMessage({ max: 199999 })
	// 	})
	// }
	//
	// return (
	// 	<>
	// 		<Toolbar />
	//
	// 		<Container className={classes.clientPushButton}>
	// 			{isPushButtonVisible && (
	// 				<>
	// 					<Button variant="contained" onClick={showNotification}>
	// 						Клиентский пуш
	// 					</Button>
	// 					<Button variant="contained" onClick={longCalculation}>
	// 						Долгие вычисление и показа пуша
	// 					</Button>
	// 				</>
	// 			)}
	// 		</Container>
	// 	</>
	// )
}

export default NotificationList
