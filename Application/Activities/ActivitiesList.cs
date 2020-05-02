using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Activities
{
    public class ActivitiesList
    {
        public class Query: IRequest<List<Activity>>{}

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _datacontext;

            public Handler(DataContext datacontext)
            {
                _datacontext = datacontext;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _datacontext.Activities.ToListAsync(cancellationToken);
                return activities;
            }
        }

    }
}