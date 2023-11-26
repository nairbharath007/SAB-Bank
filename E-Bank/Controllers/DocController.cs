using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {

        private readonly IDocService _docService;


        public DocController(IDocService docService)
        {
            _docService = docService;
        }
        private DocDto ModelToDto(Documents customer)
        {
            return new DocDto()
            {
                CustomerId = customer.CustomerId,
             
                DocumentData = customer.DocumentData,
                DocumentType = customer.DocumentType,
                Status = customer.Status,
                UploadDate = customer.UploadDate
                
            

            };
        }


        [HttpGet]
        public IActionResult GetAllDocuments()
        {
          
            var documents = _docService.GetAll();

            if (documents.Count==0)
            {
                return NotFound("No documents found");
            }
           

            return Ok(documents);
        }
       

        //admin showing data by customer id passing
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customerData = _docService.GetById(id);

            if (customerData != null)
            {
                
                return File(customerData.DocumentData, "image/jpeg"); 
            }

            throw new UserNotFoundException("Cannot find the match id");
        }



        private Documents ConvertToModel(DocDto docDto, byte[] documentData)
        {
            return new Documents()
            {
                UploadDate = DateTime.Now,
                CustomerId = docDto.CustomerId,
                DocumentData = documentData,
                DocumentType = docDto.DocumentType,
                Status = "Pending"
            };
        }
      

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _docService.GetById(id);
            if (matched != null)
            {
                _docService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("cannot find id to delete");
        }



       

        [HttpPost("upload")]
        public IActionResult UploadDocument([FromForm] CustomerDocumentUploadDto documentDto)
        {
            if (documentDto == null || documentDto.DocumentFile == null || documentDto.DocumentFile.Length <= 0)
            {
                return BadRequest("Invalid data or file");
            }

            using (var ms = new MemoryStream())
            {
                documentDto.DocumentFile.CopyTo(ms);
                var customerDocument = new Documents
                {
                    DocumentType = documentDto.DocumentType,
                    DocumentData = ms.ToArray(),
                    CustomerId = documentDto.CustomerId,
                    UploadDate = DateTime.Now,
                    Status = "Success"
                };


                var statuss = _docService.Add(customerDocument);
                if (statuss == 1)
                {
                    return Ok(new ReturnMessage() { Message = " succesfully Documnet Uploaded" });
                }
                return BadRequest("error in documnet uploading");

            }
        }

    }
















}
