import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Grid,
  List,
  ListItem,
  FormControlLabel,
  Switch
} from '@material-ui/core';
import {
  MuiPickersUtilsProvider,
  KeyboardTimePicker,
  KeyboardDatePicker,
} from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { startOfHour, addHours, compareAsc } from 'date-fns';
import jaLocale from 'date-fns/locale/ja';
import withMeetings from './withMeetings';

class MeetingPlanCreateDialog extends React.Component {
  constructor(props) {
    super(props);
    const date = new Date();
    this.state = {
      meetingName: '',
      placeName: '',
      scheduleSpecified: false,
      beginAt: addHours(startOfHour(date), 1),
      endAt: addHours(startOfHour(date), 3),
      open: false
    }
  }

  componentDidMount() {
    const date = new Date();
    this.setState({
      meetingName: '',
      placeName: '',
      scheduleSpecified: false,
      beginAt: addHours(startOfHour(date), 1),
      endAt: addHours(startOfHour(date), 3),
      open: false
    });
  }

  handleOpen() {
    this.setState({ ...this.state, open: true });
  }

  handleClose() {
    this.setState({ ...this.state, open: false });
  }

  handleSubmit() {
    const { meetingName, placeName, scheduleSpecified, beginAt, endAt } = this.state;
    this.props.createMeetingPlan({
      meetingName,
      placeName,
      beginAt: scheduleSpecified ? beginAt : '',
      endAt: scheduleSpecified ? endAt : ''
    }, () => this.setState({ ...this.state, open: false }));
  }

  handleMeetingNameChange(e) {
    this.setState({ ...this.state, meetingName: e.target.value });
  }

  handlePlaceNameChange(e) {
    this.setState({ ...this.state, placeName: e.target.value });
  }

  handleScheduleSpecifiedChange(e) {
    this.setState({ ...this.state, scheduleSpecified: e.target.checked });
  }

  handleBeginAtChange(value) {
    let beginAt = value;
    let endAt = this.state.endAt;
    if (compareAsc(beginAt, endAt) > 0) {
      endAt = beginAt;
    }
    this.setState({ ...this.state, beginAt, endAt });
  }

  handleEndAtChange(value) {
    let beginAt = this.state.beginAt;
    let endAt = value;
    if (compareAsc(beginAt, endAt) > 0) {
      beginAt = endAt;
    }
    this.setState({ ...this.state, beginAt, endAt });
  }

  render() {
    return (
      <React.Fragment>
        <Button onClick={this.handleOpen.bind(this)}>会を作成</Button>
        <MuiPickersUtilsProvider utils={DateFnsUtils} locale={jaLocale}>
          <Dialog
            open={this.state.open}
            onClose={this.handleClose.bind(this)}
            aria-labelledby="form-dialog-title"
            scroll="body"
            fullWidth
            maxWidth='sm'>
            <DialogTitle id="alert-dialog-title">会予定を作成</DialogTitle>
            <DialogContent dividers={false}>
              <List>
                <ListItem>
                  <TextField
                    type="text"
                    margin="none"
                    fullWidth
                    label="会の名前"
                    value={this.state.meetingName}
                    onChange={this.handleMeetingNameChange.bind(this)} />
                </ListItem>
                <ListItem>
                  <TextField
                    type="text"
                    margin="none"
                    fullWidth
                    label="場所"
                    value={this.state.placeName}
                    onChange={this.handlePlaceNameChange.bind(this)} />
                </ListItem>
                <ListItem>
                  <FormControlLabel
                    label="予定日入力"
                    control={
                      <Switch
                        value={this.state.scheduleSpecified}
                        onChange={this.handleScheduleSpecifiedChange.bind(this)} />} />
                </ListItem>
                <ListItem>
                  <Grid container spacing={1}>
                    <Grid item xs={6}>
                      <KeyboardDatePicker
                        fullWidth
                        margin="none"
                        format="yyyy/MM/dd"
                        label="日付"
                        value={this.state.beginAt}
                        onChange={this.handleBeginAtChange.bind(this)}
                        disabled={!this.state.scheduleSpecified}
                      />
                    </Grid>
                    <Grid item xs={6}>
                      <KeyboardTimePicker
                        fullWidth
                        margin="none"
                        format="HH:mm"
                        label="開始時間"
                        value={this.state.beginAt}
                        onChange={this.handleBeginAtChange.bind(this)}
                        disabled={!this.state.scheduleSpecified}
                      />
                    </Grid>
                  </Grid>
                </ListItem>
                <ListItem>
                  <Grid container spacing={1}>
                    <Grid item xs={6}>
                      <KeyboardDatePicker
                        fullWidth
                        margin="none"
                        format="yyyy/MM/dd"
                        label="終了日"
                        value={this.state.endAt}
                        onChange={this.handleEndAtChange.bind(this)}
                        disabled={!this.state.scheduleSpecified}
                      />
                    </Grid>
                    <Grid item xs={6}>
                      <KeyboardTimePicker
                        fullWidth
                        margin="none"
                        format="HH:mm"
                        label="終了時間"
                        value={this.state.endAt}
                        onChange={this.handleEndAtChange.bind(this)}
                        disabled={!this.state.scheduleSpecified}
                      />
                    </Grid>
                  </Grid>
                </ListItem>
              </List>
            </DialogContent>
            <DialogActions>
              <Button onClick={this.handleClose.bind(this)} color="primary">
                キャンセル
            </Button>
              <Button onClick={this.handleSubmit.bind(this)} color="primary" autoFocus>
                登録
            </Button>
            </DialogActions>
          </Dialog>
        </MuiPickersUtilsProvider>
      </React.Fragment >
    );
  }
}

export default withMeetings(MeetingPlanCreateDialog);