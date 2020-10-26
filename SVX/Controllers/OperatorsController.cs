using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVX.Models;
using SVX.Services;
using SVX.Services.Exceptions;

namespace SVX.Controllers {
    public class OperatorsController : Controller {
        private readonly OperatorService _operatorService;

        public OperatorsController(OperatorService operatorService) {
            _operatorService = operatorService;
        }

        //IactionResult = Mesmo nome da classe View = Index/Create/Delete/Details
        public async Task<IActionResult> Index() {
            var list = await _operatorService.FindAllAsync();
            return View(list);
        }

        //Rota para a View chamar via Asp.Net
        public IActionResult Create() {                  
            return View();
        }

        //Http = Rota para o Controlador chamar via Browser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operator Operator) {
            await _operatorService.InsertAsync(Operator);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _operatorService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            //try = chave estrangeira não pode ser deletada
            try {
                await _operatorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            } catch (IntegrityExceptions e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _operatorService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> OperationSearch(string name) {
            if (name == null) {
                return RedirectToAction(nameof(Error), new { message = "Name is null" });
            }
            var obj = await _operatorService.FindByNameAsync(name);
            if (obj.Count() == 0) {
                return RedirectToAction(nameof(Error), new { message = "Name not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _operatorService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //var list = await _Operatorservice.FindByIdAsync(id.Value);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operator Operator) {
            if (id != Operator.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {
                await _operatorService.UpdateAsync(Operator);
                return RedirectToAction(nameof(Index));
            } catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
