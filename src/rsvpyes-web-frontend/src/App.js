import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import theme from './theme';
import { ThemeProvider } from '@material-ui/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import Login from './containers/LoginContainer';
import ProtectedRoute from './containers/ProtectedRoute';
import Home from './components/Home';
import Meetings from './components/Meetings';
import Responses from './components/Responses';
import NotFound from './components/NotFound';


class App extends React.Component {
  render() {
    return (
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Switch>
            <ProtectedRoute path="/" exact component={Home} />
            <ProtectedRoute path="/meetings" component={Meetings} />
            <Route path="/responses" exact component={Responses} />
            <Route path="/login" component={Login} />
            <Route component={NotFound} />
          </Switch>
        </Router>
      </ThemeProvider>
    );
  }
}

export default App;
