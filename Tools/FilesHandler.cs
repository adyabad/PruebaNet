using ClosedXML.Excel;
using PruebaNet.Models;

namespace PruebaNet.Tools
{
    public class FilesHandler
    {
        public byte[]? CrearExcelRespuestas(List<RespuestaEncuesta> respuestas)
        {            
            
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Resultados");
                    worksheet.Cell(1, 1).Value = "Pregunta";
                    worksheet.Cell(1, 2).Value = "Respuesta";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 2).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                    worksheet.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;                    

                    for (int index = 1; index <= respuestas.Count; index++)
					{
						worksheet.Cell(index + 1, 1).Value = respuestas[index - 1].Pregunta;
						worksheet.Cell(index + 1, 2).Value = respuestas[index - 1].Respuesta;
					}
                    worksheet.Columns().Width = 70;
                    worksheet.Rows().Height = 20;
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
