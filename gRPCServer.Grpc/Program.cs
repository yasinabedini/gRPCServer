using gRPCServer.DAL.Context;
using gRPCServer.DAL.Repositories;
using gRPCServer.Domain.Repositories;
using gRPCServer.Domain.Services;
using gRPCServer.Grpc.Services;
using Microsoft.EntityFrameworkCore;

#region builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService,gRPCServer.BLL.Services.StudentService>();
builder.Services.AddDbContext<GrpcContext>(t => t.UseSqlServer(builder.Configuration.GetConnectionString("defult")));
builder.Services.AddGrpc();
#endregion

#region app
var app = builder.Build();
app.MapGrpcService<StudentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
#endregion