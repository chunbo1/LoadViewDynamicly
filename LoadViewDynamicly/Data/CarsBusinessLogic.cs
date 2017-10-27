using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CarsBusinessLogic.Data
{
    public class CarsBusinessLogic
    {
        public ObservableCollection<Car> GetCars()
        {
            ObservableCollection<Car> carList = new ObservableCollection<Car>();
            carList.Add(new Car("Dodge", "Ram", 22050.00, 153));
            carList.Add(new Car("Ford", "Explorer", 27175.00, 96));
            carList.Add(new Car("BMW", "Z4", 35600.00, 42));
            carList.Add(new Car("Toyota", "Camry", 20790.99, 131));

            return carList;
        }
    }

    public class Car : INotifyPropertyChanged
    {
        string m_make;
        string m_model;
        double m_baseprice;
        int m_mileage;

        public Car()
        {
        }

        public Car(string make, string model, double baseprice, int mileage)
        {
            this.Make = make;
            this.Model = model;
            this.BasePrice = baseprice;
            this.Mileage = mileage;
        }

        public string Make
        {
            get { return m_make; }
            set
            {
                if (m_make != value)
                {
                    m_make = value;
                    OnPropertyChanged("Make");
                }
            }
        }

        public string Model
        {
            get { return m_model; }
            set
            {
                if (m_model != value)
                {
                    m_model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public double BasePrice
        {
            get { return m_baseprice; }
            set
            {
                if (m_baseprice != value)
                {
                    m_baseprice = value;
                    OnPropertyChanged("BasePrice");
                }
            }
        }

        public int Mileage
        {
            get { return m_mileage; }
            set
            {
                if (m_mileage != value)
                {
                    m_mileage = value;
                    OnPropertyChanged("Mileage");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }

}
