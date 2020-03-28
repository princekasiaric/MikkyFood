using MFR.DomainModels;

namespace MFR.Persistence.Repository.Implementations
{
    public class MenuRepo : BaseRepo<Menu>, IMenuRepo
    {
        public MenuRepo(MFRDbContext context) : base(context){}

        public MFRDbContext MFRDbContext { get; set; }
    }
}
