import React from 'react'
import { AppBar, Button, Typography } from '@material-ui/core'
export const Toolbar: React.FC<any> = () => {
	return (
		<AppBar position="static">
			<Toolbar>
				<Typography variant="h6">News</Typography>fff
				<Button color="inherit">Login</Button>
			</Toolbar>
		</AppBar>
	)
}
