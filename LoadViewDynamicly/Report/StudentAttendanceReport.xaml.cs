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
    /// Interaction logic for StudentAttendanceReport.xaml
    /// </summary>
    public partial class StudentAttendanceReport : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public StudentAttendanceReport()
        {
            InitializeComponent();
            DataContext = new StudentAttendanceViewModel(this);
            (DataContext as StudentAttendanceViewModel)._view = this as StudentAttendanceReport;

        }

        public void ShowReport(string semester)
        {
            try
            {
                var param = new List<ReportParameter>();
                param.Add(new ReportParameter("semester", semester));
                ReportViewerCtl.ShowServerReport(ConfigurationManager.AppSettings["ReportServerUrl"],
                                                            ConfigurationManager.AppSettings["StudentAttendanceReport"],
                                                 param);
            }
            catch (Exception e)
            {
                log.Error("In ClassStudentEnroll.xaml.cs..ShowReport: " + e.Message);
                Environment.Exit(-1);
            }
        }



    }//Class
}
