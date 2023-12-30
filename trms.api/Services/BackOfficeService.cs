/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using System.Net.NetworkInformation;
using trms.api.Data;
using MongoDB.Driver;
using trms.api.Entities;
using trms.api.Common.Models;

namespace trms.api.Services
{
    public class BackOfficeService : BaseService<BackOffice>
    {
        public BackOfficeService(MongoContext mongoContext) : base(mongoContext.GetCollection<BackOffice>("BackOfficers"), "UserId")
        {
        }
        //call the create api and implement the method
        public override Task<BackOffice> Create(BackOffice entity)
        {
            entity.Validate();
            return base.Create(entity);
        }

        //call the GetByUserId  api and implement the method
        public async Task<List<BackOffice>> GetByUserId(string UserId)
        {

            var ret = await Collection.Find(x => x.UserId == UserId).ToListAsync();
            return ret;
        }

        //call the update api and implement the method
        public async Task<BackOffice> Update(string userId, BackOfficeDto backOfficeDto)
        {
            var backOffice = await Collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (backOffice is null)
                throw new AggregateException("User not found");
          
            backOffice.UserName = backOffice.UserName;
            backOffice.Email = backOfficeDto.Email;
            backOffice.Role = backOfficeDto.Role;

            await Collection.ReplaceOneAsync(x => x.UserId == userId, backOffice);
            return backOffice;
        }
        //call the delete api and implement the method
        public async Task<bool> Delete(string userId)
        {
            var backOffice = await Collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (backOffice is null)
                throw new AggregateException("User not found");

            var ret = await Collection.DeleteOneAsync(x => x.UserId == userId);
            return ret.DeletedCount > 0;
        }
    }
}
