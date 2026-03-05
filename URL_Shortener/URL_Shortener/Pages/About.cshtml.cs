using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.Models;

namespace URL_Shortener.Pages
{
    public class AboutModel : PageModel
    {
        [BindProperty]
        public PageContent AboutContent { get; set; }

        private readonly AppDbContext _dbContext;
        public AboutModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var entityFromDb = await _dbContext.PageContents.FindAsync(1);
            
            if(entityFromDb == null)
                return NotFound();

            AboutContent = entityFromDb;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!User.IsInRole("Admin"))
                return Forbid();
            if (!ModelState.IsValid)
                return Page();

            var entityFromDb = await _dbContext.PageContents.FindAsync(1);

            if (entityFromDb == null)
                return NotFound();

            entityFromDb.TextContent = AboutContent.TextContent;
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
