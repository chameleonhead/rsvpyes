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
    createMeetingPlan: ({meetingName, placeName, beginAt, endAt}, cb) => {
      dispatch(createMeetingPlan({meetingName, placeName, beginAt, endAt}, cb))
    }
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
);


