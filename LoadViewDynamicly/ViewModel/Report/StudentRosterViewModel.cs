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
        private DateTime _asOfDate;
        public LoadViewDynamicly.Report.StudentRoster _view { get; set; }

        public StudentRosterViewModel(StudentRoster view) : base(view)
        {
            AsOfDate = DateTime.Today;
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

        public DateTime AsOfDate
        {
            get { return _asOfDate; }
            set { _asOfDate = value; RaisePropertyChanged(() => AsOfDate); }
        }

        protected override void OnGenerateReportExecute()
        {
            //var managerFilter = ShowInternalAccounts ? true : ShowManagedAccounts ? false : (bool?) null;
            _view.ShowReport(AsOfDate);

        }

        protected override bool CanGenerateReportExecute()
        {
            return true;
        }

















    }//Class

}//NS
