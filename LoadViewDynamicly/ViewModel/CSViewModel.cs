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

namespace LoadViewDynamicly.ViewModel
{
    
    public class CSViewModel : ViewModelBase
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        private ICollectionView _customerView;
         public LoadViewDynamicly.View.CSSelectionView _view { get; set; }

        public CSViewModel()
        {
            dataItems = new MyObservableCollection<ClassStudent>();
            StoreDB db = new StoreDB();
            DataItems = db.GetClassStudents();

            //https://wpftutorial.net/DataViews.html
            //https://stackoverflow.com/questions/8597824/listbox-groupstyle-display-how-to-design-a-group-name
            //The sorting technique explained above is really simple, but also quite slow for a large amount of data, 
            //because it internally uses reflection. But there is an alternative, more performant way to do sorting by providing a custom sorter.
            _customerView = CollectionViewSource.GetDefaultView(DataItems);
            _customerView.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
            _customerView.SortDescriptions.Add(new SortDescription("Grouping", ListSortDirection.Ascending));
            _customerView.SortDescriptions.Add(new SortDescription("StudentName", ListSortDirection.Ascending));

            //Use side by side with IsSynchronizedWithCurrentItem="True"
            //We can use DataGrid instead of ListBox [SelectedItem="{Binding SelectedProduct}">]
            ///_customerView.CurrentChanged += CustomerSelectionChanged;

            listBoxSelectionChangeCommand = new RelayCommand(() => SelectionHasChanged());
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
            SelectedProduct = (ClassStudent)DataItems1.CurrentItem;
            SelectionHasChanged();
            
        }

        private void GetClassStudents()
        {
            StoreDB db = new StoreDB();
            DataItems = db.GetClassStudents();
            if (db.hasError)
                App.Messenger.NotifyColleagues("SetStatus", db.errorMessage);
        }

        private void UpdateProduct(ClassStudent p)
        {
            if (selectedProduct != null)
            {
                int index = dataItems.IndexOf(selectedProduct);
                dataItems.ReplaceItem(index, p);
                SelectedProduct = p;
            }
        }

        private void DeleteProduct()
        {
            DataItems.Remove(selectedProduct);
            RefreshCustomerView();
        }

        private void AddProduct(ClassStudent p)
        {
            DataItems.Add(p);
            RefreshCustomerView();
        }

        private void RefreshCustomerView()
        {
            //https://stackoverflow.com/questions/20188132/how-to-correctly-bind-update-a-datagrid-with-a-collectionviewsource
            _customerView = CollectionViewSource.GetDefaultView(DataItems);
            if (_customerView.GroupDescriptions == null || _customerView.GroupDescriptions.Count == 0)
            {
                _customerView.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
                _customerView.SortDescriptions.Add(new SortDescription("Grouping", ListSortDirection.Ascending));
                _customerView.SortDescriptions.Add(new SortDescription("StudentName", ListSortDirection.Ascending));
            }
            _view.lstBoxCS.ItemsSource = null;
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
                RaisePropertyChanged("DataItems");
            }
        }

        public ICollectionView DataItems1
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
