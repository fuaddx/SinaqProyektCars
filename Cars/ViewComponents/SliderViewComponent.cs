
using Cars.Contexts;
using Cars.ViewModels.SliderVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.ViewComponents
{
    public class SliderViewComponent: ViewComponent
    {
        DataDbContext _db { get; set; }

        public SliderViewComponent(DataDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Sliders.Select(c => new SliderListItemVm
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
    }
}
