using System;
using System.Collections;
using System.Collections.Generic;
using superVise.Entities;

namespace superVise.Services.Interfaces
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        Location GetById(int id);
        Location Create(Location location, int userId);
        //void Update(Location location);
        void Delete(int id);
        IEnumerable<Location> GetRangedLocationsForUser(int userId, DateTime from, DateTime to);
    }
}