/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using MongoDB.Driver;
using System.Net.NetworkInformation;
using trms.api.Entities;

namespace trms.api.Data;
//to create common api methods
public class BaseService<T> where T : class
{
    protected readonly IMongoCollection<T> Collection;
    private readonly string _key;

    public BaseService(IMongoCollection<T>  collection, string key)
    {
        Collection = collection;
        _key = key;
    }

    public async Task<List<T>> GetAll()
    {
        return await Collection.Find(x => true).ToListAsync();
    }
    
  /*  public virtual async Task<T> GetById(string key)
    {
        return await Collection.Find(x => x.GetType().GetProperty(_key).GetValue(x).ToString() == key).FirstOrDefaultAsync();
    }
    */
    public virtual async Task<T> Create(T entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }
    
    
    
    
    
}