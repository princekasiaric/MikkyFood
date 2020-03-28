using MFR.DomainModels;

namespace MFR.Persistence.Repository.Implementations
{
    public class SubMenuRepo : BaseRepo<SubMenu>, ISubMenuRepo
    {
        private readonly MFRDbContext _context;

        public SubMenuRepo(MFRDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
