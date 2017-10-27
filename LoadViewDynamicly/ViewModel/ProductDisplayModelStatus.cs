using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Media;

namespace LoadViewDynamicly.ViewModel
{
    //ClassStudent Error detection, error display and status msg
    //Note, a Delete may be performed without checking any Productt fields
    public class ProductDisplayModelStatus : ViewModelBase
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.MDH2ConnectionString);

        //Error status msg and field Brushes to indicate product field errors
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value;
                RaisePropertyChanged("Status"); }
        }
        private static SolidColorBrush errorBrush = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush okBrush = new SolidColorBrush(Colors.Black);

        private SolidColorBrush modelNumberBrush = okBrush;
        public SolidColorBrush ModelNumberBrush
        {
            get { return modelNumberBrush; }
            set { modelNumberBrush = value;
                RaisePropertyChanged("ModelNumberBrush"); }
        }

        private SolidColorBrush modelNameBrush = okBrush;
        public SolidColorBrush ModelNameBrush
        {
            get { return modelNameBrush; }
            set { modelNameBrush = value;
                RaisePropertyChanged("ModelNameBrush"); }
        }

        private SolidColorBrush categoryNameBrush = okBrush;
        public SolidColorBrush CategoryNameBrush
        {
            get { return categoryNameBrush; }
            set { categoryNameBrush = value;
                RaisePropertyChanged("CategoryNameBrush"); }
        }

        private SolidColorBrush unitCostBrush = okBrush;
        public SolidColorBrush UnitCostBrush
        {
            get { return unitCostBrush; }
            set { unitCostBrush = value;
                RaisePropertyChanged("UnitCostBrush"); }
        }


        //set error field brushes to OKBrush and status msg to OK
        public void NoError()
        {
            ModelNumberBrush = ModelNameBrush = CategoryNameBrush = UnitCostBrush = okBrush;
            Status = "OK";
        } //NoError()


        public ProductDisplayModelStatus()
        {
            NoError();
        } //ctor


        //verify the ClassStudent's unitcost is a decimal number > 0
        private bool ChkUnitCost(string costString)
        {
            if (String.IsNullOrEmpty(costString))
                return false;
            else
            {
                decimal unitCost;
                try
                {
                    unitCost = Decimal.Parse(costString);
                }
                catch
                {
                    return false;
                }
                if (unitCost < 0)
                    return false;
                else return true;
            }
        } //ChkUnitCost()


        //check all product fields for validity
        public bool ChkClassStudentForAdd(ClassStudent p)
        {            
            int errCnt = 0;
            if (String.IsNullOrEmpty(p.ClassName))
            { errCnt++; ModelNumberBrush = errorBrush; }
            else ModelNumberBrush = okBrush;
            if (String.IsNullOrEmpty(p.StudentName))
            { errCnt++; ModelNameBrush = errorBrush; }
            else ModelNameBrush = okBrush;
            if (String.IsNullOrEmpty(p.Grouping))
            { errCnt++; ModelNameBrush = errorBrush; }
            else ModelNameBrush = okBrush;
            
            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "ADD, missing or invalid fields."; return false; }
        } //ChkProductForAdd()


        //check all product fields for validity
        public bool ChkProductForUpdate(ClassStudent p)
        {
            int errCnt = 0;
            if (String.IsNullOrEmpty(p.ClassName))
            { errCnt++; ModelNumberBrush = errorBrush; }
            else ModelNumberBrush = okBrush;
            if (String.IsNullOrEmpty(p.StudentName))
            { errCnt++; ModelNameBrush = errorBrush; }
            else ModelNameBrush = okBrush;

            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "Update, missing or invalid fields."; return false; }
        } //ChkProductForUpdate()

    } //class ProductDisplayModelStatus
}
