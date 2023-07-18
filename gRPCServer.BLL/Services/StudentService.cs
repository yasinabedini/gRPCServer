using gRPCServer.Domain.Models;
using gRPCServer.Domain.Repositories;
using gRPCServer.Domain.Services;

namespace gRPCServer.BLL.Services;
public class StudentService : IStudentService
{
    private readonly IStudentRepository repository;

    public StudentService(IStudentRepository repository)
    {
        this.repository = repository;
    }
    public async IAsyncEnumerable<StudentModel> CreateAsync(IEnumerable<StudentCreate> students)
    {
        await foreach (var student in repository.CreateAsync(students))
        {
            yield return student;
        }
    }
    public async Task<bool> DeleteAsync(int id)
    {
        await Task.CompletedTask;
        return await repository.DeleteAsync(id);
    }
    public async IAsyncEnumerable<StudentModel> GetAllAsync()
    {
        await foreach (var student in repository.GetAllAsync())
        {
            yield return student;
        }
    }
    public async Task<StudentModel> GetByIdAsync(int id)
    {
        await Task.CompletedTask;
        return await repository.GetByIdAsync(id);
    }
    public async Task<bool> UpdateAsync(StudentUpdate student)
    {
        await Task.CompletedTask;
        return await repository.UpdateAsync(student);
    }
}
