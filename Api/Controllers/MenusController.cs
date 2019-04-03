using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Api.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        WebApiSharedDbContext _webApiSharedDbContext;

        public MenusController(WebApiSharedDbContext webApiSharedDbContext)
        {
            _webApiSharedDbContext = webApiSharedDbContext;
        }

        [HttpGet]
        public IActionResult GetMenus()
        {
            //var menus = _webApiSharedDbContext.Menus.Include("SubMenus");

            var menus = _webApiSharedDbContext.Menus;

            return Ok(menus);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenus(int id)
        {
            //var menu = _webApiSharedDbContext.Menus.Include("SubMenus").Where(m => m.Id == id);
            var menu = _webApiSharedDbContext.Menus.Where(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }


        // POST: api/menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image")] Menu menu)
        {

            //Menu model = new Menu()
            //{
            //    Name = "test77",
            //    Image = "testUrl77"
            //};

            if (ModelState.IsValid)
            {
                _webApiSharedDbContext.Add(menu);
                await _webApiSharedDbContext.SaveChangesAsync();
                return Ok(menu);
            }
            return Ok(menu);
        }

    }
}