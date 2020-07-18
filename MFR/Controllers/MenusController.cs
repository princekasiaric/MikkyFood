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
    public class MenusController : ControllerBase 
    {
        private readonly IMenuService _menuService;
        private readonly IFileUploadService _uploadService;

        public MenusController(IMenuService menuService, IFileUploadService uploadService)
        {
            _menuService = menuService;
            _uploadService = uploadService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var response = await _menuService.GetMenusAsync();
            if (response == null)
            {
                return NotFound(
                    new ApiResponse 
                    {
                        Status = false,
                        Message = "No menu found"
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
        [HttpGet("menu/{Id}", Name = "GetMenuById")]
        public async Task<IActionResult> GetMenuById(long Id)
        {
            var response = await _menuService.GetMenuByIdAsync(Id);
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"'{response.MenuId}' not found"
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
        public async Task<IActionResult> PostMenu([FromBody]MenuRequest request)
        {
            MenuResponse response;
            if (ModelState.IsValid)
            {
                response = await _menuService.CreateAsync(request);
                var url = Url.Link("GetMenuById", new { id = response.MenuId });
                return Created(url, new ApiResponse
                {
                    Status = true,
                    Message = "Success",
                    Result = url
                });
            }
            return BadRequest(
                new ApiResponse
                {
                    Status = false,
                    Message = "Validation Failure"
                });
        }

        [HttpPut("Menu/{Id}")]
        public async Task<IActionResult> UpdateMenu(long Id, [FromBody]MenuRequest request)
        {
            if (ModelState.IsValid)
            {
                await _menuService.UpdateAsync(Id, request);
                return StatusCode(204, new ApiResponse 
                { 
                    Status = true,
                    Message = "Success"
                });
            }
            return BadRequest(
                new ApiResponse
                {
                    Status = false,
                    Message = "Validation Failure"
                });
        }

        [HttpDelete("menu/{Id}")]
        public async Task<IActionResult> DeleteMenu(long Id)
        {
            await _menuService.DeleteAsync(Id);
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