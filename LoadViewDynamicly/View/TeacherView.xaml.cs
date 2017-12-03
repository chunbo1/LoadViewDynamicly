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
using Infragistics.Windows.DataPresenter.Events;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Infragistics.Excel;
using System.Diagnostics;
using log4net;
using System.Reflection;

namespace LoadViewDynamicly
{
    /// <summary>
    /// Interaction logic for TeacherView.xaml
    /// </summary>
    public partial class TeacherView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public TeacherView()
        {
            InitializeComponent();
            try
            {
                if (dc.DatabaseExists())
                    TeacherGrid.DataSource = dc.Teachers;
                TeacherGrid.RecordAdded += new EventHandler<RecordAddedEventArgs>(TeacherGrid_RecordAdded);
            }
            catch (Exception e)
            {
                log.Error("In TeacherView: " + e.StackTrace);
                Environment.Exit(-1);
            }
            finally
            {
            }
            
        }

        private void TeacherGrid_RecordAdded(object sender, RecordAddedEventArgs e)
        {
            Infragistics.Windows.DataPresenter.DataRecord newRow = e.Record;
            newRow.Cells["UpdateDateTime"].Value = DateTime.Now;
            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TeacherGrid.DataSource = null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dc.DatabaseExists())
            {
                /*
                var teacher_class = from a in dc.Teachers
                             join c in dc.Classes on a.ID equals c.TeacherId
                                    select new { a.FirstName, a.LastName, a.HomePhone, a.Address, a.CellPhone, a.Email, c.Location, c.ClassName};
                TeacherGrid.DataSource = teacher_class;// dc.Teachers;
                */
                dc.SubmitChanges();
                dc = null;                
            }
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            TeacherGrid.DataSource = dc.Teachers;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {            
            dc.SubmitChanges();
            Messenger messenger = App.Messenger;
            //After adding a new class, need to refresh TeacherTable
            messenger.NotifyColleagues("RefreshTeacherTable");
        }

        private void Export2Excel_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"c:\temp\3\a.xlsx";
            try
            {
                // Get the instance of the DataPresenterExcelExporter we defined in the Page's resources and call its Export
                // method passing in the XamDataGrid we want to export, the name of the file we would like
                // to export to and the format of the Workbook we want to Exporter to create.
                //
                // NOTE: there are 11 overloads to the Export method that give you control over different
                // aspects of the exporting process.
                DataPresenterExcelExporter exporter = (DataPresenterExcelExporter)this.Resources["Exporter"];
                ExportOptions op = new ExportOptions();
                op.ChildRecordCollectionSpacing = ChildRecordCollectionSpacing.None;
                op.ExcludeExpandedState = true;
                op.ExcludeSummaries = true;
                exporter.Export(TeacherGrid, fileName, WorkbookFormat.Excel2007, op);
            }
            catch (Exception ex)
            {
                // It's possible that the exporter does not have permission to write the file, so it's generally
                // a good idea to account for this possibility.
                MessageBox.Show(string.Format("Export_Message_ExcelExportError_Text", ex.Message),
                                "Export_Message_ExcelError_Caption",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return;
            }

            // Execute Excel to display the exported workbook.
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Export_Message_ExcelError_Text", ex.Message),
                                "Export_Message_ExcelError_Caption",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }

        }
    }
}
