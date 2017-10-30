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
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Infragistics.Excel;
using System.Diagnostics;
namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for CST_View.xaml
    /// </summary>
    public partial class CST_View : UserControl
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public CST_View()
        {
            InitializeComponent();
            if (dc.DatabaseExists())
                CSTGrid.DataSource = dc.vwCSTs;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CSTGrid.DataSource = null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dc.DatabaseExists())
            {
                dc.SubmitChanges();
                dc = null;
            }
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            CSTGrid.DataSource = dc.vwCSTs;

            Infragistics.Windows.DataPresenter.FieldSortDescription divisionSort = new Infragistics.Windows.DataPresenter.FieldSortDescription
            {
                FieldName = "Division",
                IsGroupBy = true
            };
            Infragistics.Windows.DataPresenter.FieldSortDescription classNameSort = new Infragistics.Windows.DataPresenter.FieldSortDescription
            {
                FieldName = "ClassName",
                IsGroupBy = true
            };
            Infragistics.Windows.DataPresenter.FieldSortDescription dayofweekSort = new Infragistics.Windows.DataPresenter.FieldSortDescription
            {
                FieldName = "Dayofweek",
                IsGroupBy = true
            };
            Infragistics.Windows.DataPresenter.FieldSortDescription timeofweekSort = new Infragistics.Windows.DataPresenter.FieldSortDescription
            {
                FieldName = "Timeofweek",
                IsGroupBy = true
            };
            CSTGrid.FieldLayouts[0].SortedFields.Add(divisionSort);
            CSTGrid.FieldLayouts[0].SortedFields.Add(classNameSort);
            CSTGrid.FieldLayouts[0].SortedFields.Add(dayofweekSort);
            CSTGrid.FieldLayouts[0].SortedFields.Add(timeofweekSort);



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
                exporter.Export(CSTGrid, fileName, WorkbookFormat.Excel2007, op);
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

    }//Class
}
