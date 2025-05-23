﻿using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal class BookingRepository(SqlServerDbContext context) : BaseRepository<BookingEntity>(context), IBookingRepository
{
    public override async Task<Result<IEnumerable<BookingEntity>>> GetAllAsync(Func<IQueryable<BookingEntity>, IQueryable<BookingEntity>>? includes = null)
    {
        try
        {
            if (includes is null)
            {
                var entities = await _dbSet
                    .Include(x => x.BookingOwner)
                    .ThenInclude(x => x.Address)
                    .ToListAsync();
                return new Result<IEnumerable<BookingEntity>> { Success = true, Data = entities };
            }

            IQueryable<BookingEntity> query = _dbSet;
            query = includes(query);
            var entitiesIncluding = await query.ToListAsync();
            return new Result<IEnumerable<BookingEntity>> { Success = true, Data = entitiesIncluding };
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<BookingEntity>>() { Success = false, ErrorMessage = $"Failed to get all entities: {e.Message}" };
        }
    }

    public override async Task<Result<BookingEntity>> GetAsync(Expression<Func<BookingEntity, bool>> expression, Func<IQueryable<BookingEntity>, IQueryable<BookingEntity>>? includes = null)
    {
        if (includes is null)
        {
            var entity = await _dbSet
                .Include(x => x.BookingOwner)
                .ThenInclude(x => x.Address)
                .FirstOrDefaultAsync(expression);

            return entity is null
                ? new Result<BookingEntity> { Success = false, ErrorMessage = $"{expression} does not exist." }
                : new Result<BookingEntity> { Success = true, Data = entity };
        }

        IQueryable<BookingEntity> query = _dbSet;
        query = includes(query);
        var entityIncluding = await query.FirstOrDefaultAsync(expression);
        return entityIncluding is null
            ? new Result<BookingEntity> { Success = false, ErrorMessage = $"{expression} does not exist." }
            : new Result<BookingEntity> { Success = true, Data = entityIncluding };
    }
}
