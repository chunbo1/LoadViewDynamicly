using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadViewDynamicly.Report;
using Reports;
using System.Collections.ObjectModel;

namespace LoadViewDynamicly.ViewModel.Report
{
    class StudentAttendanceViewModel : ReportViewModelBase<StudentAttendanceReport>
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        private string _semester;
        public LoadViewDynamicly.Report.StudentAttendanceReport _view { get; set; }

        public StudentAttendanceViewModel(StudentAttendanceReport view) : base(view)
        {
            //Semester = "2017 Fall";
            _view = view;
        }

        public string Semester
        {
            get { return _semester; }
            set
            {
                _semester = value;
                RaisePropertyChanged(() => Semester);
            }
        }

        protected override void OnLoadExecute()
        {
            try
            {
                GenerateReportCommand.Execute();
            }
            catch (Exception)
            {
            }

        }

        protected override void OnGenerateReportExecute()
        {
            //var managerFilter = ShowInternalAccounts ? true : ShowManagedAccounts ? false : (bool?) null;
            _view.ShowReport(Semester);

        }

        protected override bool CanGenerateReportExecute()
        {
            return true;
        }

        System.Data.Linq.Table<Class> semesterTable = null;
        public ObservableCollection<String> SemesterTable
        {
            get
            {
                ObservableCollection<String> mySemester = new ObservableCollection<String>();
                var query = (from s in dc.vwSemesters
                             orderby s.Semester descending
                             select s.Semester
                            ).Take(20)
                            ;
                foreach (String ss in query)
                    mySemester.Add(ss);
                return mySemester;
            }
            set
            {
                RaisePropertyChanged("SemesterTable");
            }
        }



    }//Class
}
