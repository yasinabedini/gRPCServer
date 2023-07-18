using gRPCServer.Domain.Models;
using gRPCServer.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace gRPCServer.TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SrtudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public SrtudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpPost]
        public IAsyncEnumerable<StudentModel> Create(List<StudentCreate> students)
        {
            return studentService.CreateAsync(students);
        }

    }
}