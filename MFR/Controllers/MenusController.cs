using System.Threading.Tasks;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
    [Route("api/menus")]
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

        [HttpGet]
        public async Task<IActionResult> GetMenusAsync()
        {
            var response = await _menuService.GetMenusAsync();
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"Not found"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = "Successful",
                    Result = response
                });
        }

        [HttpGet("{id}", Name = "GetMenuById")]
        public async Task<IActionResult> GetMenuByIdAsync(int id)
        {
            var response = await _menuService.GetMenuByIdAsync(id);
            if (response == null)
            {
                return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = "Not found"
                    });
            }
            return Ok(
                new ApiResponse
                {
                    Status = true,
                    Message = "Successful",
                    Result = response
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostMenuAsync([FromBody]MenuRequest request)
        {
            MenuResponse response;
            if (ModelState.IsValid)
            {
                response = await _menuService.CreateAsync(request);
                var url = Url.Link("GetMenuById", new { id = response.MenuId });
                return Created(url, new ApiResponse
                {
                    Status = true,
                    Message = "Successful",
                    Result = response
                });
            }
            return BadRequest(
                new ApiResponse
                {
                    Status = false,
                    Message = "Validation error"
                });
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutMenuAsync(long id, [FromBody]MenuRequest request)
        {
            if (ModelState.IsValid)
            {
                await _menuService.UpdateAsync(id, request);
                return StatusCode(204, new ApiResponse 
                { 
                    Status = true,
                    Message = "Successful"
                });
            }
            return BadRequest(
                new ApiResponse
                {
                    Status = false,
                    Message = "Validation error"
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuAsync(long id)
        {
            await _menuService.DeleteAsync(id);
            return StatusCode(204, new ApiResponse 
            { 
                Status = true,
                Message = "Successful"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAsync([FromBody]UploadRequest request)
        {
            var imagePath = await _uploadService.UploadImageAsync(request);
            if (imagePath == null)
            {
                return BadRequest(
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
                    Message = "File uploaded",
                    Result = imagePath
                });
        }
    }
}