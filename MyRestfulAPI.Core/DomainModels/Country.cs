﻿using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Core.DomainModels
{
    public class Country:IEntity
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Abbreviation { get; set; }

        public string Continent { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
