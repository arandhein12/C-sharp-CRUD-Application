using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Open.Aids;
using Open.Core;
using Open.Domain.Country;
using Open.Facade.Country;


namespace Open.Sentry.Controllers {
    //[Authorize]
    public class CountriesController : Controller {
        private readonly ICountryObjectsRepository reporitory;
        internal const string properties = "Alpha3Code, Alpha2Code, Name, ValidFrom, ValidTo";

        public CountriesController(ICountryObjectsRepository r) {
            reporitory = r;
        }

        public async Task<IActionResult> Index(string sortOrder = null, 
            string currentFilter = null,
            string searchString = null,
            int? page = null) {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortName"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SortAlpha3"] = sortOrder == "alpha3" ? "alpha3_desc" : "alpha3";
            ViewData["SortAlpha2"] = sortOrder == "alpha2" ? "alpha2_desc" : "alpha2";
            ViewData["SortValidFrom"] = sortOrder == "validFrom" ? "validFrom_desc" : "validFrom";
            ViewData["SortValidTo"] = sortOrder == "validTo" ? "validTo_desc" : "validTo";
            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewData["CurrentFilter"] = searchString;
            var l = await reporitory.GetObjectsList(searchString, page) as CountryObjectsList;
            return View(new CountryViewModelsList(l, sortOrder));
        }

        public IActionResult Create() {
            return View();

        }
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(properties)] CountryViewModel c) {
            await validateId(c.Alpha3Code, ModelState);
            if (!ModelState.IsValid) return View(c);
            var o = CountryObjectFactory.Create(c.Alpha3Code, c.Name, c.Alpha2Code, c.ValidFrom,
                c.ValidTo);
            await reporitory.AddObject(o);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id) {
            var c = await reporitory.GetObject(id);
            return View(CountryViewModelFactory.Create(c));

        }
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(properties)] CountryViewModel c) {
            if (!ModelState.IsValid) return View(c);
            var o = await reporitory.GetObject(c.Alpha3Code);
            o.DbRecord.Name = c.Name;
            o.DbRecord.Code = c.Alpha2Code;
            o.DbRecord.ValidFrom = c.ValidFrom ?? DateTime.MinValue;
            o.DbRecord.ValidTo = c.ValidTo ?? DateTime.MaxValue;
            reporitory.UpdateObject(o);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(string id) {
            var c = await reporitory.GetObject(id);
            return View(CountryViewModelFactory.Create(c));

        }

        public async Task<IActionResult> Delete(string id) {
            var c = await reporitory.GetObject(id);
            return View(CountryViewModelFactory.Create(c));
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var c = await reporitory.GetObject(id);
            reporitory.DeleteObject(c);
            return RedirectToAction("Index");
        }
        private async Task validateId(string id, ModelStateDictionary d) {
            if (await isIdInUse(id))
                d.AddModelError(string.Empty, idIsInUseMessage(id));
        }
        private async Task<bool> isIdInUse(string id) {
            return (await reporitory.GetObject(id))?.DbRecord?.ID == id;
        }
        private static string idIsInUseMessage(string id) {
            var name = GetMember.DisplayName<CountryViewModel>(c => c.Alpha3Code);
            return string.Format(Messages.ValueIsAlreadyInUse, id, name);
        }
    }
}


