using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dz2Async
{
    public class Program
    { 
        
        struct InfoContent
        {
            public string type;
            public string name;
            public int duration;
            public string genre;
            public string language;
            public double score;
            public int mainRoleActorId;
            public int actorId;
            public int countryId;
        }
        public class CountryInfo
    {
        public string name { get; set; }
        public string code { get; set; }
        public string timezone { get; set; }
    }

    public class Externals
    {
        public int? tvrage { get; set; }
        public int? thetvdb { get; set; }
        public string imdb { get; set; }
    }

    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Previousepisode previousepisode { get; set; }
    }

    public class Network
    {
        public int id { get; set; }
        public string name { get; set; }
        public CountryInfo country { get; set; }
        public string officialSite { get; set; }
    }

    public class Previousepisode
    {
        public string href { get; set; }
    }

    public class Rating
    {
        public double? average { get; set; }
    }

    public class Root
    {
        public double score { get; set; }
        public Show show { get; set; }
    }

    public class Schedule
    {
        public string time { get; set; }
        public List<string> days { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Show
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public List<string> genres { get; set; }
        public string status { get; set; }
        public int? runtime { get; set; }
        public int? averageRuntime { get; set; }
        public string premiered { get; set; }
        public string ended { get; set; }
        public string officialSite { get; set; }
        public Schedule schedule { get; set; }
        public Rating rating { get; set; }
        public int weight { get; set; }
        public Network network { get; set; }
        public WebChannel webChannel { get; set; }
        public object dvdCountry { get; set; }
        public Externals externals { get; set; }
        public Image image { get; set; }
        public string summary { get; set; }
        public int updated { get; set; }
        public Links _links { get; set; }
    }

    public class WebChannel
    {
        public int id { get; set; }
        public string name { get; set; }
        public CountryInfo country { get; set; }
        public string officialSite { get; set; }
    }
    
    static async Task SearchEngFilms(Context context)
    {

        IQueryable<Content> info = from pr in context.Contents where pr.Language == "English" select pr;
        List<Content> list = info.ToList();
        Console.WriteLine($"Search by genre");
        foreach (Content informations in info)
            Console.WriteLine($"{informations.Id} {informations.Name} {informations.Description} " +
                              $"{informations.Language} {informations.Score}");
    }
    
    static async Task SearchMainActor(Context context, int actId)
    {
        int idType = context.Actors
            .Where(c => c.Id == actId)
            .Select(c => c.Id)
            .FirstOrDefault();

        Console.WriteLine($"Id for search {idType}");

        IQueryable<Content> information = from pr in context.Contents where pr.MainRoleActorId == idType select pr;
        List<Content> list = information.ToList();
        Console.WriteLine($"Search by main actor");
        foreach (Content informations in information)
            Console.WriteLine($"{informations.Id} {informations.Name} {informations.Description} " +
                              $"{informations.ActorId} {informations.Score}");
    }
    
    static async Task SearchByName(Context context, string name)
    {

        IQueryable<Content> info = from pr in context.Contents where pr.Name == name select pr;
        List<Content> list = info.ToList();
        Console.WriteLine($"Search by name");
        foreach (Content informations in info)
            Console.WriteLine($"{informations.Id} {informations.Name} {informations.Description} " +
                              $"{informations.Score}");
    }

        static async Task Main (string[] args)
        {
            Context context = new Context();
            await context.Database.EnsureCreatedAsync();
            InfoContent infoContent;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                HttpResponseMessage pointsResponse = await client.GetAsync("https://api.tvmaze.com/search/shows?q=ted" );
                Console.WriteLine(pointsResponse);
            
                if (pointsResponse.IsSuccessStatusCode)
                {
                    List<Root> roots = await pointsResponse.Content.ReadFromJsonAsync<List<Root>>();
                    foreach (Root root in roots)
                    {
                        Console.WriteLine(root.show.name);
                        Content newFilm = new Content()
                        {
                            Name = root.show.name,
                            Description = root.show.summary,
                            Language = root.show.language,
                            Score = root.score
                        };
                        context.Contents.Add(newFilm);
                        await context.SaveChangesAsync();
                        Console.WriteLine("Film was added succesfully");
                    }

                    // ENG Films
                    await SearchEngFilms(context);
                    
                    // Name Search
                    Console.WriteLine($"Enter name for search:");
                    infoContent.name = Console.ReadLine();
                    await SearchByName(context, infoContent.name);
                    
                    // Search film info by main actor id
                    Console.WriteLine("Enter main actor id:");
                    string id_actor_main = Console.ReadLine();
                    infoContent.mainRoleActorId = Convert.ToInt32(id_actor_main);
                    await SearchMainActor(context, infoContent.mainRoleActorId);
                }
            }

        }
    }
}
