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

namespace LoadViewDynamicly.Report
{
    /// <summary>
    /// Interaction logic for StudentRoster.xaml
    /// </summary>
    public partial class StudentRoster : UserControl
    {
        public StudentRoster()
        {
            InitializeComponent();

            DataContext = new StudentRosterViewModel();
            (DataContext as StudentRosterViewModel)._view = this as StudentRoster;
        }










    }//Cls
}//NS
