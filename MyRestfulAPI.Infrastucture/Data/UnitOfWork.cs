using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Infrastucture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _myContext;

        public UnitOfWork(MyDbContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<bool> SaveAsync()
        {
            return await _myContext.SaveChangesAsync() > 0;
        }
    }
}
