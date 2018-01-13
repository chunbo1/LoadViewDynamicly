using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using gala=GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LoadViewDynamicly.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using log4net;
using System.Reflection;
using LoadViewDynamicly.Report;

//TEST GitHub
namespace LoadViewDynamicly.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static MainWindowViewModel _instance = new MainWindowViewModel();
        public static MainWindowViewModel Instance { get { return _instance; } }
        
        public MainWindowViewModel()
        {
            //http://dotnetpattern.com/mvvm-light-messenger
            gala.Messenger.Default.Register<SwitchViewMessage>(this, (switchViewMessage) =>
            {
                SwitchView(switchViewMessage.ViewName);
            });

            UserControl studentView = new StudentView();
            UserControl teacherView = new TeacherView();
            UserControl classView = new ClassView();
            UserControl csSelectionView = new CSMain();
            UserControl scheduleView = new ScheduleView();
            UserControl studentRosterView = new StudentRoster();
            UserControl classStudentEnrollView= new ClassStudentEnroll();
            UserControl classPnLReportView = new ClassPnLReport();
            UserControl changeStudentAttendanceView = new StudentAttendanceReport();

            this.VIEWSpsaces.Add("StudentView", studentView);
            this.VIEWSpsaces.Add("TeacherView", teacherView);
            this.VIEWSpsaces.Add("ClassView", classView);
            this.VIEWSpsaces.Add("ClassManagementView", csSelectionView);
            this.VIEWSpsaces.Add("ScheduleView", scheduleView);
            this.VIEWSpsaces.Add("StudentRosterView", studentRosterView);
            this.VIEWSpsaces.Add("ChangeClassStudentEnrollView", classStudentEnrollView);
            this.VIEWSpsaces.Add("ClassPnLReportView", classPnLReportView);
            this.VIEWSpsaces.Add("ChangeStudentAttendanceView", changeStudentAttendanceView);

            ViewModelBase studentViewModel = new StudentViewModel() { Text = "Student View" };
            ViewModelBase teacherViewModel = new TeacherViewModel() { Text = "Teacher View" };
            ViewModelBase classViewModel = new ClassViewModel() { Text = "Class View" };
            

            this.VMspaces.Add("StudentViewModel", studentViewModel);
            this.VMspaces.Add("TeacherViewModel", teacherViewModel);
            this.VMspaces.Add("ClassViewModel", classViewModel);
            StatusBar = "App Started at " + DateTime.Now;

        }
        
        private RelayCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                {
                    if (exitCommand == null)
                        exitCommand = new RelayCommand(exit);
                }
                return exitCommand;
            }
        }

        private void exit()
        {
            //try { c.Close(); }//CLose WCF Client
            //catch { }
            Application.Current.Shutdown();
        }

        private FrameworkElement _contentControlView;
        public FrameworkElement ContentControlView
        {
            get { return _contentControlView; }
            set
            {
                _contentControlView = value;
                RaisePropertyChanged("ContentControlView");
            }
        }

        private String statusBar;
        public String StatusBar
        {
            get { return statusBar; }
            set
            {
                statusBar = value;
                RaisePropertyChanged("StatusBar");
            }
        }

        public ICommand ChangeStudentViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("StudentView");

                });
            }
        }
        
        public ICommand ChangeTeacherViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("TeacherView");
                });
            }
        }

        public ICommand ChangeClassViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ClassView");
                });
            }
        }

        public ICommand ChangeClassStudentViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ClassManagementView");
                });
            }
        }
        
        public ICommand ChangeScheduleViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ScheduleView");
                });
            }
        }

        public ICommand ChangeStudentRosterCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("StudentRosterView");
                });
            }
        }

        public ICommand ChangeClassStudentEnrollCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ChangeClassStudentEnrollView");
                });
            }
        }

        public ICommand ChangeClassPnLReportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ChangeClassPnLView");
                });
            }
        }

        public ICommand ChangeStudentAttendanceReportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SwitchView("ChangeStudentAttendanceView");
                });
            }
        }


        public void SwitchView(string viewName)
        {
            switch (viewName)
            {
                case "StudentView":
                    ContentControlView = VIEWSpsaces["StudentView"];
                    ContentControlView.DataContext = VMspaces["StudentViewModel"];
                    break;

                case "TeacherView":
                    ContentControlView = VIEWSpsaces["TeacherView"];
                    ContentControlView.DataContext = VMspaces["TeacherViewModel"];
                    break;

                case "ClassView":
                    ContentControlView = VIEWSpsaces["ClassView"];
                    ContentControlView.DataContext = VMspaces["ClassViewModel"];
                    break;

                case "ClassManagementView":
                    ContentControlView = VIEWSpsaces["ClassManagementView"];
                    //ContentControlView.DataContext = VMspaces["CSViewModel"];
                    break;

                case "ScheduleView":
                    ContentControlView = VIEWSpsaces["ScheduleView"];
                    break;

                case "StudentRosterView":
                    ContentControlView = VIEWSpsaces["StudentRosterView"];
                    break;

                case "ChangeClassStudentEnrollView":
                    ContentControlView = VIEWSpsaces["ChangeClassStudentEnrollView"];
                    break;

                case "ChangeClassPnLView":
                    ContentControlView = VIEWSpsaces["ClassPnLReportView"];
                    break;

                case "ChangeStudentAttendanceView":
                    ContentControlView = VIEWSpsaces["ChangeStudentAttendanceView"];
                    break;

            }
            MainWindowViewModel.Instance.StatusBar = $"Loaded {viewName}";
        }

        Dictionary<String, ViewModelBase> _vmSpaces;
        public Dictionary<String, ViewModelBase> VMspaces
        {
            get
            {
                if (_vmSpaces == null)
                {
                    _vmSpaces = new Dictionary<String, ViewModelBase>();
                    
                }
                return _vmSpaces;
            }
        }

        Dictionary<String, UserControl> _viewSpacses;
        public Dictionary<String, UserControl> VIEWSpsaces
        {
            get
            {
                if (_viewSpacses == null)
                {
                    _viewSpacses = new Dictionary<String, UserControl>();

                }
                return _viewSpacses;
            }
        }


    }//MainWindowViewModel




















}//NS
