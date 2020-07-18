using System.Threading.Tasks;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
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

        [AllowAnonymous]
        [HttpGet("subMenu/{Id:long}", Name = "GetSubMenuById")]
        public async Task<IActionResult> GetSubMenuById(long Id)
        {
            var response = await _subMenuService.GetSubMenuByIdAsync(Id);
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

        [AllowAnonymous]
        [HttpGet("{menu}")]
        public async Task<IActionResult> GetSubMenusByMenu(string menu)
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

        [HttpPost]
        public async Task<IActionResult> PostSubMenu([FromBody]SubMenuRequest request)
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

        [HttpPut("SubMenu/{Id}")]
        public async Task<IActionResult> UpdateSubMenu(long Id, [FromBody]SubMenuRequest request)
        {
            if (ModelState.IsValid)
            {
                await _subMenuService.UpdateSubMenuAsync(Id, request);
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

        [HttpDelete("subMenu/{Id}")]
        public async Task<IActionResult> DeleteSubMenu(long Id)
        {
            await _subMenuService.DeleteSubMenuAsync(Id);
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }

        [HttpPost(template: "upload")]
        public async Task<IActionResult> Upload([FromBody]UploadRequest request)
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
                    Message = "Success",
                    Result = imagePath
                });
        }
    }
}