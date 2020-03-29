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
using log4net;
using System.Reflection;
using Infragistics.Windows.DataPresenter.Events;

namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for MembershipView.xaml
    /// </summary>
    public partial class MembershipView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        public MembershipView()
        {
            InitializeComponent();
            try
            {
                if (dc.DatabaseExists())
                    MembershipGrid.DataSource = dc.Memberships;
                MembershipGrid.RecordAdded += new EventHandler<RecordAddedEventArgs>(MembershipGrid_RecordAdded);
                log.Info("MembershipView Constructor called");
            }
            catch (Exception e)
            {
                log.Error("In MembershipView: " + e.StackTrace);
                Environment.Exit(-1);
            }
            finally
            {
            }

        }

        private void MembershipGrid_RecordAdded(object sender, RecordAddedEventArgs e)
        {
            Infragistics.Windows.DataPresenter.DataRecord newRow = e.Record;
            newRow.Cells["UpdateDateTime"].Value = DateTime.Now;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MembershipGrid.DataSource = null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dc.DatabaseExists())
            {
                dc.SubmitChanges();
                dc = null;
            }
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            MembershipGrid.DataSource = dc.Memberships;
            log.Info("MembershipView Load_Click called");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
            Messenger messenger = App.Messenger;
            //After adding a new member, need to refresh MembeshipTable
            //messenger.NotifyColleagues("RefreshMembeshipTable");

        }


    }//class
}
