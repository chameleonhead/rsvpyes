import React from 'react';
import { Container, CssBaseline, Paper, Typography } from '@material-ui/core';
import withMeetings from './withMeetings';

function Meeting(props) {
  return (
    <Paper elevation={1}>
      <Typography role="h1">{props.meeting.name}</Typography>
    </Paper>
  );
}

function Meetings(props) {
  let meetings = props.meetings || [];
  return (
    <React.Fragment>
      <CssBaseline />
      <Container>
        <h1>会一覧</h1>
        {meetings.map(meeting => <Meeting key={meeting.id} meeting={meeting} />)}
      </Container>
    </React.Fragment>
  );
}

class MeetingsContainer extends React.Component {
  componentDidMount() {
    this.props.fetchMeetings();
  }
  render() {
    return (<Meetings {... this.props} />)
  }
}

export default withMeetings(MeetingsContainer);