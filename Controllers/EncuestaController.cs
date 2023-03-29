using Microsoft.AspNetCore.Mvc;
using PruebaNet.Models;
using PruebaNet.Repositories;
using PruebaNet.Tools;

namespace PruebaNet.Controllers
{
    public class EncuestaController : Controller
    {
        private readonly IEncuestaRepository _repository;
        private readonly FilesHandler _fileHandler;
        public EncuestaController(IEncuestaRepository repository, FilesHandler fileHandler)
        {
            _repository = repository;
            _fileHandler = fileHandler;
        }
        
        public async Task<IActionResult> ListaEncuestas()
        {
            var encuestas = await _repository.GetEncuestas();
            return View(encuestas);
        }

        public IActionResult ContestarEncuesta()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarEncuesta(List<RespuestaEncuesta> respuestas)
        {
			try
			{
                var encuestaId = await _repository.GuardarEncuesta();
                await _repository.GuardarRespuestas(respuestas, encuestaId);
                return Json("OK");
            }
            catch (Exception ex)
			{
                return Json(ex.Message);
			}
            
        }


        public async Task<IActionResult> DescargarEncuesta(int encuestaId)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"encuesta-{encuestaId}.xlsx";
            var respuestas = await _repository.GetRespuestaByEncuestaId(encuestaId);
            var content = _fileHandler.CrearExcelRespuestas(respuestas.ToList());
            return File(content, contentType, fileName);
        }


    }
}
