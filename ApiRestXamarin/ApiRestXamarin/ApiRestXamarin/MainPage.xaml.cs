using ApiRestXamarin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListData();
        }

        // ********************************************************
        // ********************* METODOS CRUD *********************
        // ********************************************************

        // Metodo para listar todos los usuarios
        async public void ListData()
        {
            string content = await client.GetStringAsync(Url);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            _user = new ObservableCollection<User>(users);
            listViewUsers.ItemsSource = _user;
        }

        // Metodo para crear un usuario
        async public void CreateUser()
        {
            User user = new User()
            {
                Name = entryNameUser.Text
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(Url, content);

            ListData();
        }

        // Metodo para eliminar un usuario
        async public void DeleteUser(string position)
        {
            HttpResponseMessage response = null;
            response = await client.DeleteAsync(Url+"/"+position);

            ListData();
        }

        // ************************************************************
        // ********************* EVENTOS DE TOUCH *********************
        // ************************************************************

        public void ClickCreateUser(object sender, EventArgs e)
        {
            CreateUser();
        }

        public void ClickDeleteUser(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DeleteUser(mi.CommandParameter.ToString());
        }

        public void ClickUpdateUser(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var item = mi.BindingContext as User;

            User user = new User()
            {
                Id = item.Id,
                Name = item.Name
            };

            showWindowUpdate(user);
        }

        async public void showWindowUpdate(User user)
        {
            await Navigation.PushAsync(new UpdatePage(user));
        }

    }
}
