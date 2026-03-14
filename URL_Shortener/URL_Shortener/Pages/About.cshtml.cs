using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using URL_Shortener.Data;
using URL_Shortener.Models;

namespace URL_Shortener.Pages
{
    public class AboutModel : PageModel
    {
        private const int ABOUT_PAGE_ID = 1;

        [BindProperty]
        public PageContent AboutContent { get; set; }
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _dbContext;
        public AboutModel(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            var entityFromDb = await _dbContext.PageContents.FindAsync(ABOUT_PAGE_ID);
            
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
            {
                return Page();
            }

            var entityFromDb = await _dbContext.PageContents.FindAsync(ABOUT_PAGE_ID);

            if (entityFromDb == null)
                return NotFound();

            entityFromDb.TextContent = AboutContent.TextContent;
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
