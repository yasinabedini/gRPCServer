using gRPCServer.Domain.Models;
namespace gRPCServer.Domain.Repositories;
public interface IStudentRepository
{
    IAsyncEnumerable<StudentModel> CreateAsync(IEnumerable<StudentCreate> students);
    IAsyncEnumerable<StudentModel> GetAllAsync();
    Task<StudentModel> GetByIdAsync(int id);
    Task<bool> UpdateAsync(StudentUpdate student);
    Task<bool> DeleteAsync(int id);
}
