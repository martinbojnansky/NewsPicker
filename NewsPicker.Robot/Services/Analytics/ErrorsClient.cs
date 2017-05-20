using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Database.Controllers;

namespace NewsPicker.Robot.Services.Analytics
{
    public class ErrorsClient
    {
        private ErrorsController _errors = new ErrorsController();

        public void Log(string message, Action function)
        {
            try
            {
                function();
            }
            catch (Exception ex)
            {
                _errors.Add($"{message}\n{ex.ToString()}");
            }
        }

        public async Task LogAsync(string message, Func<Task> function)
        {
            try
            {
                await function();
            }
            catch (Exception ex)
            {
                _errors.Add($"{message}\n{ex.ToString()}");
            }
        }
    }
}
