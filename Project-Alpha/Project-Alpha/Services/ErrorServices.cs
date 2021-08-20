using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_Alpha.Services
{
    class ErrorServices
    {
       
        public static async void ErrorDialog(Exception ex)
        {
           
          
            await new MessageDialog("We are sorry, but something just went very very wrong, trying to save your work. " +
                                        "\n\nError: " + ex.Message,
                                        "🙈 App Blow Up Sky High").ShowAsync();

            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }


    }
}
