export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export function requestLogin(userName, password) {
  return { type: LOGIN_REQUEST, userName, password };
}

export const LOGIN_ACCEPTED = 'LOGIN_ACCEPTED';
export function acceptLogin(accessToken) {
  return { type: LOGIN_ACCEPTED, accessToken };
}