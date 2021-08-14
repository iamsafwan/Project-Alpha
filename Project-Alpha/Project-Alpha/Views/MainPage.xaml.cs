using Project_Alpha.Services;

using System;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Windows.UI.Xaml.Controls;

using System.Diagnostics;
using System.Collections.Generic;
using Windows.System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.IO;
using Windows.Storage.Pickers;

namespace Project_Alpha.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        // ShellPageInfoServices UserInfo = new ShellPageInfoServices(); ; for taking binding approach

        public MainPage()
        {
            this.InitializeComponent();
            FetchUserInfo();
         
            //this.DataContext = UserInfo; for taking binding approach
         
        }

        public async Task FetchUserInfo()
        {

            IReadOnlyList<User> users = await User.FindAllAsync();

            int userNumber = 1;
            foreach (User user in users)
            {
                string displayName = (string)await user.GetPropertyAsync(KnownUserProperties.DisplayName);
                Displayname.Text = displayName;
                // Choosing a generic name if no actual name.
                if (String.IsNullOrEmpty(displayName))
                {

                    displayName = "User #" + userNumber.ToString();
                    userNumber++;
                 
                    string a = (string)await user.GetPropertyAsync(KnownUserProperties.FirstName);
                    string b = (string)await user.GetPropertyAsync(KnownUserProperties.LastName);
                  
                    string name = string.Format("{0} {1}", a, b).ToString();
                 
                    Displayname.Text = name;
                    if (String.IsNullOrEmpty(name))
                    {
                      //alternative to fetch username
                        string[] separatingStrings = { "\\" };

                        string[] words = WindowsIdentity.GetCurrent().Name.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                        Displayname.Text = words[1];

                    }
                }

                IRandomAccessStreamReference streamReference = await user.GetPictureAsync(UserPictureSize.Size1080x1080);
               
                if (streamReference != null)
                {
                    IRandomAccessStream stream = await streamReference.OpenReadAsync();
                    BitmapImage Image = new BitmapImage();
                    Image.SetSource(stream);
                    UserPicture.ProfilePicture = Image;
                    //ss.Source = Image;
                    //Debug.WriteLine(bitmapImage.UriSource.AbsoluteUri);
                    //UserInfo.Location = bitmapImage.UriSource.ToString();
                }

                

            }



        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
