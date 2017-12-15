using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LoadViewDynamicly.Report;
using Reports;

namespace LoadViewDynamicly.ViewModel.Report
{
    class StudentRosterViewModel : ReportViewModelBase<StudentRoster>
    {
        public LoadViewDynamicly.Report.StudentRoster _view { get; set; }

        public StudentRosterViewModel(StudentRoster view) : base(view)
        {
            _view = view;
        }

        
        protected override void OnLoadExecute()
        {
            DateTime? asOfDate = null;

            try
            {
                asOfDate = DateTime.Today;
                GenerateReportCommand.Execute();
            }
            catch (Exception)
            {
            }

          
        }

        protected override void OnGenerateReportExecute()
        {
            //var managerFilter = ShowInternalAccounts ? true : ShowManagedAccounts ? false : (bool?) null;
            _view.ShowReport(DateTime.Today);

        }

        protected override bool CanGenerateReportExecute()
        {
            return true;
        }

















    }//Class

}//NS
