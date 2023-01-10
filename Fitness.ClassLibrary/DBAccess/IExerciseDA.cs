using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess
{
    public interface IExerciseDA
    {
        public Task<IEnumerable<int>> Insert(ExerciseModel exercise);
        public Task Update(ExerciseModel exercise);
        public Task Delete(ExerciseModel exercise);
        IEnumerable<ExerciseModel> SelectAll();
    }
}