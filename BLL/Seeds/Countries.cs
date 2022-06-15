using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BLL.Seeds
{
    public class Countries
    {
        public List<BLL.DB.Master.Countries> Get()
        {
            var Ar = JsonSerializer.Deserialize<List<CountriesJsonVm>>(File.ReadAllText(Path.Combine(@"..\BLL\Seeds", "CountriesAr.json")));
            var En = JsonSerializer.Deserialize<List<CountriesJsonVm>>(File.ReadAllText(Path.Combine(@"..\BLL\Seeds", "CountriesEn.json")));
            var Cities = JsonSerializer.Deserialize<List<CitiesJsonVm>>(File.ReadAllText(Path.Combine(@"..\BLL\Seeds", "Cities.json")));
            return (from ar in Ar join en in En on ar.Id equals en.Id
                    select new BLL.DB.Master.Countries
                    {
                        Code = ar.Id,
                        NameAr = ar.Value,
                        NameEn = en.Value,
                        Cities = Cities.Where(c=> c.CountryId == ar.Id).Select(a=> new BLL.DB.Master.Cities
                        {
                            NameAr = a.Name,
                            NameEn = a.Name,
                            Longitude = a.Lng,
                            Latitude = a.Lat
                        }).ToList()
                    }).ToList();
        }
        class CountriesJsonVm
        {
            public string Id { get; set; }
            public string Value { get; set; }
        }
        class CitiesJsonVm
        {
            public string Name { get; set; }
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
            public string CountryId { get; set; }
        }
    }
}
