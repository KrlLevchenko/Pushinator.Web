import React from 'react';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    useHistory
} from "react-router-dom";
import Login from "./Pages/Login";
import NotificationList from "./Pages/Notifications/List";
import UsersList from "./Pages/Users/List";
import apiClient from "./ApiClient"

import 'react-toastify/dist/ReactToastify.css';
import {ToastContainer} from "react-toastify";

const RedirectLogic = ()=>{
    const history = useHistory();
    if (apiClient.token){
        history.push('/notifications')
    } else {
        history.push('/login')
    }
    
    return <></>
}

ReactDOM.render(
  <React.StrictMode>
      <ToastContainer />
      <Router>
          <Switch>
              <Route path="/" exact>
                  <RedirectLogic/>
              </Route>
              <Route path="/login">
                  <Login />
              </Route>
              
              <Route path="/notifications">
                  <NotificationList />
              </Route>
              <Route path="/users">
                  <UsersList />
              </Route>
          </Switch>
      </Router>
      
  </React.StrictMode>,
  document.getElementById('root')
);