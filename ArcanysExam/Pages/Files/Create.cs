using ArcanysExam.Data;
using ArcanysExam.Models;
using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArcanysExam.Pages.Files
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public byte[] Content { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Command, File>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
                .ForMember(dest => dest.Blob, opt => opt.MapFrom(src => src.Content));
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ExamContext _db;
            private readonly IMapper _mapper;

            public Handler(ExamContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                // Check for duplicate filename
                if (DoesFileAlreadyExist(request.Name))
                {
                    throw new FileExistException(request.Name, "File already exist.");
                }
                if (DoesFileSizeLimitExceeded(request.Content))
                {
                    throw new FileLimitException(request.Name, "File exceeds to 2MB.");
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var file = _mapper.Map<Command, File>(request);

                _db.Files.Add(file);

                await _db.SaveChangesAsync(cancellationToken);

                return file.Id;
            }

            private bool DoesFileAlreadyExist(string name)
            {
                return _db.Files.Any(f => f.Name == name.Trim());
            }

            private bool DoesFileSizeLimitExceeded(byte[] bytes)
            {
                const float b = 1024.0F;

                var inMb = (bytes.Length / b) / b;

                return inMb > 2;
            }
        }
    }
}
