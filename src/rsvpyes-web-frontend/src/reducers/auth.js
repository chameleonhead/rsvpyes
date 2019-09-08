import { LOGIN_REQUESTED, LOGIN_ACCEPTED } from '../actions';

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
    default:
      return state;
  }
};

export default auth;