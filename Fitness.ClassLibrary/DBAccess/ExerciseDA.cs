using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess;

public class ExerciseDA : SQLDataAccess, IExerciseDA
{

    public Task<IEnumerable<int>> Insert(ExerciseModel exercise)
        => LoadData_Async<int,dynamic>("INSERT INTO Exercise(Name, Link, Muscle) values (@Name, @Link, @MuscleId); SELECT last_insert_rowid()", new
        {
            exercise.Name,
            exercise.Link,
            MuscleId = exercise.Muscle!.Id
        });
    public Task Update(ExerciseModel exercise)
     => SaveData_Async("UPDATE Exercise SET [Name] = @Name, Link = @Link, Muscle = @MuscleId WHERE Id = @Id", new
     {
         exercise.Id,
         exercise.Name,
         exercise.Link,
         MuscleId = exercise.Muscle!.Id
     });
    public Task Delete(ExerciseModel exercise)
        => SaveData_Async("DELETE FROM Exercise WHERE Id = @Id", new { exercise.Id });
    public IEnumerable<ExerciseModel> SelectAll()
        => LoadData<ExerciseModel, dynamic>("SELECT * FROM v_Exercise", new { });

}
