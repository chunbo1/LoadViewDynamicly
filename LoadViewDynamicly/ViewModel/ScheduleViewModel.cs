﻿using System;
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

namespace LoadViewDynamicly.ViewModel
{
    class ScheduleViewModel : ViewModelBase
    {
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

            if (Asof == null) Asof = String.Format("{0:MM/dd/yyyy}", DateTime.Today);
        } //ctor

        System.Data.Linq.Table<Class> classTable = null;
        public MyObservableCollection<MyClass> ClassTable
        {
            get
            {
                MyObservableCollection<MyClass> myClasses = new MyObservableCollection<MyClass>();
                var query = from s in classTable
                            where s.Enabled == true
                            select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek, s.Enabled);

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
                        select new MyClass(s.ID, s.Division, s.ClassName, s.Semester, s.Dayofweek, s.Timeofweek, s.Enabled);

            foreach (MyClass ss in query)
                myClasses.Add(ss);
            return myClasses;
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

        private String asof;
        public String Asof
        {
            get
            {
                return asof;
            }
            set
            {
                asof = value;
                RaisePropertyChanged("Asof");
            }
        }

        private RelayCommand loadStudentCommand;
        public ICommand LoadStudentCommand
        {
            get { return loadStudentCommand ?? (loadStudentCommand = new RelayCommand(() => LoadStudent(), 
                        () => (Asof.Trim().Length > 0 && ClassId > 0 && TeacherId > 0 )));
                }
        }

        StoreDB db = null;
        bool loadStudentClicked = false;
        CommunityViewModel CommunityVM = null;
        private void LoadStudent()
        {
            if (db == null) db = new StoreDB();
            //Instead of binding ScheduleGrid here, we bind it in xaml
            //<igDP:XamDataPresenter DataSource="{Binding Path=Members}" 
            CommunityVM = new CommunityViewModel(db.GetStudentsByClassA(ClassId)); //db.GetStudentsByClassA(4);
            _view.ScheduleGrid.DataContext = CommunityVM;
            //_view.DataContext = new CommunityViewModel(db.GetStudentsByClassA(4));
            loadStudentClicked = true;
        }

        private RelayCommand saveAttendanceCommand;
        public ICommand SaveAttendanceCommand
        {
            get
            {
                return saveAttendanceCommand ?? (saveAttendanceCommand = new RelayCommand(
                      () => SaveAttendance(),
                      () => (Asof.Trim().Length > 0 && ClassId > 0 && TeacherId > 0 && loadStudentClicked)
                      ));
            }
        }

        private void SaveAttendance()
        {
            List<string> checkedNames = new List<string>();
            List<string> uncheckedNames = new List<string>();

            if (db == null) db = new StoreDB();
            foreach (ScheduleStudentViewModel p in CommunityVM.Members)
            {
                if (p.IsChecked)
                    checkedNames.Add(p.FullName);
                else
                    uncheckedNames.Add(p.FullName);
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
