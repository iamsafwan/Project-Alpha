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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Automation.Peers;
using Windows.ApplicationModel.DataTransfer;

namespace Project_Alpha.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        DataPackage dataPackage = new DataPackage();
        Random RRandom = new Random();
        // ShellPageInfoServices UserInfo = new ShellPageInfoServices(); ; for taking binding approach
        public enum NotifyType
        {
            StatusMessage,
            ErrorMessage
        };
        public MainPage()
        {
            this.InitializeComponent();
            FetchUserInfo();
         
            //this.DataContext = UserInfo; for taking binding approach
         
        }

        public async Task FetchUserInfo()
        {
            try {
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
            catch (Exception ex)
            {
                ErrorServices.ErrorDialog(ex);
            }
        }

        //public void NotifyUser(string strMessage, NotifyType type)
        //{
        //    // If called from the UI thread, then update immediately.
        //    // Otherwise, schedule a task on the UI thread to perform the update.
        //    if (Dispatcher.HasThreadAccess)
        //    {
        //        UpdateStatus(strMessage, type);
        //    }
        //    else
        //    {
        //        var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateStatus(strMessage, type));
        //    }
        //}

        //private void UpdateStatus(string strMessage, NotifyType type)
        //{
        //    switch (type)
        //    {
        //        case NotifyType.StatusMessage:
        //            StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Green);
        //            break;
        //        case NotifyType.ErrorMessage:
        //            StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        //            break;
        //    }

        //    StatusBlock.Text = strMessage;

        //    // Collapse the StatusBlock if it has no text to conserve real estate.
        //    StatusBorder.Visibility = (StatusBlock.Text != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
        //    if (StatusBlock.Text != String.Empty)
        //    {
        //        StatusBorder.Visibility = Visibility.Visible;
        //        StatusPanel.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        StatusBorder.Visibility = Visibility.Collapsed;
        //        StatusPanel.Visibility = Visibility.Collapsed;
        //    }

        //    // Raise an event if necessary to enable a screen reader to announce the status update.
        //    var peer = FrameworkElementAutomationPeer.FromElement(StatusBlock);
        //    if (peer != null)
        //    {
        //        peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
        //    }
        //}


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

      

        private void BladeSecondGenerate_Click(object sender, RoutedEventArgs e)
        {
            BladeSecondGenerateButton.IsChecked = false;
            BladeSecondGenerateButton.IsChecked = true;
            try
            {
                int number;

                bool isParsable = int.TryParse(BladeSecondPasswordLength.Text, out number);

                PasswordTB.Text = isParsable && number != 0 ? PasswordServices.Generator(number) : PasswordServices.Generator(11);
                //Console.WriteLine("Could not be parsed.");

                // add for BladeSecondPassTypeCheck based generation aka easy to remember password

                PasswordTB.Visibility = Visibility.Visible;
                CopyButton.Visibility = Visibility.Visible;
              
                PasswordTB.MaxWidth = SecondBlade.Width - CopyButton.Width;
            }
            catch (Exception ex)
            {
                ErrorServices.ErrorDialog(ex);
                CopyButton.Visibility = Visibility.Collapsed;
                PasswordTB.Visibility = Visibility.Visible;
                PasswordTB.Text = "Error";
            }
            //add slider or value picker to input value to generator
        }
     


        private void BladeThreeSearch_Click(object sender, RoutedEventArgs e)
        {
            BladeThirdSearchButton.IsChecked = false;
            BladeThirdSearchButton.IsChecked = true;

        }

        private void BladeFourSave_Click(object sender, RoutedEventArgs e)
        {

            BladeFourSaveButton.IsChecked = false;
            BladeFourSaveButton.IsChecked = true;
           

        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.SetText(PasswordTB.Text);
            Clipboard.SetContent(dataPackage);

        }

        private void BladeSecondSimplePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            BladeSecondSimplePasswordButton.IsChecked = false;
            BladeSecondSimplePasswordButton.IsChecked = true;

            BladeSecondAdvancePasswordButton.IsChecked = false;
            if (BladeSecondSimplePasswordButton.IsChecked != false)
            {
                BladeSecondPassLengthCheck.Visibility = Visibility.Collapsed;
               
                BladeSecondPasswordLength.Visibility = Visibility.Collapsed;
                BladeSecondPassLengthCheck.IsEnabled = false;
                BladeSecondPasswordLength.IsEnabled = false;
                //BladeSecondPassTypeCheck.Visibility = Visibility.Collapsed;   Implement later
                //BladeSecondPassTypeCheck.IsEnabled = false;
            }

        }

        private void BladeSecondAdvancePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            BladeSecondAdvancePasswordButton.IsChecked = false;
            BladeSecondAdvancePasswordButton.IsChecked = true;

            BladeSecondSimplePasswordButton.IsChecked = false;

            if (BladeSecondAdvancePasswordButton.IsChecked != false)
            {
                BladeSecondPasswordLength.Visibility = Visibility.Visible;
                BladeSecondPassLengthCheck.Visibility = Visibility.Visible;
                BladeSecondPassLengthCheck.IsEnabled = true;
                //BladeSecondPassTypeCheck.Visibility = Visibility.Visible;

            }

            //add logic for BladeSecondPassTypeCheck aka easy to remember password

        }

        private void BladeSecondPassLengthCheck_Click(object sender, RoutedEventArgs e)
        {
            
            if ((bool)BladeSecondPassLengthCheck.IsChecked)
            {
                BladeSecondPasswordLength.IsEnabled = true;
              

            }
            if (BladeSecondPassLengthCheck.IsChecked == false)
            {
                BladeSecondPasswordLength.IsEnabled = false;
                
            }
        }
    }
}
