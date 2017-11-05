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


//TEST GitHub
namespace LoadViewDynamicly.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Exit
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
        #endregion

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

            this.VIEWSpsaces.Add("StudentView", studentView);
            this.VIEWSpsaces.Add("TeacherView", teacherView);
            this.VIEWSpsaces.Add("ClassView", classView);
            this.VIEWSpsaces.Add("CSSelectionView", csSelectionView);
            this.VIEWSpsaces.Add("ScheduleView", scheduleView);

            ViewModelBase studentViewModel = new StudentViewModel() { Text = "Student View" };
            ViewModelBase teacherViewModel = new TeacherViewModel() { Text = "Teacher View" };
            ViewModelBase classViewModel = new ClassViewModel() { Text = "Class View" };
            
            this.VMspaces.Add("StudentViewModel", studentViewModel);
            this.VMspaces.Add("TeacherViewModel", teacherViewModel);
            this.VMspaces.Add("ClassViewModel", classViewModel);
           

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
                    SwitchView("CSSelectionView");
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

                case "CSSelectionView":
                    ContentControlView = VIEWSpsaces["CSSelectionView"];
                    //ContentControlView.DataContext = VMspaces["CSViewModel"];
                    break;

                case "ScheduleView":
                    ContentControlView = VIEWSpsaces["ScheduleView"];
                    break;

            }
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
