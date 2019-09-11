export default {
    today: () => {
        const date = new Date();
        return new Date(date.getFullYear(), date.getMonth(), date.getDate());
    },
    nowBy: roundMin => {
        const date = new Date();
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), Math.round(date.getMinutes() / roundMin));
    },
    addHours: (date, hoursToAdd) => {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), Math.round(date.getMinutes() / roundMin) + hoursToAdd * 60);
    }
}