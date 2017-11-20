﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using gala=GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using log4net;
using System.Reflection;


namespace LoadViewDynamicly.ViewModel
{
    public class TeacherViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        public ICommand ChangeToStudentViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    gala.Messenger.Default.Send<SwitchViewMessage>(new SwitchViewMessage { ViewName = "StudentView" });
                });
            }
        }
    }
}
