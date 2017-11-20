using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LoadViewDynamicly.Model;
using log4net;

namespace LoadViewDynamicly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static StoreDB storeDB = new StoreDB();
        internal static StoreDB StoreDB { get { return storeDB; } }



        internal static Messenger Messenger
        {
            get { return _messenger; }
        }

        readonly static Messenger _messenger = new Messenger();



    }//App
}
