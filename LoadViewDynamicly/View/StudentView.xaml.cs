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
using System.Data.Linq;
using log4net;
using System.Reflection;

namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public StudentView()
        {
            InitializeComponent();
            try
            {
                if (dc.DatabaseExists())
                    StudentGrid.DataSource = dc.Students;
                log.Info("StudentView Constructor called");
            }
            catch (Exception e)
            {
                log.Error("In StudentView: " + e.StackTrace);
                Environment.Exit(-1);
            }
            finally
            {
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            StudentGrid.DataSource = null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dc.DatabaseExists())
            {
                dc.SubmitChanges();
                dc = null;
            }
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            StudentGrid.DataSource = dc.Students;
            log.Info("StudentView Load_Click called");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
            Messenger messenger = App.Messenger;
            //After adding a new student, need to refresh StudentTable
            messenger.NotifyColleagues("RefreshStudentTable");

        }
    }//Class

     

}//NS
