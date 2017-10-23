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

namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
        public StudentView()
        {
            InitializeComponent();
            if (dc.DatabaseExists())
                StudentGrid.DataSource = dc.Students;
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

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
        }
    }//Class

     

}//NS
