﻿using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Infrastucture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}