using System.Threading.Tasks;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
    [Route("api/submenus")]
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
        public async Task<IActionResult> GetSubMenus()
        {
            var response = await _subMenuService.GetAllSubMenuAsync();
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

        [HttpGet("{id}", Name = "GetSubMenuById")]
        public async Task<IActionResult> GetSubMenuByIdAsync(long id)
        {
            var response = await _subMenuService.GetSubMenuByIdAsync(id);
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

        [HttpGet("{menu:string}")]
        public async Task<IActionResult> GetSubMenuByMenuAsync(string menu)
        {
            var response = await _subMenuService.GetSubMenusByMenuAsync(menu);
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
        public async Task<IActionResult> PostSubMenuAsync([FromBody]SubMenuRequest request)
        {
            SubMenuResponse response;
            if (ModelState.IsValid)
            {
                response = await _subMenuService.AddSubMenuAsync(request);
                var url = Url.Link("GetSubMenuById", new { id = response.SubMenuId });
                return Created(url, new ApiResponse { 
                    Status = true,
                    Message = "Successful",
                    Result = response
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation error"
            });
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutSubMenuAsynce(long id, [FromBody]SubMenuRequest request)
        {
            if (ModelState.IsValid)
            {
                await _subMenuService.UpdateSubMenuAsync(id, request);
                return StatusCode(204, new ApiResponse
                {
                    Status = true,
                    Message = "Successful"
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation error"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuAsync(long id)
        {
            await _subMenuService.DeleteSubMenuAsync(id);
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