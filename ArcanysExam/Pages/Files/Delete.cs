using ArcanysExam.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArcanysExam.Pages.Files
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ExamContext _db;

            public Handler(ExamContext db) => _db = db;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var file = await _db.Files.FindAsync(request.Id);

                _db.Files.Remove(file);

                return default;
            }
        }
    }
}
