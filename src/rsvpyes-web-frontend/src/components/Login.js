import React from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Container } from '@material-ui/core';
import withAuth from '../containers/withAuth';

function Login(props) {
  let { from } = props.location.state || { from: { pathname: "/" } };
  let redirectToReferrer = props.auth.isAuthenticated;

  if (redirectToReferrer) return <Redirect to={from} />;

  return (
    <Container>
      <h1>Login</h1>
      <Button onClick={props.login}>LOGIN</Button>
    </Container>
  );
}

export default withAuth(Login);