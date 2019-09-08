import { LOGIN_REQUEST, LOGIN_ACCEPTED } from '../actions';

export const initialState = {
  isAuthenticated: false
};

export default function auth(state = initialState, action) {
  switch (action.type) {
    case LOGIN_REQUEST:
      return Object.assign({}, state, {
        isAuthenticated: true,
      });
    case LOGIN_ACCEPTED:
      return state;
    default:
      return state;
  }
};