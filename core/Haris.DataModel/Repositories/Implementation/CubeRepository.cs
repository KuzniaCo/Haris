using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Haris.DataModel.DataModels;

namespace Haris.DataModel.Repositories.Implementation
{
    public class CubeRepository : ICubeRepository
    {
        private readonly HarisDbContext _context;
        private IDbSet<Cube> _cubes;


        public CubeRepository(HarisDbContext context)
        {
            _context = context;
            _cubes = _context.Cubes;
        }

        public void CreateCube(Cube cube)
        {
            _cubes.Add(cube);
            _context.SaveChangesAsync();
        }

        public Cube GetCube(string address)
        {
            return _cubes.Include(x=>x.OutputCubes)
                .Include(x => x.WebHooks)
                .FirstOrDefault(x => x.CubeAddress.Contains(address));
        }

        public IQueryable<Cube> GetCubes()
        {
            return _cubes.Include(x=>x.OutputCubes);
        }

        public void AddLog(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void UpdateOutput(OutputCube outputCube)
        {
            _context.OutputCubes.Attach(outputCube);
            _context.Entry(outputCube).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Log> GetValues(string address)
        {
            var cube = _context.Cubes.Include(x => x.Logs).FirstOrDefault(x => x.CubeAddress.Equals(address));
            return cube.Logs.OrderByDescending(x=>x.Date).Take(20).ToList();
        }

        public void UpdateCube(Cube cube)
        {
            _context.Cubes.Attach(cube);
            _context.Entry(cube).State = EntityState.Modified;;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
