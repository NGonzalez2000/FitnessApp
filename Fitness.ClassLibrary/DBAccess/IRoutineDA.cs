using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess
{
    public interface IRoutineDA
    {
        Task Delete(RoutineModel routine);
        Task<IEnumerable<int>> Insert(RoutineModel routine);
        IEnumerable<RoutineModel> SelectAll();
        Task Update(RoutineModel routine);
        Task InsertExercise(RoutineModel routine, ExerciseModel exercise);
        Task DeleteExercise(RoutineModel routine, ExerciseModel exercise);
        Task<IEnumerable<ExerciseModel>> SelectExercises(RoutineModel routine);
    }
}