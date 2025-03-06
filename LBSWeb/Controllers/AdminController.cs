using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessObject;
using BusinessObject.BaseModel;
using LBSWeb.Service.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LBSWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly INotyfService _notyf;

        public AdminController(IBookService bookService, INotyfService notyf)
        {
            _bookService = bookService;
            _notyf = notyf;
        }
        [Authorize(Roles = $"{Role.Admin},{Role.Author},{Role.Manager}")]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Role.Author))
            {
                return RedirectToAction("Books");
            }

            var result = await _bookService.ShortReport();
            return View(result.Data);
        }

        [Authorize(Roles = $"{Role.Author}")]
        [HttpPost]
        [Route("GenerateSummary")]
        public async Task<IActionResult> GenerateSummary(string input)
        {
            var res = await _bookService.GenerateSummary(input);
            return Json(res);
        }
        
        [HttpPost]
        [Authorize(Roles = $"{Role.Author}")]
        [Route("GeneratePoster")]
        public async Task<IActionResult> GeneratePoster(string input)
        {
            var result = await _bookService.GeneratePoster(input);
            return Json(result);
        }

        [Authorize(Roles = $"{Role.Admin},{Role.Author}")]
        [Route("Books")]
        public async Task<IActionResult> Books()
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = await _bookService.GetAllBookByUser(userName);
            ViewBag.Books = res.DataList;
            return View();
        }

        [Authorize(Roles = $"{Role.Admin}")]
        [Route("ListCategories")]
        public async Task<IActionResult> ListCategories()
        {
            var result = await _bookService.GetCategories();
            ViewBag.Categories = result.DataList;
            return View();
        }

        [Authorize(Roles = $"{Role.Admin}")]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _bookService.DeleteCategory(id);
            return Json(result.Message);
        }

        [Authorize(Roles = $"{Role.Admin}")]
        [HttpPost]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Category model)
        {
            var result = await _bookService.UpdateCategory(model);
            if (result.IsSussess) _notyf.Success(result.Message);
            else _notyf.Error(result.Message);
            return RedirectToAction("ListCategories");
        }

        [Authorize(Roles = $"{Role.Author}")]
        [Route("{id}/CreateChapterBook")]
        public async Task<IActionResult> CreateChapterBook(int id)
        {
            ViewBag.BookId = id;         
            var result = await _bookService.GetBook(id);
            var resultChapterBook = await _bookService.GetListBookChapter(id);

            var startChapterId = resultChapterBook.DataList.Count + 1;

            ViewBag.StartChapterId = startChapterId;

            ViewBag.BookName = result.Data.Name;
            return View(new BookChapter { BookId = id, ChaperId = startChapterId });
        }

        [Authorize(Roles = $"{Role.Author}")]
        [Route("DeleteChapterBook/{chapterId}")]
        public async Task<IActionResult> DeleteChapterBook(string chapterId)
        {
            var result = await _bookService.DeleteChapterBook(chapterId);
            //if (result.IsSussess) _notyf.Success(result.Message);
            return Json(result);
        }

        [Authorize(Roles = $"{Role.Author}")]
        [Route("{id}/UpdateChapterBook/{chapterId}")]
        public async Task<IActionResult> UpdateChapterBook(int id,string chapterId, string returnUrl)
        {
            ViewBag.BookId = id;
            ViewBag.ReturnUrl = returnUrl;
            var result = await _bookService.GetBook(id);
            var resultChapterBook = await _bookService.GetBookChapter(chapterId);
            ViewBag.BookName = result.Data.Name;
            return View(resultChapterBook.Data);
        }

        [HttpPost]
        [Authorize(Roles = $"{Role.Author}")]
        [Route("UpdateChapterBook")]
        public async Task<IActionResult> UpdateChapterBook(BookChapter bookChapter)
        {
            var result = await _bookService.UpdateBookChapter(bookChapter);
            if (result.IsSussess)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("ChapterBooks");
            }
            else
            {
                _notyf.Error(result.Message);
                return View(bookChapter);
            }
        }

    }
}
