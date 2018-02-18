using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Android.Support.V4.App;

namespace Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon", MainLauncher = true)]
    public class MainActivity : SupportActivity
    {
        int count = 1;
        private EditText username;
        private EditText password;
        private TextView result;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            username = FindViewById<EditText>(Resource.Id.username_input);
            password = FindViewById<EditText>(Resource.Id.password_input);
            result = FindViewById<TextView>(Resource.Id.result);

            FindViewById<Button>(Resource.Id.getData).Click += async delegate { await GetData(); };
        }

        private async Task GetData()
        {
            try
            {
                var data = await Task.Run(async () => await GetData(username.Text, password.Text));
                result.Text = data;
            }
            catch (Exception ex)
            {
                var dialog = new AlertDialog.Builder(this)
                    .SetMessage(ex.Message)
                    .SetNeutralButton("OK", delegate { })
                    .Show();
            }
        }

        private async Task<string> GetData(string username, string password)
        {
            using (var client = new WebClient())
            {
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.Headers.Add(HttpRequestHeader.Authorization, $"Basic {authHeaderValue}");

                var baseUri = new Uri(Resources.GetString(Resource.String.base_url));
                var uri = new Uri(baseUri, "/api/values");

                var response = await client.DownloadStringTaskAsync(uri);

                return response;
            }
        }
    }
}
