using System.Linq;
using System.Collections.Generic;
using Open.Core;
using Open.Domain.Country;

namespace Open.Facade.Country {
    public class CountryViewModelsList : PaginatedList<CountryViewModel> {

        public CountryViewModelsList(IPaginatedList<CountryObject> l, string sortOrder= null) {
            if (l is null) return;
            PageIndex = l.PageIndex;
            TotalPages = l.TotalPages;
            var countries = new List<CountryViewModel>();
            IOrderedEnumerable<CountryViewModel> ordered;
            foreach (var e in l) { countries.Add(CountryViewModelFactory.Create(e)); }
            switch (sortOrder)
            {
                case "name_desc":
                    ordered = countries.OrderByDescending(s => s.Name);
                    break;
                case "alpha3":
                    ordered = countries.OrderBy(s => s.Alpha3Code);
                    break;
                case "alpha3_desc":
                    ordered = countries.OrderByDescending(s => s.Alpha3Code);
                    break;
                case "alpha2":
                    ordered = countries.OrderByDescending(s => s.Alpha2Code);
                    break;
                case "validFrom":
                    ordered = countries.OrderBy(s => s.ValidFrom);
                    break;
                case "validFrom_desc":
                    ordered = countries.OrderByDescending(s => s.ValidFrom);
                    break;
                case "validTo":
                    ordered = countries.OrderBy(s => s.ValidTo);
                    break;
                case "validTo_desc":
                    ordered = countries.OrderByDescending(s => s.ValidTo);
                    break;
                default:
                    ordered = countries.OrderBy(s => s.Name);
                    break;
                
            }
            AddRange(ordered);

            
        }
    }
}
    
