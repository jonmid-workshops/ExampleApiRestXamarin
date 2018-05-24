using ApiRestXamarin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiRestXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePage : ContentPage
	{
        private const string Url = "https://jonmidpruebanode.herokuapp.com/users";
        private readonly HttpClient client = new HttpClient();
        private User user;

        public UpdatePage (User user)
		{
			InitializeComponent ();
            this.user = user;
            BindingContext = user;
            //Debug.WriteLine("DATA: "+ user.Name);
		}

        // Metodo para actualizar un usuario
        async public void UpdateUser(object sender, EventArgs e)
        {
            user.Name = entryNameUserUpdate.Text;

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(Url + "/" + user.Id, content);

            await Navigation.PushAsync(new MainPage());
        }
    }
}