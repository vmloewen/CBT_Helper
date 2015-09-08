using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CBT_Helper.Controllers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace CBT_Helper
{

    public delegate void EditThoughtRecordEventHandler(object sender, ThoughtRecord thoughtRecord, RoutedEventArgs e);

    public delegate void DeleteThoughtRecordEventHandler(object sender, ThoughtRecord thoughtRecord, RoutedEventArgs e);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThoughtRecordDetailsPage : Page
    {
        public EditThoughtRecordEventHandler EditThoughtRecord;

        public DeleteThoughtRecordEventHandler DeleteThoughtRecord;

        public ThoughtRecordDetailsPage()
        {
            this.InitializeComponent();

            EditThoughtRecord += Controller.Instance.ThoughtRecordEdited;
            DeleteThoughtRecord += Controller.Instance.ThoughtRecordDeleted;

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            detailsPageDataBinding.DataContext = null; // if this line isn't here, then the data won't be updated on this page when edited
            detailsPageDataBinding.DataContext = (ThoughtRecord)e.Parameter;
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ThoughtRecord thoughtRecord = (ThoughtRecord)detailsPageDataBinding.DataContext;
            if (EditThoughtRecord != null)
            {
                EditThoughtRecord(this, thoughtRecord, e);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ThoughtRecord thoughtRecord = (ThoughtRecord) detailsPageDataBinding.DataContext;
            if (DeleteThoughtRecord != null)
            {
                DeleteThoughtRecord(this, thoughtRecord, e);
            }
        }
    }
}
