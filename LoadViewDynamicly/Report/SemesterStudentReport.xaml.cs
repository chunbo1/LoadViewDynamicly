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
    /// Interaction logic for SemesterStudentReport.xaml
    /// </summary>
    public partial class SemesterStudentReport : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public SemesterStudentReport()
        {
            InitializeComponent();
            DataContext = new SemesterStudentViewModel(this);
            (DataContext as SemesterStudentViewModel)._view = this as SemesterStudentReport;

        }

        public void ShowReport()
        {
            try
            {
                var param = new List<ReportParameter>();
                //param.Add(new ReportParameter("semester", semester));
                ReportViewerCtl.ShowServerReport(ConfigurationManager.AppSettings["ReportServerUrl"],
                                                            ConfigurationManager.AppSettings["SemesterStudentReport"],
                                                 param);
            }
            catch (Exception e)
            {
                log.Error("In         public SemesterStudentReport.xaml.cs..ShowReport: " + e.Message);
                Environment.Exit(-1);
            }
        }






    }//class
}
