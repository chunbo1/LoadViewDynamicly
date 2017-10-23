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
using System.Windows.Shapes;
using LoadViewDynamicly.ViewModel;

namespace LoadViewDynamicly
{
    /// <summary>
    /// Interaction logic for MainRibbon.xaml
    /// </summary>
    public partial class MainRibbon : Infragistics.Windows.Ribbon.XamRibbonWindow
    {
        public MainRibbon()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
