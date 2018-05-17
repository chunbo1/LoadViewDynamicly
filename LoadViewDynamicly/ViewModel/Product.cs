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

    //if forghet inherit INotifyPropertyChanged, TuitionDiscount never get auto calculated because PropertyChanged always = null
    //By inheriting INotifyPropertyChanged, WPF data-binding infrastructure will add a PropertyChanged handler when you set the object as a DataContext
    public class ClassStudent : IEquatable<ClassStudent>, INotifyPropertyChanged//if forghet inherit INotifyPropertyChanged
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        bool IEquatable<ClassStudent>.Equals(ClassStudent other)
        {//Used by dataItems.IndexOf(selectedProduct);
         //used by DataItems.Contains(DisplayedProduct)
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
                //https://stackoverflow.com/questions/2235890/inotifypropertychanged-and-calculated-property
                if (tuitionPaid.HasValue && ClassTuition.HasValue)
                    OnPropertyChanged("TuitionDiscount");
            }
        }

        private double? tuitionDiscount;
        public double? TuitionDiscount
        {
            get {
                //After adding a new student in Class, we need to retrieve ClassTuition; StoreDB..GetClassStudents gets ClassTuition
                //If it comes here from Delete student, we dont want to invoke spGetClassTuition
                
                if ( ClassTuition == null && App.last_action.Equals("Add") && ClassId!= null)
                {
                    dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);
                    ClassTuition = dc.spGetClassTuition(ClassId).First().Tuition;
                    log.Info("In Products ClassStudent..TuitionDiscount, it invoked dc.spGetClassTuition(ClassId)!!! Extra Cost!!!");
                }
                if (ClassTuition == null && tuitionPaid == null)
                    return 0;
                else
                    return 1 - ((double)tuitionPaid) / ClassTuition;                             
                }
            set
            {
                tuitionDiscount = value;
                OnPropertyChanged("TuitionDiscount");
            }
        }


        private double? outstandingBalance;
        public double? OutstandingBalance
        {
            get
            {
                if (tuitionPaid == null)
                    return 0;
                else
                    return ((double)tuitionPaid) - (cashReceived==null ? 0 : cashReceived) - (CheckReceived == null ? 0 : CheckReceived) - (CreditCardReceived== null ? 0 : CreditCardReceived) - (OtherReceived == null ? 0 : OtherReceived);
            }

        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value;
                OnPropertyChanged("Comment"); }
        }

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                invoiceNumber = value;
                OnPropertyChanged("InvoiceNumber");
            }
        }


        private string casherName;
        public string CasherName
        {
            get { return casherName; }
            set
            {
                casherName = value;
                OnPropertyChanged("CasherName");
            }
        }


        private double? cashReceived;
        public double? CashReceived
        {
            get { return cashReceived; }
            set
            {
                cashReceived = value;
                OnPropertyChanged("CashReceived");
            }
        }


        private double? checkReceived;
        public double? CheckReceived
        {
            get { return checkReceived; }
            set
            {
                checkReceived = value;
                OnPropertyChanged("CheckReceived");
            }
        }


        private string checkNumber;
        public string CheckNumber
        {
            get { return checkNumber; }
            set
            {
                checkNumber = value;
                OnPropertyChanged("CheckNumber");
            }
        }


        private double? creditCardReceived;
        public double? CreditCardReceived
        {
            get { return creditCardReceived; }
            set
            {
                creditCardReceived = value;
                OnPropertyChanged("CreditCardReceived");
            }
        }


        private double? otherReceived;
        public double? OtherReceived
        {
            get { return otherReceived; }
            set
            {
                otherReceived = value;
                OnPropertyChanged("OtherReceived");
            }
        }


        private string otherSource;
        public string OtherSource
        {
            get { return otherSource; }
            set
            {
                otherSource = value;
                OnPropertyChanged("OtherSource");
            }
        }


        private DateTime? registrationDate;
        public DateTime? RegistrationDate
        {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
                OnPropertyChanged("RegistrationDate");
            }
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
                       string comment, DateTime updateDateTime, string grouping, double? tuitionDiscount,
                       string invoiceNumber, string casherName, double? cashReceived, double? checkReceived, string checkNumber, double? creditCardReceived, double? otherReceived, string otherSource, DateTime? registrationDate
                       )
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

            InvoiceNumber = invoiceNumber;
            CasherName = casherName;
            CashReceived = cashReceived;
            CheckReceived = checkReceived;
            CheckNumber = checkNumber;
            CreditCardReceived = creditCardReceived;
            OtherReceived = otherReceived;
            OtherSource = otherSource;
            RegistrationDate = registrationDate;
        }
        

        //Deep copy logic is not used 
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

        public string InvoiceNumber { get; set; }
        public string CasherName { get; set; }
        public double? CashReceived { get; set; }
        public double? CheckReceived { get; set; }
        public string CheckNumber { get; set; }
        public double? CreditCardReceived { get; set; }
        public double? OtherReceived { get; set; }
        public string OtherSource { get; set; }
        public DateTime? RegistrationDate { get; set; }



        public SqlClassStudent() { }


        public SqlClassStudent(ClassStudent p)
        {
            Id = p.ID;
            StudentId = p.StudentId;
            ClassId = p.ClassId;
            TuitionPaid = p.TuitionPaid;
            Comment = p.Comment;
            UpdateDateTime = p.UpdateDateTime;
            InvoiceNumber = p.InvoiceNumber;
            CasherName = p.CasherName;
            CashReceived = p.CashReceived;
            CheckReceived = p.CheckReceived;
            CheckNumber = p.CheckNumber;
            CreditCardReceived = p.CreditCardReceived;
            OtherReceived = p.OtherReceived;
            OtherSource = p.OtherSource;
            RegistrationDate = p.RegistrationDate;
            //TuitionDiscount = p.TuitionDiscount;
        }

        public ClassStudent SqlProduct2Product()
        {
            return new ClassStudent(Id, StudentId, StudentName, ClassId, ClassName, ClassTuition, TuitionPaid, Comment,  UpdateDateTime, Grouping, 1-TuitionPaid/ClassTuition,
                InvoiceNumber, CasherName, CashReceived, CheckReceived, CheckNumber, CreditCardReceived, OtherReceived, OtherSource, RegistrationDate);
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



    //Used by ScheduleView
    public class StudentDetail
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //For DB use only!
        private int id;
        public int ID {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        
        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private DateTime? birthDate;
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        private string contactName;
        public string ContactName
        {
            get { return contactName; }
            set
            {
                contactName = value;
                OnPropertyChanged("ContactName");
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string cellPhone;
        public string CellPhone
        {
            get { return cellPhone; }
            set
            {
                cellPhone = value;
                OnPropertyChanged("CellPhone");
            }
        }

        private string homePhone;
        public string HomePhone
        {
            get { return homePhone; }
            set
            {
                homePhone = value;
                OnPropertyChanged("HomePhone");
            }
        }

        private string studentPhone;
        public string StudentPhone
        {
            get { return studentPhone; }
            set
            {
                studentPhone = value;
                OnPropertyChanged("StudentPhone");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        private DateTime? updateDateTime;
        public DateTime? UpdateDateTime
        {
            get { return updateDateTime; }
            set
            {
                updateDateTime = value;
                OnPropertyChanged("UpdateDateTime");
            }
        }

        public StudentDetail()
        { }

        public StudentDetail(int id, string firstName, string lastName, string gender,
            DateTime? birthDate, string contactName, string address, string email, string cellPhone,
            string homePhone, string studentPhone, string comment, DateTime? updateDateTime)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            ContactName = contactName;
            Address = address;
            Email = email;
            CellPhone = cellPhone;
            HomePhone = homePhone;
            StudentPhone = studentPhone;
            Comment = comment;
            UpdateDateTime = updateDateTime;
        }
    }//Class StudentDetail

}//NS
