using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class BookingService(IBookingRepository repo) : IBookingService
{
    private readonly IBookingRepository _repo = repo;

}
