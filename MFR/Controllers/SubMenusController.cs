using System.Threading.Tasks;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenusController : ControllerBase
    {
        private readonly ISubMenuService _subMenuService;
        private readonly IFileUploadService _uploadService;

        public SubMenusController(ISubMenuService subMenuService, IFileUploadService uploadService)
        {
            _subMenuService = subMenuService;
            _uploadService = uploadService;
        }

        [HttpGet]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetSubMenus()
        {
            var response = await _subMenuService.GetAllSubMenuAsync();
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = "SubMenus not found"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = "Success",
                    Result = response
                });
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("submenu/{id}", Name = "GetSubMenuById")]
        public async Task<IActionResult> GetSubMenuByIdAsync(long id)
        {
            var response = await _subMenuService.GetSubMenuByIdAsync(id);
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"'{response.SubMenuId}' not found"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = "Success",
                    Result = response
                });
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("menu/{menu:string}/submenus")]
        public async Task<IActionResult> GetSubMenusByMenuAsync(string menu)
        {
            var response = await _subMenuService.GetSubMenusByMenuAsync(menu);
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"'{menu}' not found"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = "Success",
                    Result = response
                });
        }

        [HttpPost("submenu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSubMenuAsync([FromBody]SubMenuRequest request)
        {
            SubMenuResponse response;
            if (ModelState.IsValid)
            {
                response = await _subMenuService.AddSubMenuAsync(request);
                var url = Url.Link("GetSubMenuById", new { id = response.SubMenuId });
                return Created(url, new ApiResponse { 
                    Status = true,
                    Message = "Success",
                    Result = url
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation Failure"
            });
        }

        [HttpPut("submenu/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutSubMenuAsynce(long id, [FromBody]SubMenuRequest request)
        {
            if (ModelState.IsValid)
            {
                await _subMenuService.UpdateSubMenuAsync(id, request);
                return StatusCode(204, new ApiResponse
                {
                    Status = true,
                    Message = "Success"
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation Failure"
            });
        }

        [HttpDelete("submenu/{id}")]
        public async Task<IActionResult> DeleteSubMenuAsync(long id)
        {
            await _subMenuService.DeleteSubMenuAsync(id);
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }

        [HttpPost("upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAsync([FromBody]UploadRequest request)
        {
            var imagePath = await _uploadService.UploadImageAsync(request);
            if (imagePath == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = "Could not upload file"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = $"Success",
                    Result = imagePath
                });
        }
    }
}