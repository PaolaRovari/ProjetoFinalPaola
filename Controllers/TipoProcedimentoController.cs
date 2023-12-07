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
    public class TipoProcedimentoController : Controller
    {
        private readonly Contexto _context;

        public TipoProcedimentoController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoProcedimento
        public async Task<IActionResult> Index()
        {
              return _context.TipoProcedimento != null ? 
                          View(await _context.TipoProcedimento.ToListAsync()) :
                          Problem("Entity set 'Contexto.TipoProcedimento'  is null.");
        }

        // GET: TipoProcedimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoProcedimento == null)
            {
                return NotFound();
            }

            var tipoProcedimento = await _context.TipoProcedimento
                .FirstOrDefaultAsync(m => m.TipoProcedimentoId == id);
            if (tipoProcedimento == null)
            {
                return NotFound();
            }

            return View(tipoProcedimento);
        }

        // GET: TipoProcedimento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProcedimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoProcedimentoId,TipoProcedimentoNome")] TipoProcedimento tipoProcedimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProcedimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProcedimento);
        }

        // GET: TipoProcedimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoProcedimento == null)
            {
                return NotFound();
            }

            var tipoProcedimento = await _context.TipoProcedimento.FindAsync(id);
            if (tipoProcedimento == null)
            {
                return NotFound();
            }
            return View(tipoProcedimento);
        }

        // POST: TipoProcedimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int TipoProcedimentoId, [Bind("TipoProcedimentoId,TipoProcedimentoNome")] TipoProcedimento tipoProcedimento)
        {
            if (TipoProcedimentoId != tipoProcedimento.TipoProcedimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProcedimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProcedimentoExists(tipoProcedimento.TipoProcedimentoId))
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
            return View(tipoProcedimento);
        }

        // GET: TipoProcedimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoProcedimento == null)
            {
                return NotFound();
            }

            var tipoProcedimento = await _context.TipoProcedimento
                .FirstOrDefaultAsync(m => m.TipoProcedimentoId == id);
            if (tipoProcedimento == null)
            {
                return NotFound();
            }

            return View(tipoProcedimento);
        }

        // POST: TipoProcedimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int TipoProcedimentoId)
        {
            if (_context.TipoProcedimento == null)
            {
                return Problem("Entity set 'Contexto.TipoProcedimento'  is null.");
            }
            var tipoProcedimento = await _context.TipoProcedimento.FindAsync(TipoProcedimentoId);
            if (tipoProcedimento != null)
            {
                _context.TipoProcedimento.Remove(tipoProcedimento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProcedimentoExists(int id)
        {
          return (_context.TipoProcedimento?.Any(e => e.TipoProcedimentoId == id)).GetValueOrDefault();
        }
    }
}
