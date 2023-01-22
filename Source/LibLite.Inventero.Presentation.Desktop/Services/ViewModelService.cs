using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibLite.Inventero.Presentation.Desktop.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly IServiceProvider _services;

        public ViewModelService(IServiceProvider services)
        {
            _services = services;
        }

        public T Get<T>() where T : ObservableObject
        {
            return _services.GetRequiredService<T>();
        }
    }
}
