using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Webkit;
using Android.Widget;
using Java.Interop;


namespace webview
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        WebView mWebview; //это контейнер для просмотра HTML
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
          
            mWebview = new WebView(this);
            mWebview.Settings.JavaScriptEnabled = true; //это разрешение работа JS скриптов
            mWebview.Settings.DomStorageEnabled = true; //это разрешение на запись в память браузера
            mWebview.Settings.BuiltInZoomControls = true; //это разрешение на масштабирование пальцами щипком
            mWebview.Settings.DisplayZoomControls = false; //это запрет вывода кнопок масштаба
            mWebview.Settings.CacheMode = CacheModes.NoCache; //это отключает либо включает кэширование данных 
            

            mWebview.Settings.AllowFileAccess = true;

            mWebview.Settings.AllowContentAccess = true;
            string s = "file:///android_asset/Content/index.html";
            mWebview.LoadUrl(s); //это загрузка локального файла из папки Asset/Content
            SetContentView(mWebview);
            
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }/*
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }*/
}