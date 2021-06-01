using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Open.Core;
using Open.Data.Country;
using Open.Domain.Country;

namespace Open.Infra.Country{
  public  class CountryObjectsRepository : ICountryObjectsRepository{
      private readonly CountryDbContext db;

      public CountryObjectsRepository(CountryDbContext context)
      {
          db = context;
      }
      

     public async Task<CountryObject>GetObject(string id) { 
          var o = await db.Countries.FindAsync(id);
          return new CountryObject(o);
      }
      public async Task<IEnumerable<CountryObject>> GetObjectsList(string searchString = null, int? page = null, 
          int? pageSize = null) {
          var countries = from s in db.Countries select s;
          if (!string.IsNullOrEmpty(searchString)) {
              searchString = searchString.ToLower();
              countries = countries.Where(
                      s => s.ID.ToLower().Contains(searchString)
                           || s.Name.ToLower().Contains(searchString)
                           || s.Code.ToLower().Contains(searchString)
                           || s.ValidFrom.ToString(CultureInfo.CurrentCulture).Contains(searchString)
                           || s.ValidTo.ToString(CultureInfo.CurrentCulture).Contains(searchString));
          }

          var list =
              await PaginatedList<CountryDbRecord>.CreateAsync(countries.AsNoTracking(), page,
                  pageSize);
          return new CountryObjectsList(list);
      }

      public async Task<CountryObject>AddObject(CountryObject o) {
          db.Countries.Add(o.DbRecord);
          await db.SaveChangesAsync();
          return o;
          }

    public async void UpdateObject(CountryObject o) {
          db.Countries.Update(o.DbRecord);
          await db.SaveChangesAsync();
          
      }

      public async void DeleteObject(CountryObject o) {
          db.Countries.Remove(o.DbRecord);
          await db.SaveChangesAsync();

      }
      public bool IsInitialized() {
          db.Database.EnsureCreated();
          return db.Countries.Any();
      }
  }
}

