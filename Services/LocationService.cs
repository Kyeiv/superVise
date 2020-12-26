using System;
using System.Collections.Generic;
using System.Linq;
using superVise.Entities;
using superVise.Entities.Context;
using superVise.Helpers.Exceptions;
using superVise.Services.Interfaces;

namespace superVise.Services
{
    public class LocationService : ILocationService
    {
        private DataContext _context;

        public LocationService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Locations;
        }

        public Location GetById(int id)
        {
            return _context.Locations.Find(id);
        }

        public Location Create(Location location, int userId)
        {
            // validation
            if (location.Timestamp.Year < 1900)
                throw new AppException("Timestamp \"" + location.Timestamp + "\" is bad");

            var user = _context.Users.Find(userId);

            if (user == null)
                throw new AppException("Cannot find user with given id");

            location.User = user;

            _context.Locations.Add(location);
            _context.SaveChanges();

            return location;
        }

        // public void Update(Location location)
        // {
        //     throw new System.NotImplementedException();
        // }

        public void Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if(location == null) return;
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }

        public IEnumerable<Location> GetRangedLocationsForUser(int userId, DateTime from, DateTime to)
        {
            var result = _context.Locations.Where(l => l.User.Id == userId && l.Timestamp >= from && l.Timestamp <= to)
                .Select(
                    item =>
                        new Location
                        {
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            Timestamp = item.Timestamp,
                            User = new User
                            {
                                Id = item.User.Id
                            }
                        });

            return result;
        }
    }
}