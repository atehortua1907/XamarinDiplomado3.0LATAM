using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SALLab10;

namespace Lab10
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int Counter = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            /*var ClickMe = FindViewById<Button>(Resource.Id.ClickMe);
            var ClickCounter = FindViewById<TextView>(Resource.Id.ClickCounter);
            ClickMe.Click += (s, e) =>
             {
                 Counter++;
                 ClickCounter.Text = Resources.GetQuantityString(
                     Resource.Plurals.numberOfClicks, Counter, Counter);

                 var Player = Android.Media.MediaPlayer.Create(this, Resource.Raw.sound);
                 Player.Start();
             };

            var ContenHeader = FindViewById<TextView>(Resource.Id.ContentHeader);
            ContenHeader.Text = GetText(Resource.String.ContentHeader);

            Android.Content.Res.AssetManager Manager = this.Assets;
            using (var Reader = new System.IO.StreamReader(Manager.Open("Contenido.txt")))
            {
                ContenHeader.Text += $"\n\n{Reader.ReadToEnd()}";
            }*/
            var ValidateButton = FindViewById<Button>(Resource.Id.ButtonValidate);
            ValidateButton.Click += (sender, e) =>
            {
                Validar();
            };
        }

        private async void Validar()
        {
            var StudentEmail = FindViewById<EditText>(Resource.Id.StudenEmail);
            var Password = FindViewById<EditText>(Resource.Id.PasswordStudent);
            var ConfirmationText = FindViewById<TextView>(Resource.Id.ConfirmationText);

            ServiceClient ServiceClient = new ServiceClient();
            string myDevice = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail.Text, Password.Text, myDevice);
            ConfirmationText.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
        }
    }
}

