using Cars.Contexts;
using Cars.Models;
using Cars.ViewModels.SliderVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        DataDbContext _db {  get; set; }
        IWebHostEnvironment _env;
        public SliderController(DataDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await  _db.Sliders.Select(c=> new SliderListItemVm
            {
                Id = c.Id,
                Title = c.Title,
                Title1 = c.Title1,
                Title2 = c.Title2,
                Description = c.Description,
                HeaderDescription = c.HeaderDescription,
                IsDeleted = c.IsDeleted,
                ImageUrl = c.ImageUrl,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                
            }).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
       [HttpPost]
       public async Task<IActionResult>Create(SliderCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            string filename = null;
            if (vm.MainImage!=null)
            {
                filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
                using (Stream fs = new FileStream(Path.Combine(_env.WebRootPath, "Assets", "image", "stories", filename), FileMode.Create))
                {
                    await vm.MainImage.CopyToAsync(fs);
                }
            }

            Slider slider = new Slider
            {
                Title = vm.Title,
                Title1 = vm.Title1,
                Title2 = vm.Title2,
                Description = vm.Description,
                HeaderDescription = vm.HeaderDescription,
                ImageUrl = filename,
            };
            _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVm
            {
                Title = data.Title,
                Title1 = data.Title1,
                Title2 = data.Title2,
                Description = data.Description,
                HeaderDescription = data.HeaderDescription,
                ImageUrl= data.ImageUrl,
            });
        }
        [HttpPost]
        public async Task<IActionResult>Update(int? id, SliderUpdateVm vm)
        {
            if (id == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Title1= vm.Title1;
             data.Title2=vm.Title2 ;
            data.Description = vm.Description;
            data.HeaderDescription = vm.HeaderDescription;
            data.Title = vm.Title;
            if (!string.IsNullOrEmpty(data.ImageUrl))
            {
                string pathname = Path.Combine(_env.WebRootPath, "Assets", "image", "stories", data.ImageUrl);
                if(System.IO.File.Exists(pathname))
                {
                    System.IO.File.Delete(pathname);
                }
            }
            string filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
            using (Stream fs = new FileStream(Path.Combine(_env.WebRootPath, "Assets", "image", "stories", filename), FileMode.Create))
            {
                await vm.MainImage.CopyToAsync(fs);
            }
            data.ImageUrl = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RestoreData(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>DeleteFromData(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
