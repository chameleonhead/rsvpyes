import { FETCH_MEETINGS_REQUESTED, FETCH_MEETINGS_SUCCEEDED } from '../actions';

const initialState = {
  isFetching: false,
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
        meetings: action.meetings
      });
    default:
      return state;
  }
};

export default meetingsReducer;