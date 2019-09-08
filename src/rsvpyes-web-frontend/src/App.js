import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { ThemeProvider } from '@material-ui/styles';
import theme from './theme';
import Login from './containers/LoginContainer';
import ProtectedRoute from './containers/ProtectedRoute';
import Home from './components/Home';
import Meetings from './components/Meetings';
import NotFound from './components/NotFound';


class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isAuthenticated: false
    };
  }

  render() {
    return (
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Switch>
            <Route path="/login" component={Login} />
            <ProtectedRoute path="/" exact component={Home} />
            <ProtectedRoute path="/meetings" component={Meetings} />
            <Route component={NotFound} />
          </Switch>
        </Router>
      </ThemeProvider>
    );
  }
}

export default App;
