using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatingApp.API.Data
{

    public class ValueRepository : IValueRepository
    {
        private readonly DataContext _context = null;

        public ValueRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public ValueRepository()
        {
        }

        public async Task<IEnumerable<Value>> GetAllValues()
        {
            try
            {
                return await _context.Values.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Value> GetValue(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Values
                                .Find(Value => Value.Id == id
                                        || Value.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddValue(Value item)
        {
            try
            {
                await _context.Values.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveValue(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Values.DeleteOneAsync(
                        Builders<Value>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateValue(string id, string body)
        {
            var filter = Builders<Value>.Filter.Eq(s => s.Id, id);
            var update = Builders<Value>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult
                    = await _context.Values.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateValue(string id, Value item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Values
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllValues()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Values.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<bool> UpdateValueDocument(string id, string body)
        {
            throw new NotImplementedException();
        }

       
    }
}
