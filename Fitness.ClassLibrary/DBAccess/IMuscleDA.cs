using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess
{
    public interface IMuscleDA
    {
        public Task<IEnumerable<int>> Insert(MuscleModel muscle);
        public Task Update(MuscleModel muscle);
        public Task Delete(MuscleModel muscle);
        public IEnumerable<MuscleModel> SelectAll();
    }
}
