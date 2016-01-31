using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CBT_Helper.Controllers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CBT_Helper

{

    public delegate void SelectThoughtRecordEventHandler(object sender, ItemClickEventArgs e);

    public delegate void LoadThoughtRecordDataEventHandler(object sender, NavigationEventArgs e);

    public delegate void OpenThoughtRecordCreationPageEventHandler(object sender, RoutedEventArgs e);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThoughtRecordListPage : Page
    {
        // TODO :(

        public SelectThoughtRecordEventHandler SelectThoughtRecord;

        public OpenThoughtRecordCreationPageEventHandler OpenThoughtRecordCreationPage;

        public ThoughtRecordListPage()
        {
            this.InitializeComponent();

            SelectThoughtRecord += Controller.Instance.ThoughtRecordSelected;
            OpenThoughtRecordCreationPage += Controller.Instance.ThoughtRecordCreationPageOpened;
        }

        public void ReloadItemsList()
        {
            var result = from item in Controller.Instance.GetThoughtRecords()
                         group item by new { Id = item.YearMonth, MonthYearFullMonth = item.MonthYearFullMonth }
                             into grp
                             orderby grp.Key.Id descending
                             select grp;

            cvsEvents.Source = result;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*if (LoadThoughtRecord != null)
            {
                LoadThoughtRecord(this, e);
            }*/
            ReloadItemsList();
        }

        private void HandleEventListItemClick(object sender, ItemClickEventArgs e)
        {
            if (SelectThoughtRecord != null)
            {
                SelectThoughtRecord(this, e);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OpenThoughtRecordCreationPage != null)
            {
                OpenThoughtRecordCreationPage(this, e);
            }
        }
    }
}
