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
using log4net;
using System.Reflection;


namespace LoadViewDynamicly.Report
{
    /// <summary>
    /// Interaction logic for StudentRoster.xaml
    /// </summary>
    public partial class StudentRoster : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            try { 
            var param = new List<ReportParameter>();
            param.Add(new ReportParameter("asof2", asOfDate.ToString()));
            ReportViewerCtl.ShowServerReport(ConfigurationManager.AppSettings["ReportServerUrl"],
                                                        ConfigurationManager.AppSettings["StudentRosterReport"],
                                             param);
            }
            catch (Exception e)
            {
                log.Error("In StudentRoster.xaml.cs..ShowReport: " + e.Message);
                Environment.Exit(-1);
            }
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
