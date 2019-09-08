import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import withAuth from './withAuth';

function ProtectedRoute({ component: Component, auth, ...rest }) {
  return (
    <Route {...rest} render={props =>
      auth.isAuthenticated
        ? <Component {...props} />
        : <Redirect to={{
          pathname: "/login",
          state: { from: props.location }
        }} />
    } />
  )
}

export default withAuth(ProtectedRoute);