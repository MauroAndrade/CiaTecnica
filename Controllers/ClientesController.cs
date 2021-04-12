using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CiaTecnica.Models;
using AutoMapper;
using CiaTecnica.DTO;
using System.Text.RegularExpressions;

namespace CiaTecnica.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ClientesController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var cliente = await _context.Cliente.ToListAsync();
            var clienteDTO = _mapper.Map<List<ClienteDTO>>(cliente);
            return View(clienteDTO);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Cnpj_Cpf,IsPessoaFisica,DataNascimento,Nome_RazaoSocial,NomeFantasia,Sobrenome,Cep,Logradouro,Numero,Completemento,Bairro,Cidade,Uf")] ClienteDTO clienteDTO)
        {
            if (clienteDTO.IsPessoaFisica)
            {
                var dataNascimento = clienteDTO.DataNascimento;

                if (Convert.ToDateTime(dataNascimento.Value).AddYears(19) >= DateTime.Now)
                {
                    ModelState.AddModelError("DataNascimento", "A idade permitida para cadastro de uma pessoa física é de 19 anos.");
                    return View(clienteDTO);
                }

            }

            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                cliente.Cep = FormataCep(cliente.Cep);
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDTO);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return View(clienteDTO);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Cnpj_Cpf,IsPessoaFisica,DataNascimento,Nome_RazaoSocial,NomeFantasia,Sobrenome,Cep,Logradouro,Numero,Completemento,Bairro,Cidade,Uf")] ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cliente = _mapper.Map<Cliente>(clienteDTO);
                    cliente.Cep = FormataCep(cliente.Cep);
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(clienteDTO.ClienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDTO);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ClienteId == id);
        }

        public string FormataCep(string cep)
        {
            return Regex.Replace(cep, "[^0-9,]", "");
        }
    }
}
