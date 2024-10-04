using System;
using System.Text.Json;

public class ApiCaller
{
    public async Task<string> FetchDataAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

public class Result
{
    public string name { get; set; }
}

public class Planet
{
    public IEnumerable<Result> results { get; set; }
}

public class Person
{
    public IEnumerable<Result> results { get; set; }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        ApiCaller caller = new ApiCaller();

        // Fetch data from multiple APIs asynchronously
        Task<string> task1 = caller.FetchDataAsync("https://swapi.dev/api/planets/");
        Task<string> task2 = caller.FetchDataAsync("https://swapi.dev/api/people/");

        // Process the results when they become available
        string data1 = await task1;
        string data2 = await task2;

        //Console.WriteLine("Planet Name: " + data1);
        //Console.WriteLine("Person Name: " + data2);

        // Deserialize JSON into a Planet object
        var planetData = JsonSerializer.Deserialize<Planet>(data1);

        // Display the names of the planets
        Console.WriteLine("Names of the Planets:");
        if (planetData?.results != null)
        {
            foreach (var planets in planetData.results)
            {
                Console.WriteLine(planets.name);
            }
        }



        var personData = JsonSerializer.Deserialize<Person>(data2);

        // Display the names of the people
        Console.WriteLine("\n\nNames of the People:");
        if (personData?.results != null)
        {
            foreach (var  people in personData.results)
            {
                Console.WriteLine(people.name);
            }
        }
    }
}