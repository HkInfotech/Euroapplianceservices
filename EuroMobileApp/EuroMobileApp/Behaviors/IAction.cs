using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.Behaviors
{
    public interface IAction
    {
        Task<bool> Execute(object sender, object parameter);
    }
}
