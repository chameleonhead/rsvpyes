export const LOGIN_REQUESTED = 'LOGIN_REQUESTED';
export function requestLogin(userName, password) {
  return { type: LOGIN_REQUESTED, userName, password };
}

export const LOGIN_ACCEPTED = 'LOGIN_ACCEPTED';
export function acceptLogin(accessToken) {
  return { type: LOGIN_ACCEPTED, accessToken };
}