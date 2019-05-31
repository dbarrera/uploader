using ArcanysExam.Data;
using ArcanysExam.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArcanysExam.Pages.Files
{
    public class List
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public List<Model> Files { get; set; }

            public class Model
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<File, Result.Model>();
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ExamContext _db;
            private readonly IConfigurationProvider _configuration;

            public Handler(ExamContext db, IConfigurationProvider configuration)
            {
                _db = db;
                _configuration = configuration;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var files = await _db.Files
                    .ProjectToListAsync<Result.Model>(_configuration);

                return new Result { Files = files };
            }
        }
    }
}
