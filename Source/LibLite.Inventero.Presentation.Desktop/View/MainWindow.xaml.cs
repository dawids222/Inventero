﻿using LibLite.Inventero.Presentation.Desktop.ViewModel;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}
