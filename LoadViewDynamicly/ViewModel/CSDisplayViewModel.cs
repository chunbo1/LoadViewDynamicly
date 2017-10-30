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
            messenger.Register("RefreshClassTable", (Action)(() => ClassTable = refreshClassTable()));

            if (dc.DatabaseExists())
            {
                classTable = dc.Classes;
                studentTable = dc.Students;
            }
        } //ctor

        System.Data.Linq.Table<Class> classTable = null;
        public MyObservableCollection<MyClass> ClassTable
        {
            get
            {
                MyObservableCollection<MyClass> myClasses = new MyObservableCollection<MyClass>();
                var query = from s in classTable
                            select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek);

                foreach (MyClass ss in query)
                    myClasses.Add(ss);
                return myClasses;
            }
            set
            {
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
                            orderby s.FirstName
                            select new MyStudent(s.ID, "" + 
                            s.FirstName + " " + s.LastName + " " + ((s.CellPhone.Length>0) ? s.CellPhone: s.HomePhone),
                            s.FirstName, s.LastName )
                            ;
                            
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
                        select new MyStudent(s.ID, 
                        "" + s.FirstName + " " + s.LastName + " " + ((s.CellPhone.Length > 0) ? s.CellPhone : s.HomePhone),
                        s.FirstName, s.LastName);

            foreach (MyStudent ss in query)
                myStudents.Add(ss);
            return myStudents;            
        }

        public MyObservableCollection<MyClass> refreshClassTable()
        {
            MyObservableCollection<MyClass> myClasses = new MyObservableCollection<MyClass>();
            var query = from s in classTable
                        select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek);

            foreach (MyClass ss in query)
                myClasses.Add(ss);
            return myClasses;
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
            MyClass cc = (MyClass)ClassTable.SingleOrDefault(c => c.ID == (DisplayedProduct.ClassId ?? 0));
            DisplayedProduct.ClassName = cc.ClassName;
            DisplayedProduct.StudentName = StudentTable.SingleOrDefault(s => s.ID == (DisplayedProduct.StudentId ?? 0)).FullName;
            //GetClassStudents() has Grouping, DisplayedProduct does the same
            DisplayedProduct.Grouping = cc.ClassName + " " + cc.Semester + " " + cc.Dayofweek + " " + cc.Timeofweek;

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

            if (DisplayedProduct.ClassId == null) { stat.Status = "Please pick up a Class"; return; }
            if (DisplayedProduct.StudentId == null) { stat.Status = "Please pick up a Student"; return; }
            //Sync DisplayedProduct's ID with Name
            MyClass cc = (MyClass) ClassTable.SingleOrDefault(c => c.ID == (DisplayedProduct.ClassId ?? 0));
            DisplayedProduct.ClassName = cc.ClassName;
            DisplayedProduct.StudentName = StudentTable.SingleOrDefault(s => s.ID == (DisplayedProduct.StudentId ?? 0)).FullName;
            //GetClassStudents() has Grouping, DisplayedProduct does the same
            DisplayedProduct.Grouping = cc.ClassName + " " + cc.Semester + " " + cc.Dayofweek + " " + cc.Timeofweek;

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
