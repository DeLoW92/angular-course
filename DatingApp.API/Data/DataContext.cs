using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DatingApp.API.Data
{
    //En esta clase defino mi contexto de datos, en el caso de mongo, las colecciones de las que voy a tirar.
    public class DataContext 
    {
         private readonly IMongoDatabase _database = null;

    public DataContext(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            _database = client.GetDatabase(settings.Value.Database);
    }

    public IMongoCollection<Value> Values
    {
        get
        {
            return _database.GetCollection<Value>("Value");
        }
    }

    public IMongoCollection<User> Users
    {
        get
        {
            return _database.GetCollection<User>("Users");
        }
    }
    }
}