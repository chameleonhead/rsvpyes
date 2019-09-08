import { combineReducers } from 'redux'
import auth from './auth';
import meetings from './meetings';
import { LOGOUT } from '../actions';

const reducers = {
  auth,
  meetings
};

const appReducer = combineReducers(reducers);

const rootReducer = (state, action) => {
  if (action.type === LOGOUT) {
    state = undefined;
  }
  return appReducer(state, action);
}

export default rootReducer;