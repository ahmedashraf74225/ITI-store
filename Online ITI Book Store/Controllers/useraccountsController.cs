using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Online_ITI_Book_Store.Data;
using Online_ITI_Book_Store.Models;

namespace Online_ITI_Book_Store.Controllers
{
    public class useraccountsController : Controller
    {
        private readonly Online_ITI_Book_StoreContext _context;

        public useraccountsController(Online_ITI_Book_StoreContext context)
        {
            _context = context;
        }

        // GET: useraccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.useraccounts.ToListAsync());
        }


        // GET: useraccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }


        // POST: useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,email")] useraccounts useraccounts)
        {
            useraccounts.role = "customer";
            _context.Add(useraccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(login));

        }
 
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=localhost\\sqlexpress;Initial Catalog=mynewdb3;Integrated Security=True");
                string sql;
            sql = "SELECT * FROM useraccounts where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
                string id = Convert.ToString((int)reader["Id"]);
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("userid", id);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("catalogue", "books");
                else
                    return RedirectToAction("Index", "books");

            }
            else
            {
                ViewData["Message"] = "wrong user name or password";
                return View();
            }
        }


        // GET: useraccounts/Edit/5
        public async Task<IActionResult> Edit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var useraccounts = await _context.useraccounts.FindAsync(id);
            return View(useraccounts);
        }

        // POST: useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,email")] useraccounts useraccounts)
        {
                    _context.Update(useraccounts);
                    await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(login));
            
           
        }

        private bool useraccountsExists(int id)
        {
          return (_context.useraccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
