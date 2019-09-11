import React from 'react';
import { Dialog } from '@material-ui/core';
import DateUtils from '../utils/DateUtils';

class MeetingPlanCreate extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            meetingName: '',
            userName: '',
            userId: null,
            beginAt: DateUtils.nowBy(15),
            endAt: DateUtils.addHours(DateUtils.nowBy(15), 2)
        }
    }

    render() {
        return null;
    }
}