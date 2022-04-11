﻿using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        ICargoCategoryRepository CargoCategories { get; }
        ICargoRepository Cargoes { get; }
        ICustomerRepository Customers { get; }
        IRouteRepository Routes { get; }
        ITransportRepository Transport { get; }
        Task SaveAsync();
    }
}
