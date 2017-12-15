using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Reports
{
    public abstract class ReportViewModelBase<T> : ViewModelBase
    {
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand GenerateReportCommand { get; private set; }

        protected ReportViewModelBase(IUnityContainer container, T view) : base()
        {
        }

        #region " Overrides "
        

        protected  void OnRegisterCommands()
        {
            LoadCommand = new RelayCommand(OnLoadExecute);
            GenerateReportCommand = new RelayCommand(OnGenerateReportExecute, CanGenerateReportExecute);
        }

        protected   void OnLoad()
        {
            LoadCommand.Execute("");
        }

        #endregion

        protected abstract void OnGenerateReportExecute();
        protected abstract void OnLoadExecute();

        protected virtual bool CanGenerateReportExecute()
        {
            return true;
        }

    }
}
