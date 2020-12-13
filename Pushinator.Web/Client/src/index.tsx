import React from 'react';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Switch,
    Route,
} from "react-router-dom";
import Login from "./Pages/Login";
import NotificationList from "./Pages/Notifications/List";
import UsersList from "./Pages/Users/List";


ReactDOM.render(
  <React.StrictMode>
      <Router>
          <Switch>
              <Route path="/login">
                  <Login />
              </Route>
              <Route path="/">
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