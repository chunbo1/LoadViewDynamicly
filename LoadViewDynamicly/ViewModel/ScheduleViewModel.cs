using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using System.ComponentModel;
using LoadViewDynamicly.Model;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace LoadViewDynamicly.ViewModel
{
    class ScheduleViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public LoadViewDynamicly.View.ScheduleView _view { get; set; }

        public ScheduleViewModel()
        {
            Messenger messenger = App.Messenger;
            //After adding a new class, need to refresh ClassTable
            messenger.Register("RefreshClassTable", (Action)(() => ClassTable = refreshClassTable()));
            messenger.Register("RefreshTeacherTable", (Action)(() => TeacherTable = refreshTeacherTable()));

            if (dc.DatabaseExists())
            {
                classTable = dc.Classes;
                teacherTable = dc.Teachers;
            }

            ClassDate = DateTime.Today;
            HeaderComment = "Please enter comment for this class: ";
        } //ctor

        System.Data.Linq.Table<Class> classTable = null;
        public MyObservableCollection<MyClass> ClassTable
        {
            get
            {
                MyObservableCollection<MyClass> myClasses = new MyObservableCollection<MyClass>();
                var query = from s in classTable
                            where s.Enabled == true
                            select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek, s.Enabled, s.Tuition);

                foreach (MyClass ss in query)
                    myClasses.Add(ss);
                return myClasses;
            }
            set
            {
                RaisePropertyChanged("ClassTable");
            }
        }

        System.Data.Linq.Table<Teacher> teacherTable = null;
        public MyObservableCollection<MyTeacher> TeacherTable
        {
            get
            {
                MyObservableCollection<MyTeacher> myTeachers= new MyObservableCollection<MyTeacher>();
                var query = from s in teacherTable
                            select new MyTeacher(s.ID, s.CellPhone, s.FirstName, s.LastName, s.Enabled);

                foreach (MyTeacher ss in query)
                    myTeachers.Add(ss);
                return myTeachers;
            }
            set
            {
                RaisePropertyChanged("TeacherTable");
            }
        }

        public MyObservableCollection<MyClass> refreshClassTable()
        {
            MyObservableCollection<MyClass> myClasses = new MyObservableCollection<MyClass>();
            var query = from s in classTable
                        where s.Enabled == true
                        select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek, s.Enabled, s.Tuition);

            foreach (MyClass ss in query)
                myClasses.Add(ss);
            return myClasses;
        }

        public string GetClassName(int classId)
        {
            MyClass mc= ClassTable.Where(n => n.ID == classId).First();
            return mc.FullName;
        }

        public MyObservableCollection<MyTeacher> refreshTeacherTable()
        {
            MyObservableCollection<MyTeacher> myTeachers = new MyObservableCollection<MyTeacher>();
            var query = from s in teacherTable
                        select new MyTeacher(s.ID, s.CellPhone, s.FirstName, s.LastName, s.Enabled);

            foreach (MyTeacher ss in query)
                myTeachers.Add(ss);
            return myTeachers;
        }

        private DateTime classDate;
        public DateTime ClassDate
        {
            get { return classDate; }
            set
            {
                classDate = value;
                RaisePropertyChanged("ClassDate");
            }
        }

        private String startTime;
        public String StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                RaisePropertyChanged("StartTime");
            }
        }

        private String endTime;
        public String EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                RaisePropertyChanged("EndTime");
            }
        }

        private String headerComment;
        public String HeaderComment
        {
            get
            {
                return headerComment;
            }
            set
            {
                headerComment = value;
                RaisePropertyChanged("HeaderComment");
            }
        }

        private RelayCommand loadStudentCommand;
        public ICommand LoadStudentCommand
        {
            get { return loadStudentCommand ?? (loadStudentCommand = new RelayCommand(() => LoadStudent(), 
                        () => (ClassDate != null && ClassId > 0 && TeacherId > 0 )));
                }
        }

        StoreDB db = null;
        bool loadStudentClicked = false;
        CommunityViewModel CommunityVM = null;
        private void LoadStudent()
        {
            try
            {
                if (db == null) db = new StoreDB();
                //Instead of binding StudentAttendanceGrid here, we bind it in xaml
                //<igDP:XamDataPresenter DataSource="{Binding Path=Members}" 
                CommunityVM = new CommunityViewModel(db.GetStudentsByClassA(ClassId)); //db.GetStudentsByClassA(4);
                _view.StudentAttendanceGrid.DataContext = CommunityVM;
                loadStudentClicked = true;
                MainWindowViewModel.Instance.StatusBar = $"Load Students for class {ClassId}";
            }
            catch (Exception e)
            {
                log.Error("In ScheduleViewModel.cs..LoadStudent: " + e.Message);
                Environment.Exit(-1);
            }
        }

        
        private RelayCommand loadCLassAttendanceCommand;
        public ICommand LoadCLassAttendanceCommand
        {
            get
            {
                return loadCLassAttendanceCommand ?? (loadCLassAttendanceCommand = new RelayCommand(() => loadClassAttendance(),
                      () => true));
            }
        }

        //need to pass in a classname parameter ???????
        private void loadClassAttendance()
        {
            try
            {
                RefreshClassAttendanceGrid(ClassFullName);
                ResetStudentAttendanceGrid();
                classAttendanceRecordClicked = false;//so can't delete Class header before selecting a record
                MainWindowViewModel.Instance.StatusBar = $"Load attendance history for class {ClassFullName}";
            }           
            catch (Exception e)
            {
                log.Error("In ScheduleViewModel.cs..loadClassAttendance: " + e.Message);
                Environment.Exit(-1);
            }
}

        private void ResetStudentAttendanceGrid()
        {
            //dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            _view.StudentAttendance1Grid.DataSource = null;

        }

        private RelayCommand loadStudentAttendanceCommand;
        public ICommand LoadStudentAttendanceCommand
        {
            get
            {
                return loadStudentAttendanceCommand ?? (loadStudentAttendanceCommand = new RelayCommand(() => LoadStudentAttendance(),
                      () => (ClassDate != null && ClassId > 0 && TeacherId > 0)));
            }
        }

        //Trigger is not used because it's invoked before ActiveDataItem bind to ActiveHeader 
        //ActiveDataItem="{Binding ActiveHeader, Mode=TwoWay}"
        private void LoadStudentAttendance()
        {
            //dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            //_view.StudentAttendance1Grid.DataSource = dc.spGetClassAttendanceDetail(ActiveHeader.AttendanceHeader);
          
        }

        private RelayCommand loadStudentDetailCommand;
        public ICommand LoadStudentDetailCommandXXX
        {
            get
            {
                return loadStudentDetailCommand ?? (loadStudentDetailCommand = new RelayCommand(() => LoadStudentDetailXXX(),
                      () => (true)));
            }
        }

        private void LoadStudentDetailXXX()//loadClassAttandenceDetail
        {

            //Occurs befoire ActiveDataItem="{Binding ActiveAttendanceDetail}
            //so at this point, ActiveAttendanceDetail = null
            //int a = ActiveAttendanceDetail.ID;
        }

        private RelayCommand saveAttendanceCommand;
        public ICommand SaveAttendanceCommand
        {
            get
            {
                return saveAttendanceCommand ?? (saveAttendanceCommand = new RelayCommand(
                      () => SaveAttendance(),
                      () => (ClassDate != null && ClassId > 0 && TeacherId > 0 && loadStudentClicked)
                      ));
            }
        }

        private void SaveAttendance()
        {
            try
            {
                if (db == null) db = new StoreDB();
                int? headerId = App.StoreDB.AddSchedulesHeader(ClassId, TeacherId, ClassDate, StartTime, EndTime, null, HeaderComment);
                bool flag = false;
                foreach (ScheduleStudentViewModel p in CommunityVM.Members)
                {
                    if (p.IsChecked) flag = true; else flag = false;
                    App.StoreDB.AddSchedulesDetail((int)headerId, p.ID, flag, p.Comment);
                }

                //Only when classAttendanceHeaderCombo has same class, then refresh classAttendanceHeader
                //Combo2 has classFullname, combo1
                _view.classAttendanceHeaderCombo2.SelectedValue = GetClassName(ClassId);
                //if (_view.classAttendanceHeaderCombo2.SelectedValue.ToString().Equals(GetClassName(ClassId)))
                RefreshClassAttendanceGrid(ClassFullName);
            }
            catch (Exception e)
            {
                log.Error("In ScheduleViewModel.cs..SaveAttendance: " + e.Message);
                Environment.Exit(-1);
            }


        }

        private void RefreshClassAttendanceGrid(string className)
        {
            try
            {
                if (dc.DatabaseExists())
                {
                    dc.SubmitChanges();
                    dc = null;
                }
                dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
                _view.ClassAttendanceGrid.DataSource = dc.spClassAttendanceHeader(className);
                classAttendanceRecordClicked = false;//so can't delete Class header before selecting a record
            }
            catch (Exception e)
            {
                log.Error("In ScheduleViewModel..RefreshClassAttendanceGrid: " + e.Message);
                Environment.Exit(-1);
            }

        }

        private RelayCommand deleteAttendanceHeaderCommand;
        public ICommand DeleteAttendanceHeaderCommand
        {
            get
            {
                return deleteAttendanceHeaderCommand ?? (deleteAttendanceHeaderCommand = new RelayCommand(
                      () => DeleteAttendanceHeader(),
                      () => (classAttendanceRecordClicked)
                      ));
            }
        }

        private void DeleteAttendanceHeader()
        {
            try
            {
                dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
                dc.spDeleteSchedulesHeader(ActiveHeader.AttendanceHeader);
                int headerId = ActiveHeader.AttendanceHeader;

                if (ClassFullName.Trim().Length > 0)
                {
                    RefreshClassAttendanceGrid(ClassFullName);
                    ResetStudentAttendanceGrid();
                    MainWindowViewModel.Instance.StatusBar = $"Deleted attendance header {headerId} for {ClassFullName}";
                }
            }
            catch (Exception e)
            {
                log.Error("In ScheduleViewModel..DeleteAttendanceHeader: " + e.Message);
                Environment.Exit(-1);
            }

        }

        bool classAttendanceRecordClicked = false;
        private spClassAttendanceHeaderResult activeHeader;
        public spClassAttendanceHeaderResult ActiveHeader
        {
            get { return activeHeader; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        activeHeader = value;
                        RaisePropertyChanged("ActiveHeader");
                        dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
                        _view.StudentAttendance1Grid.DataSource = dc.spGetClassAttendanceDetail(activeHeader.AttendanceHeader);
                        classAttendanceRecordClicked = true;
                        MainWindowViewModel.Instance.StatusBar = $"You just selected class attendance header {activeHeader.AttendanceHeader}";
                    }
                    else
                    {
                        StackTrace stackTrace = new StackTrace();
                        string LastAction = stackTrace.GetFrame(1).GetMethod().Name;

                        log.Info("In ScheduleViewModel..ActiveHeader SET: value = null and LastAction = " + LastAction);
                    }
                }
                catch (Exception e)
                {
                    log.Error("In ScheduleViewModel..ActiveHeader SET: " + e.Message + e.StackTrace);
                    Environment.Exit(-1);
                }

            }
        }

        bool classAttendanceDetailClicked = false;
        private spGetClassAttendanceDetailResult activeAttendanceDetail;
        public spGetClassAttendanceDetailResult ActiveAttendanceDetail
        {
            get { return activeAttendanceDetail; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        activeAttendanceDetail = value;                        
                        RaisePropertyChanged("ActiveAttendanceDetail");
                        classAttendanceDetailClicked = true;
                        App.Messenger.NotifyColleagues("StudentSelectionChanged", activeAttendanceDetail.ID);
                        //MainWindowViewModel.Instance.StatusBar = $"You just selected student {ActiveAttendanceDetail.Student}";
                    }
                    else
                    {
                        StackTrace stackTrace = new StackTrace();
                        string LastAction = stackTrace.GetFrame(1).GetMethod().Name;

                        log.Info("In ScheduleViewModel..ActiveAttendanceDetail SET: value = null and LastAction = " + LastAction);
                    }
                }
                catch (Exception e)
                {
                    log.Error("In ScheduleViewModel..ActiveAttendanceDetail SET: " + e.Message);
                    Environment.Exit(-1);
                }

            }
        }

        private int classId = 0;
        public int ClassId
        {
            get { return classId; }
            set
            {
                classId = value;
                RaisePropertyChanged("ClassId");

            }

        }

        private string classFullName;
        public string ClassFullName
        {
            get { return classFullName; }
            set
            {
                classFullName = value;
                RaisePropertyChanged("ClassFullName");

            }
        }

        private int teacherId = 0;
        public int TeacherId
        {
            get { return teacherId; }
            set
            {
                teacherId = value;
                RaisePropertyChanged("TeacherId");

            }

        }


    }//Class ScheduleViewModel





}//NS
