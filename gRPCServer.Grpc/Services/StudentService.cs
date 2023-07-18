using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPCServer.Domain.Models;
using gRPCServer.Domain.Services;
using gRPCServer.gRPC.Protos;
using static gRPCServer.gRPC.Protos.StudentService;

namespace gRPCServer.Grpc.Services;

public class StudentService : StudentServiceBase
{
    private readonly IStudentService studentService;

    public StudentService(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    public override async Task Create(IAsyncStreamReader<StudentCreateRequest> requestStream, IServerStreamWriter<StudentReplay> responseStream, ServerCallContext context)
    {
        List<StudentCreate> newStudents = new List<StudentCreate>();
        await foreach (var request in requestStream.ReadAllAsync())
        {
            newStudents.Add(new StudentCreate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FatherName = request.FatherName,
                StudentNumber = request.StudentNumber
            });
            await responseStream.WriteAsync(new StudentReplay
            {
                FirstName = request.FirstName,
                FatherName = request.LastName,
                LastName = request.LastName,
                StudentNumber = request.StudentNumber
            });
        }
        studentService.CreateAsync(newStudents);
        await Task.CompletedTask;
    }

    public override async Task GetAll(Empty request, IServerStreamWriter<StudentReplay> responseStream, ServerCallContext context)
    {
        await foreach (var student in studentService.GetAllAsync())
        {
            await responseStream.WriteAsync(new StudentReplay
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber
            });
        }
        await Task.CompletedTask;
    }

    public override async Task<StudentReplay> GetById(GetStudentByIdRequest request, ServerCallContext context)
    {
        var result = await studentService.GetByIdAsync(request.Id);
        var student = new StudentReplay
        {
            FirstName = result.FirstName,
            LastName = result.LastName,
            FatherName = result.FatherName,
            StudentNumber = result.StudentNumber
        };
        return student;
    }

    public override async Task<StuentSuccessReplay> Update(StudentUpdateRequest request, ServerCallContext context)
    {
        var result = await studentService.UpdateAsync(new StudentUpdate
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            FatherName = request.FatherName,
        });
        await Task.CompletedTask;
        return new StuentSuccessReplay
        {
            Success = result
        };
    }

    public override async Task<StuentSuccessReplay> Delete(GetStudentByIdRequest request, ServerCallContext context)
    {
        var result = await studentService.DeleteAsync(request.Id);
        await Task.CompletedTask;
        return new StuentSuccessReplay
        {
            Success = result
        };
    }
}
