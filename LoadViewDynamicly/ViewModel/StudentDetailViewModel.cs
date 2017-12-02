using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using System.Reflection;
using System.Data.Linq;

namespace LoadViewDynamicly.ViewModel
{
    class StudentDetailViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        public StudentDetailViewModel()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("StudentSelectionChanged", (Action<int>)(param => ProcessStudent(param)));

        }//ctor

        public void ProcessStudent(int id)
        {
            dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
            ISingleResult<spGetStudentDetailResult> stu = dc.spGetStudentDetail(id);
            spGetStudentDetailResult student = stu.Single();
            CurrentStudent = new StudentDetail(student.ID, student.FirstName, student.LastName, student.Gender, 
                DateTime.ParseExact(String.IsNullOrEmpty(student.BirthDate) ? "20000101": student.BirthDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), 
                student.ContactName, student.Address, student.Email, student.CellPhone, student.HomePhone, student.StudentPhone, student.Comment,
                student.UpdateDateTime);
            MainWindowViewModel.Instance.StatusBar = $"Populate StudentDetail for {student.FirstName} {student.LastName}";

        }

        private StudentDetail currentStudent = new StudentDetail();
        public StudentDetail CurrentStudent
        {
            get { return currentStudent; }
            set
            {
                currentStudent = value;
                RaisePropertyChanged("CurrentStudent");
            }
        }
        



    }//Class StudentDetailViewModel






}
