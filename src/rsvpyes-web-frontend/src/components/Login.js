import React from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Container, TextField, Paper, Grid, Typography } from '@material-ui/core';
import withAuth from './withAuth';

class Login extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      userName: '',
      password: ''
    }
  }

  handleLogin() {
    this.props.login({
      userName: this.state.userName, 
      password: this.state.password
    });
  }

  render() {
    let { from } = this.props.location.state || { from: { pathname: "/" } };
    let redirectToReferrer = this.props.auth.isAuthenticated;

    if (redirectToReferrer) return <Redirect to={from} />;

    return (
      <Container maxWidth="sm" style={{ marginTop: '80px' }}>
        <Paper style={{ padding: '20px', paddingTop: '10px' }}>
          <h1>Login</h1>
          <form>
            <Grid container spacing={3}>
              {this.props.auth.error && (<Grid item xs={12}><Typography color="error">{this.props.auth.error}</Typography></Grid>)}
              <Grid item xs={12}>
                <TextField label="ユーザー名" value={this.state.userName} onChange={e => this.setState({ ...this.state, userName: e.target.value })} fullWidth />
              </Grid>
              <Grid item xs={12}>
                <TextField label="パスワード" type="password" value={this.state.value} onChange={e => this.setState({ ...this.state, password: e.target.value })} fullWidth />
              </Grid>
              <Grid item xs={12}>
                <Grid >
                  <Button color="primary" variant="outlined" onClick={this.handleLogin.bind(this)}>ログイン</Button>
                </Grid>
              </Grid>
            </Grid>
          </form>
        </Paper>
      </Container>
    );
  }
}

export default withAuth(Login);