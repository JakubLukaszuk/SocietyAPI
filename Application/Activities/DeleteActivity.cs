using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistance;

namespace Application.Activities
{
    public class DeleteActivity
    {
         public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Activity activity = await _context.Activities.FindAsync(request.Id);

                if(activity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new {activity ="Not found"});
                }

                _context.Remove(activity);

                bool isRequestSuccess = await _context.SaveChangesAsync() > 0;

                if(isRequestSuccess)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}