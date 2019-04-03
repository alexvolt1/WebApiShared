using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Helper;
using MVC.Utility;
using Newtonsoft.Json;
using Shared.Data;
using Shared.Models;

namespace MVC.Controllers
{
    public class MenusController : Controller
    {
        private readonly WebApiSharedDbContext _context;

        ApiHelper _api = new ApiHelper();

        public MenusController(WebApiSharedDbContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index2()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync(SD.ClientApiMenus);

            dynamic json = null;
            string temp = "";
            if (res.IsSuccessStatusCode)
            {
                temp = res.Content.ReadAsStringAsync().Result;
                json = JsonConvert.DeserializeObject(temp);
            }
            IEnumerable<Menu> model = JsonConvert.DeserializeObject<IEnumerable<Menu>>(temp);
            //return View(JsonConvert.DeserializeObject(temp));
            ViewData["json"] = json;
            //return View(res);
            //return Ok(json);
            //return View(await _context.Menus.ToListAsync());
            return View(model);
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
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

        public IActionResult Create2()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Id,Name,Image")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var myContent = JsonConvert.SerializeObject(menu);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    //var result = await client.PostAsync(SD.ClientApiMenus, byteContent).Result;

                    HttpClient client = _api.Initial();
                    HttpResponseMessage res = await client.PostAsync(SD.ClientApiMenus, byteContent);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                    //System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            return View(menu);




        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
