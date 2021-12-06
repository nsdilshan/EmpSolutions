using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpiteSolutions.Data;
using EmpiteSolutions.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmpiteSolutions.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMailService _mailService;

        public InventoryController(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // GET: Inventory
        [Authorize(Roles = "Admin,Manager,Viewer")]
        public async Task<IActionResult> Index()
        {
            var productList = await _context.Inventory.ToListAsync();
            var inventoryItems = new List<InventoryViewModel>();

            foreach(var item in productList)
            {
                inventoryItems.Add(new InventoryViewModel
                {
                    Id = item.Id,
                    ItemDescription = item.ItemDescription,
                    Quantity = item.Quantity
                });
            }
            return View(inventoryItems);
        }

        // GET: Inventory/Details/5
        [Authorize(Roles = "Admin,Manager,Viewer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryDetail = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inventoryDetail == null)
            {
                return NotFound();
            }

            var inventoryItem = new InventoryViewModel
            {
                Id = inventoryDetail.Id,
                ItemDescription = inventoryDetail.ItemDescription,
                Quantity = inventoryDetail.Quantity
            };

            return View(inventoryItem);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemDescription,Quantity")] InventoryViewModel inventoryViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var inventoryItem = new Inventory
                {
                    Id = inventoryViewModel.Id,
                    ItemDescription = inventoryViewModel.ItemDescription,
                    Quantity = inventoryViewModel.Quantity
                };

                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryViewModel);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryViewModel = await _context.Inventory.FindAsync(id);
            if (inventoryViewModel == null)
            {
                return NotFound();
            }

            var inventoryItem = new InventoryViewModel
            {
                Id = inventoryViewModel.Id,
                ItemDescription = inventoryViewModel.ItemDescription,
                Quantity = inventoryViewModel.Quantity
            };

            return View(inventoryItem);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemDescription,Quantity")] InventoryViewModel inventoryViewModel)
        {
            if (id != inventoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryItem = new Inventory
                    {
                        Id = inventoryViewModel.Id,
                        ItemDescription = inventoryViewModel.ItemDescription,
                        Quantity = inventoryViewModel.Quantity
                    };

                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryViewModelExists(inventoryViewModel.Id))
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
            return View(inventoryViewModel);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryViewModel = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryViewModel == null)
            {
                return NotFound();
            }
            var inventoryItem = new InventoryViewModel
            {
                Id = inventoryViewModel.Id,
                ItemDescription = inventoryViewModel.ItemDescription,
                Quantity = inventoryViewModel.Quantity
            };

            return View(inventoryViewModel);
        }

        // POST: Inventory/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryViewModel = await _context.Inventory.FindAsync(id);
            _context.Inventory.Remove(inventoryViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryViewModelExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SendEmail()
        {
            return View("SendEmail");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendEmail(SendEmailViewModel sendEmailView)
        {
            if (ModelState.IsValid)
            {
                await _mailService.SendEmailAsync(sendEmailView.ToEmail, sendEmailView.Subject, sendEmailView.EmailBody);
                return RedirectToAction("index", "inventory");
            }
            return View();
        }
    }
}
