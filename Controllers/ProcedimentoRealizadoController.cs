using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalPaola.Models;

namespace ProjetoFinalPaola.Controllers
{
    public class ProcedimentoRealizadoController : Controller
    {
        private readonly Contexto _context;

        public ProcedimentoRealizadoController(Contexto context)
        {
            _context = context;
        }

        // GET: ProcedimentoRealizado
        public async Task<IActionResult> Index(string pesquisa)
        {

            if (pesquisa == null)
            {
                var contexto = _context.ProcedimentoRealizado
                    .Include(p => p.Cliente)
                    .Include(p => p.Colaborador)
                    .Include(p => p.LocalRealizacao)
                    .Include(p => p.Procedimento);
                return View(await contexto.ToListAsync());
            }
            else
            {
                var contexto = _context.ProcedimentoRealizado
                    .Include(p => p.Cliente)
                    .Include(p => p.Colaborador)
                    .Include(p => p.LocalRealizacao)
                    .Include(p => p.Procedimento)
                    .OrderBy(x => x.Cliente.ClienteNome)
                    .Where(x => x.Cliente.ClienteNome.Contains(pesquisa));
                return View(await contexto.ToListAsync());
            }

        }

        // GET: ProcedimentoRealizado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProcedimentoRealizado == null)
            {
                return NotFound();
            }

            var procedimentoRealizado = await _context.ProcedimentoRealizado
                .Include(p => p.Cliente)
                .Include(p => p.Colaborador)
                .Include(p => p.LocalRealizacao)
                .Include(p => p.Procedimento)
                .FirstOrDefaultAsync(m => m.ProcedimentoRealizadoId == id);
            if (procedimentoRealizado == null)
            {
                return NotFound();
            }

            return View(procedimentoRealizado);
        }

        // GET: ProcedimentoRealizado/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteNome");
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "ColaboradorId", "ColaboradorNome");
            ViewData["LocalRealizacaoId"] = new SelectList(_context.LocalRealizacao, "LocalRealizacaoId", "LocalRealizacaoNome");
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimento, "ProcedimentoId", "ProcedimentoNome");
            return View();
        }

        // POST: ProcedimentoRealizado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcedimentoRealizadoId,ClienteId,ColaboradorId,ProcedimentoId,LocalRealizacaoId,DataRealizacao,ObservacaoRealizacao")] ProcedimentoRealizado procedimentoRealizado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedimentoRealizado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteNome", procedimentoRealizado.ClienteId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "ColaboradorId", "ColaboradorNome", procedimentoRealizado.ColaboradorId);
            ViewData["LocalRealizacaoId"] = new SelectList(_context.LocalRealizacao, "LocalRealizacaoId", "LocalRealizacaoNome", procedimentoRealizado.LocalRealizacaoId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimento, "ProcedimentoId", "ProcedimentoNome", procedimentoRealizado.ProcedimentoId);
            return View(procedimentoRealizado);
        }

        // GET: ProcedimentoRealizado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProcedimentoRealizado == null)
            {
                return NotFound();
            }

            var procedimentoRealizado = await _context.ProcedimentoRealizado.FindAsync(id);
            if (procedimentoRealizado == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteNome", procedimentoRealizado.ClienteId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "ColaboradorId", "ColaboradorNome", procedimentoRealizado.ColaboradorId);
            ViewData["LocalRealizacaoId"] = new SelectList(_context.LocalRealizacao, "LocalRealizacaoId", "LocalRealizacaoNome", procedimentoRealizado.LocalRealizacaoId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimento, "ProcedimentoId", "ProcedimentoNome", procedimentoRealizado.ProcedimentoId);
            return View(procedimentoRealizado);
        }

        // POST: ProcedimentoRealizado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProcedimentoRealizadoId, [Bind("ProcedimentoRealizadoId,ClienteId,ColaboradorId,ProcedimentoId,LocalRealizacaoId,DataRealizacao,ObservacaoRealizacao")] ProcedimentoRealizado procedimentoRealizado)
        {
            if (ProcedimentoRealizadoId != procedimentoRealizado.ProcedimentoRealizadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimentoRealizado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimentoRealizadoExists(procedimentoRealizado.ProcedimentoRealizadoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteNome", procedimentoRealizado.ClienteId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "ColaboradorId", "ColaboradorNome", procedimentoRealizado.ColaboradorId);
            ViewData["LocalRealizacaoId"] = new SelectList(_context.LocalRealizacao, "LocalRealizacaoId", "LocalRealizacaoNome", procedimentoRealizado.LocalRealizacaoId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimento, "ProcedimentoId", "ProcedimentoNome", procedimentoRealizado.ProcedimentoId);
            return View(procedimentoRealizado);
        }

        // GET: ProcedimentoRealizado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProcedimentoRealizado == null)
            {
                return NotFound();
            }

            var procedimentoRealizado = await _context.ProcedimentoRealizado
                .Include(p => p.Cliente)
                .Include(p => p.Colaborador)
                .Include(p => p.LocalRealizacao)
                .Include(p => p.Procedimento)
                .FirstOrDefaultAsync(m => m.ProcedimentoRealizadoId == id);
            if (procedimentoRealizado == null)
            {
                return NotFound();
            }

            return View(procedimentoRealizado);
        }

        // POST: ProcedimentoRealizado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProcedimentoRealizadoId)
        {
            if (_context.ProcedimentoRealizado == null)
            {
                return Problem("Entity set 'Contexto.ProcedimentoRealizado'  is null.");
            }
            var procedimentoRealizado = await _context.ProcedimentoRealizado.FindAsync(ProcedimentoRealizadoId);
            if (procedimentoRealizado != null)
            {
                _context.ProcedimentoRealizado.Remove(procedimentoRealizado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimentoRealizadoExists(int id)
        {
          return (_context.ProcedimentoRealizado?.Any(e => e.ProcedimentoRealizadoId == id)).GetValueOrDefault();
        }

        // POST: ProcedimentoRealizado/Imprimir Exame/5
        public async Task<IActionResult> Imprimir(int? id)
        {
            if (id == null || _context.ProcedimentoRealizado == null)
            {
                return NotFound();
            }
            var resultado = await _context.ProcedimentoRealizado
                .Include(r => r.Procedimento)
                .Include(r => r.Cliente)
                .Include(r => r.Colaborador)
                .Include(r => r.LocalRealizacao)
                .FirstOrDefaultAsync(m => m.ProcedimentoRealizadoId == id);

            if (resultado == null)
            {
                return NotFound();
            }
            return View(resultado);
        }
    }
}
