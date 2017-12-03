using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.ComponentModel;
using LoadViewDynamicly.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Data;
using log4net;
using System.Reflection;

namespace LoadViewDynamicly.ViewModel
{
    
    public class CSViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        private static CSViewModel _instance = new CSViewModel();
        public static CSViewModel Instance { get { return _instance; } }

        private ICollectionView _customerView;
        public LoadViewDynamicly.View.CSSelectionView _view { get; set; }

        public CSViewModel()
        {
            dataItems = new MyObservableCollection<ClassStudent>();
            GetClassStudents();

            //Use side by side with IsSynchronizedWithCurrentItem="True"
            //We can use DataGrid instead of ListBox [SelectedItem="{Binding SelectedProduct}">]
            ///_customerView.CurrentChanged += CustomerSelectionChanged;

            listBoxSelectionChangeCommand = new RelayCommand(() => SelectionHasChanged() );
            App.Messenger.Register("ProductCleared", (Action)(() => SelectedProduct = null));
            App.Messenger.Register("GetClassStudents", (Action)(() => GetClassStudents()));
            App.Messenger.Register("UpdateProduct", (Action<ClassStudent>)(param => UpdateProduct(param)));
            App.Messenger.Register("DeleteProduct", (Action)(() => DeleteProduct()));
            App.Messenger.Register("AddProduct", (Action<ClassStudent>)(param => AddProduct(param)));
        }

        //Tempreally disabled; used for DataGrid
        private void CustomerSelectionChangedXXX(object sender, EventArgs e)
        {
            // React to the changed selection
            SelectedProduct = (ClassStudent)DataItems1XXX.CurrentItem;
            SelectionHasChanged();
            
        }

        private void GetClassStudents()
        {
            StoreDB db = new StoreDB();
            try
            {
                DataItems = db.GetClassStudents();
            }
            catch (Exception e)
            {
                log.Error("In CSViewModel..GetClassStudents: " + e.StackTrace);
                Environment.Exit(-1);
            }
            
            //https://wpftutorial.net/DataViews.html
            //https://stackoverflow.com/questions/8597824/listbox-groupstyle-display-how-to-design-a-group-name
            //https://stackoverflow.com/questions/20188132/how-to-correctly-bind-update-a-datagrid-with-a-collectionviewsource
            //The sorting technique explained above is really simple, but also quite slow for a large amount of data, 
            //because it internally uses reflection. But there is an alternative, more performant way to do sorting by providing a custom sorter.
            //when DataItems points to a new object, we need to GetDefaultView(DataItems) and add Grouping
            //CSSelectionView ItemsSource="{Binding DataItems}"; We dont need to bind to DataItems1 
            _customerView = CollectionViewSource.GetDefaultView(DataItems);
            if (_customerView.GroupDescriptions == null || _customerView.GroupDescriptions.Count == 0)
            {
                _customerView.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
                _customerView.SortDescriptions.Add(new SortDescription("Grouping", ListSortDirection.Ascending));
                _customerView.SortDescriptions.Add(new SortDescription("StudentName", ListSortDirection.Ascending));
            }

            if (db.hasError)
                App.Messenger.NotifyColleagues("SetStatus", db.errorMessage);
        }

        private void UpdateProduct(ClassStudent p)
        {
            if (selectedProduct != null)
            {
                int index = dataItems.IndexOf(selectedProduct);
                if (index > -1)
                {
                    dataItems.ReplaceItem(index, p);
                    SelectedProduct = p;
                }
                else
                    Console.WriteLine("Index out of boundry " + selectedProduct.Grouping);

            }
        }

        private void DeleteProduct()
        {
            DataItems.Remove(selectedProduct);
            //RefreshCustomerView();
        }

        private void AddProduct(ClassStudent p)
        {
            //No need for deep copy
            DataItems.Add(p);
            //RefreshCustomerView();
        }

        private void RefreshCustomerViewXXX()
        {
            //https://stackoverflow.com/questions/20188132/how-to-correctly-bind-update-a-datagrid-with-a-collectionviewsource            
            _customerView = CollectionViewSource.GetDefaultView(DataItems);
            if (_customerView.GroupDescriptions == null || _customerView.GroupDescriptions.Count == 0)
            {
                _customerView.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
                _customerView.SortDescriptions.Add(new SortDescription("Grouping", ListSortDirection.Ascending));
                _customerView.SortDescriptions.Add(new SortDescription("StudentName", ListSortDirection.Ascending));
            }
            _view.lstBoxCS.ItemsSource = _customerView;
        }

        private void SelectionHasChanged()
        {
            App.Messenger.NotifyColleagues("ProductSelectionChanged", selectedProduct);

        }

        private ClassStudent selectedProduct;
        public ClassStudent SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        private MyObservableCollection<ClassStudent> dataItems;
        public MyObservableCollection<ClassStudent> DataItems
        {
            get {
                return dataItems; 
                }
            //If dataItems replaced by new collection, WPF must be told
            set
            {
                dataItems = value;
                RaisePropertyChanged("DataItems");//without this, adding new items doesnt reflect in ListBox
            }
        }

        //DataItems1 hooks up to _customerView: Navigation, Filtering, Grouping, Sorting
        //https://wpftutorial.net/DataViews.html
        //CSSelectionView ItemsSource="{Binding DataItems}"; We don't need to bind to DataItems1
        public ICollectionView DataItems1XXX
        {
            get { return _customerView; }
            set
            {
                _customerView = value;
                RaisePropertyChanged("DataItems1");
            }
        }

        private RelayCommand listBoxSelectionChangeCommand;
        public ICommand ListBoxSelectionChangeCommand
        {
            get { return listBoxSelectionChangeCommand; }
        }


    }//Class



    public class MyObservableCollection<Product> : ObservableCollection<Product>
    {
        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Reset));
        }


        public void ReplaceItem(int index, Product item)
        {
            base.SetItem(index, item);
        }

    } // class MyObservableCollection


}//NS
