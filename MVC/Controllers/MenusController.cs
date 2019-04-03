using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Helper;
using MVC.Models;
using MVC.Utility;
using Newtonsoft.Json;
using Shared.Models;

namespace MVC.Controllers
{
    public class MenusController : Controller
    {
        //private readonly WebApiSharedDbContext _context;

        private readonly HttpClient _client;

        ApiHelper _api = new ApiHelper();

        public MenusController()
        {
            _client = _api.Initial();
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiMenus);
            IEnumerable<Menu> menus = null;
            if (res.IsSuccessStatusCode)
            {
                menus = JsonConvert.DeserializeObject<IEnumerable<Menu>>(res.Content.ReadAsStringAsync().Result);
            }
            return View(menus);
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiMenus + "/" + id);
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<Menu> menus = null;
            menus = JsonConvert.DeserializeObject<IEnumerable<Menu>>(res.Content.ReadAsStringAsync().Result);
            Menu menu = menus.FirstOrDefault();
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    var myContent = JsonConvert.SerializeObject(menu);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpClient client = _api.Initial();
                    HttpResponseMessage res = await client.PostAsync(SD.ClientApiMenus, byteContent);
                    return RedirectToAction(nameof(Index));
               // }
               // catch(Exception ex)
               // {

                //}
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiMenus + "/" + id);
            IEnumerable<Menu> menus = null;

            if (id == null)
            {
                return NotFound();
            }

            menus = JsonConvert.DeserializeObject<IEnumerable<Menu>>(res.Content.ReadAsStringAsync().Result);

            Menu menu = menus.FirstOrDefault();

            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image")] Menu menu)
        //{
        //    if (id != menu.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(menu);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MenuExists(menu.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(menu);
        //}

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiMenus + "/" + id);
            IEnumerable<Menu> menus = null;
            if (id == null)
            {
                return NotFound();
            }
            menus = JsonConvert.DeserializeObject<IEnumerable<Menu>>(res.Content.ReadAsStringAsync().Result);

            Menu menu = menus.FirstOrDefault();

            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var menu = await _context.Menus.FindAsync(id);
        //    _context.Menus.Remove(menu);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MenuExists(int id)
        //{
        //    return _context.Menus.Any(e => e.Id == id);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
