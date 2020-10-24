using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVX.Models;
using SVX.Services;

namespace SVX.Controllers {
    public class ServersController : Controller {
        private readonly ServerService _serverService;
        private readonly ClientService _clientService;

        public ServersController(ServerService serverService, ClientService clientService) {
            _serverService = serverService;
            _clientService = clientService;
        }

        //IactionResult = Mesmo nome da classe View = Index/Create/Delete/Details
        public async Task<IActionResult> Index() {
            var list = await _serverService.FindAllAsync();
            return View(list.OrderBy(a => a.Client.Name));
        }

        //Rota para a View chamar via Asp.Net
        public async Task<IActionResult> Create() {
            var clients = await _clientService.FindAllAsync();
            var viewModel = new ServerFormViewModel { Clients = clients };
            return View(viewModel);
        }

        //Http = Rota para o Controlador chamar via Browser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Server server) {
            await _serverService.InsertAsync(server);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _serverService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            await _serverService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _serverService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _serverService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Client> clients = await _clientService.FindAllAsync();
            ServerFormViewModel viewModel = new ServerFormViewModel { Server = obj, Clients = clients };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Server server) {
            if (id != server.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {
                await _serverService.UpdateAsync(server);
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
