import {
  LOGIN_URL,
  MEETING_PLANS_URL
} from "./constants";

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
    return fetch(MEETING_PLANS_URL)
      .then(response => response.json())
      .then(json => dispatch(receiveMeetings(json.meetings)))
      .catch(console.error);
  }
}


export const CREATE_MEETING_PLAN_REQUESTED = 'CREATE_MEETING_PLAN_REQUESTED';
export function requestMeetingCreation() {
  return { type: CREATE_MEETING_PLAN_REQUESTED };
}

export const CREATE_MEETING_PLAN_SUCCEEDED = 'CREATE_MEETING_PLAN_SUCCEEDED';
export function meetingCreationSuccess() {
  return { type: CREATE_MEETING_PLAN_SUCCEEDED };
}

export function createMeetingPlan({meetingName, placeId, placeName, beginAt, endAt}) {
  return dispatch => {
    dispatch(requestMeetingCreation());
    const method = "POST";
    const body = new URLSearchParams();
    body.set('meetingName', meetingName);
    body.set('placeId', placeId);
    body.set('placeName', placeName);
    body.set('beginAt', beginAt);
    body.set('endAt', endAt);
    const headers = {
      'Accept': 'application/json',
      'Content-Type': 'application/x-www-form-urlencoded'
    };
    return fetch(MEETING_PLANS_URL, { method, headers, body })
      .then(response => response.json())
      .then(json => dispatch(meetingCreationSuccess(json.access_token)));
  }
}