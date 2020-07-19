using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistance;

namespace Application.Activities
{
    public class CreateActivity
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title {get; set;}
            public string Category {get; set;}
            public string Description {get; set;}
            public DateTime Date {get; set;}
            public string City {get; set;}
            public string PlaceOfEvent {get; set;}
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x=> x.Title).NotEmpty();
                RuleFor(x=> x.Category).NotEmpty();
                RuleFor(x=> x.City).NotEmpty();
                RuleFor(x=> x.Description).NotEmpty();
                RuleFor(x=> x.Date).NotEmpty();
                RuleFor(x=>x.PlaceOfEvent).NotEmpty();
            }
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
                Activity activity = new Activity{
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    PlaceOfEvent = request.PlaceOfEvent
                };

                _context.Activities.Add(activity);
                bool isRequestSuccess = await _context.SaveChangesAsync() > 0;

                if(isRequestSuccess){
                    return Unit.Value;
                }

                throw new Exception("Problem saving changes");

            }
        }
    }
}