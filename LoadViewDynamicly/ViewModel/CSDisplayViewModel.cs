using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadViewDynamicly.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using System.ComponentModel;

namespace LoadViewDynamicly.ViewModel
{
    class CSDisplayViewModel : ViewModelBase
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        private bool isSelected = false;
        
        public CSDisplayViewModel()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("ProductSelectionChanged", (Action<ClassStudent>)(param => ProcessProduct(param)));
            messenger.Register("SetStatus", (Action<String>)(param => stat.Status = param));
            //After adding a new student, need to refresh StudentTable
            messenger.Register("RefreshStudentTable", (Action)(() => StudentTable = refreshStudentTable()));
            

            if (dc.DatabaseExists())
            {
                classTable = dc.Classes;
                studentTable = dc.Students;
            }
        } //ctor

        System.Data.Linq.Table<Class> classTable = null;
        public System.Data.Linq.Table<Class> ClassTable
        {
            get { return classTable; }
            set
            {
                classTable = value;
                RaisePropertyChanged("ClassTable");
            }
        }

        System.Data.Linq.Table<Student> studentTable = null;
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public MyObservableCollection<MyStudent> StudentTable
        {
            get {
                MyObservableCollection<MyStudent> myStudents = new MyObservableCollection<MyStudent>();
                var query = from s in studentTable
                            select new MyStudent(s.ID, s.ID + 
                            " " + s.FirstName + " " + s.LastName + " " + s.BirthDate,
                            s.FirstName, s.LastName );
                            
                foreach (MyStudent ss in query)
                    myStudents.Add(ss);
                return myStudents;
            }
            set
            {
                RaisePropertyChanged("StudentTable");
            }
        }

        public MyObservableCollection<MyStudent> refreshStudentTable()
        {
            MyObservableCollection<MyStudent> myStudents = new MyObservableCollection<MyStudent>();
            var query = from s in studentTable
                        select new MyStudent(s.ID, s.ID +
                        " " + s.FirstName + " " + s.LastName + " " + s.BirthDate,
                        s.FirstName, s.LastName);

            foreach (MyStudent ss in query)
                myStudents.Add(ss);
            return myStudents;            
        }

        //data checks and status indicators done in another class
        private readonly ProductDisplayModelStatus stat = new ProductDisplayModelStatus();
        public ProductDisplayModelStatus Stat { get { return stat; } }

        private ClassStudent displayedProduct = new ClassStudent();
        public ClassStudent DisplayedProduct
        {
            get { return displayedProduct; }
            set { displayedProduct = value;
                RaisePropertyChanged("DisplayedProduct"); }
        }


        private RelayCommand getProductsCommand;
        public ICommand GetProductsCommand
        {
            get { return getProductsCommand ?? (getProductsCommand = new RelayCommand(() => GetClassStudent())); }
        }

        private void GetClassStudent()
        {
            isSelected = false;
            stat.NoError();
            DisplayedProduct = new ClassStudent();
            App.Messenger.NotifyColleagues("GetClassStudents");
        }


        private RelayCommand clearCommand;
        public ICommand ClearCommand
        {
            get { return clearCommand ?? (clearCommand = new RelayCommand(() => ClearClassStudentDisplay()/*, ()=>isSelected*/)); }
        }

        private void ClearClassStudentDisplay()
        {
            isSelected = false;
            stat.NoError();
            DisplayedProduct = new ClassStudent();
            App.Messenger.NotifyColleagues("ProductCleared");
        } //ClearProductDisplay()


        private RelayCommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand ?? (updateCommand = new RelayCommand(() => UpdateClassStudent(), () => isSelected)); }
        }

        private void UpdateClassStudent()
        {
            //Sync DisplayedProduct's ID with Name
            DisplayedProduct.ClassName = ClassTable.SingleOrDefault(c => c.ID == (DisplayedProduct.ClassId ?? 0)).ClassName;
            DisplayedProduct.StudentName = StudentTable.SingleOrDefault(s => s.ID == (DisplayedProduct.StudentId ?? 0)).FullName;

            if (!stat.ChkProductForUpdate(DisplayedProduct)) return;
            if (!App.StoreDB.UpdateProduct(DisplayedProduct))
            {
                stat.Status = App.StoreDB.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("UpdateProduct", DisplayedProduct);
        } //UpdateProduct()

       
        private RelayCommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(() => DeleteClassStudent(), () => isSelected)); }
        }


        private void DeleteClassStudent()
        {
            if (!App.StoreDB.DeleteProduct(DisplayedProduct.ID))
            {
                stat.Status = App.StoreDB.errorMessage;
                return;
            }
            isSelected = false;
            App.Messenger.NotifyColleagues("DeleteProduct");

            //This is necessary to create a brand-new instance for DisplayedProduct
            ClearClassStudentDisplay();
        } //DeleteProduct


        private RelayCommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand ?? (addCommand = new RelayCommand(() => AddProduct(), () => !isSelected)); }
        }


        private void AddProduct()
        {
            //Sync DisplayedProduct's ID with Name
            DisplayedProduct.ClassName = ClassTable.SingleOrDefault(c => c.ID == (DisplayedProduct.ClassId ?? 0)).ClassName;
            DisplayedProduct.StudentName = StudentTable.SingleOrDefault(s => s.ID == (DisplayedProduct.StudentId ?? 0)).FullName;

            if (!stat.ChkClassStudentForAdd(DisplayedProduct)) return;
            if (!App.StoreDB.AddProduct(DisplayedProduct))
            {
                stat.Status = App.StoreDB.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("AddProduct", DisplayedProduct);
            //This is necessary to create a brand-new instance for DisplayedProduct
            ClearClassStudentDisplay();
        } //AddProduct()


        public void ProcessProduct(ClassStudent p)
        {
            if (p == null) {
                /*DisplayedProduct = null;*/
                isSelected = false;
                return;
            }
            ClassStudent temp = new ClassStudent();
            temp.CopyProduct(p);
            DisplayedProduct = temp;
            isSelected = true;
            stat.NoError();
        } // ProcessProduct()



    }//Class
}//NS
