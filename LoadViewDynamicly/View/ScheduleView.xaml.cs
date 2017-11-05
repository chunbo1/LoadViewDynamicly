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
using LoadViewDynamicly.ViewModel;
using LoadViewDynamicly.Model;


namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : UserControl
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        public ScheduleView()
        {
            InitializeComponent();
            DataContext = new ScheduleViewModel();
            (DataContext as ScheduleViewModel)._view = this as ScheduleView;
        }

        private void Load_ClickXXX(object sender, RoutedEventArgs e)
        {
            StoreDB db = new StoreDB();
            ScheduleGrid.DataSource = db.GetStudentsByClass(4);

        }



        void OnShowRecords(object sender, RoutedEventArgs e)
        {
            List<string> checkedNames = new List<string>();
            List<string> uncheckedNames = new List<string>();

            var community = base.DataContext as CommunityViewModel;
            foreach (ScheduleStudentViewModel p in community.Members)
            {
                if (p.IsChecked)
                    checkedNames.Add(p.FullName);
                else
                    uncheckedNames.Add(p.FullName);
            }

            checkedNames.Sort();
            uncheckedNames.Sort();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CheckBox Checked");
            foreach (string s in checkedNames)
                sb.AppendLine(s);

            sb.AppendLine();

            sb.AppendLine("CheckBox UnChecked");
            foreach (string s in uncheckedNames)
                sb.AppendLine(s);

            string msg = sb.ToString();
            string caption = "MessageBox_Caption";
            MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }//Class
}//NS
