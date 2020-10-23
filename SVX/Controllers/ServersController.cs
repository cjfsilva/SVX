using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SVX.Models;
using SVX.Services;
using SVX.Services.Exceptions;

namespace SVX.Controllers {
    public class ServersController : Controller {
        private readonly ServerService _serverService;
        private readonly ClientService _clientService;

        public ServersController(ServerService serverService, ClientService clientService) {
            _serverService = serverService;
            _clientService = clientService;
        }

        //IactionResult = Mesmo nome da classe View = Index/Create/Delete/Details
        public IActionResult Index() {
            var list = _serverService.FindAll();
            return View(list);
        }

        //Rota para a View chamar via Asp.Net
        public IActionResult Create() {
            var clients = _clientService.FindAll();
            var viewModel = new ServerFormViewModel { Clients = clients };
            return View(viewModel);
        }

        //Http = Rota para o Controlador chamar via Browser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Server server) {
            _serverService.Insert(server);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = _serverService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _serverService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = _serverService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = _serverService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Client> clients = _clientService.FindAll();
            ServerFormViewModel viewModel = new ServerFormViewModel { Server = obj, Clients = clients };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Server server) {
            if (id != server.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {
                _serverService.Update(server);
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
