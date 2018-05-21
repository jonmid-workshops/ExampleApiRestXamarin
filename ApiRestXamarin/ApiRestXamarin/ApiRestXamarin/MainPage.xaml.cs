using ApiRestXamarin.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApiRestXamarin
{
	public partial class MainPage : ContentPage
	{

        private const string Url = "https://jonmidpruebanode.herokuapp.com/users";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<User> _user;

        public MainPage()
		{
			InitializeComponent();
		}

        async public void ListData()
        {
            string content = await client.GetStringAsync(Url);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            _user = new ObservableCollection<User>(users);
            listViewUsers.ItemsSource = _user;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListData();
        }

        public void ClickListData(object sender, EventArgs e)
        {
            ListData();
        }
    }
}
