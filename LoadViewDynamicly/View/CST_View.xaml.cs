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

        }
    }//Class
}
