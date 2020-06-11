using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var button = FindViewById<Button>(Resource.Id.button1);
            var textEdit = FindViewById<EditText>(Resource.Id.editText1);

            button.Click += (obj, e) =>
            {
                if (textEdit.Text.Length > 0)
                {
                    var greetings = string.Format("Hello, {0}", textEdit.Text);
                    new AlertDialog.Builder(this)
                    .SetTitle("Greetings")
                    .SetMessage(greetings)
                    .Show();
                }
                else
                {
                    new AlertDialog.Builder(this)
                    .SetTitle("Warning")
                    .SetMessage("You did not provide a username!")
                    .Show();
                }
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}