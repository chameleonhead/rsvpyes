import React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { ThemeProvider } from '@material-ui/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import store from './store';
import theme from './theme';
import RsvpYesAppBar from './components/RsvpYesAppBar';
import Login from './components/Login';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './components/Home';
import Meetings from './components/Meetings';
import Responses from './components/Responses';
import NotFound from './components/NotFound';

function App(props) {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <RsvpYesAppBar />
          <Switch>
            <ProtectedRoute path="/" exact component={Home} />
            <ProtectedRoute path="/meetings" component={Meetings} />
            <Route path="/responses" exact component={Responses} />
            <Route path="/login" component={Login} />
            <Route component={NotFound} />
          </Switch>
        </Router>
      </ThemeProvider>
    </Provider>
  );
}

export default App;
