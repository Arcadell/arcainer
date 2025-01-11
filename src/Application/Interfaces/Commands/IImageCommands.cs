using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces.Commands
{
    public interface IImageCommands
    {
        List<Image> GetImages(ImageFilter imageFilter);
    }
}