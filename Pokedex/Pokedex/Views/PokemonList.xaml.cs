using Newtonsoft.Json;
using NUnit.Framework;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonList : ContentPage
    {

        public string Url;
        public string Next;
        public string Previous;
        public PokemonList()
        {

            InitializeComponent();
            Url = "https://pokeapi.co/api/v2/pokemon/";
            _ = GetPokemon(Url);

        }

        public async  Task<bool> GetPokemon(string s)
        {

            HttpClient http = new HttpClient();
            var response = await http.GetAsync(s);
            if (response.IsSuccessStatusCode)
            {
                var respString = await response.Content.ReadAsStringAsync();
                var json_d =  JsonConvert.DeserializeObject<PokemonApiModel>(respString);

                List<PokemonListModel> pokelist = new List<PokemonListModel>();

                Next = json_d.Next;
                Previous = json_d.Previous;
                if (string.IsNullOrEmpty(Previous))
                {
                        btnprev.IsVisible = false;
                }
                else {
                    btnprev.IsVisible = true;
                }

                foreach (var poke in json_d.Results)
                {
                    PokemonListModel n = new PokemonListModel();
                    n.Name = poke.Name;
                    n.Url = poke.Url;
                    n.UrlImg = "https://img.pokemondb.net/artwork/" + poke.Name + ".jpg";

                    pokelist.Add(n);
                 }

                ListPoke.ItemsSource = pokelist;

            }
            else
            {
                return false;
        
            }

            return true;
        }

        private async void btnprev_Clicked(object sender, EventArgs e)
        {
            await GetPokemon(Previous);
        }

        private async void btnnext_Clicked(object sender, EventArgs e)
        {
            await GetPokemon(Next);
        }
    }
}