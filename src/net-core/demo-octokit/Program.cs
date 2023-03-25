using Microsoft.Extensions.Configuration;
using Octokit;
using System.Reflection;

// Build a config object, using env vars and JSON providers.
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .Build();

var client = new GitHubClient(new ProductHeaderValue(config["demo-app"]));
client.Credentials = new Credentials(config["github-token"]);

var repos = await client.Repository.GetAllForOrg(config["organization"]);

repos.ToList().ForEach(repo =>
{
    Console.WriteLine(repo.FullName);
});