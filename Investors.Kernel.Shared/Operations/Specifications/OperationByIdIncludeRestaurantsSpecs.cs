﻿using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationByIdIncludeRestaurantsSpecs : Specification<Operation, OperationByIdIncludeRestaurantsResponse>,
        ISingleResultSpecification<Operation, OperationByIdIncludeRestaurantsResponse>
    {
        public OperationByIdIncludeRestaurantsSpecs(int operationId, bool isNoTracking = false)
        {

            Query.Include(x => x.Restaurants.Where(c => c.Status == Status.Active));

            Query.Where(x => x.Id == operationId && x.Status == Status.Active);

            Query.Take(1);

            Query.Select(operation => new OperationByIdIncludeRestaurantsResponse(
                operation.Id,
                operation.Description,
                operation.UrlLogo,
                operation.Email,
                operation.Restaurants.Where(c => c.Status == Status.Active)
                    .Select(x => new RestaurantResponse(
                        x.Id,
                        x.Description,
                        x.UrlLogo,
                        x.Email
                        )).ToList()
                ));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }


            Query.AsSplitQuery();
        }
    }
}