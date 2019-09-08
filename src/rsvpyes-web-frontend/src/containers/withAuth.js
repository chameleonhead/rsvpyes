import { connect } from 'react-redux'
import { requestLogin } from '../actions'

const mapStateToProps = (state, ownProps) => {
  return {
    auth: state.auth
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    onLogin: () => {
      dispatch(requestLogin(null, null))
    }
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
);


