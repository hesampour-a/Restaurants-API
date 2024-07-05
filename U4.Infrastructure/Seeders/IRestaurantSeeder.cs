using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U4.Infrastructure.Seeders
{
    public interface IRestaurantSeeder
    {
        public Task Seed();
        public Task AssignAdminRole();
    }
}