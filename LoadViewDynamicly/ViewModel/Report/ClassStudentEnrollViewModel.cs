using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadViewDynamicly.Report;
using Reports;

namespace LoadViewDynamicly.ViewModel.Report
{
    class ClassStudentEnrollViewModel : ReportViewModelBase<ClassStudentEnroll>
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        private string _semester;
        public LoadViewDynamicly.Report.ClassStudentEnroll _view { get; set; }

        public ClassStudentEnrollViewModel(ClassStudentEnroll view) : base(view)
        {
            Semester = "Fall 2017";
            _view = view;
        }

        public string Semester
        {
            get { return _semester; }
            set { _semester = value; RaisePropertyChanged(() => Semester); }
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
        public List<String> SemesterTable
        {
            get
            {
                List<String> mySemester = new List<String>();
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
