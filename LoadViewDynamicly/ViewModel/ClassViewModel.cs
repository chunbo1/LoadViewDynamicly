﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using gala = GalaSoft.MvvmLight.Messaging ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoadViewDynamicly.ViewModel
{
    class ClassViewModel: ViewModelBase
    {
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

        public ICommand ChangeToTeacherViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    gala.Messenger.Default.Send<SwitchViewMessage>(new SwitchViewMessage { ViewName = "TeacherView" });
                });
            }
        }
    }
}
