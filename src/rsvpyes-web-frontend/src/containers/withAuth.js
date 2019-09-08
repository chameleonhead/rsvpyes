import { connect } from 'react-redux'
import { requestLogin, logout } from '../actions'

const mapStateToProps = (state, ownProps) => {
  return {
    auth: state.auth
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    login: () => {
      dispatch(requestLogin(null, null))
    },
    logout: () => {
      dispatch(logout());
    }
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
);


