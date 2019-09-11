import {
  FETCH_MEETINGS_REQUESTED,
  FETCH_MEETINGS_SUCCEEDED,
  CREATE_MEETING_PLAN_REQUESTED,
  CREATE_MEETING_PLAN_SUCCEEDED
} from '../actions';

const initialState = {
  isFetching: false,
  isRequesting: false,
  meetings: [],
};

function meetingsReducer(state = initialState, action) {
  switch (action.type) {
    case FETCH_MEETINGS_REQUESTED:
      return Object.assign({}, state, {
        isFetching: true,
      });
    case FETCH_MEETINGS_SUCCEEDED:
      return Object.assign({}, state, {
        isFetching: false,
        meetings: action.payload
      });
    case CREATE_MEETING_PLAN_REQUESTED:
      return Object.assign({}, state, {
        isRequesting: true,
      });
    case CREATE_MEETING_PLAN_SUCCEEDED:
      return Object.assign({}, state, {
        isRequesting: false,
      });
    default:
      return state;
  }
};

export default meetingsReducer;