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
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class ClassView : UserControl
    {

        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public ClassView()
        {
            InitializeComponent();
            if (dc.DatabaseExists())
                Grid.DataSource = dc.Classes;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Grid.DataSource = null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (dc.DatabaseExists())
            {
                dc.SubmitChanges();
                dc = null;
            }
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            Grid.DataSource = dc.Classes;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
        }


    }
}
