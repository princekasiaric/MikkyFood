using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels;
using MFR.Persistence.UnitOfWork;
using MFR.Persistence.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Core.Service.Implementation
{
    public class SubMenuService : ISubMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubMenuService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubMenuResponse> AddSubMenuAsync(SubMenuRequest request)
        {
            var subMenu = _mapper.Map<SubMenu>(request);
            await _unitOfWork.SubMenus.AddSubMenuAsync(subMenu);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<SubMenuResponse>(subMenu);
        }

        public async Task DeleteSubMenuAsync(long id)
        {
            var subMenu = await _unitOfWork.SubMenus.GetSubMenuByIdAsync(id);
            if (subMenu != null)
            {
                _unitOfWork.SubMenus.DeleteSubMenu(subMenu);
                await _unitOfWork.SaveAsync();
            }
            throw new EntityNotFoundException($"SubMenu Id '{subMenu.SubMenuId}' not found.");
        }

        public async Task<ICollection<SubMenuResponse>> GetAllSubMenuAsync() 
            => _mapper.Map<ICollection<SubMenuResponse>>(await _unitOfWork.SubMenus.GetAllSubMenuAsync());

        public async Task<SubMenuResponse> GetSubMenuByIdAsync(long id) 
            => _mapper.Map<SubMenuResponse>(await _unitOfWork.SubMenus.GetSubMenuByIdAsync(id));

        public async Task<ICollection<SubMenuResponse>> GetSubMenuByOrderByNameAsync() 
            => _mapper.Map<ICollection<SubMenuResponse>>(await _unitOfWork.SubMenus.GetSubMenuByOrderByNameAsync());

        public async Task<ICollection<SubMenuResponse>> GetSubMenusByMenuAsync(string menu) 
            => _mapper.Map<ICollection<SubMenuResponse>>(await _unitOfWork.SubMenus.FindByCondition(sb => sb.Menu.Name == menu));

        public async Task UpdateSubMenuAsync(long id, SubMenuRequest request) 
        {
            var subMenu = await _unitOfWork.SubMenus.GetSubMenuByIdAsync(id);
            if (subMenu != null)
            {
                subMenu.Name = request.Name;
                subMenu.Price = request.Price;
                subMenu.Description = request.Description;
                _unitOfWork.SubMenus.UpdateSubMenu(subMenu);
                await _unitOfWork.SaveAsync();
            }
            throw new EntityNotFoundException($"SubMenu Id '{subMenu.SubMenuId}' not found.");
        }
    }
}
