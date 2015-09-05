using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CBT_Helper.Controllers
{
    class Controller
    {

        private IRepository _repository;

        private IList<ThoughtRecord> _items; 

        private Controller()
        {
            _repository = new Repository();
            _items = _repository.GetAll().OrderByDescending(d => d.DateTimeValue).ToList();
        }

        private static Controller _instance;

        public static Controller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        #region Create, Read, Update, Delete

        public void NewThoughtRecordCreated(object sender, ThoughtRecord thoughtRecord)
        {
            _repository.Create(thoughtRecord);
            _items = _items.Where(g => g.EventGuid != thoughtRecord.EventGuid).ToList();
            _items.Add(thoughtRecord);
            _items = _items.OrderByDescending(d => d.DateTimeValue).ToList();

            // TODO: if write fails, remove from list
            Page page = sender as Page;
            page.Frame.Navigate(typeof(ThoughtRecordListPage));
        }

        public IList<ThoughtRecord> GetThoughtRecords()
        {
            return _items;
        }

        public void ThoughtRecordEdited(object sender, ThoughtRecord thoughtRecord, RoutedEventArgs e)
        {
            Page page = sender as Page;
            page.Frame.Navigate(typeof (NewThoughtRecordPage), thoughtRecord);
        }

        public void ThoughtRecordDeleted(object sender, ThoughtRecord thoughtRecord, RoutedEventArgs e)
        {
            Page page = sender as Page;
            _repository.DeleteById(thoughtRecord.EventGuid);
            _items.Remove(thoughtRecord);
            page.Frame.Navigate(typeof(ThoughtRecordListPage));
        }

        #endregion

        #region Page Navigation

        public void ThoughtRecordSelected(object sender, ItemClickEventArgs e)
        {
            Page page = sender as Page;
            page.Frame.Navigate(typeof(ThoughtRecordDetailsPage), e.ClickedItem);
        }

        public void ThoughtRecordCreationPageOpened(object sender, RoutedEventArgs e)
        {
            Page page = sender as Page;
            page.Frame.Navigate(typeof(NewThoughtRecordPage));
        }

        #endregion

    }
}
