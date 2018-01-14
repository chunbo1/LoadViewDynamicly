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
    class SemesterStudentViewModel : ReportViewModelBase<SemesterStudentReport>

    {
        public LoadViewDynamicly.Report.SemesterStudentReport _view { get; set; }

        public SemesterStudentViewModel(SemesterStudentReport view) : base(view)
        {
            //Semester = "2017 Fall";
            _view = view;
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
            _view.ShowReport();

        }

        protected override bool CanGenerateReportExecute()
        {
            return true;
        }



    }//class
}
