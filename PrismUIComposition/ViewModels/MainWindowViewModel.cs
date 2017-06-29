﻿using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;

namespace PrismUIComposition.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand ExitCommand { get; private set; }

        public MainWindowViewModel()
        {
            ExitCommand = new DelegateCommand<object>(ExecuteExit, CanExecuteExit);
        }

        private void ExecuteExit(object obj)
        {
        }

        private bool CanExecuteExit(object arg)
        {
            return true;
        }
    }
}
