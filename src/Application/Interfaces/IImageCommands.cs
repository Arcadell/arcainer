using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IImageCommands
    {
        List<Image> GetImages(ImageFilter imageFilter);
    }
}