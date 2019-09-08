export const LOGIN_REQUESTED = 'LOGIN_REQUESTED';
export function requestLogin(userName, password) {
  return { type: LOGIN_REQUESTED, userName, password };
}

export const LOGIN_ACCEPTED = 'LOGIN_ACCEPTED';
export function acceptLogin(accessToken) {
  return { type: LOGIN_ACCEPTED, accessToken };
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
  return { type: FETCH_MEETINGS_SUCCEEDED, meetings: meetings };
}

export function fetchMeetings() {
  return dispatch => {
    dispatch(requestMeetings());
    return fetch('meetings.json')
      .then(response => response.json())
      .then(json => dispatch(receiveMeetings(json.meetings)));
  }
}