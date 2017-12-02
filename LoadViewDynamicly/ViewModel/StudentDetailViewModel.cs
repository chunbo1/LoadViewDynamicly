using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;


namespace LoadViewDynamicly.ViewModel
{
    class StudentDetailViewModel : ViewModelBase
    {

        private StudentDetail displayedProduct = new StudentDetail();
        public StudentDetail DisplayedProduct
        {
            get { return displayedProduct; }
            set
            {
                displayedProduct = value;
                RaisePropertyChanged("DisplayedProduct");
            }
        }

        private void PopulateStudentDetail()
        {

            DisplayedProduct = new StudentDetail();
            //App.Messenger.NotifyColleagues("GetClassStudents");
            MainWindowViewModel.Instance.StatusBar = $"Populate StudentDetail";
        }



    }//Class StudentDetailViewModel






}
