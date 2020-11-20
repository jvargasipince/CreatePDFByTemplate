using CreatePDFfromTemplate.Model;
using CreatePDFfromTemplate.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CreatePDFfromTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetContract()
        {
            return Ok();
        }


        [HttpPost]
        public IActionResult GenerateDocumentKeyNUA(ContractInfo contractInfo)
        {

            if (contractInfo == null)
            {
                throw new ArgumentNullException(nameof(contractInfo));
            }

            GeneratePDF generatePDF = new GeneratePDF();

            string newDocumentFileName = generatePDF.GenerateInvestorDocument(contractInfo);

            if (string.IsNullOrWhiteSpace(newDocumentFileName))
                return BadRequest("Un error ocurrió al crear el archivo.");

            return Ok(newDocumentFileName);

        }

    }
}
