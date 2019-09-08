import React from 'react';
import { Redirect } from 'react-router-dom';
import Container from '@material-ui/core/Container';
import { Button } from '@material-ui/core';

class Login extends React.Component {

  constructor(props) {
    super(props);
    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    this.props.onLogin();
  }

  render() {
    let { from } = this.props.location.state || { from: { pathname: "/" } };
    let redirectToReferrer = this.props.auth.isAuthenticated;

    if (redirectToReferrer) return <Redirect to={from} />;

    return (
      <Container>
        <h1>Login</h1>
        <Button onClick={this.handleClick}>LOGIN</Button>
      </Container>
    );
  }
}

export default Login;