using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SALLab11;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        Validate ValidationData;
        int Counter = 0;

        protected override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            FindViewById<Button>(Resource.Id.StartActivity).Click += (sender, e) =>
             {
                 var ActivityIntent =
                 new Android.Content.Intent(this, typeof(SecondActivity));
                 StartActivity(ActivityIntent);
             };

            //Utilizar FragmentManager para recuperar el Fragmento
            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                //No ha sido almacenado, agregar el fragmento a la Activity
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }

            if (bundle != null)
            {
                Counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }

            var ClickCounter =
                FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text =
                Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
            ClickCounter.Text += $"\n{Data.ToString()}";
            ClickCounter.Click += (sender, e) =>
            {
                Counter++;
                ClickCounter.Text =
                    Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
                //Modificar con cualquier valor solo para verificar la persistencia
                Data.Real++;
                Data.Imaginary++;
                //Mostrar el valor de los miembros
                ClickCounter.Text += $"\n{Data.ToString()}";
            };

            //Utilizar FragmentManager para recuperar el Fragmento
            ValidationData = (Validate)this.FragmentManager.FindFragmentByTag("ValidationData");
            if (ValidationData == null)
            {
                //No ha sido almacenado, agregar el fragmento a la Activity
                ValidationData = new Validate();
                var FragmentTransactionValidation = this.FragmentManager.BeginTransaction();
                FragmentTransactionValidation.Add(ValidationData, "ValidationData");
                FragmentTransactionValidation.Commit();
            }
            Validar();
        }

        private async void Validar()
        {
            string StudentEmail = "atehortua1907@hotmail.com";
            string Password = "51pegasib";
            var ValidationTex = FindViewById<TextView>(Resource.Id.ValidationText);
            ServiceClient ServiceClient = new ServiceClient();
            string myDevice = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            //Modificar con cualquier valor solo para verificar la persistencia
            ValidationData.Status = Result.Status.ToString();
            ValidationData.FullName = Result.Fullname;
            ValidationData.Token = Result.Token;
            //Mostrar el valor de los miembros
            ValidationTex.Text = ValidationData.ToString();
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", Counter);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }
    }
}

