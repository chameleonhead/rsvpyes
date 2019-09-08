import { combineReducers } from 'redux'
import authReducer from './authReducer';
import meetingsReducer from './meetingsReducer';
import { LOGOUT } from '../actions';

const reducers = {
  auth: authReducer,
  meetings: meetingsReducer
};

const appReducer = combineReducers(reducers);

const rootReducer = (state, action) => {
  if (action.type === LOGOUT) {
    state = undefined;
  }
  return appReducer(state, action);
}

export default rootReducer;