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
using log4net;
using System.Reflection;

namespace LoadViewDynamicly.View
{
    /// <summary>
    /// Interaction logic for CSDisplayView.xaml
    /// </summary>
    public partial class CSDisplayView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public CSDisplayView()
        {
            InitializeComponent();
            
            DataContext = new CSDisplayViewModel();
            /* 
             * Xaml section triggers instantiation of CSDisplayViewModel
             * We only need to keep one VM instiation either in XAML or in code
            <UserControl.DataContext >
                < vm:CSDisplayViewModel />
            </UserControl.DataContext >
            */
        }
    }
}
