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
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task CreateAsync(MenuRequest request)
        {
            await _unitOfWork.Menus.AddMenuAsync(_mapper.Map<Menu>(request));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long id) 
        {
            var menu = await _unitOfWork.Menus.GetMenuOnlyByIdAsync(id);
            if (menu != null)
            {
                // code will go in here after implementing jwt in order to resolve the common issue with jwt claims 
                _unitOfWork.Menus.DeleteMenu(menu);
                await _unitOfWork.SaveAsync();
            }
            throw new EntityNotFoundException($"Menu Id '{menu.MenuId}' not found.");
        }

        public async Task<ICollection<MenuResponse>> GetMenusAsync() 
            => _mapper.Map<ICollection<MenuResponse>>(await _unitOfWork.Menus.GetAllMenuAsync());

        public async Task<ICollection<MenuResponse>> GetMenusWithSubMenusAsync() 
            => _mapper.Map<ICollection<MenuResponse>>(await _unitOfWork.Menus.GetAllMenuWithSubMenusAsync());

        public async Task<MenuResponse> GetMenuWithSubMenusByIdAsync(long id) 
            => _mapper.Map<MenuResponse>(await _unitOfWork.Menus.GetMenuWithSubMenusByIdAsync(id));

        public async Task UpdateAsync(long id, MenuRequest request) 
        {
            var menu = await _unitOfWork.Menus.GetMenuOnlyByIdAsync(id);
            if (menu != null)
            {
                menu.Name = request.Name;
                _unitOfWork.Menus.UpdateMenu(menu);
                await _unitOfWork.SaveAsync();
            }
            throw new EntityNotFoundException($"Menu Id '{id}' not found.");
        }
    }
}
