using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistance;

namespace Application.Activities
{
    public class EditActivity
    {
         public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title {get; set;}
            public string Category {get; set;}
            public string Description {get; set;}
            public DateTime? Date {get; set;}
            public string City {get; set;}
            public string PlaceOfEvent {get; set;}
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
                    throw new Exception("Could not fint activity");
                }

                activity.Title = request.Title ?? activity.Title;
                activity.Description = request.Description ?? activity.Description;
                activity.Category = request.Category ?? activity.Category;
                activity.Date = request.Date ?? activity.Date;
                activity.City = request.City ?? activity.City;
                activity.PlaceOfEvent = request.PlaceOfEvent ?? activity.PlaceOfEvent;

                bool isRequestSuccess = await _context.SaveChangesAsync() > 0;

                if(isRequestSuccess){
                    return Unit.Value;
                }

                throw new Exception("Problem saving changes");

            }
        }
    }
}