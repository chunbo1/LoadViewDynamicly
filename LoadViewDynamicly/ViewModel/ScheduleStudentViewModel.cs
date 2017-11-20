using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using log4net;
using System.Reflection;


namespace LoadViewDynamicly.ViewModel
{
    public class ScheduleStudentViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly MyStudent _person;
        bool _isChecked;
        
        public ScheduleStudentViewModel(MyStudent person)
        {
            _person = person;
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value == _isChecked)
                    return;

                _isChecked = value;

                this.RaisePropertyChanged("IsChecked");
            }
        }

        public int ID { get { return _person.ID; } }
        public string FullName { get { return _person.FullName; } }
        public string Comment {
            get { return _person.Comment; }
            //with a setter, the field is editable in datagrid
            set {
                _person.Comment = value;
                this.RaisePropertyChanged("Comment");
            }
        }


    }//Class ScheduleStudentViewModel


    #region CommunityViewModel [nested class]
    /// <summary>
    /// A presentation-friendly class that contains a list of PersonViewModel objects
    /// and manages an aggregated check state for the group, via the get/set property
    /// called AllMembersAreChecked.
    /// </summary>
    public class CommunityViewModel : ViewModelBase
    {
        public CommunityViewModel(List<ScheduleStudentViewModel> members)
        {
            this.Members = members;

            foreach (ScheduleStudentViewModel member in members)
                member.PropertyChanged += (sender, e) =>
                {
                    // Raising PropertyChanged for the AllMembersAreChecked
                    // property causes the data binding system to query its
                    // getter for the new value.
                    if (e.PropertyName == "IsChecked")
                        this.RaisePropertyChanged("AllMembersAreChecked");
                };
        }

        public List<ScheduleStudentViewModel> Members { get;  private set; }

        public bool? AllMembersAreChecked
        {
            get
            {
                // Determine if all members have the same 
                // value for the IsChecked property.
                bool? value = null;
                for (int i = 0; i < this.Members.Count; ++i)
                {
                    if (i == 0)
                    {
                        value = this.Members[0].IsChecked;
                    }
                    else if (value != this.Members[i].IsChecked)
                    {
                        value = null;
                        break;
                    }
                }

                return value;
            }
            set
            {
                if (value == null)
                    return;

                foreach (ScheduleStudentViewModel member in this.Members)
                    member.IsChecked = value.Value;
            }
        }

        #region INotifyPropertyChanged Members
        
        #endregion // INotifyPropertyChanged Members
    }

#endregion // CommunityViewModel [nested class]















}//NS
