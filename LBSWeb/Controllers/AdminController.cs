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
        [Authorize(Roles = $"{Role.Admin},{Role.Author}")]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            //if (User.IsInRole("Staff"))
            //{
            //    return RedirectToAction("ListPost");
            //}
            var result = new ReportModel();
            return View(result);
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

    }
}
