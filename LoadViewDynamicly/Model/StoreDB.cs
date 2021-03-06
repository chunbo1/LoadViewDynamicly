﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using LoadViewDynamicly.ViewModel;

namespace LoadViewDynamicly.Model
{
    class StoreDB
    {
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
                                TuitionPaid = q.TuitionPaid,            
                                Comment = q.Comment,
                                UpdateDateTime = q.UpdateDateTime
                            };
                foreach (SqlClassStudent sp in query)
                    products.Add(sp.SqlProduct2Product());
            } //try
            catch (Exception ex)
            {
                errorMessage = "GetProducts() error, " + ex.Message;
                hasError = true;
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
            }
            return !hasError;
        } //AddProduct()



    }//Class
}//NS
