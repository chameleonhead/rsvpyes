import auth from './auth';
import { combineReducers } from 'redux'

const reducers = {
  auth
};

const appReducer = combineReducers(reducers);

const rootReducer = (state, action) => {
  if (action.type === 'SIGN_OUT') {
    state = undefined;
  }
  return appReducer(state, action);
}

export default rootReducer;