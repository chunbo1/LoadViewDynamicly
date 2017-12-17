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
using System.Reflection;
using Microsoft.Reporting.WinForms;
using Core.Extensions;
using log4net;

namespace Reports
{
    /// <summary>
    /// Interaction logic for ReportViewerControl.xaml
    /// </summary>
    /// http://www.wpf-tutorial.com/misc-controls/the-windowsformshost-control/
    /// WindowsFormsHost Control
    
    public partial class ReportViewerControl : UserControl
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ReportViewerControl()
        {
            InitializeComponent();
        }

        ~ReportViewerControl()
        {
            Dispose(false);
        }

        public void Reset()
        {
            var lReportViewerCtl = WinFormsHostCtl.Child as ReportViewer;
            if (lReportViewerCtl != null)
                lReportViewerCtl.Reset();
        }

        private Action ReportCompletedCallback { get; set; }

        public void ShowLocalReport(Assembly assembly, string reportEmbeddedResourceName, IEnumerable<ReportDataSource> dataSources,
                            IEnumerable<ReportParameter> parameters, DisplayMode displayMode, Action reportCompletedCallback = null)
        {
            var resource = assembly.GetManifestResourceStream(reportEmbeddedResourceName);
            ProcessReport(rptViewer =>
            {
                rptViewer.LocalReport.LoadReportDefinition(resource);
                dataSources.Iterate(source => rptViewer.LocalReport.DataSources.Add(source));
                rptViewer.LocalReport.SetParameters(parameters);
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.SetDisplayMode(displayMode);
                if (displayMode == DisplayMode.Normal)
                {
                    rptViewer.ZoomMode = ZoomMode.Percent;
                    rptViewer.ZoomPercent = 100;
                }
                else
                {
                    rptViewer.ZoomMode = ZoomMode.PageWidth;
                }
            }, reportCompletedCallback);
        }
        public void ShowLocalReport(string reportEmbeddedResourceName, IEnumerable<ReportDataSource> dataSources,
                            IEnumerable<ReportParameter> parameters, DisplayMode displayMode, Action reportCompletedCallback = null)
        {
            ShowLocalReport(Assembly.GetCallingAssembly(), reportEmbeddedResourceName, dataSources, parameters,
                            displayMode, reportCompletedCallback);
        }

        public void ShowServerReport(string serverUrl, string reportPath, IEnumerable<ReportParameter> parameters, Action reportCompletedCallback = null, DisplayMode displayMode = DisplayMode.Normal)
        {
            ProcessReport(rptViewer =>
            {
                rptViewer.ServerReport.ReportServerUrl = new Uri(serverUrl);
                rptViewer.ServerReport.ReportServerCredentials.NetworkCredentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                rptViewer.ServerReport.ReportPath = reportPath;
                rptViewer.ServerReport.SetParameters(parameters);
                rptViewer.ProcessingMode = ProcessingMode.Remote;
                rptViewer.ShowParameterPrompts = false;
                rptViewer.ShowPromptAreaButton = false;
                rptViewer.SetDisplayMode(displayMode);
                if (displayMode == DisplayMode.Normal)
                {
                    rptViewer.ZoomMode = ZoomMode.Percent;
                    rptViewer.ZoomPercent = 100;
                }
                else
                {
                    rptViewer.ZoomMode = ZoomMode.PageWidth;
                }
            }, reportCompletedCallback);
        }

        private void OnReportRenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            if (ReportCompletedCallback != null)
                ReportCompletedCallback();
        }

        private void ProcessReport(Action<ReportViewer> reportProcessingCallback, Action reportCompletedCallback)
        {
            try
            {
                var lReportViewerCtl = WinFormsHostCtl.Child as ReportViewer;
                if (lReportViewerCtl == null) return;
                ReportCompletedCallback = reportCompletedCallback;
                lReportViewerCtl.RenderingComplete -= OnReportRenderingComplete;
                lReportViewerCtl.Reset();
                reportProcessingCallback(lReportViewerCtl);
                lReportViewerCtl.ShowProgress = true;
                lReportViewerCtl.ShowStopButton = false;
                lReportViewerCtl.RefreshReport();
                lReportViewerCtl.RenderingComplete += OnReportRenderingComplete;
            }
            catch (Exception e)
            {
                log.Error("In ReportViewerControl.cs..ProcessReport: " + e.Message);
                Environment.Exit(-1);
            }

        }

        #region " IDisposable "

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing)
                return;

            var lReportViewerCtl = WinFormsHostCtl.Child as ReportViewer;
            if (lReportViewerCtl == null)
                return;
            lReportViewerCtl.RenderingComplete -= OnReportRenderingComplete;
            lReportViewerCtl.Dispose();
            WinFormsHostCtl.Dispose();
            _disposed = true;
        }
        #endregion


    }//Class




}
