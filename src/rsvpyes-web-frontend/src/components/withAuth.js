import { connect } from 'react-redux'
import { login, logout } from '../redux/actions'

const mapStateToProps = (state, ownProps) => {
  return {
    auth: state.auth
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    login: ({ userName, password }) => {
      dispatch(login({ userName, password }))
    },
    logout: () => {
      dispatch(logout());
    }
  }
}

const withAuth = connect(
  mapStateToProps,
  mapDispatchToProps
);

export default withAuth;


