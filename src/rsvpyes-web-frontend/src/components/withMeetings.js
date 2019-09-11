import { connect } from 'react-redux'
import { fetchMeetings, createMeetingPlan } from '../redux/actions'

const mapStateToProps = (state, ownProps) => {
  return {
    meetings: state.meetings.meetings
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    fetchMeetings: () => {
      dispatch(fetchMeetings());
    },
    createMeetings: ({meetingName, placeId, placeName, beginAt, endAt}) => {
      dispatch(createMeetingPlan({meetingName, placeId, placeName, beginAt, endAt}))
    }
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
);


