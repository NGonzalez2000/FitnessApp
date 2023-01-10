using Fitness.ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess;

public class MuscleDA : SQLDataAccess, IMuscleDA
{
    public Task<IEnumerable<int>> Insert(MuscleModel muscle)
        => LoadData_Async<int,dynamic>("INSERT INTO Muscles(Name) values(@Name); SELECT last_insert_rowid()", new { muscle.Name });
    public Task Update(MuscleModel muscle)
        => SaveData_Async("UPDATE Muscles SET [Name] = @Name WHERE Id = @Id", new { muscle.Id, muscle.Name });
    public Task Delete(MuscleModel muscle)
        => SaveData_Async("DELETE FROM Muscles WHERE Id = @Id", new { muscle.Id });
    public IEnumerable<MuscleModel> SelectAll()
        => LoadData<MuscleModel, dynamic>("SELECT * FROM Muscles ORDER BY name", new {});
}
