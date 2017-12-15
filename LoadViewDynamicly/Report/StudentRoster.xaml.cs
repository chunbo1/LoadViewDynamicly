using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoadViewDynamicly.ViewModel.Report;
using Microsoft.Reporting.WinForms;
using System.Configuration;

namespace LoadViewDynamicly.Report
{
    /// <summary>
    /// Interaction logic for StudentRoster.xaml
    /// </summary>
    public partial class StudentRoster : UserControl
    {
        public StudentRoster()
        {
            InitializeComponent();

            DataContext = new StudentRosterViewModel(this);
            (DataContext as StudentRosterViewModel)._view = this as StudentRoster;
        }

        public string Title
        {
            get { return "Student Roster Report"; }
        }

        public void ShowReport(DateTime asOfDate)
        {
            var param = new List<ReportParameter>();
            param.Add(new ReportParameter("asof2", asOfDate.ToString()));
            ReportViewerCtl.ShowServerReport(ConfigurationManager.AppSettings["ReportServerUrl"],
                                                        @"/Market Data Validations/WHStalePrice",
                                             param);
        }
        // Client Side Report

        //ReportViewerCtl.ShowLocalReport("SFM.Treasury.UI.Reports.EquityMargin-ByBrokerByAccount.rdlc",
        //                                new List<ReportDataSource>
        //                                    {
        //                                        new ReportDataSource("EquityMarginDataset", equityMarginDetails),
        //                                    },
        //                                new List<ReportParameter>
        //                                    {
        //                                       new ReportParameter("report_date", asOfDate.ToString()),
        //                                       new ReportParameter("totalEquity", totalEquity.ToString()),
        //                                       new ReportParameter("totalLMV", totalLMV.ToString()),
        //                                       new ReportParameter("totalSMV", totalSMV.ToString()),
        //                                       new ReportParameter("totalCashBal", totalCashBal.ToString()),
        //                                       new ReportParameter("totalLongReq", totalLongReq.ToString()),
        //                                       new ReportParameter("totalShortReq", totalShortReq.ToString()),
        //                                       new ReportParameter("totalReq", totalReq.ToString()),
        //                                       new ReportParameter("totalExcessDeficit", totalExcessDeficit.ToString())
        //                                    }, DisplayMode.Normal);






    }//Cls
}//NS
