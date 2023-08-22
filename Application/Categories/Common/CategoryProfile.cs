namespace Application.Categories.Common
{
    internal class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
