/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace trms.api.Data;
//create the mongodb connection 
public class MongoContext
{
    private readonly DbConfiguration _config;
    private readonly MongoClient _client;
    //add the db Configuration
    public MongoContext(IOptions<DbConfiguration> config)
    {
        _config = config.Value;
        _client = new MongoClient(_config.ConnectionString);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        var database = _client.GetDatabase(_config.DatabaseName);
        return database.GetCollection<T>(name);
    }
    
    
}