import { LOGIN_REQUESTED, LOGIN_ACCEPTED, LOGOUT } from '../actions';

const initialState = {
  isAuthenticated: false,
};

function auth(state = initialState, action) {
  switch (action.type) {
    case LOGIN_REQUESTED:
      return Object.assign({}, state, {
        isAuthenticated: true,
      });
    case LOGIN_ACCEPTED:
      return state;
    case LOGOUT:
      return Object.assign({}, state, {
        isAuthenticated: false,
      });
    default:
      return state;
  }
};

export default auth;