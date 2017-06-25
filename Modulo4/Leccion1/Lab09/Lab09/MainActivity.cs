using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SALLab09;

namespace Lab09
{
    [Activity(Label = "Lab09", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Validate();
        }

        private async void Validate()
        {
            string StudentEmail = "atehortua1907@hotmail.com";
            string Password = "51pegasib";
            var UserName = FindViewById<TextView>(Resource.Id.UserNameValue);
            var Status = FindViewById<TextView>(Resource.Id.StatusValue);
            var Token = FindViewById<TextView>(Resource.Id.TokenValue);
            ServiceClient ServiceClient = new ServiceClient();
            string myDevice = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            UserName.Text = Result.Fullname;
            Status.Text = Result.Status.ToString();
            Token.Text = Result.Token;             
        }
    }
}

