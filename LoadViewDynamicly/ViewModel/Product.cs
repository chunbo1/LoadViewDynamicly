using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using log4net;
using System.Reflection;

namespace LoadViewDynamicly.ViewModel
{
    //Class for the GUI to display and modify products.
    //All product properties the GUI can touch are strings.
    //A single integer property, ProductId, is for database use only.
    //It is assigned by the DB when it creates a new product.  It is used
    //to identify a product and must not be modified by the GUI.
    [Serializable]
    public class ClassStudent : IEquatable<ClassStudent>
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        bool IEquatable<ClassStudent>.Equals(ClassStudent other)
        {
            return Grouping == other.Grouping && StudentId == other.StudentId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //For DB use only!
        private int id;
        public int ID { get { return id; } }

        private int? studentId;
        public int? StudentId
        {
            get { return studentId; }
            set
            {
                studentId = value;
                OnPropertyChanged("StudentId");
            }
        }

        private string studentname;
        public string StudentName
        {
            get { return studentname; }
            set
            {
                studentname = value;
                OnPropertyChanged("StudentName");
            }
        }

        private int? classId;
        public int? ClassId
        {
            get { return classId; }
            set
            {
                classId = value;
                OnPropertyChanged("ClassId");
            }
        }

        private string classname;
        public string ClassName
        {
            get { return classname; }
            set
            {
                classname = value;
                OnPropertyChanged("ClassName");
            }
        }

        private double? classTuition;
        public double? ClassTuition
        {
            get { return classTuition; }
            set
            {
                classTuition = value;
                OnPropertyChanged("ClassTuition");
            }
        }

        private string stucls_name;
        public string StuClsName
        {
            get { return StudentName + " - " + ClassName; }
            set
            {
                stucls_name = value;
                OnPropertyChanged("StuClsName");
            }
        }
        private int? tuitionPaid;
        public int? TuitionPaid
        {
            get { return tuitionPaid; }
            set
            {
                tuitionPaid = value;
                OnPropertyChanged("TuitionPaid");
                if (tuitionPaid.HasValue && ClassTuition.HasValue)
                    TuitionDiscount = 1-((double) tuitionPaid) / ClassTuition;
            }
        }

        private double? tuitionDiscount;
        public double? TuitionDiscount
        {
            get { return tuitionDiscount; }
            set
            {
                tuitionDiscount = value;
                OnPropertyChanged("TuitionDiscount");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value;
                OnPropertyChanged("Comment"); }
        }
        
        private DateTime updateDateTime;
        public DateTime UpdateDateTime
        {
            get { return updateDateTime; }
            set { updateDateTime = value;
                OnPropertyChanged("UpdateDateTime"); }
        }

        private string grouping;
        public string Grouping
        {
            get { return grouping; }
            set
            {
                grouping = value;
                OnPropertyChanged("Grouping");
            }
        }
        public ClassStudent()
        {
        }

        public ClassStudent(int _Id, int? studentId, string studentName, 
            int? classId, string className, double? classTuition, int? tuitionPaid,
                       string comment, DateTime updateDateTime, string grouping, double? tuitionDiscount)
        {
            this.id = _Id;
            StudentId = studentId;
            StudentName = studentName;
            ClassId = classId;
            ClassName = className;
            ClassTuition = classTuition;
            TuitionPaid = tuitionPaid;
            TuitionDiscount = tuitionDiscount;
            Comment = comment;
            UpdateDateTime = updateDateTime;
            Grouping = grouping;
        }
        

        public void CopyProduct(ClassStudent p)
        {
            this.id = p.ID;
            this.StudentId = p.StudentId;
            this.StudentName = p.StudentName;
            this.ClassId = p.ClassId;
            this.ClassName = p.ClassName;
            this.ClassTuition = p.ClassTuition;
            this.TuitionDiscount = p.TuitionDiscount;
            this.TuitionPaid = p.tuitionPaid;
            this.Comment = p.Comment;
            this.UpdateDateTime = p.UpdateDateTime;            
            this.Grouping = p.grouping;
        }

        //Creating a new ClassStudent in the DB assigns the ProductId
        //Update the ProductId from the value in the corresponding SqlProduct
        public void ProductAdded2DB(SqlClassStudent sqlProduct)
        {
            this.id = sqlProduct.Id;
        }

    } //class ClassStudent


    //Communiction to/from SQL uses this class for product
    //It has a decimal, not string, definition for UnitCost
    //Consversion routines SqlClassStudent <--> ClassStudent provided
    public class SqlClassStudent
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string StudentName { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public double? ClassTuition { get; set; }
        public int? TuitionPaid { get; set; }
        public double? TuitionDiscount { get; set; }
        public string Comment { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string Grouping { get; set; }

        public SqlClassStudent() { }

        public SqlClassStudent(int id, int studentId, int classId, int tuitionPaid,
                      string comment, DateTime timestamp, string grouping)
        {
            Id = id;
            StudentId = studentId;
            ClassId = classId;
            TuitionPaid = tuitionPaid;
            Comment = comment;
            UpdateDateTime = timestamp;
            Grouping = grouping;
        }

        public SqlClassStudent(ClassStudent p)
        {
            Id = p.ID;
            StudentId = p.StudentId;
            ClassId = p.ClassId;
            TuitionPaid = p.TuitionPaid;
            Comment = p.Comment;
            UpdateDateTime = p.UpdateDateTime;
            TuitionDiscount = p.TuitionDiscount;
        }

        public ClassStudent SqlProduct2Product()
        {
            return new ClassStudent(Id, StudentId, StudentName, ClassId, ClassName, ClassTuition, TuitionPaid, Comment,  UpdateDateTime, Grouping, 1-TuitionPaid/ClassTuition);
        } //SqlProduct2Product()
    }//Class SqlClassStudent


    public class MyStudent
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public string Comment { get; set; }

        public MyStudent(int id, string comment, string firstname, string lastname)
        {
            ID = id;
            Comment = comment;
            FullName = firstname + " " + lastname;
        }
    }//Class

    public class MyClass
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string FullName { get; set; }
        public string Semester { get; set; }
        public string Dayofweek { get; set; }
        public string Timeofweek { get; set; }
        public bool? Enabled { get; set; }
        public double? TuitionPaid { get; set; }
        public MyClass(int id, string division, string className, string semester, string dayofweek, string timeofweek, bool? enabled, double? tuitionPaid)
        {
            ID = id;
            ClassName = className;
            Semester = semester;
            Dayofweek = dayofweek;
            Timeofweek = timeofweek;
            Enabled = enabled;
            FullName = className.Trim() + " " + semester.Trim() + " " + dayofweek.Trim() + " " + timeofweek.Trim();
            TuitionPaid = tuitionPaid;
        }
    }//MyClass

    public class MyTeacher
    {
        public int ID { get; set; }

        public string Cellphone { get; set; }

        public string FullName { get; set; }
        public bool? Enabled { get; set; }
        public MyTeacher(int id, string cellphone, string firstname, string lastname, bool? enabled)
        {
            ID = id;
            Cellphone = cellphone;
            FullName = firstname + " " + lastname;
            Enabled = enabled;
        }
    }//MyTeacher



}//NS
