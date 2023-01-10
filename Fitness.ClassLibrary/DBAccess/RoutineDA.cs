using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess;

public class RoutineDA : SQLDataAccess, IRoutineDA
{
    public Task<IEnumerable<int>> Insert(RoutineModel routine)
    {
        return LoadData_Async<int, dynamic>("INSERT INTO Routines(Name,DurationMin,DurationSec,RestMin,RestSec,Laps) " +
                "values(@Name,@DurationMin,@DurationSec,@RestMin,@RestSec,@Laps); " +
                "SELECT last_insert_rowid()", new
                {
                    routine.Name,
                    DurationMin = routine.Duration.Minutes,
                    DurationSec = routine.Duration.Seconds,
                    RestMin = routine.Rest.Minutes,
                    RestSec = routine.Rest.Seconds,
                    routine.Laps
                });
    }

    public Task Update(RoutineModel routine)
        => SaveData_Async("UPDATE Routines set Name = @Name, DurationMin = @DurationMin , DurationSec = @DurationSec , RestMin = @RestMin, RestSec = @RestSec, Laps = @Laps WHERE Id = @Id",
            new 
            {
                routine.Id,
                routine.Name,
                DurationMin = routine.Duration.Minutes,
                DurationSec = routine.Duration.Seconds,
                RestMin = routine.Rest.Minutes,
                RestSec = routine.Rest.Seconds,
                routine.Laps
            });
    public Task Delete(RoutineModel routine)
        => SaveData_Async("DELETE FROM Routines WHERE Id = @Id", new { routine.Id });

    public IEnumerable<RoutineModel> SelectAll()
        => LoadData<RoutineModel, dynamic>("SELECT Id, Name, DurationMin, DurationSec, RestMin , RestSec, Laps FROM Routines", new { });

    public Task InsertExercise(RoutineModel routine, ExerciseModel exercise)
        => SaveData_Async("INSERT INTO RoutineExercises (RoutineId,ExerciseId) values(@RoutineId,@ExerciseId)", new {RoutineId = routine.Id, ExerciseId = exercise.Id} );
    public Task DeleteExercise(RoutineModel routine, ExerciseModel exercise)
        => SaveData_Async("DELETE FROM RoutineExercises WHERE RoutineId = @RoutineId and ExerciseId = @ExerciseId", new { RoutineId = routine.Id, ExerciseId = exercise.Id });

    public Task<IEnumerable<ExerciseModel>> SelectExercises(RoutineModel routine)
    => LoadData_Async<ExerciseModel, dynamic>("select * from v_RoutineExercises where RoutineId = @RoutineId", new { RoutineId = routine.Id });
}
