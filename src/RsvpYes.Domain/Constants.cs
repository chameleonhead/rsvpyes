namespace RsvpYes.Domain
{
    internal class Constants
    {
        public const string MeetingPlanAlreadyFixed = "会の予定はすでに確定しています。";
        public const string FixScheduleAndPlaceFirst = "日程と場所が決まっていません。";
        public const string ParticipantAlreadyExists = "既に登録済みの参加者です。";
        public const string ParticipantNotExistsError = "指定された参加者は存在しません。";
        public const string CandidateScheduleAlreadyExistsError = "既に存在する日程です。";
        public const string CandidateScheduleNotExistsError = "指定された日程は存在しません。";
        public const string CandidatePlaceAlreadyExistsError = "既に存在する候補場所です。";
        public const string CandidatePlaceNotExistsError = "指定された候補場所は存在しません。";
        public const string DurationMustBeGreaterOrEqualToZeroError = "時間には負の値を指定できません。";
        public const string EndTimeMustBeGreaterOrEqualToStartTimeError = "終了時間は開始時間以降を指定してください。";
        public const string UserMailAddressCanStoreLessThanOrEqualToFiveError = "ユーザーのメールアドレスは最大5件まで登録できます。";
        public const string UserPhoneNumberCanStoreLessThanOrEqualToFiveError = "ユーザーの電話番号は最大5件まで登録できます。";
        public const string UserMailAddressNotExistsError = "指定されたメールアドレスがユーザーにはありません。";
        public const string UserPhoneNumberNotExistsError = "指定された電話番号がユーザーにはありません。";
        public const string UserMailAddressMustHaveAtLeastOneError = "ユーザーにはメールアドレスが最低でも1つは必要です。";
    }
}