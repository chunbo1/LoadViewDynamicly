using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using LoadViewDynamicly.ViewModel;
using log4net;
using System.Reflection;

namespace LoadViewDynamicly.Model
{
    class StoreDB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public bool hasError = false;
        public string errorMessage;
        public StoreDB()
        {

        }
        public MyObservableCollection<ViewModel.ClassStudent> GetClassStudents()
        {
            hasError = false;
            MyObservableCollection<ViewModel.ClassStudent> products = new MyObservableCollection<ViewModel.ClassStudent>();
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                var query = from q in dc.ClassStudents
                            join c in dc.Classes on q.ClassId equals c.ID
                            join s in dc.Students on q.StudentId equals s.ID

                            select new SqlClassStudent
                            {
                                Id = q.ID,
                                StudentId = q.StudentId,
                                StudentName = s.FirstName + " " + s.LastName,
                                ClassId = q.ClassId,
                                ClassName = c.ClassName,
                                ClassTuition = c.Tuition,
                                TuitionPaid = q.TuitionPaid,            
                                Comment = q.Comment,
                                UpdateDateTime = q.UpdateDateTime,
                                Grouping = c.ClassName + " " + c.Semester + " " + c.Dayofweek + " " + c.Timeofweek
                            };
                foreach (SqlClassStudent sp in query)
                    products.Add(sp.SqlProduct2Product());
            } //try
            catch (Exception ex)
            {
                errorMessage = "GetProducts() error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..GetClassStudents: " + ex.Message);
            }
            return products;
        } //GetProducts()

        public bool UpdateProduct(ViewModel.ClassStudent displayP)
        {
            try
            {
                SqlClassStudent p = new SqlClassStudent(displayP);
                DataClasses1DataContext dc = new DataClasses1DataContext();
                dc.UpdateCS(p.Id, p.StudentId, p.ClassId, p.TuitionPaid, p.Comment);
            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..UpdateProduct: " + ex.StackTrace);
            }
            return (!hasError);
        } //UpdateProduct()

        public bool DeleteProduct(int productId)
        {
            hasError = false;
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                dc.DeleteCS(productId);
            }
            catch (Exception ex)
            {
                errorMessage = "Delete error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..DeleteProduct: " + ex.StackTrace);
            }
            return !hasError;
        }// DeleteProduct()

        public bool AddProduct(ViewModel.ClassStudent displayP)
        {
            hasError = false;
            try
            {
                SqlClassStudent p = new SqlClassStudent(displayP);
                DataClasses1DataContext dc = new DataClasses1DataContext();
                int? newId = 0;
                dc.AddCS(p.StudentId, p.ClassId, p.TuitionPaid, p.Comment, ref newId);
                p.Id = (int)newId;
                displayP.ProductAdded2DB(p);    //update corresponding ClassStudent Id using SqlClassStudent
            }
            catch (Exception ex)
            {
                errorMessage = "Add error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..AddProduct: " + ex.StackTrace);
            }
            return !hasError;
        } //AddProduct()

        public MyObservableCollection<ViewModel.MyStudent> GetStudentsByClass(int classId)
        {
            hasError = false;
            MyObservableCollection<ViewModel.MyStudent> products = new MyObservableCollection<ViewModel.MyStudent>();
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                var query = from q in dc.vwStudentByClasses
                            where q.ClassId == classId
                            select new MyStudent(q.ID, q.BirthDate + " " + q.Gender, q.FirstName, q.LastName);
                            
                foreach (MyStudent sp in query)
                    products.Add(sp);
            } //try
            catch (Exception ex)
            {
                errorMessage = "GetProducts() error, " + ex.Message;
                hasError = true;
            }
            return products;
        } //GetProducts()

        public List<ScheduleStudentViewModel> GetStudentsByClassA(int classId)
        {
            hasError = false;
            List<ScheduleStudentViewModel> products = new List<ScheduleStudentViewModel>();
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                var query = from q in dc.vwStudentByClasses
                            where q.ClassId == classId
                            select new ScheduleStudentViewModel(new MyStudent(q.ID, q.BirthDate + " " + q.Gender, q.FirstName, q.LastName));

                foreach (ScheduleStudentViewModel sp in query)
                    products.Add(sp);
            } //try
            catch (Exception ex)
            {
                errorMessage = "GetProducts() error, " + ex.Message;
                hasError = true;
            }
            return products;
        } //GetStudentsByClassA()

        public int? AddSchedulesHeader(int classId, int teacherId, DateTime classDate, 
            string startTime, string endTime, string status, string comment)
        {
            int? newId = 0;
            hasError = false;
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();                
                dc.AddSchedulesHeader(classId, teacherId, classDate, startTime, endTime, status, comment, ref newId);
            }
            catch (Exception ex)
            {
                errorMessage = "Add error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..AddSchedulesHeader: " + ex.StackTrace);
            }
            return newId;
        } //AddSchedulesHeader

        public bool AddSchedulesDetail(int headerId, int studentId, bool status, string comment)
        {
            hasError = false;
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                int? newId = 0;
                dc.AddSchedulesDetail(headerId, studentId, status, comment, ref newId);
            }
            catch (Exception ex)
            {
                errorMessage = "Add error, " + ex.Message;
                hasError = true;
                log.Error("In StoreDB..AddSchedulesDetail: " + ex.StackTrace);
            }
            return !hasError;
        } //AddSchedulesHeader



























    }//Class
}//NS
