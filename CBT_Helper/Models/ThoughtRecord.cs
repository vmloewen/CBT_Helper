using System;

namespace CBT_Helper
{
    public class ThoughtRecord
    {
        #region Instance Variables

        private DateTime _dateTimeValue;

        public DateTime DateTimeValue
        {
            get { return _dateTimeValue; }
            set
            {
                _dateTimeValue = value;
                Day = _dateTimeValue.ToString("MMMM d, yyyy");
                DayOfMonth = _dateTimeValue.Day.ToString();
                Time = _dateTimeValue.ToString("h:mm tt");
                MonthYear = _dateTimeValue.ToString("MM/yyyy");
                MonthYearFullMonth = _dateTimeValue.ToString("MMMM yyyy");
            }
        }

        // String version of the Date part of DateTimeValue
        public string Day { get; private set; }

        public string DayOfMonth { get; private set; }
        
        // String version of the Time part of DateTimeValue
        public string Time { get; private set; }

        // Used to group the list of events
        public string MonthYear { get; private set; }

        public string MonthYearFullMonth { get; private set; }

        public string Situation { get; set; }

        public string Feelings { get; set; }

        public string Thoughts { get; set; }

        public string UnderlyingThoughts { get; set; }

        public string ReplacementThoughts { get; set; }

        public Guid EventGuid { get; set; }

        #endregion
    }
}
