import { LOGIN_URL } from "./constants";

export const LOGIN_REQUESTED = 'LOGIN_REQUESTED';
export function requestLogin({ userName, password }) {
  return { type: LOGIN_REQUESTED, payload: { userName, password } };
}

export const LOGIN_ACCEPTED = 'LOGIN_ACCEPTED';
export function acceptLogin(accessToken) {
  return { type: LOGIN_ACCEPTED, payload: accessToken };
}

export const LOGIN_FAILED = 'LOGIN_FAILED';
export function failLogin(message) {
  return { type: LOGIN_FAILED, payload: message };
}

export function login({ userName, password }) {
  return dispatch => {
    dispatch(requestLogin({ userName, password }));
    const method = "POST";
    const body = new URLSearchParams();
    body.set('grant_type', 'password');
    body.set('client_id', 'rsvpyes.client');
    body.set('client_secret', 'secret');
    body.set('username', userName);
    body.set('password', password);
    const headers = {
      'Accept': 'application/json',
      'Content-Type': 'application/x-www-form-urlencoded'
    };
    return fetch(LOGIN_URL, { method, headers, body })
      .then(response => response.json())
      .then(json => json.access_token
        ? dispatch(acceptLogin(json.access_token))
        : dispatch(failLogin('ログインできません。ユーザー名またはパスワードを確認してください。'))
      )
      .catch(_ => dispatch(failLogin('ログインできません。ユーザー名またはパスワードを確認してください。')))
  }
}

export const LOGOUT = 'LOGOUT';
export function logout() {
  return { type: LOGOUT };
}

export const FETCH_MEETINGS_REQUESTED = 'FETCH_MEETINGS_REQUESTED';
export function requestMeetings() {
  return { type: FETCH_MEETINGS_REQUESTED };
}

export const FETCH_MEETINGS_SUCCEEDED = 'FETCH_MEETINGS_SUCCEEDED';
export function receiveMeetings(meetings) {
  return { type: FETCH_MEETINGS_SUCCEEDED, payload: meetings };
}

export function fetchMeetings() {
  return dispatch => {
    dispatch(requestMeetings());
    return fetch('meetings.json')
      .then(response => response.json())
      .then(json => dispatch(receiveMeetings(json.meetings)))
      .catch(console.error);
  }
}