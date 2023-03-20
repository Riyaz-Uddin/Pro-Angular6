using AngularCrudeMaster.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace AngularCrudeMaster.Controllers
{
    public class BookStore : Controller
    {
        private BCShopDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostEnvironment;
        public BookStore(BCShopDBContext _db, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostEnvironment)
        {
            db = _db;
            _HostEnvironment = HostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile files)
        {
            string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            filename = this.EnsureCorrectFilename(filename);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await files.CopyToAsync(output);
            return Ok();
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(_HostEnvironment.WebRootPath, "Uploads", filename);
        }
        [HttpPost]
        public string AddBCShopVM([FromBody] BCShopVM md)
        {
            RemoveBCShopVM(md.BookCategory.CategoryId);
            BookCategory m = new BookCategory() { CategoryId = md.BookCategory.CategoryId, CategoryName = md.BookCategory.CategoryName, Author = md.BookCategory.Author };
            db.BookCategory.Add(m);
            db.SaveChanges();
            foreach (var c in md.BookItem)
            {
                BookItem d = new BookItem()
                {
                    BookId = c.BookId,
                    BookName = c.BookName,
                    CategoryId = c.CategoryId,
                    Description = c.Description,
                    Active = c.Active,
                    Date = DateTime.Parse(c.Date.ToShortDateString()),
                    Price= c.Price,
                    Image = c.Image
                };
                db.BookItem.Add(d);
            }
            db.SaveChanges();
            return "1";
        }

        [HttpPost]
        public string RemoveBCShopVM(string id)
        {
            List<BookItem> st5 = db.BookItem.Where(xx => xx.CategoryId == id).ToList();
            db.BookItem.RemoveRange(st5);
            db.SaveChanges();
            BookCategory st6 = db.BookCategory.Find(id);
            if (st6 != null)
            {
                db.BookCategory.Remove(st6);
            }
            db.SaveChanges();

            return "1";
        }

        public JsonResult GetAllCategory()
        {
            var a = (from d in db.BookCategory select new { d.CategoryId, d.CategoryName, d.Author });
            return Json(a);
        }

        public JsonResult GetCategory(string id)
        {
            var a = (from d in db.BookCategory where d.CategoryId==id select new { d.CategoryId, d.CategoryName, d.Author });
            return Json(a);
        }

        public JsonResult GetItems(string id)
        {
            List<BookItem> a = null;
            a=(from d in db.BookItem where d.CategoryId == id select d).ToList();
            return Json(a);
        }
       
        public JsonResult GetAllItems()
        {
            var a = (from d in db.BookItem select new { d.BookId, d.BookName, d.CategoryId, d.Description, d.Active, d.Date, d.Price, d.Image});
            return Json(a);
        }
        public ActionResult ShowMe()
        {
            IEnumerable<BookCategory> s = db.BookCategory.ToList();
            return View(s);
        }

        public ActionResult ShowItems(string sid = "0")
        {
            List<BookItem> s = db.BookItem.Where(xx => xx.CategoryId == sid).ToList();
            return View(s);
        }

        public ActionResult Create2(string sid = "0")
        {
            ViewBag.sid = sid;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    
                    var files = Request.Form.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        IFormFile file = files[i];
                        string fname;

                        fname = file.FileName;
                        string webRootPath = _HostEnvironment.WebRootPath;
                        string fname1 = "";
                        fname1 = Path.Combine(webRootPath, "Uploads/" + fname);
                        file.CopyTo(new FileStream(fname1, FileMode.Create));
                    }

                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

      

    }
}
