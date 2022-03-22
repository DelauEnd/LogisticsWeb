﻿using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Repository.Repositories
{
    public class CargoCategoryRepository : RepositoryBase<CargoCategory>, ICargoCategoryRepository
    {
        public CargoCategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCategory(CargoCategory category)
            => Create(category);

        public void DeleteCategory(CargoCategory category)
            => Delete(category);

        public async Task<IEnumerable<CargoCategory>> GetAllCategoriesAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .ToListAsync();

        public async Task<CargoCategory> GetCategoryByCargoIdAsync(int id, bool trackChanges)
            => await FindByCondition(category =>
                 category.Cargoes.Any(cargo => cargo.Id == id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<CargoCategory> GetCategoryByIdAsync(int id, bool trackChanges)
            => await FindByCondition(category =>
                category.Id == id, trackChanges)
                .SingleOrDefaultAsync();
    }
}
