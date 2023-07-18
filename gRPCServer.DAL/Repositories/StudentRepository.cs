using gRPCServer.DAL.Context;
using gRPCServer.DAL.Entities;
using gRPCServer.Domain.Models;
using gRPCServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace gRPCServer.DAL.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly GrpcContext context;

    public StudentRepository(GrpcContext context)
    {
        this.context = context;
    }
    public async IAsyncEnumerable<StudentModel> CreateAsync(IEnumerable<StudentCreate> students)
    {
        foreach (var student in students)
        {
            await context.Students.AddAsync(new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber
            });
        context.SaveChanges();
        }

        foreach (var student in students)
        {
            var studentModel = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber
            };
            yield return studentModel;
        }          
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student is null)
        {
            throw new NullReferenceException();
        }
        context.Students.Remove(student);
        var result = await context.SaveChangesAsync();
        await Task.CompletedTask;
        return result == 1;
    }
    public async IAsyncEnumerable<StudentModel> GetAllAsync()
    {
        foreach (var student in await context.Students.ToListAsync())
        {
            var studentModel = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber
            };

            yield return studentModel;
        }
    }
    public async Task<StudentModel> GetByIdAsync(int id)
    {
        var student = await context.Students.FindAsync(id);

        await Task.CompletedTask;

        if (student is null)
        {
            throw new NullReferenceException();
        }

        return new StudentModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FatherName = student.FatherName,
            StudentNumber = student.StudentNumber,
        };
    }
    public async Task<bool> UpdateAsync(StudentUpdate student)
    {
        var updateStudent = new Student
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FatherName = student.FatherName
        };
        context.Entry(student).Property(t => t.FirstName).IsModified = true;
        context.Entry(student).Property(t => t.LastName).IsModified = true;
        context.Entry(student).Property(t => t.FatherName).IsModified = true;
        return await context.SaveChangesAsync() == 1;
    }
}
