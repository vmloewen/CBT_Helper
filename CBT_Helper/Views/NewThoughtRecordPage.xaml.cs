using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CBT_Helper.Controllers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CBT_Helper
{

    public delegate void CreateNewThoughtRecordEventHandler(object sender, ThoughtRecord thoughtRecord);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewThoughtRecordPage : Page
    {
        private ThoughtRecord _thoughtRecord;

        public CreateNewThoughtRecordEventHandler CreateNewThoughtRecord;

        public NewThoughtRecordPage()
        {
            this.InitializeComponent();
            CreateNewThoughtRecord += Controller.Instance.NewThoughtRecordCreated;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _thoughtRecord = (ThoughtRecord) e.Parameter;
            newThoughtRecordPageDataBinding.DataContext = _thoughtRecord;
            if (_thoughtRecord != null)
            {
                datePicker.Date = _thoughtRecord.DateTimeValue;
                timePicker.Time = _thoughtRecord.DateTimeValue.TimeOfDay;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateOfEvent = new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day, timePicker.Time.Hours, timePicker.Time.Minutes, timePicker.Time.Seconds);
            if (_thoughtRecord == null)
            {
                _thoughtRecord = CreateNewEventFromInputs(dateOfEvent, situationTextBox, feelingsTextBox,
                    thoughtsTextBox, underlyingThoughtsTextBox, replacementThoughtsTextBox);
            }
            else
            {
                UpdateThoughtRecord(dateOfEvent, situationTextBox, feelingsTextBox,
                    thoughtsTextBox, underlyingThoughtsTextBox, replacementThoughtsTextBox);
            }

            if (CreateNewThoughtRecord != null)
            {
                CreateNewThoughtRecord(this, _thoughtRecord);
            }
        }

        private ThoughtRecord CreateNewEventFromInputs(DateTime date, TextBox situation, TextBox feelings, TextBox thoughts, TextBox underlyingThoughts, TextBox replacementThoughts)
        {
            ThoughtRecord newThoughtRecord = new ThoughtRecord()
            {
                DateTimeValue = date,
                EventGuid = Guid.NewGuid(),
                Situation = situation.Text,
                Feelings = feelings.Text,
                Thoughts = thoughts.Text,
                UnderlyingThoughts = underlyingThoughts.Text,
                ReplacementThoughts = replacementThoughts.Text
            };

            return newThoughtRecord;
        }

        private void UpdateThoughtRecord(DateTime date, TextBox situation, TextBox feelings, TextBox thoughts,
            TextBox underlyingThoughts, TextBox replacementThoughts)
        {
            _thoughtRecord.DateTimeValue = date;
            _thoughtRecord.Situation = situation.Text;
            _thoughtRecord.Feelings = feelings.Text;
            _thoughtRecord.Thoughts = thoughts.Text;
            _thoughtRecord.UnderlyingThoughts = underlyingThoughts.Text;
            _thoughtRecord.ReplacementThoughts = replacementThoughts.Text;

            // DO NOT SET GUID >:(
        }
    }
}
