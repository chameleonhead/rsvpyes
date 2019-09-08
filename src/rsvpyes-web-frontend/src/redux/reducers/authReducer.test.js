import { requestLogin } from '../actions';
import auth from './authReducer';

it('state change into requested', () => {
  let newState = auth({ isAuthenticated: false }, requestLogin('user1', 'password'));
  expect(newState).toEqual({ isAuthenticated: true });
});
