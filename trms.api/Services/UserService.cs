/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using Mapster;
using MongoDB.Driver;
using trms.api.Entities;

namespace trms.api.Data;

public class UserService : BaseService<User>
{
    public UserService(MongoContext mongoContext) : base(mongoContext.GetCollection<User>("users"), "NIC")
    {
    }
    //call the create api and implement the method
    public override Task<User> Create(User entity)
    {
        entity.Validate();
        return base.Create(entity);
    }

    //call the GetTraveler api and implement the method
   /* public async Task<List<User>> GetTravelers()
    {

        var ret = await Collection
            .Find(x => x.Role == "Traveler")
            .ToListAsync();

        return ret;
    }*/
    //call the GetByNIC api and implement the method
    public async Task<List<User>> GetByNIC(string NIC)
    {

        var ret = await Collection.Find(x => x.NIC == NIC).ToListAsync();
        return ret;
    }
    //call the login api and implement the  method
    public async Task<User> Login(LoginDto loginDto)
    {
        
        var ret = await Collection
            .Find(x=>x.Password == loginDto.Password && x.UserName == loginDto.UserName)
            .FirstOrDefaultAsync();
        if(ret is null)
            throw new AggregateException("Invalid username or password");
        
        return ret;
    }
    //call the update api and implement the method
    public async Task<User> Update(string nic, UserDto userDto)
    {
        var user = await Collection.Find(x => x.NIC == nic).FirstOrDefaultAsync();
        if (user is null)
            throw new AggregateException("User not found"); 
        user.Active = userDto.Active;
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
       // user.Role = userDto.Role;
        
        await Collection.ReplaceOneAsync(x => x.NIC == nic, user);
        return user;
    }
    //call the delete api and implement the method
    public async Task<bool> Delete(string nic)
    {
        var user = await Collection.Find(x => x.NIC == nic).FirstOrDefaultAsync();
        if (user is null)
            throw new AggregateException("User not found"); 
        
        var ret = await Collection.DeleteOneAsync(x => x.NIC == nic);
        return ret.DeletedCount > 0;
    }

   

}