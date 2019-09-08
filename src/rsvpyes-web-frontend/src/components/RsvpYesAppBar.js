import React from 'react';
import { withRouter } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button } from '@material-ui/core';
import withAuth from './withAuth';

function RsvpYesAppBar(props) {
  if (props.location.pathname === '/login') {
    return null;
  }

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h5">RsvpYes</Typography>
        {props.auth.isAuthenticated
          ? <Button color="inherit" onClick={() => {
            props.logout();
            props.history.push('/');
          }}>LOGOUT</Button>
          : <Button color="inherit" onClick={() => props.history.push('/login')}>LOGIN</Button>}
      </Toolbar>
    </AppBar>
  );
}

export default withRouter(withAuth(RsvpYesAppBar)); 