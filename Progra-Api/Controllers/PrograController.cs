using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progra_Api.Data;
using Progra_Api.Data.Migrations;
using Progra_Api.Models;

namespace Progra_Api.Controllers
{
	[Route("api/progra")]
	[ApiController]
	public class PrograController : Controller
	{
		private readonly PrograDBContext _prograDBContext;
		public PrograController(PrograDBContext progaDBContext)
		{
			_prograDBContext = progaDBContext;
		}

		[HttpGet]
		[Route("~/api/GetAll")]
		public async Task<IEnumerable<Language>> Get()
		{
			return await _prograDBContext.Languages.ToListAsync();
		}

		[HttpGet]
		[Route("~/api/Get/{id}")]
		[ProducesResponseType(typeof(Language), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var language = await _prograDBContext.Languages.FindAsync(id);
			return language == null ? NotFound() : Ok(language);
		}

		[HttpPost]
		[Route("~/api/Add")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(Language language)
		{
			await _prograDBContext.Languages.AddAsync(language);
			await _prograDBContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = language.ID }, language);
		}

		[HttpPut]
		[Route("~/api/Update/{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, Language language)
		{
			if (id != language.ID) return BadRequest();

			_prograDBContext.Entry(language).State = EntityState.Modified;
			await _prograDBContext.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		[Route("~/api/Delete/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(int id)
		{
			var languageToDelete = _prograDBContext.Languages.Find(id);
			if (languageToDelete == null) return NotFound();

			_prograDBContext.Languages.Remove(languageToDelete);
			await _prograDBContext.SaveChangesAsync();

			return NoContent();
		}
	}
}
