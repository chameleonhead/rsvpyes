import { LOGIN_REQUESTED, LOGIN_ACCEPTED, LOGOUT, LOGIN_FAILED } from '../actions';

const initialState = {
  isRequesting: false,
  isAuthenticated: false,
  accessToken: null,
  error: null
};

function authReducer(state = initialState, action) {
  switch (action.type) {
    case LOGIN_REQUESTED:
      return Object.assign({}, state, {
        isRequesting: true,
        isAuthenticated: false,
        error: null
      });
    case LOGIN_ACCEPTED:
      return Object.assign({}, state, {
        isRequesting: false,
        isAuthenticated: true,
        accessToken: action.payload
      });
    case LOGIN_FAILED:
      return Object.assign({}, state, {
        isRequesting: false,
        isAuthenticated: false,
        error: action.payload
      });
    case LOGOUT:
      return Object.assign({}, state, {
        isAuthenticated: false,
        accessToken: null
      });
    default:
      return state;
  }
};

export default authReducer;