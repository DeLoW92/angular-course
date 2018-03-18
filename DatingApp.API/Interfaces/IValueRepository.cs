using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface IValueRepository
    {
        Task<IEnumerable<Value>> GetAllValues();
        Task<Value> GetValue(string id);

        // add new Value document
        Task AddValue(Value item);

        // remove a single document / Value
        Task<bool> RemoveValue(string id);

        // update just a single document / Value
        Task<bool> UpdateValue(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateValueDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllValues();
       
    }
}