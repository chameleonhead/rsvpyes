import { connect } from 'react-redux'
import { fetchMeetings } from '../redux/actions'

const mapStateToProps = (state, ownProps) => {
  return {
    meetings: state.meetings.meetings
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    fetchMeetings: () => {
      dispatch(fetchMeetings());
    }
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
);


